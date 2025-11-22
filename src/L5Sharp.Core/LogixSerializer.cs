using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A static deserialization class for <see cref="ILogixElement"/> objects and their derivatives.
/// 
/// </summary>
public static class LogixSerializer
{
    /// <summary>
    /// A collection mapping <see cref="Type"/> objects to their associated element names, which represent
    /// the registered names or identifiers for those types within the system.
    /// Used internally to manage and retrieve metadata about registered <see cref="LogixElement"/> types.
    /// </summary>
    private static readonly Dictionary<Type, string[]> Types = [];

    /// <summary>
    /// A registry mapping XML element names to deserializer functions for converting
    /// XML elements into their corresponding <see cref="ILogixElement"/> representations.
    /// Used internally to facilitate parsing and reconstruction of L5X elements.
    /// </summary>
    private static readonly Dictionary<string, Func<XElement, ILogixElement>> ElementDeserializers = [];

    /// <summary>
    /// A dictionary mapping string-based data type identifiers to deserializer functions that parse
    /// <see cref="XElement"/> objects into corresponding <see cref="ILogixElement"/> instances.
    /// Used internally to define and retrieve deserialization logic for specific user-defined structure types
    /// within the L5X serialization system.
    /// </summary>
    private static readonly Dictionary<string, Func<XElement, ILogixElement>> DataDeserializers = [];

    /// <summary>
    /// Registers a specified deserialization function for a given <see cref="ILogixElement"/> type and associates it with one or more names.
    /// </summary>
    /// <typeparam name="TElement">The type of <see cref="ILogixElement"/> to register.</typeparam>
    /// <param name="deserializer">A function that converts an <see cref="XElement"/> into an instance of the specified <typeparamref name="TElement"/>.</param>
    /// <param name="names">One or more names to associate with the specified type and deserialization function.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="deserializer"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when no names are provided in the <paramref name="names"/> parameter.</exception>
    /// <remarks>
    /// If the specified type is a derivative of <see cref="StructureData"/> then we assume it is a custom data type
    /// registration, and the provided name should be the name of the data type. When we encounter a structure type
    /// with a matching data type name, the provided function is used to create the concrete instance of the type. All
    /// other element types will be identified by the XML element name(s).
    /// </remarks>
    public static void Register<TElement>(Func<XElement, ILogixElement> deserializer, params string[] names)
        where TElement : ILogixElement
    {
        if (deserializer is null)
            throw new ArgumentNullException(nameof(deserializer));

        if (names.Length == 0)
            throw new ArgumentException($"At least one name is required to register type {typeof(TElement)}");

        var type = typeof(TElement);
        var keys = names.ToList();

        //Register the type with the collection of provided named keys for lookup.
        Types[type] = names;

        //If this is a LogixData derivative, then we will register it by data type name.
        //Otherwise, we register it to the standard element name lookup. 
        if (typeof(LogixData).IsAssignableFrom(type))
        {
            keys.ForEach(k => DataDeserializers[k] = deserializer);
        }
        else
        {
            keys.ForEach(k => ElementDeserializers[k] = deserializer);
        }
    }

    /// <summary>
    /// Checks if the specified <see cref="Type"/> has been registered within the system.
    /// </summary>
    /// <param name="type">The type to check for registration.</param>
    /// <returns>True if the type is registered; otherwise, false.</returns>
    public static bool IsRegistered(Type type)
    {
        return Types.ContainsKey(type);
    }

    /// <summary>
    /// Determines if a deserializer for the specified XML element or data type name has been registered.
    /// </summary>
    /// <param name="typeName">The name of the XML element to check for registration.</param>
    /// <returns>True if the specified XML element name has a registered deserializer; otherwise, false.</returns>
    public static bool IsRegistered(string typeName)
    {
        return ElementDeserializers.ContainsKey(typeName) || DataDeserializers.ContainsKey(typeName);
    }

    /// <summary>
    /// Retrieves the names associated with a specified type from the registry.
    /// If the type is not present in the registry, an empty array is returned.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> for which the associated element names are to be retrieved.</param>
    /// <returns>An array of strings containing the element names associated with the specified type.</returns>
    public static string[] NamesFor(Type type)
    {
        return Types.TryGetValue(type, out var names) ? names : [];
    }

    /// <summary>
    /// Serializes the specified <see cref="ILogixElement"/> into its <see cref="string"/> XML representation.
    /// </summary>
    /// <param name="element">The <see cref="ILogixElement"/> to be serialized.</param>
    /// <returns>A <see cref="string"/> that represents the serialized form of the provided <see cref="ILogixElement"/>.</returns>
    public static string Serialize(ILogixElement element) => element.Serialize().ToString();

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to deserialize.</param>
    /// <typeparam name="TElement">The return type of the deserialized element.</typeparam>
    /// <returns>A new object of the specified type representing the deserialized element.</returns>
    /// <remarks>
    /// The return object must specify a public constructor accepting a <see cref="XElement"/> parameter for
    /// deserialization to work.
    /// </remarks>
    public static TElement Deserialize<TElement>(this XElement element) where TElement : ILogixElement
    {
        return Deserialize(element).As<TElement>();
    }

    /// <summary>
    /// Deserializes the specified <see cref="XElement"/> into an instance of <see cref="ILogixElement"/>.
    /// </summary>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>An instance of <see cref="ILogixElement"/> that represents the deserialized content of the XML element.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="element"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when no deserializer is registered for the XML element's name.</exception>
    public static ILogixElement Deserialize(this XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        if (IsDeserializableDataType(element, out var dataType))
            return dataType;

        if (ElementDeserializers.TryGetValue(element.Name.LocalName, out var deserializer))
            return deserializer(element);

        throw new InvalidOperationException($"No deserializer is registered for element: '{element.Name.LocalName}'");
    }

