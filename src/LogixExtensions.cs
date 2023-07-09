using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;
using L5Sharp.Enums;
using Module = L5Sharp.Components.Module;

namespace L5Sharp;

/// <summary>
/// Container for all public extensions methods that add functionality to the base components of the library.
/// </summary>
public static class LogixExtensions
{
    #region ComponentExtensions

    /// <summary>
    /// The scope type of the component.
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> option indicating the container type for the scoped component.</value>
    /// <remarks>
    /// </remarks>
    public static Scope Scope<TElement>(this LogixElement<TElement> component) where TElement : LogixElement<TElement>
    {
        return Enums.Scope.FromElement(component.Serialize());
    }

    /// <summary>
    /// The scope name of the component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the container (program, controller, routine) name of the component.</value>
    /// <remarks>
    /// </remarks>
    public static string Container<TElement>(this LogixElement<TElement> component)
        where TElement : LogixElement<TElement>
    {
        var containers = Enums.Scope.All().Select(s => s.XName.ToString());

        var ancestor = component.Serialize().Ancestors()
            .FirstOrDefault(a => containers.Any(c => c == a.Name))?.LogixName();

        return ancestor ?? string.Empty;
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
        var component = element.Elements().SingleOrDefault(e => e.LogixName() == name);
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

    #region InstructionExtensions

    /// <summary>
    /// Returns the AOI instruction logic with the parameters of the instruction replaced with the provided neutral
    /// text signature arguments.
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

        // All instructions primary logic is contained in the routine names 'Logic'
        var logic = instruction.Routines.FirstOrDefault(r => r.Name == "Logic");

        var rll = logic?.Content<Rung>();
        if (rll is null) return Enumerable.Empty<NeutralText>();

        //Skip first operand as it is always the AOI tag, which does not have corresponding parameter within the logic.
        var arguments = text.Operands().Select(o => o.ToString()).Skip(1).ToList();

        //Only required parameters are part of the instruction signature
        var parameters = instruction.Parameters.Where(p => p.Required is true).Select(p => p.Name).ToList();

        //Deserialize a mapping of the provided text operand arguments to instruction parameter names.
        var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

        //Replace all parameter names with argument names in the instruction logic text, and return the results.
        return rll.Select(r => r.Text)
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
    /// Gets the parent module of this module component.
    /// </summary>
    /// <returns>A <see cref="Module"/> representing the parent of this module if it exists; otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This method relies on the object being attached to the L5X hierarchy in order to find it's parent. If the module
    /// was created in memory and not yet added to the L5X, then this method will return <c>null</c>.
    /// </remarks>
    public static Module? Parent(this Module module)
    {
        var parent = module.Serialize().Parent?.Elements().FirstOrDefault(m => m.LogixName() == module.ParentModule);
        return parent is not null ? new Module(parent) : default;
    }

    /// <summary>
    /// Gets the child modules of this module component.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> of <see cref="Module"/> components that have the parent module configured
    /// as the current module.
    /// </returns>
    public static IEnumerable<Module> Modules(this Module module)
    {
        return module.Serialize().Parent?.Elements()
                   .Where(m => m.Attribute(L5XName.ParentModule)?.Value == module.Name)
                   .Select(e => new Module(e))
               ?? Enumerable.Empty<Module>();
    }

    /// <summary>
    /// Returns a collection of all non-null <see cref="Tag"/> objects for the current Module, including all
    /// input, output, and config tags.
    /// </summary>
    /// <value>An <see cref="IEnumerable{T}"/> containing the base tags for the Module.</value>
    public static IEnumerable<Tag> Tags(this Module module)
    {
        var tags = new List<Tag>();

        if (module.Config is not null)
            tags.Add(module.Config);

        foreach (var connection in module.Connections)
        {
            if (connection.Input is not null)
                tags.Add(connection.Input);

            if (connection.Output is not null)
                tags.Add(connection.Output);
        }

        return tags;
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
        return rungs.Where(r => r.Container() == container);
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

    #region ElementExtensions

    /// <summary>
    /// Gets the <c>Name</c> attribute value for the current <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> instance.</param>
    /// <returns>A <see cref="string"/> representing the name value.</returns>
    /// <remarks>
    /// This is a helper since we access and use the name attribute so often I just wanted to make
    /// the code more concise.
    /// </remarks>
    public static string LogixName(this XElement element) => element.Attribute(L5XName.Name)?.Value ?? string.Empty;

    /// <summary>
    /// A helper for determining a <c>Module</c> tag name for an input, output, or config tag element.
    /// </summary>
    /// <param name="element">The current module tag element.</param>
    /// <param name="suffix">The string suffix to append to the determines tag name. Default is 'C' for config tag.</param>
    /// <returns>A <see cref="string"/> representing the tag name of the module tag.</returns>
    public static string ModuleTagName(this XNode element, string suffix = "C")
    {
        var moduleName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.Name)?.Value;

        var parentName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.ParentModule)?.Value;

        var slot = element
            .Ancestors(L5XName.Module)
            .Descendants(L5XName.Port)
            .Where(p => bool.Parse(p.Attribute(L5XName.Upstream)?.Value!)
                        && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                        && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
            .Select(p => p.Attribute(L5XName.Address)?.Value)
            .FirstOrDefault();

        return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
    }

    #endregion

    #region StringExtensions

    /// <summary>
    /// Determines if the current string is equal to string.Empty.
    /// </summary>
    /// <param name="value">The string input to analyze.</param>
    /// <returns>true if the string is empty. Otherwise false.</returns>
    public static bool IsEmpty(this string value) => value.Equals(string.Empty);

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
    /// Determines if the current string is a value <see cref="TagName"/> string.
    /// </summary>
    /// <param name="input">The string input to analyze.</param>
    /// <returns><c>true</c> if the string is a valid tag name string; otherwise, <c>false</c>.</returns>
    public static bool IsTagName(this string input) => Regex.IsMatch(input,
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$");

    /// <summary>
    /// Converts this string value to a <see cref="TagName"/> value type.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A <see cref="TagName"/> containing the text representing the tag name value.</returns>
    public static TagName ToTagName(this string value) => new(value);

    #endregion

    #region InternalExtensions

    /// <summary>
    /// Returns the string value as a <see cref="XName"/> value object.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A <see cref="XName"/> object containing the string value.</returns>
    /// <remarks>This is to make converting from string to XName concise.</remarks>
    internal static XName XName(this string value) => System.Xml.Linq.XName.Get(value);

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
    /// Determines if a type is derived from the base type, even if the base type is a generic type.
    /// </summary>
    /// <param name="type">The type to test.</param>
    /// <param name="baseType">The base type to check.</param>
    /// <returns><c>true</c> if <c>type</c> inherits directly or indirectly from <c>baseType</c>.</returns>
    internal static bool IsDerivativeOf(this Type type, Type baseType)
    {
        if (type == baseType) return false;

        var current = type.BaseType;

        while (current != typeof(object) && current != null)
        {
            var definition = current.IsGenericType ? current.GetGenericTypeDefinition() : current;
            if (definition == baseType) return true;
            current = current.BaseType;
        }

        return false;
    }

    #endregion
}