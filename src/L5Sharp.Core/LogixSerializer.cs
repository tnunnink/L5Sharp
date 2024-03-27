using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A static deserialization class for <see cref="LogixElement"/> objects and their derivatives.
/// This class uses a dictionary to cache deserialization functions for types deriving from LogixElement.
/// We are using compiled expression functions as they are more performant that invoking constructors via reflection.
/// We are also caching them for reuse so we don't have to build them each time we call <see cref="Deserialize"/>.
/// </summary>
public static class LogixSerializer
{
    /// <summary>
    /// A collection of types to exclude from the deserialization factories since we handle them separately.
    /// </summary>
    private static readonly List<Type> Exclusions =
    [
        typeof(ComplexType), typeof(StringType), typeof(ArrayType<>), typeof(NullType)
    ];

    /// <summary>
    /// The global cache for all <see cref="LogixElement"/> object deserializer delegate functions.
    /// </summary>
    private static readonly Lazy<Dictionary<string, Func<XElement, LogixElement>>> Deserializers = new(() =>
        Scan().ToDictionary(k => k.Key, v => v.Value), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// A system wide lookup of all <see cref="LogixType"/> objects by L5XType name obtained using reflection. This does
    /// not include the internal generic types such as StructureType, ComplexType, StringType, ArrayType, or ArrayType{T},
    /// or NullType, but rather types that can be instantiated (atomic or complex) for which we may need to create a generic
    /// array of.
    /// </summary>
    private static readonly Dictionary<string, Type> DataTypes =
        AppDomain.CurrentDomain.GetAssemblies().SelectMany(FindDataTypes).ToDictionary(k => k.L5XType(), v => v);

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
    public static TElement Deserialize<TElement>(this XElement element) 
        where TElement : LogixElement => (TElement)Deserialize(element);

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the first matching <see cref="LogixElement"/> type found in the
    /// element hierarchy.
    /// </summary>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>If the element is or has a parent of a known deserializable type, then a new <see cref="LogixElement"/>
    /// of the first found type in the XML tree; Otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This method will traverse the XML tree until it reaches a <c>XElement</c> with a name matching
    /// a known deserializable logic element type. Once it finds that type/element pair, it will deserialize it as that
    /// type and return the result. This is important for any feature that would require deserialization of
    /// logix elements when the type is not known at compile type or perhaps returns a collection of different element
    /// types. It is up to the caller to infer or cast the resulting element type appropriately. 
    /// </remarks>
    public static LogixElement Deserialize(this XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        while (true)
        {
            if (Deserializers.Value.TryGetValue(element.L5XType(), out var deserializer))
                return deserializer.Invoke(element);
            element = element.Parent ??
                      throw new InvalidOperationException(
                          $"Could not find deserializable type for element {element.Name}.");
        }
    }

    /// <summary>
    /// Creates our global deserialization collection to initialize our dictionary lookup. This will add custom data
    /// deserializer functions defined in this file, as well as scan this and all loaded assemblies for deserializable
    /// types the are defined in this an other libraries in order to make our <see cref="LogixSerializer"/> implementation
    /// aware of the types it can create.
    /// </summary>
    private static IEnumerable<KeyValuePair<string, Func<XElement, LogixElement>>> Scan()
    {
        var deserializers = new List<KeyValuePair<string, Func<XElement, LogixElement>>>();

        //Adds custom data deserializer functions defined in this file below.
        deserializers.AddRange(DataDeserializers());

        //Scan and add types from this assembly first.
        var sharp = typeof(LogixSerializer).Assembly;
        deserializers.AddRange(Introspect(sharp));

        //Scan other loaded assemblies for use defined types.
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a != sharp);
        foreach (var assembly in assemblies)
            deserializers.AddRange(Introspect(assembly));

        return deserializers;
    }

    /// <summary>
    /// Performs reflection scanning of provided <see cref="Assembly"/> to get all public non abstract types
    /// inheriting from <see cref="LogixElement"/> that have the supported deserialization constructor,
    /// and returns the <c>L5XType</c> and compiled deserialization delegate pair. This is used to initialize the
    /// set of concrete deserializer functions for all known logix element objects.
    /// </summary>
    private static IEnumerable<KeyValuePair<string, Func<XElement, LogixElement>>> Introspect(Assembly assembly)
    {
        var deserializers = new List<KeyValuePair<string, Func<XElement, LogixElement>>>();

        var types = assembly.GetTypes().Where(IsDeserializableType);

        foreach (var type in types)
        {
            var deserializer = type.Deserializer<LogixElement>();
            deserializers.AddRange(type.L5XTypes()
                .Select(t => new KeyValuePair<string, Func<XElement, LogixElement>>(t, deserializer)));
        }

        return deserializers;
    }

