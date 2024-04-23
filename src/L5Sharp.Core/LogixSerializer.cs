using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    //The prefix for types inheriting from a LogixType object.
    private const string DataTypePrefix = "DataType:";

    /// <summary>
    /// A collection of types to exclude from the deserialization factories since we handle them separately.
    /// </summary>
    private static readonly List<Type> Exclusions =
    [
        typeof(LogixContainer<>),
        typeof(LogixInfo),
        typeof(ComplexData),
        typeof(StringData),
        typeof(ArrayData<>),
        typeof(NullData)
    ];

    /// <summary>
    /// The global cache for all <see cref="LogixElement"/> object deserializer delegate functions.
    /// </summary>
    private static readonly Lazy<Dictionary<string, Func<XElement, LogixElement>>> Deserializers = new(() =>
        Scan().ToDictionary(k => k.Key, v => v.Value), LazyThreadSafetyMode.ExecutionAndPublication);

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

            //We have to differentiate a "DataType" element and any other element since in theory you could create a
            //data type with the name of a component or element, and we don't want our serializer to confuse the two types.
            //Therefore we prepend all LogixType types with our internal known prefix and will use that when lookup occurs.
            var isDataType = typeof(LogixData).IsAssignableFrom(type);
            var names = type.L5XTypes().Select(n => isDataType ? $"{DataTypePrefix}{n}" : $"{n}");
            var functions = names.Select(n => new KeyValuePair<string, Func<XElement, LogixElement>>(n, deserializer));
            deserializers.AddRange(functions);
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
               type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, [typeof(XElement)], null) 
                   is not null;
    }

    /// <summary>
    /// Returns a collection of custom deserializer functions that will know how to deserialize the complex
    /// data structures found on tag elements.
    /// </summary>
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
    /// Builds a deserialization expression delegate which returns the specified type using the current type information.
    /// </summary>
    /// <param name="type">The current type for which to build the expression.</param>
    /// <typeparam name="TReturn">The return type of the expression delegate.</typeparam>
    /// <returns>A <see cref="Func{TResult}"/> which accepts a <see cref="XElement"/> and returns the specified
    /// return type.</returns>
    /// <remarks>
    /// This extension is the basis for how we build the deserialization functions using reflection and
    /// expression trees. Using compiled expression trees is much more efficient that calling the invoke method for a type's
    /// constructor info obtained via reflection. This method makes all the necessary checks on the current type, ensuring the
    /// returned deserializer delegate will execute without exception.
    /// </remarks>
    private static Func<XElement, TReturn> Deserializer<TReturn>(this Type type)    
    {
        if (type is null) 
            throw new ArgumentNullException(nameof(type));

        if (type.IsAbstract)
            throw new ArgumentException($"Can not build deserializer expression for abstract type '{type.Name}'.");

        if (!typeof(TReturn).IsAssignableFrom(type))
            throw new ArgumentException(
                $"The type {type.Name} is not assignable (inherited) from '{typeof(TReturn).Name}'.");

        var constructor = type.GetConstructor([typeof(XElement)]);

        if (constructor is null || !constructor.IsPublic)
            throw new ArgumentException(
                $"Can not build expression for type '{type.Name}' without public constructor accepting a XElement parameter.");

        var parameter = Expression.Parameter(typeof(XElement), "e");
        var expression = Expression.New(constructor, parameter);
        return Expression.Lambda<Func<XElement, TReturn>>(expression, parameter).Compile();
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
            return LogixData.Null;

        if (format == DataFormat.String)
            return DeserializeString(element);

        if (element.FirstNode is not XElement data)
            return LogixData.Null;

        //We could just call deserialize again, but we risk infinite loops if the child is not registered or recognizable.
        //Probably not going to happen, but we can stop and throw an exception if it is not the expected element, which is probably better.
        return data.Name.LocalName switch
        {
            L5XName.DataValue => DeserializeAtomic(data),
            L5XName.DataValueMember => DeserializeAtomic(data),
            L5XName.Element => DeserializeElement(data),
            L5XName.Array => DeserializeArray(data),
            L5XName.ArrayMember => DeserializeArray(data),
            L5XName.Structure => DeserializeStructure(data),
            L5XName.StructureMember => DeserializeStructure(data),
            L5XName.AlarmAnalogParameters => new ALARM_ANALOG(data),
            L5XName.AlarmDigitalParameters => new ALARM_DIGITAL(data),
            L5XName.MessageParameters => new MESSAGE(data),
            //todo need to add remaining special predefined parameter types (Axis, Motion, Coordinate) 
            _ => throw new NotSupportedException($"The element '{data.Name}' is not a supported data element.")
        };
    }

    /// <summary>
    /// Handles deserializing an element to a atomic value type. This will get the data type name and value and use
    /// our predefined parse function on AtomicType to instantiate the data 
    /// </summary>
    private static LogixElement DeserializeAtomic(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);
        var value = element.Get(L5XName.Value);
        return AtomicData.Parse(dataType, value);
    }

    /// <summary>
    /// Handles deserializing an array to an array type logix element. Since the user can still case a generic
    /// <see cref="ArrayData{TLogixType}"/> down to a more specific type using the Cast function, all we really need to
    /// do is return a generic ArrayType wrapping the element. The DeserializeElement will ultimately get elements
    /// that are concrete typed.
    /// </summary>
    private static LogixElement DeserializeArray(XElement element)
    {
        return new ArrayData<LogixData>(element);
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
        return structure is not null ? DeserializeStructure(structure) : LogixData.Null;
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StructureData"/> logix type. Will check for registered types
    /// to create the concrete type if available. Otherwise we resort to a more generic <see cref="StringData"/>
    /// for string element structures or <see cref="ComplexData"/> for everything else.
    /// </summary>
    private static LogixElement DeserializeStructure(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);
        var key = $"{DataTypePrefix}{dataType}";

        if (Deserializers.Value.TryGetValue(key, out var deserializer))
            return deserializer.Invoke(element);

        return element.IsStringData() ? new StringData(element) : new ComplexData(element);
    }

    /// <summary>
    /// Handles deserializing an element to a <see cref="StringData"/> logix type. Will check for registered types to
    /// create the concrete type if available. Otherwise we resort to a more generic <see cref="StringData"/> object.
    /// </summary>
    private static LogixElement DeserializeString(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);
        var key = $"{DataTypePrefix}{dataType}";

        return Deserializers.Value.TryGetValue(key, out var deserializer)
            ? deserializer.Invoke(element)
            : new StringData(element);
    }

    /// <summary>
    /// Determines if the provided element has a structure that represents a <see cref="StringData"/> structure,
    /// structure member, array, or array member.
    /// </summary>
    /// <param name="element">The element to check for the known string data structure.</param>
    /// <returns><c>true</c> if the element has the string type structure, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// This is needed to determine if we are deserializing a complex type or string type. String structure is unique
    /// in that it will have a data value member called DATA with a ASCII radix, a non-null element value, and a
    /// data type attribute value equal to that of the parent structure element attribute. If we don't intercept this
    /// structure prior to deserializing it, we will encounter exceptions because it doesn't conform to the normal
    /// convention that data value members should represent and atomic structure. My thought is Logix did this to conserve
    /// space in the L5X, but not sure.
    /// </remarks>
    private static bool IsStringData(this XElement? element)
    {
        if (element is null) return false;

        //If this is a structure or structure member it could potentially be the string structure.
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