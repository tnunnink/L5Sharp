using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Predefined;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// A specialized static factory for deserializing <see cref="LogixType"/> objects from <see cref="XElement"/>.
/// </summary>
/// <remarks>
/// This class is built to specifically find and create concrete instances of atomic, predefined, and custom
/// user defined logix types at runtime. This allows the user to cast a given logix type down to the most specific
/// type as it is deserialized from the L5X tag data structure.
/// </remarks>
public static class LogixData
{
    /// <summary>
    /// A system wide lookup of all <see cref="LogixType"/> objects by L5XType name obtained using reflection. This does
    /// not include the internal generic types such as StructureType, ComplexType, StringType, ArrayType, or ArrayType{T},
    /// or NullType, but rather types that can be instantiated (atomic or complex) for which we may need to create a generic
    /// array of.
    /// </summary>
    private static readonly Dictionary<string, Type> Lookup =
        AppDomain.CurrentDomain.GetAssemblies().SelectMany(FindLogixTypes).ToDictionary(k => k.L5XType(), v => v);

    /// <summary>
    /// The global cache for all <see cref="StructureType"/> and <see cref="StringType"/> object deserializer
    /// delegate functions. This is what we are using to create strongly typed logix type objects at runtime.
    /// </summary>
    private static readonly Lazy<Dictionary<string, Func<XElement, LogixType>>> Deserializers = new(() =>
        AppDomain.CurrentDomain.GetAssemblies().Distinct().SelectMany(Introspect).ToDictionary(k => k.Key, k => k.Value));

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
            L5XName.Parameter => DeserializeData(element),
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
    /// <param name="dataType">The data type name to check.</param>
    /// <returns><c>true</c> if the type is registered; otherwise, <c>false</c>.</returns>
    public static bool IsRegistered(string dataType) => Deserializers.Value.ContainsKey(dataType);

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
        var key = type.L5XType();
        var deserializer = type.Deserializer<LogixType>();

        if (!Deserializers.Value.TryAdd(key, deserializer))
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
    /// This is to assist with easily registering types within a specific assembly.
    /// </remarks>
    public static void Scan(Assembly assembly)
    {
        foreach (var pair in Introspect(assembly).Where(pair => !Deserializers.Value.TryAdd(pair.Key, pair.Value)))
            throw new InvalidOperationException($"The type {pair.Key} is already registered.");
    }

    /// <summary>
    /// Handles deserializing the data element of the root tag. This method will forward call down the chain
    /// based on the format of the data structure.
    /// </summary>
    private static LogixType DeserializeData(XContainer element)
    {
        var data = element.Elements()
            .FirstOrDefault(e => DataFormat.Supported.Any(f => f == e.Attribute(L5XName.Format)?.Value));

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
        var dataType = element.Get(L5XName.DataType);
        var value = element.Get(L5XName.Value);
        return Atomic.Parse(dataType, value);
    }

    /// <summary>
    /// Handles deserializing an array element to a <see cref="ArrayType"/>...
    /// </summary>
    private static LogixType DeserializeArray(XElement element)
    {
        var dataType = element.Get(L5XName.DataType);

        //We either know the type (atomic or registered), or we create the generic string or complex type.
        var type = Lookup.TryGetValue(dataType, out var known) ? known
            : HasStringStructure(element) ? typeof(StringType)
            : typeof(ComplexType);

        var arrayType = typeof(ArrayType<>).MakeGenericType(type);

        if (Deserializers.Value.TryGetValue(arrayType.FullName, out var cached))
            return cached.Invoke(element);

        var deserializer = arrayType.Deserializer<LogixType>();
        Deserializers.Value.Add(arrayType.FullName, deserializer);
        return deserializer.Invoke(element);
    }

