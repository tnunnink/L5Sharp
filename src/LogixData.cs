using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;

namespace L5Sharp;

/// <summary>
/// A specialized static factory for deserializing <see cref="LogixType"/> objects from <see cref="XElement"/>.
/// </summary>
public static class LogixData
{
    /// <summary>
    /// The global constructor cache for all <see cref="StructureType"/> objects. This is what we are using to create
    /// strongly typed structure type objects at runtime.
    /// </summary>
    private static readonly Lazy<Dictionary<string, ConstructorInfo>> Constructors =
        new(() =>
        {
            return ScanMode switch
            {
                ScanMode.None => new Dictionary<string, ConstructorInfo>(),
                ScanMode.Internal => ScanTypes(typeof(LogixData).Assembly)
                    .ToDictionary(k => k.Key, k => k.Value),
                ScanMode.All => AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(ScanTypes)
                    .ToDictionary(k => k.Key, k => k.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(ScanMode),
                    "Can evaluate constructor cache from current scan mode.")
            };
        });

    /// <summary>
    /// The <see cref="ScanMode"/> specifying how <see cref="LogixData"/> should perform reflection scanning
    /// for types to pre-register for deserialization.
    /// </summary>
    /// <remarks>
    /// This class will perform reflection based scanning for types matching the all the following criteria:
    /// Public non-abstract classes that derive from <see cref="LogixElement{TElement}"/> or <see cref="LogixType"/> and have public
    /// constructors accepting a <see cref="XElement"/> object. Types match this criteria will be cached and constructed
    /// at runtime. 
    /// </remarks>
    public static ScanMode ScanMode { get; set; } = ScanMode.All;

    /// <summary>
    /// Returns the singleton null <see cref="LogixType"/> object.
    /// </summary>
    public static LogixType Null => NullType.Instance;

    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="element">The element to deserialize.</param>
    /// <returns>A <see cref="LogixType"/> representing the data value, structure, or array or the provided element.</returns>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="NotSupportedException"><c>element</c> has a name that is not supported for deserialization.</exception>
    public static LogixType Deserialize(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        return element.Name.ToString() switch
        {
            L5XName.Tag => DeserializeData(element),
            L5XName.LocalTag => DeserializeData(element),
            L5XName.ConfigTag => DeserializeData(element),
            L5XName.InputTag => DeserializeData(element),
            L5XName.OutputTag => DeserializeData(element),
            L5XName.Data => DeserializeFormatted(element),
            L5XName.DefaultData => DeserializeFormatted(element),
            L5XName.DataValue => DeserializeAtomic(element),
            L5XName.DataValueMember => DeserializeAtomic(element),
            L5XName.Element => DeserializeElement(element),
            L5XName.Array => DeserializeArray(element),
            L5XName.ArrayMember => DeserializeArray(element),
            L5XName.Structure => DeserializeStructure(element),
            L5XName.StructureMember => DeserializeStructure(element),
            L5XName.AlarmAnalogParameters => new ALARM_ANALOG(element),
            L5XName.AlarmDigitalParameters => new ALARM_DIGITAL(element),
            L5XName.MessageParameters => new MESSAGE(element),
            _ => throw new NotSupportedException(
                $"The element name '{element.Name}' is not able to be used to deserialize a logix type.")
        };
    }

    /// <summary>
    /// Determines if a type with the specified type name is registered for deserialization.
    /// </summary>
    /// <param name="name">The type name to check.</param>
    /// <returns><c>true</c> if the type is registered; otherwise, <c>false</c>.</returns>
    public static bool IsRegistered(string name) => Constructors.Value.Keys.Any(t => t == name);

