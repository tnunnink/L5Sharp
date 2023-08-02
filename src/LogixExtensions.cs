using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Types;
using Module = L5Sharp.Components.Module;

namespace L5Sharp;

/// <summary>
/// Container for all public extensions methods that add additional functionality to the base elements of the library.
/// </summary>
public static class LogixExtensions
{
    #region GenericExtensions

    /// <summary>
    /// Gets the <c>Name</c> attribute value for the current <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> instance.</param>
    /// <returns>A <see cref="string"/> representing the name value if found; Otherwise, <c>empty</c>.</returns>
    /// <remarks>
    /// This is a helper since we access and use the name attribute so often I just wanted to make
    /// the code more concise.
    /// </remarks>
    public static string LogixName(this XElement element) => element.Attribute(L5XName.Name)?.Value ?? string.Empty;

    /// <summary>
    /// Gets the element's attached <see cref="LogixContent"/> instance if it exists. 
    /// </summary>
    /// <returns>If the current element is attached to a L5X document (i.e. has a root content element),
    /// then a new <see cref="LogixContent"/> instance wrapping the root; Otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This allows attached logix elements to reach back up to the content file in order to traverse or retrieve
    /// other elements in the L5X. This is helpful for other extensions that need rely on the L5X to perform functions.
    /// </remarks>
    public static LogixContent? Content(this ILogixSerializable element)
    {
        var content = element.Serialize().Ancestors(L5XName.RSLogix5000Content).FirstOrDefault();
        return content is not null ? new LogixContent(content) : default;
    }

