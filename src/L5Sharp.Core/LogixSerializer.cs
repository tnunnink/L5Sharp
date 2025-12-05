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
    private static readonly Dictionary<Type, HashSet<string>> Types = [];

    /// <summary>
    /// A registry mapping XML element names to deserializer functions for converting
    /// XML elements into their corresponding <see cref="ILogixElement"/> representations.
    /// Used internally to facilitate parsing and reconstruction of L5X elements.
    /// </summary>
    private static readonly Dictionary<string, Func<XElement, ILogixElement>> Deserializers = [];

    /// <summary>
    /// Register custom functions statically once the class is initialized.
    /// Everything else is registered via source generators.
    /// </summary>
    static LogixSerializer()
    {
        RegisterFormattedData();
        RegisterAtomicData();
        RegisterStructureData();
        RegisterArrayElementData();
    }

    /// <summary>
    /// Registers a specified deserialization function for a given <see cref="ILogixElement"/> type and associates it with one or more names.
    /// </summary>
    /// <typeparam name="TElement">The type of <see cref="ILogixElement"/> to register.</typeparam>
    /// <param name="deserializer">A function that converts an <see cref="XElement"/> into an instance of the specified <typeparamref name="TElement"/>.</param>
    /// <param name="names">One or more names to associate with the specified type and deserialization function.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="deserializer"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when no names are provided in the <paramref name="names"/> parameter.</exception>
    public static void Register<TElement>(Func<XElement, ILogixElement> deserializer, params string[] names)
        where TElement : ILogixElement
    {
        if (deserializer is null)
            throw new ArgumentNullException(nameof(deserializer));

        if (names.Length == 0)
            throw new ArgumentException($"At least one name is required to register type {typeof(TElement)}");

        var type = typeof(TElement);
        var keys = names.ToList();

        if (Types.TryGetValue(type, out var set))
            keys.ForEach(k => set.Add(k));
        else
            Types[type] = [..keys];

        keys.ForEach(k => Deserializers[k] = deserializer);
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
    /// Determines if a deserializer for the specified XML element name has been registered.
    /// </summary>
    /// <param name="element">The name of the XML element to check for registration.</param>
    /// <returns>True if the specified XML element name has a registered deserializer; otherwise, false.</returns>
    public static bool IsRegistered(string element)
    {
        return Deserializers.ContainsKey(element);
    }

    /// <summary>
    /// Retrieves the names associated with a specified type from the registry.
    /// If the type is not present in the registry, an empty set is returned.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> for which the associated element names are to be retrieved.</param>
    /// <returns>An <see cref="HashSet{T}"/> of strings containing the element names associated with the specified type.</returns>
    public static HashSet<string> NamesFor(Type type)
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
        return (TElement)Deserialize(element);
    }

    /// <summary>
    /// Deserializes the specified <see cref="XElement"/> into an instance of <see cref="ILogixElement"/>.
    /// </summary>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>An instance of <see cref="ILogixElement"/> that represents the deserialized content of the XML element.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="element"/> is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when no deserializer is registered for the XML element's name.</exception>
    public static ILogixElement Deserialize(this XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var key = element.Name.LocalName;

        if (Deserializers.TryGetValue(key, out var deserializer))
            return deserializer(element);

        throw new NotSupportedException($"No deserializer is registered for element: '{key}'");
    }


    /// <summary>
    /// Registers a method to handle the deserialization of formatted data elements.
    /// We don't have a type that corresponds to Data or DefaultData, and we need to handle some of the unique
    /// data formats such as string and L5K. All other formats (decorated, alarm, message, etc.) should be deserializable
    /// based on the child element name.
    /// </summary>
    private static void RegisterFormattedData()
    {
        Register<LogixData>(e =>
            {
                var format = DataFormat.Parse(e.Attribute(L5XName.Format)?.Value ?? throw e.L5XError(L5XName.Format));

                if (format == DataFormat.L5K)
                    throw new NotSupportedException("L5K is not a supported data format for deserialization.");

                if (format == DataFormat.String)
                    return new StringData(e);

                if (e.FirstNode is XElement formatted)
                    return formatted.Deserialize<LogixData>();

                return LogixType.Null;
            },
            L5XName.Data,
            L5XName.DefaultData
        );
    }

    /// <summary>
    /// Registers atomic data types for deserialization. Maps the DataValue element to specific atomic data types,
    /// such as BOOL, SINT, and REAL, based on the type name provided within the <see cref="XElement"/> instance.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when attempting to register an unsupported atomic data type.</exception>
    private static void RegisterAtomicData()
    {
        Register<AtomicData>(e =>
            {
                return e.DataType() switch
                {
                    nameof(BOOL) => new BOOL(e),
                    nameof(SINT) => new SINT(e),
                    nameof(USINT) => new USINT(e),
                    nameof(INT) => new INT(e),
                    nameof(UINT) => new UINT(e),
                    nameof(DINT) => new DINT(e),
                    nameof(UDINT) => new UDINT(e),
                    nameof(LINT) => new LINT(e),
                    nameof(ULINT) => new ULINT(e),
                    nameof(REAL) => new REAL(e),
                    nameof(LREAL) => new LREAL(e),
                    _ => throw new NotSupportedException(
                        $"The type '{e.DataType()}' is not a supported atomic data value.")
                };
            },
            L5XName.DataValue,
            L5XName.DataValueMember
        );
    }

    /// <summary>
    /// Registers the custom structure data deserializer function to handle string data elements.
    /// If not a string structure, we will always return the base <see cref="StructureData"/> instance.
    /// This type can be cast down to more concrete types using As method on <see cref="ILogixElement"/>   
    /// </summary>
    private static void RegisterStructureData()
    {
        Register<StructureData>(e =>
            {
                //Detect if the structure element contains a nested string value (CData node)
                //so that we can deserialize as string data.
                if (e.Elements().Any(m => m.FirstNode is XCData))
                {
                    return new StringData(e);
                }

                return new StructureData(e);
            },
            L5XName.Structure,
            L5XName.StructureMember
        );
    }

    /// <summary>
    /// Registers a custom deserialization function for array element data.
    /// The method ensures proper handling of different types of element structures,
    /// including those with nested string values or CDATA sections.
    /// This method dynamically maps the corresponding data type for deserialization purposes.
    /// </summary>
    /// <exception cref="NotSupportedException">
    /// Thrown when the provided XML structure or attributes are not supported for deserialization.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the structure of the provided XElement is invalid or incomplete.
    /// </exception>
    private static void RegisterArrayElementData()
    {
        Register<LogixData>(e =>
            {
                if (e.Attribute(L5XName.Value) is not null)
                {
                    return e.DataType() switch
                    {
                        nameof(BOOL) => new BOOL(e),
                        nameof(SINT) => new SINT(e),
                        nameof(USINT) => new USINT(e),
                        nameof(INT) => new INT(e),
                        nameof(UINT) => new UINT(e),
                        nameof(DINT) => new DINT(e),
                        nameof(UDINT) => new UDINT(e),
                        nameof(LINT) => new LINT(e),
                        nameof(ULINT) => new ULINT(e),
                        nameof(REAL) => new REAL(e),
                        nameof(LREAL) => new LREAL(e),
                        _ => throw new NotSupportedException(
                            $"The type '{e.DataType()}' is not a supported atomic data value.")
                    };
                }

                if (e.FirstNode is not XElement structure)
                    throw e.L5XError(L5XName.Structure);

                //Detect if the structure element contains a nested string value (CData node)
                //so that we can deserialize as string data.
                if (structure.Elements().Any(m => m.FirstNode is XCData))
                {
                    return new StringData(structure);
                }

                return new StructureData(structure);
            },
            L5XName.Element
        );
    }
}