    /// <summary>
    /// Checks whether the type is deserializable by this library. This means that it inherits <see cref="LogixElement"/>,
    /// is a public non-abstract type, and has a constructor accepting a single <see cref="XElement"/> parameter.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type is deserializable; otherwise, <c>false</c>.</returns>
    private static bool IsDeserializableType(Type type)
    {
        return typeof(LogixElement).IsAssignableFrom(type) &&
               !Exclusions.Contains(type) &&
               type is { IsAbstract: false, IsPublic: true } &&
               type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(XElement) },
                   null) is not null;
    }

    /// <summary>
    /// Returns a collection of custom deserializer functions that will know how to deserialize the complex
    /// data structures found on tag elements.
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<KeyValuePair<string, Func<XElement, LogixElement>>> DataDeserializers()
    {
        var deserializers = new List<KeyValuePair<string, Func<XElement, LogixElement>>>
        {
            new(L5XName.Data, DeserializeData),
            new(L5XName.DefaultData, DeserializeData),
            new(L5XName.DataValue, DeserializeAtomic),
            new(L5XName.DataValueMember, DeserializeAtomic),
            new(L5XName.Element, DeserializeElement),
            new(L5XName.Array, DeserializeArray),
            new(L5XName.ArrayMember, DeserializeArray),
            new(L5XName.Structure, DeserializeStructure),
            new(L5XName.StructureMember, DeserializeStructure)
        };

        return deserializers;
    }

    /// <summary>
    /// Performs reflection scanning of provided <see cref="Assembly"/> to get all public non abstract types
    /// inheriting from <see cref="LogixType"/>. 
    /// </summary>
    private static IEnumerable<Type> FindDataTypes(Assembly assembly)
    {
        return assembly.GetTypes().Where(t =>
            typeof(LogixType).IsAssignableFrom(t)
            && t is { IsAbstract: false, IsPublic: true }
            && t != typeof(ComplexType) && t != typeof(StringType)
            && t != typeof(ArrayType) && t != typeof(ArrayType<>)
            && t != typeof(NullType));
    }

    #region DataSerialization

    /// <summary>
    /// Handles deserializing a data or default data element with a format attribute to a logix data object.
    /// This method will forward call down the chain based on the format of the data structure.
    /// </summary>
    private static LogixElement DeserializeData(XElement element)
    {
        var format = element.Attribute(L5XName.Format)?.Value.TryParse<DataFormat>();

        if (format is null)
            return LogixType.Null;

        if (format == DataFormat.String)
            return DeserializeString(element);

        if (element.FirstNode is not XElement data)
            return LogixType.Null;

        //We could just call deserialize again, but we risk infinite loops if the child is not registered or recognizable.
        //Probably not going to happen, but we can stop and throw an exception if it is not the expected element, which is probably better.
        return data.Name.ToString() switch
        {
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
            _ => throw new NotSupportedException($"The element '{element.Name}' is not a supported data element.")
        };
    }

    /// <summary>
    /// Handles deserializing an element to a atomic value type. This will get the data type name  and value and use
    /// our predefined parse function on AtomicType to instantiate the data 
    /// </summary>
    private static LogixElement DeserializeAtomic(XElement element)
    {
        var dataType = element.DataType();
        var value = element.Get(L5XName.Value);
        return AtomicType.Parse(dataType, value);
    }

    /// <summary>
    /// Handles deserializing an array to an array type logix element. This will get the data type name and use that
    /// to lookup the corresponding type information. Once we get that we can form a generic deserializer, check for
    /// it's existence, and deserialize the provided element.
    /// </summary>
    private static LogixElement DeserializeArray(XElement element)
    {
        var dataType = element.DataType();

        //We either know the type (atomic or registered), or we create the generic string or complex type.
        var type = DataTypes.TryGetValue(dataType, out var known) ? known
            : element.IsStringData() ? typeof(StringType)
            : typeof(ComplexType);

        var arrayType = typeof(ArrayType<>).MakeGenericType(type);
        var arrayName = arrayType.FullName!;

        if (Deserializers.Value.TryGetValue(arrayName, out var cached))
            return cached.Invoke(element);

        var deserializer = arrayType.Deserializer<LogixType>();
        Deserializers.Value.Add(arrayName, deserializer);
        return deserializer.Invoke(element);
    }

    /// <summary>
    /// Handles deserializing an array index element to a logix type, either atomic, string, or structure,
    /// depending on the state of the element.
    /// </summary>
    private static LogixElement DeserializeElement(XElement element)
    {
        var value = element.Attribute(L5XName.Value);
        if (value is not null) return DeserializeAtomic(element);

        var structure = element.Element(L5XName.Structure);
        return structure is not null ? DeserializeStructure(structure) : LogixType.Null;
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StructureType"/> logix type. Will check for registered types
    /// to create the concrete type if available. Otherwise we resort to a more generic <see cref="StringType"/>
    /// for string element structures or <see cref="ComplexType"/> for everything else.
    /// </summary>
    private static LogixElement DeserializeStructure(XElement element)
    {
        var dataType = element.DataType();

        if (Deserializers.Value.TryGetValue(dataType, out var deserializer))
            return deserializer.Invoke(element);

        return element.IsStringData() ? new StringType(element) : new ComplexType(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StringType"/> logix type. Will check for registered types to
    /// create the concrete type if available. Otherwise we resort to a more generic <see cref="StringType"/> object.
    /// </summary>
    private static LogixElement DeserializeString(XElement element)
    {
        var dataType = element.DataType();

        return Deserializers.Value.TryGetValue(dataType, out var deserializer)
            ? deserializer.Invoke(element)
            : new StringType(element);
    }

    #endregion
}