    /// <summary>
    /// Determines if the current string is a value <see cref="TagName"/> string.
    /// </summary>
    /// <param name="input">The string input to analyze.</param>
    /// <returns><c>true</c> if the string is a valid tag name string; otherwise, <c>false</c>.</returns>
    public static bool IsTagName(this string input) => Regex.IsMatch(input,
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$");

    /// <summary>
    /// Tests the current string to indicate whether it is a valid Logix component name value. 
    /// </summary>
    /// <param name="name">The string name to test.</param>
    /// <returns><c>true</c> if <c>name</c> passes the Logix component name requirements; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Valid name must contain only alphanumeric or underscores, start with a letter or underscore,
    /// and be between 1 and 40 characters.
    /// </remarks>
    public static bool IsComponentName(this string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        var characters = name.ToCharArray();
        if (name.Length > 40) return false;
        if (!(char.IsLetter(characters[0]) || characters[0] == '_')) return false;
        return characters.All(c => char.IsLetter(c) || char.IsDigit(c) || c == '_');
    }

    /// <summary>
    /// Converts the current one dimensional array of logix type objects to a <see cref="ArrayType{TLogixType}"/>
    /// of the concrete logix type. 
    /// </summary>
    /// <param name="array">The array to convert.</param>
    /// <typeparam name="TLogixType">The logix type of the elements of the array.</typeparam>
    /// <returns>An <see cref="ArrayType{TLogixType}"/> representing the current array containing all the elements
    /// of the array.</returns>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <remarks>
    /// This extension uses reflection and compiled lambda functions to create the concrete generic array type
    /// from the current logix type array, and is used by logix type for implicitly converting arrays to a logix type.
    /// </remarks>
    public static ArrayType<TLogixType> ToArrayType<TLogixType>(this IEnumerable<TLogixType> array) 
        where TLogixType : LogixType
    {
        if (array is null) throw new ArgumentNullException(nameof(array));
        var arrayType = typeof(ArrayType<>).MakeGenericType(typeof(TLogixType));
        var parameterType = typeof(TLogixType[]);
        var constructor = arrayType.GetConstructor(new[] { parameterType })!;
        var parameter = Expression.Parameter(parameterType, "array");
        var creator = Expression.New(constructor, parameter);
        var lambda = Expression.Lambda<Func<TLogixType[], ArrayType<TLogixType>>>(creator, parameter);
        var func = lambda.Compile();
        return func.Invoke(array.ToArray());
    }
    
    /// <summary>
    /// Converts the current array object to a <see cref="ArrayType{TLogixType}"/> of the concrete logix type by
    /// inspecting the first element's type in the array. 
    /// </summary>
    /// <param name="array">The array to convert.</param>
    /// <returns>
    /// An <see cref="ArrayType"/> representing the containing all the elements of the array object as
    /// the the concrete generic type array.
    /// </returns>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <remarks>
    /// This extension uses reflection and compiled lambda functions to create the concrete array type
    /// from the current logix type array, and is used by logix type for implicitly converting arrays to a logix type.
    /// </remarks>
    public static ArrayType ToArrayType(this Array array)
    {
        if (array is null) throw new ArgumentNullException(nameof(array));
        var type = array.Cast<LogixType>().First().GetType();
        var arrayType = typeof(ArrayType<>).MakeGenericType(type);
        var parameterType = typeof(Array);
        var constructor = arrayType.GetConstructor(new[] { parameterType })!;
        var parameter = Expression.Parameter(parameterType, "array");
        var creator = Expression.New(constructor, parameter);
        var lambda = Expression.Lambda<Func<Array, ArrayType>>(creator, parameter);
        var func = lambda.Compile();
        return func.Invoke(array);
    }

    #endregion

    #region ComponentExtensions

    /// <summary>
    /// The scope type of the component.
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> option indicating the container type for the scoped component.</value>
    /// <remarks>
    /// </remarks>
    public static Scope Scope(this LogixElement component)
    {
        var containers = Enums.Scope.All().Where(s => s != Enums.Scope.Null).Select(s => s.L5XName.ToString());

        var ancestor = component.Serialize().Ancestors()
            .FirstOrDefault(a => containers.Any(c => c == a.Name))?.Name.ToString();

        return ancestor switch
        {
            L5XName.Routine => Enums.Scope.Routine,
            L5XName.Program => Enums.Scope.Program,
            L5XName.AddOnInstructionDefinition => Enums.Scope.Instruction,
            L5XName.Controller => Enums.Scope.Controller,
            _ => Enums.Scope.Null
        };
    }

    /// <summary>
    /// Gets the name of the scoped container for the current logix element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the container (program, controller, routine) name of the component.</value>
    /// <remarks>
    /// </remarks>
    public static string ScopeName(this LogixElement component)
    {
        var containers = Enums.Scope.All().Select(s => s.L5XName.ToString());

        var logixName = component.Serialize().Ancestors()
            .FirstOrDefault(a => containers.Any(c => c == a.Name))
            ?.LogixName();

        return logixName ?? string.Empty;
    }

    #endregion

    #region ContainerExtensions

    /// <summary>
    /// Determines if a component with the specified name exists in the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <returns><c>true</c> if a component with the specified name exists; otherwise, <c>false</c>.</returns>
    public static bool Contains<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        return container.Serialize().Elements().Any(e => e.LogixName() == name);
    }