    /// <summary>
    /// Handles deserializing an array index element to a logix type, either atomic, string, or structure,
    /// depending on the state of the element.
    /// </summary>
    private static LogixType DeserializeElement(XElement element)
    {
        var dataType = element.Parent?.Get(L5XName.DataType) ?? throw element.L5XError(L5XName.DataType);
        var value = element.Attribute(L5XName.Value);
        var structure = element.Element(L5XName.Structure);

        return value is not null ? Atomic.Parse(dataType, value.Value)
            : structure is not null ? DeserializeStructure(structure)
            : throw element.L5XError(L5XName.Element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StructureType"/> logix type.
    /// Will check for registered types to create the concrete type if available.
    /// Otherwise we resort to a more generic <see cref="StringType"/> of string element structures or
    /// <see cref="ComplexType"/> for everything else.
    /// </summary>
    private static LogixType DeserializeStructure(XElement element)
    {
        var dataType = element.Get(L5XName.DataType);
        if (IsRegistered(dataType)) return Deserializers.Value[dataType].Invoke(element);
        return HasStringStructure(element) ? new StringType(element) : new ComplexType(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StringType"/> logix type.
    /// Will check for registered type to create the concrete type if available.
    /// Otherwise we resort to a more generic <see cref="StringType"/> object.
    /// </summary>
    private static LogixType DeserializeString(XElement element)
    {
        var dataType = element.Parent?.Get(L5XName.DataType) ?? throw element.L5XError(L5XName.DataType);
        return IsRegistered(dataType) ? Deserializers.Value[dataType].Invoke(element) : new StringType(element);
    }

    /// <summary>
    /// Determines if the provided element has a structure that represents a <see cref="StringType"/> structure,
    /// structure member, array, or array member. This is needed to determine if we are deserializing a complex type
    /// or string type. String structure is unique in that it will have a data value member called DATA with a ASCII
    /// radix, a non-null element value, and a data type attribute value equal to that of the parent structure element attribute.
    /// </summary>
    private static bool HasStringStructure(XElement element)
    {
        //If this is a structure or structure member it could potentially be the string structure.
        if (element.Name == L5XName.Structure || element.Name == L5XName.StructureMember)
        {
            return element.Elements(L5XName.DataValueMember).Any(e =>
                e?.Value is not null
                && e.Attribute(L5XName.Name)?.Value == "DATA"
                && e.Attribute(L5XName.DataType)?.Value == e.Parent?.Attribute(L5XName.DataType)?.Value
                && e.Attribute(L5XName.Radix)?.Value == "ASCII");
        }

        //If this is an array or array member, we need to get elements and check if they are all string structure or not.
        if (element.Name == L5XName.Array || element.Name == L5XName.ArrayMember)
        {
            return element.Elements().Select(e => e.Element(L5XName.Structure)).All(HasStringStructure);
        }

        return false;
    }

    /// <summary>
    /// Performs reflection scanning of provided <see cref="Assembly"/> to get all public non abstract types
    /// inheriting from <see cref="StructureType"/> or <see cref="StringType"/> that have the supported
    /// deserialization constructor, and returns the <c>L5XType</c> and compiled deserialization delegate pair.
    /// This is used to initialize the set of concrete deserializer functions for all know predefined and user defined
    /// logix type objects.
    /// </summary>
    private static IEnumerable<KeyValuePair<string, Func<XElement, LogixType>>> Introspect(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t =>
            (typeof(StructureType).IsAssignableFrom(t) || typeof(StringType).IsAssignableFrom(t))
            && t != typeof(ComplexType) && t != typeof(StringType)
            && t is { IsAbstract: false, IsPublic: true }
            && t.GetConstructor(new[] { typeof(XElement) }) is not null);

        foreach (var type in types)
        {
            var deserializer = type.Deserializer<LogixType>();
            yield return new KeyValuePair<string, Func<XElement, LogixType>>(type.L5XType(), deserializer);
        }
    }

    /// <summary>
    /// Performs reflection scanning of provided <see cref="Assembly"/> to get all public non abstract types
    /// inheriting from <see cref="LogixType"/>. 
    /// </summary>
    private static IEnumerable<Type> FindLogixTypes(Assembly assembly)
    {
        return assembly.GetTypes().Where(t =>
            typeof(LogixType).IsAssignableFrom(t)
            && t is { IsAbstract: false, IsPublic: true }
            && t != typeof(ComplexType) && t != typeof(StringType)
            && t != typeof(ArrayType) && t != typeof(ArrayType<>)
            && t != typeof(NullType));
    }
}