    #region DataSerialization

    /// <summary>
    /// Determines if the specified <see cref="XElement"/> represents a deserializable data type
    /// and attempts to deserialize it into an <see cref="ILogixElement"/> instance.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to evaluate and potentially deserialize.</param>
    /// <param name="dataType">
    /// When this method returns, contains the deserialized <see cref="ILogixElement"/> object if successful;
    /// otherwise, null.
    /// </param>
    /// <returns>
    /// True if the specified <see cref="XElement"/> represents a deserializable data type and was successfully
    /// deserialized; otherwise, false.
    /// </returns>
    private static bool IsDeserializableDataType(XElement element, out ILogixElement dataType)
    {
        dataType = null!;
        if (!element.IsDataElement()) return false;

        dataType = element.Name.LocalName switch
        {
            L5XName.Data or L5XName.DefaultData => DeserializeData(element),
            L5XName.DataValue or L5XName.DataValueMember => DeserializeAtomic(element),
            L5XName.StructureMember when element.IsStringData() => DeserializeString(element),
            L5XName.Structure or L5XName.StructureMember => DeserializeStructure(element),
            L5XName.Array or L5XName.ArrayMember => new ArrayData(element),
            //All remaining specialized data formats should be handled by concrete LogixData implementations
            //that are decorated with the appropriate LogixElementAttribute 
            _ => element.Deserialize()
        };

        return true;
    }


    /// <summary>
    /// Handles deserializing a data or default data element with a format attribute to a logix data object.
    /// For this element we need to intercept string-formatted data. Otherwise, we can get the first child node and
    /// recursively call our deserialize method to get the correct instance.
    /// </summary>
    private static ILogixElement DeserializeData(XElement element)
    {
        var format = DataFormat.TryParse(element.Attribute(L5XName.Format)?.Value);

        if (format is not null && format == DataFormat.String)
            return DeserializeString(element);

        if (format is not null && format != DataFormat.L5K && element.FirstNode is XElement data)
            return data.Deserialize();

        return LogixType.Null;
    }

    /// <summary>
    /// Handles deserializing an element to an atomic value type. This will get the data type name and value and use
    /// our predefined parse function on AtomicType to instantiate the data 
    /// </summary>
    private static ILogixElement DeserializeAtomic(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);

        if (DataDeserializers.TryGetValue(dataType, out var deserializer))
            return deserializer(element);

        throw new NotSupportedException($"Atomic data type '{dataType}' not currently supported");
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StructureData"/> logix type.
    /// Will check for registered types to create the concrete type if available.
    /// Otherwise, we resort to the base <see cref="StringData"/> type.
    /// </summary>
    private static ILogixElement DeserializeStructure(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);

        if (DataDeserializers.TryGetValue(dataType, out var deserializer))
            return deserializer(element);

        return new StructureData(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StringData"/> logix type.
    /// Will check for registered types to create the concrete type if available.
    /// Otherwise, we resort to the base <see cref="StringData"/> type.
    /// </summary>
    private static ILogixElement DeserializeString(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);

        if (DataDeserializers.TryGetValue(dataType, out var deserializer))
            return deserializer(element);

        return new StringData(element);
    }

    /// <summary>
    /// Determines whether the specified <see cref="XElement"/> represents a data-related element
    /// such as Data, DefaultData, Array, Structure, or their respective members.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to evaluate.</param>
    /// <returns>
    /// <c>true</c> if the element is a data-related element; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsDataElement(this XElement element)
    {
        return element.Name.LocalName
            is L5XName.Data
            or L5XName.DefaultData
            or L5XName.DataValue
            or L5XName.DataValueMember
            or L5XName.Array
            or L5XName.ArrayMember
            or L5XName.Structure
            or L5XName.StructureMember;
    }

    /// <summary>
    /// Determines if the provided element has a structure that represents a <see cref="StringData"/> structure,
    /// structure member, array, or array member.
    /// </summary>
    /// <param name="element">The element to check for the known string data structure.</param>
    /// <returns><c>true</c> if the element has the string type structure, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This is needed to determine if we are deserializing a complex type or string type. String structure is unique
    /// in that it will have a data value member called DATA with an ASCII radix, a non-null element value, and a
    /// data type attribute value equal to that of the parent structure element attribute. If we don't intercept this
    /// structure before deserializing it, we will encounter exceptions because it doesn't conform to the normal
    /// convention that data value members should represent and atomic structure. My thought is Logix did this to conserve
    /// space in the L5X, but not sure.
    /// </remarks>
    private static bool IsStringData(this XElement? element)
    {
        if (element is null) return false;

        //If this is a structure or structure member, it could potentially be the string structure.
        if (element.Name.LocalName is L5XName.Structure or L5XName.StructureMember)
        {
            return element.Elements(L5XName.DataValueMember).Any(e =>
                e.Attribute(L5XName.Name)?.Value == "DATA"
                && e.Attribute(L5XName.DataType)?.Value == e.Parent?.Attribute(L5XName.DataType)?.Value
                && e.Attribute(L5XName.Radix)?.Value == "ASCII");
        }

        //If this is an array or array member, we need to get elements and check if they are all string structure or not.
        return element.Name.LocalName is L5XName.Array or L5XName.ArrayMember
               && element.Elements().Select(e => e.Element(L5XName.Structure)).All(x => x.IsStringData());
    }

    #endregion
}