    /// <summary>
    /// Returns a component with the specified name if it exists in the container, otherwise returns <c>null</c>.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent{TComponent}"/> of the specified type if found; Otherwise, <c>null</c>.</returns>
    public static TComponent? Find<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        var element = container.Serialize();
        var component = element.Elements().FirstOrDefault(e => e.LogixName() == name);
        return component is not null ? LogixSerializer.Deserialize<TComponent>(component) : default;
    }

    /// <summary>
    /// Returns a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to find.</param>
    /// <typeparam name="TComponent">The component type to return.</typeparam>
    /// <returns>A <see cref="LogixComponent{TComponent}"/> of the specified type.</returns>
    /// <exception cref="InvalidOperationException">No component having <c>name</c> exists in the container.</exception>
    public static TComponent Get<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        var element = container.Serialize();
        var component = element.Elements().SingleOrDefault(e => e.LogixName() == name);
        return component is not null
            ? LogixSerializer.Deserialize<TComponent>(component)
            : throw new InvalidOperationException($"No component with name {name} was found in container.");
    }

    /// <summary>
    /// Removes a component with the specified name from the container.
    /// </summary>
    /// <param name="container">The logix container of component objets.</param>
    /// <param name="name">The name of the component to remove.</param>
    public static void Remove<TComponent>(this LogixContainer<TComponent> container, string name)
        where TComponent : LogixComponent<TComponent>
    {
        container.Serialize().Elements().SingleOrDefault(c => c.LogixName() == name)?.Remove();
    }

    #endregion

    #region LogixTypeExtensions

    /// <summary>
    /// Traverses the type/member hierarchy of the <see cref="StructureType"/> data and builds a collection of
    /// <see cref="DataType"/> objects based on all the user defined types in the tree.
    /// </summary>
    /// <param name="type">The structure type for which to generate a list of user defined type objects.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing a <see cref="DataType"/> for each user type in the
    /// structure's type/member hierarchy</returns>
    public static IEnumerable<DataType> ToUDT(this StructureType type)
    {
        var results = new List<DataType>();

        if (type.Class == DataTypeClass.User)
        {
            var userType = new DataType
            {
                Name = type.Name,
                Class = type.Class,
                Family = type.Family,
                Members = new LogixContainer<DataTypeMember>(type.Members.Select(m => new DataTypeMember
                {
                    Name = m.Name,
                    DataType = m.DataType.Name,
                    Dimension = m.DataType is ArrayType array ? array.Dimensions : Dimensions.Empty,
                    Radix = Radix.Default(m.DataType),
                    ExternalAccess = ExternalAccess.ReadWrite
                }))
            };

            results.Add(userType);
        }

        foreach (var member in type.Members)
            if (member.DataType is ComplexType structureType)
                results.AddRange(structureType.ToUDT());

        return results;
    }

    #endregion

    #region DataTypeExtensions

    /// <summary>
    /// Returns all <see cref="DataType"/> instances that are dependent on the specified data type name.
    /// </summary>
    /// <param name="dataTypes">The logix collection of data types.</param>
    /// <param name="name">The name of the data type for which to find dependencies.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="DataType"/> that are dependent on the specified data type name.</returns>
    /// <remarks>
    /// This extension serves as an example of how one could extend the API of specific component collections to
    /// add custom XML queries against the source L5X and return materialized components.
    /// </remarks>
    public static IEnumerable<DataType> DependentsOf(this LogixContainer<DataType> dataTypes, string name)
    {
        return dataTypes.Serialize().Descendants(L5XName.DataType)
            .Where(e => e.Descendants(L5XName.Member).Any(m => m.Attribute(L5XName.DataType)?.Value == name))
            .Select(e => new DataType(e));
    }

    #endregion

    #region TagExtensions

    /// <summary>
    /// Returns all <see cref="Tag"/> objects in the entire L5X, including controller, program, AOI, and module tags.
    /// </summary>
    /// <param name="content">The <see cref="LogixContent"/> representing the L5X file to query.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all <see cref="Tag"/> objects in the L5X.</returns>
    /// <remarks>
    /// Rockwell certainly is not consistent in terms of where tags are serialized in the L5X, though it has reasoning.
    /// The API of this library sort of reflects that with all the different places to look for tag objects
    /// (controller, programs, AOIs, Modules).
    /// This extension is here to support a single interface through which to retrieve a flat list of all tags in the L5X
    /// regardless of container and element name. This method supports controller, program, AOI, and module config, input,
    /// and outputs tags.
    /// </remarks>
    public static IEnumerable<Tag> Tags(this LogixContent content)
    {
        var supportedNames = new List<string>
        {
            L5XName.Tag,
            L5XName.LocalTag,
            L5XName.ConfigTag,
            L5XName.InputTag,
            L5XName.OutputTag
        };

        return content.L5X.Descendants().Where(e => supportedNames.Any(n => n == e.Name)).Select(e => new Tag(e));
    }


    /// <summary>
    /// Returns a filtered collection of <see cref="Tag"/> that are contained in the specified program name.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects.</param>
    /// <param name="programName">The program name to filter the tags for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tags in the specified program.</returns>
    public static IEnumerable<Tag> In(this IEnumerable<Tag> tags, string programName)
    {
        return tags.Where(t => t.ScopeName() == programName);
    }

    /// <summary>
    /// Returns a filtered collection of <see cref="Tag"/> that have the specified <see cref="Enums.Scope"/> value.
    /// </summary>
    /// <param name="tags">A collection of <see cref="Tag"/> objects.</param>
    /// <param name="scope">The <see cref="Enums.Scope"/> to filter the tags for.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing tags in the specified program.</returns>
    public static IEnumerable<Tag> In(this IEnumerable<Tag> tags, Scope scope)
    {
        return tags.Where(t => t.Scope() == scope);
    }

    #endregion

    #region InstructionExtensions

    /// <summary>
    /// Returns the AOI instruction logic with the parameters tag names replaced with the operand tag names of the
    /// provided neutral text signature.
    /// </summary>
    /// <param name="instruction">The <see cref="AddOnInstruction"/> component.</param>
    /// <param name="text">The text signature of the instruction arguments.</param>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> representing all the instruction's
    /// logic, with each instruction parameter tag name replaced with the arguments from the provided text.
    /// </returns>
    /// <remarks>
    /// This is helpful when trying to perform deep analysis on logic. By "flattening" the logic we can
    /// reason or evaluate it as if it was written in line. Currently only supports <see cref="Rung"/>
    /// content or code type.
    /// </remarks>
    public static IEnumerable<NeutralText> Logic(this AddOnInstruction instruction, NeutralText text)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text));

        // All instructions primary logic is contained in the routine named 'Logic'
        var logic = instruction.Routines.FirstOrDefault(r => r.Name == "Logic");

        var rungs = logic?.Content<Rung>();
        if (rungs is null) return Enumerable.Empty<NeutralText>();

        //Skip first operand as it is always the AOI tag, which does not have corresponding parameter within the logic.
        var arguments = text.Operands().Select(o => o.ToString()).Skip(1).ToList();

        //Only required parameters are part of the instruction signature
        var parameters = instruction.Parameters.Where(p => p.Required is true).Select(p => p.Name).ToList();

        //Deserialize a mapping of the provided text operand arguments to instruction parameter names.
        var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

        //Replace all parameter names with argument names in the instruction logic text, and return the results.
        return rungs.Select(r => r.Text)
            .Select(t => mapping.Aggregate(t, (current, pair) =>
            {
                if (!pair.Argument.IsTagName()) return current;
                var replace = $@"(?<=[^.]){pair.Parameter}\b";
                return Regex.Replace(current, replace, pair.Argument.ToString());
            }))
            .ToList();
    }

    #endregion

    #region ModuleExtensions

    /// <summary>
    /// Gets the slot number of the current module if one exists. If the module does not have an slot, returns null.
    /// </summary>
    /// <value>An <see cref="byte"/> representing the slot number of the module.</value>
    /// <remarks>
    /// This is a helper that just looks for an upstream <see cref="Port"/> with a valid slot byte number.
    /// </remarks>
    public static byte? Slot(this Module module) =>
        module.Ports.FirstOrDefault(p => p.Upstream && p.Address.IsSlot)?.Address.ToSlot();

    /// <summary>
    /// Gets the IP address of the current module if one exists. If the module does not have an IP, returns null.
    /// </summary>
    /// <value>An <see cref="IPAddress"/> representing the IP of the module.</value>
    /// <remarks>
    /// This is a helper property that just looks for <see cref="Port"/> for an Ethernet port with a
    /// valid IP address.
    /// </remarks>
    public static IPAddress? IP(this Module module) => module.Ports
        .FirstOrDefault(p => p is { Type: "Ethernet", Address.IsIPv4: true })?.Address
        .ToIPAddress();

    /// <summary>
    /// Gets the <c>Local</c> module or module that represents the controller of the module collection.
    /// </summary>
    /// <param name="modules">A collection of modules.</param>
    /// <returns>A single <see cref="Module"/> which is named local if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This is a helper to concisely get the controller or root local module from the modules collection.</remarks>
    public static Module? Local(this IEnumerable<Module> modules) => modules.SingleOrDefault(m => m.Name == "Local");

    /// <summary>
    /// Gets the parent module of this module component defined in the current L5X content.
    /// </summary>
    /// <returns>A <see cref="Module"/> representing the parent of this module if it exists; otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties (ParentModule, ParentModPortId)
    /// defining the parent/child IO tree relationship. It would be nice to navigate this hierarchy programatically,
    /// hence the reason for this extension method. Of course, this requires the module is attached to the L5X content.
    /// In-memory created modules will inherently return an empty collection, as there is no L5X structure to introspect.
    /// </remarks>
    public static Module? Parent(this Module module)
    {
        var parent = module.Serialize().Parent?.Elements().FirstOrDefault(m => m.LogixName() == module.ParentModule);
        return parent is not null ? new Module(parent) : default;
    }

    /// <summary>
    /// Gets the child modules of this module component defined in the current L5X content. 
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="Module"/> components that have the <c>ParentModule</c> property
    /// configured as the name of this module.
    /// </returns>
    /// <remarks>
    /// The L5X structure serializes modules in a flat list with each element having properties (ParentModule, ParentModPortId)
    /// defining the parent/child IO tree relationship. It would be nice to navigate this hierarchy programatically,
    /// hence the reason for this extension method. Of course, this requires the module is attached to the L5X content.
    /// In-memory created modules will inherently return an empty collection, as there is no L5X structure to introspect.
    /// </remarks>
    public static IEnumerable<Module> Modules(this Module module)
    {
        return module.Serialize().Parent?.Elements()
                   .Where(m => m.Attribute(L5XName.ParentModule)?.Value == module.Name)
                   .Select(e => new Module(e))
               ?? Enumerable.Empty<Module>();
    }

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// config, input, and output tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    /// <remarks>
    /// Since module tags are nested within different layers of complex types, it can be difficult to just
    /// get a single list of all module tags. This extension makes that easy by sifting through the object and returning
    /// a flat list containing all non-null config, input, and output tags defined for the <see cref="Module"/> component.
    /// </remarks>
    public static IEnumerable<Tag> Tags(this Module module)
    {
        var tags = new List<Tag>();

        if (module.Communications is null) return tags;

        if (module.Communications.ConfigTag is not null)
            tags.Add(module.Communications.ConfigTag);

        foreach (var connection in module.Communications.Connections)
        {
            if (connection.InputTag is not null)
                tags.Add(connection.InputTag);

            if (connection.OutputTag is not null)
                tags.Add(connection.OutputTag);
        }

        return tags;
    }

    /// <summary>
    /// Returns the module's config tag object contained in the communications element.
    /// </summary>
    /// <param name="module">The current <see cref="Module"/> component.</param>
    /// <returns>A <see cref="Tag"/> containing the module's config tag data.</returns>
    /// <remarks>This is a simple helper to make accessing the module config data more concise.</remarks>
    public static Tag? Config(this Module module) => module.Communications?.ConfigTag;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="child"></param>
    /// <param name="address"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void Add(this Module parent, Module child, Address? address = default)
    {
        var parentPort = parent.Ports.FirstOrDefault(p => p.Upstream is false);

        if (parentPort is null)
            throw new InvalidOperationException();

        if (parentPort.Type == "Ethernet" && address is null) address = Address.DefaultIP();
        if (parentPort.Address.IsSlot && parent.IsAttached && address is null) address = parent.NextSlot();
        address ??= Address.DefaultSlot();

        var childPort = new Port { Id = 1, Type = parentPort.Type, Address = address, Upstream = true };

        child.ParentModule = parent.Name;
        child.ParentPortId = parentPort.Id;
        child.Ports.Add(childPort);

        //Adds the module to the underlying collection.
        if (!child.IsAttached) parent.Serialize().Parent.Add(child.Serialize());
    }

    /// <summary>
    /// Gets the next largest slot number for the current module by introspecting the slot numbers of all other
    /// child modules of this parent module. 
    /// </summary>
    /// <param name="module">The current <see cref="Module"/> component.</param>
    /// <returns>
    /// A <see cref="Address"/> containing the next highest slot number in the rack/chassis.
    /// </returns>
    public static Address NextSlot(this Module module)
    {
        var children = module.Serialize().Parent?.Elements()
            .Where(m => m.Attribute(L5XName.ParentModule)?.Value == module.Name);

        var next = children?.Select(c => c.Descendants(L5XName.Port)
                .FirstOrDefault(p => p.Attribute(L5XName.Upstream)?.Value.Parse<bool>() is true &&
                                     byte.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
                ?.Attribute(L5XName.Address)?.Value.Parse<byte>())
            .OrderByDescending(b => b)
            .FirstOrDefault();

        return next.HasValue ? Address.Slot(next.Value) : Address.DefaultSlot();
    }

    #endregion

    #region RungExtensions

    /// <summary>
    /// Gets the parent <see cref="Routine"/> of this <see cref="Rung"/> instance if it is attached.
    /// </summary>
    /// <param name="rung">The current <see cref="Rung"/> object.</param>
    /// <returns>A <see cref="Routine"/> instance representing the containing routine of the rung if found; Otherwise, <c>null</c>.</returns>
    public static Routine? Routine(this Rung rung)
    {
        var routine = rung.Serialize().Ancestors(L5XName.Routine).FirstOrDefault();
        return routine is not null ? new Routine(routine) : default;
    }

    /// <summary>
    /// Returns a flat list of <see cref="NeutralText"/> representing all base and nested AOI logic in the
    /// collection of <see cref="Rung"/> objects.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all the <see cref="NeutralText"/>, including nested instruction
    /// text, found in the rung collection.
    /// </returns>
    /// <remarks>
    /// This extension was specifically created to assist in getting a flat list of logic, including
    /// nested AOI logic, for specialized querying purposes, such as finding tag references within nested logic.
    /// This method will replace the instruction logic parameters with the neutral text operands of the instruction signature,
    /// so to get the effective flattened list of executing <see cref="NeutralText"/> code.
    /// </remarks>
    public static IEnumerable<NeutralText> Flatten(this IEnumerable<Rung> rungs)
    {
        var code = new List<NeutralText>();
        var collection = rungs.ToList();

        var content = collection.FirstOrDefault()?.Content();
        if (content is null)
            throw new InvalidOperationException("Can not flatten rungs that are not attached to a L5X content file.");

        var aoiLookup = content.Instructions.ToDictionary(k => k.Name, v => v);
        var text = collection.Select(r => r.Text);

        foreach (var line in text)
        {
            var references = aoiLookup.SelectMany(l => line.SplitByKey(l.Key)).ToList();

            if (references.Count == 0)
            {
                code.Add(line);
                continue;
            }

            foreach (var logic in from reference in references
                     let key = reference.Keys().FirstOrDefault()
                     let instruction = aoiLookup[key]
                     select Logic(instruction, reference))
            {
                code.AddRange(logic);
            }
        }

        return code;
    }

    /// <summary>
    /// Filters the current collection to text contained in the specified container.
    /// </summary>
    /// <param name="rungs">The collection of <see cref="Rung"/> to filter.</param>
    /// <param name="container">The container name in which to filter the text collection.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Rung"/> filtered to the specific container..</returns>
    /// <remarks><c>container</c> can be either a program, routine, or instruction name. You can also chain calls in any order
    /// to scope the rung collection to a specific combination of program/routine or instruction/routine.</remarks>
    public static IEnumerable<Rung> In(this IEnumerable<Rung> rungs, string container)
    {
        return rungs.Where(r => r.ScopeName() == container);
    }

    #endregion

    #region NeutralTextExtensions

    /// <summary>
    /// Returns all referenced tag names and their corresponding list of <see cref="NeutralText"/> logic references in
    /// the current collection of <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="text">A collection of <see cref="NeutralText"/> rung logic.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> where each tag name is a key and it's corresponding value is
    /// a <see cref="List{T}"/> containing all the logic referencing the tag found in the file.</returns>
    /// <remarks>
    /// This is useful for performing quick lookup of logic references by tag name.
    /// </remarks>
    public static Dictionary<TagName, List<NeutralText>> ToTagLookup(this IEnumerable<NeutralText> text)
    {
        var results = new Dictionary<TagName, List<NeutralText>>();

        foreach (var line in text)
        {
            var tags = line.Tags();

            foreach (var tag in tags)
            {
                if (!results.ContainsKey(tag))
                {
                    results.Add(tag, new List<NeutralText> { line });
                    continue;
                }

                results[tag].Add(line);
            }
        }

        return results;
    }

    #endregion

    #region InternalExtensions

    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>true if the string is empty. Otherwise false.</returns>
    internal static bool IsEmpty(this string value) => value.Equals(string.Empty);

    /// <summary>
    /// Returns the string value as a <see cref="XName"/> value object.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A <see cref="XName"/> object containing the string value.</returns>
    /// <remarks>This is to make converting from string to XName concise.</remarks>
    internal static XName XName(this string value) => System.Xml.Linq.XName.Get(value);

    /// <summary>
    /// A concise method for getting a required attribute value parsed as the specified type from a XElement object.
    /// </summary>
    /// <param name="element">The element containing the attribute to retrieve.</param>
    /// <param name="name">The name of the attribute value to get.</param>
    /// <typeparam name="T">The type to parse the attribute value as.</typeparam>
    /// <returns>The value of the element's specified attribute value parsed as the specified generic type parameter.</returns>
    /// <exception cref="InvalidOperationException">No attribute with <c>name</c> exists for the current element.</exception>
    internal static T Get<T>(this XElement element, XName name)
    {
        var value = element.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : throw element.L5XError(name);
    }

    /// <summary>
    /// Gets the L5X element name for the specified type. 
    /// </summary>
    /// <param name="type">The type to get the L5X element name for.</param>
    /// <returns>A <see cref="System.Xml.Linq.XName"/> representing the name of the element that corresponds to the type.</returns>
    /// <remarks>
    /// All this does is first look for the class attribute <see cref="XmlTypeAttribute"/> to use as the explicitly
    /// configured name, and if not found, returns the type name as the default element name.
    /// </remarks>
    internal static string L5XType(this Type type)
    {
        var attribute = type.GetCustomAttribute<XmlTypeAttribute>();
        return attribute is not null ? attribute.TypeName : type.Name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    internal static InvalidOperationException L5XError(this XElement element, XName name)
    {
        var message = $"The required attribute or child element '{name}' does not exist for {element.Name}.";
        var line = ((IXmlLineInfo)element).HasLineInfo() ? ((IXmlLineInfo)element).LineNumber : -1;
        var exception = new InvalidOperationException(message);
        exception.Data.Add("target", name);
        exception.Data.Add("line", line);
        exception.Data.Add("element", element);
        return exception;
    }

    /// <summary>
    /// Builds a deserialization expression delegate which returns the specified type using the current type information.
    /// </summary>
    /// <param name="type">The current type for which to build the expression.</param>
    /// <typeparam name="TReturn">The return type of the expression delegate.</typeparam>
    /// <returns>A <see cref="Func{TResult}"/> which accepts a <see cref="XElement"/> and returns the specified
    /// return type.</returns>
    /// <remarks>
    /// This extension is the basis for how we build the deserialization functions using reflection and
    /// expression trees. Using compiled expression trees is much more efficient that calling the invoke method for a type's
    /// constructor info obtained via reflection. This method make all the necessary check on the current type, ensuring the
    /// deserializer delegate will execute without exception.
    /// </remarks>
    internal static Func<XElement, TReturn> Deserializer<TReturn>(this Type type)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));

        if (type.IsAbstract)
            throw new ArgumentException($"Can not build deserializer expression for abstract type '{type.Name}'.");

        if (!typeof(TReturn).IsAssignableFrom(type))
            throw new ArgumentException(
                $"The type {type.Name} is not assignable (inherited) from '{typeof(TReturn).Name}'.");

        var constructor = type.GetConstructor(new[] { typeof(XElement) });

        if (constructor is null || !constructor.IsPublic)
            throw new ArgumentException(
                $"Can not build expression for type '{type.Name}' without public constructor accepting a XElement parameter.");

        var parameter = Expression.Parameter(typeof(XElement), "element");
        var factory = Expression.New(constructor, parameter);
        var lambda = Expression.Lambda(factory, parameter);
        return (Func<XElement, TReturn>)lambda.Compile();
    }

    #endregion
}