    /// <summary>
    /// Register a custom <see cref="StructureType"/> so that it may be instantiated during deserialization of a L5X.
    /// The type must implement a constructor accepting a single <see cref="XElement"/> argument.
    /// </summary>
    /// <typeparam name="TStructure">The type of the logix type.</typeparam>
    /// <exception cref="ArgumentException"><c>TStructure</c> is abstract -or- does not have a public constructor
    /// accepting a single <see cref="XElement"/> object.</exception>
    /// <exception cref="InvalidOperationException"><c>TStructure</c> is already registered.</exception>
    public static void Register<TStructure>() where TStructure : StructureType
    {
        var type = typeof(TStructure);

        if (type.IsAbstract) throw new ArgumentException("Can not register abstract types for deserialization.");

        var constructor = type.GetConstructor(new[] { typeof(XElement) });
        if (constructor is null)
            throw new ArgumentException(
                $"Can not register type without a constructor accepting a single {typeof(XElement)} object.");

        var key = type.L5XType();

        if (!Constructors.Value.TryAdd(key, constructor))
            throw new InvalidOperationException($"The type {key} is already registered.");
    }

    /// <summary>
    /// Scans the provided assembly using reflection for public non-abstract types inheriting <see cref="StructureType"/>
    /// that have the required deserialization constructor and registers the type so that it may be instantiated during
    /// deserialization of a L5X. 
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <exception cref="InvalidOperationException">A type is already registered.</exception>
    /// <remarks>
    /// This is to assist with easily registering types within a specific assembly. Note that if <see cref="ScanMode"/>
    /// is set to <see cref="ScanMode.All"/>, all assemblies in the app domain will automatically be scanned.
    /// Therefore, only use this method if <see cref="ScanMode"/> is set to <see cref="ScanMode.Internal"/> or
    /// <see cref="ScanMode.None"/>.
    /// </remarks>
    public static void Scan(Assembly assembly)
    {
        foreach (var pair in ScanTypes(assembly).Where(pair => !Constructors.Value.TryAdd(pair.Key, pair.Value)))
            throw new InvalidOperationException($"The type {pair.Key} is already registered.");
    }

    /// <summary>
    /// Handles deserializing the data element of the root tag. This method will forward call down the chain
    /// based on the format of the data structure.
    /// </summary>
    private static LogixType DeserializeData(XContainer element)
    {
        var supportedFormats = DataFormat.All().Where(f => f != DataFormat.L5K);

        //Don't specify element name to support both Data and DefaultData elements.
        //Just look for elements with supportable Format attribute.
        var data = element.Elements()
            .FirstOrDefault(e => e.Attribute(L5XName.Format) is not null
                                 && supportedFormats.Any(f => f.Value == e.Attribute(L5XName.Format)!.Value));

        return data is not null ? Deserialize(data) : Null;
    }

    /// <summary>
    /// Handles deserializing a formatted data element to a logix type. This method will forward call down the chain
    /// based on the format of the data structure.
    /// </summary>
    private static LogixType DeserializeFormatted(XElement element)
    {
        DataFormat.TryFromName(element.Attribute(L5XName.Format)?.Value, out var format);
        if (format is null) return Null;
        if (format == DataFormat.String) return DeserializeString(element);
        return element.FirstNode is XElement root ? Deserialize(root) : Null;
    }

    /// <summary>
    /// Handles deserializing an element to a atomic value type logix type.
    /// This method will call upon <see cref="Atomic"/> to generate the known concrete atomic type we need.
    /// This implementation ignores any specified Radix because we have found cases where the Radix does not match
    /// the actual format of the value, therefore, we always infer the format, and then parse.
    /// </summary>
    private static LogixType DeserializeAtomic(XElement element)
    {
        var name = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);
        var value = element.Attribute(L5XName.Value)?.Value ?? throw new L5XException(L5XName.Value, element);
        return Atomic.Parse(name, value);
    }

    /// <summary>
    /// Handles deserializing an array element to a <see cref="ArrayType"/>.
    /// </summary>
    private static LogixType DeserializeArray(XElement element) => new ArrayType(element);

    /// <summary>
    /// Handles deserializing an array index element to a logix type, either atomic, string, or structure,
    /// depending on the state of the element.
    /// </summary>
    private static LogixType DeserializeElement(XElement element)
    {
        var value = element.Attribute(L5XName.Value);
        var structure = element.Element(L5XName.Structure);
        var name = element.Parent?.Attribute(L5XName.DataType)?.Value ??
                   throw new L5XException(L5XName.DataType, element);

        return value is not null ? Atomic.Parse(name, value.Value) :
            structure is not null ? DeserializeStructure(structure) :
            throw new L5XException(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StructureType"/> logix type.
    /// Will check for registered types to create the concrete type if available.
    /// Otherwise we resort to a more generic <see cref="StringType"/> of string or <see cref="ComplexType"/> for everything else.
    /// </summary>
    private static LogixType DeserializeStructure(XElement element)
    {
        var dataType = element.Attribute(L5XName.DataType)?.Value ?? throw new L5XException(L5XName.DataType, element);

        if (IsRegistered(dataType))
            return DeserializeType(dataType, element);

        return HasStringStructure(element) ? new StringType(element) : new ComplexType(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StringType"/> logix type.
    /// Will check for registered type to create the concrete type if available.
    /// Otherwise we resort to a more generic <see cref="StringType"/> object.
    /// </summary>
    private static LogixType DeserializeString(XElement element)
    {
        var dataType = element.Parent?.Attribute(L5XName.DataType)?.Value ??
                       throw new L5XException(L5XName.DataType, element);

        return IsRegistered(dataType) ? DeserializeType(dataType, element) : new StringType(element);
    }

    /// <summary>
    /// Determines if the provided element has a structure that indicates it is a <see cref="StringType"/> structure.
    /// </summary>
    private static bool HasStringStructure(XElement element)
    {
        return element.Descendants(L5XName.DataValueMember).Any(e =>
            e?.Value is not null
            && e.Attribute(L5XName.Name)?.Value == "DATA"
            && e.Attribute(L5XName.DataType)?.Value == element.Attribute(L5XName.DataType)?.Value
            && e.Attribute(L5XName.Radix)?.Value == "ASCII");
    }

    /// <summary>
    /// Performs deserialization of the specified structure type name by getting the cached constructor for the type
    /// and invoking it.
    /// </summary>
    private static LogixType DeserializeType(string typeName, XElement element) =>
        (LogixType)Constructor(typeName).Invoke(new object[] { element });

    /// <summary>
    /// Gets a constructor info object provided the type name.
    /// </summary>
    private static ConstructorInfo Constructor(string typeName)
    {
        if (!Constructors.Value.TryGetValue(typeName, out var constructor))
            throw new InvalidOperationException($"Type '{typeName}' is not registered for deserialization.");

        return constructor;
    }

    /// <summary>
    /// Performs reflection scanning of provided <see cref="Assembly"/> to get all public non abstract types
    /// inheriting from <see cref="StructureType"/> that have the supported deserialization constructor, and returns
    /// the "L5XType" and <see cref="ConstructorInfo"/> pair for fast lookup. 
    /// </summary>
    private static IEnumerable<KeyValuePair<string, ConstructorInfo>> ScanTypes(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t =>
            t.IsDerivativeOf(typeof(StructureType))
            && t != typeof(ComplexType)
            && t is { IsAbstract: false, IsPublic: true }
            && t.GetConstructor(new[] { typeof(XElement) }) is not null);

        foreach (var type in types)
        {
            var constructor = type.GetConstructor(new[] { typeof(XElement) });
            yield return new KeyValuePair<string, ConstructorInfo>(type.L5XType(), constructor);
        }
    }
}

/// <summary>
/// An enum option specifying how <see cref="LogixData"/> should scan for custom structure types.
/// </summary>
public enum ScanMode
{
    /// <summary>
    /// Indicates that <see cref="LogixData"/> should scan internal and external assemblies for
    /// <see cref="StructureType"/> matching the deserialization criteria.
    /// </summary>
    All,

    /// <summary>
    /// Indicates that <see cref="LogixData"/> should scan only internal assemblies for
    /// <see cref="StructureType"/> matching the deserialization criteria.
    /// </summary>
    Internal,

    /// <summary>
    /// Indicates that <see cref="LogixData"/> should not perform reflection scanning for
    /// <see cref="StructureType"/> to automatically register for deserialization.
    /// </summary>
    None
}