using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// A static deserialization class for <see cref="LogixElement"/> objects and their derivatives.
/// </summary>
public static class LogixSerializer
{
    /// <summary>
    /// The global cache for all <see cref="LogixElement"/> object deserializer delegate functions.
    /// </summary>
    private static readonly Lazy<Dictionary<string, Func<XElement, LogixElement>>> Deserializers = new(() =>
            Introspect(typeof(LogixSerializer).Assembly).ToDictionary(k => k.Key, v => v.Value),
        LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Serializes the <see cref="LogixElement"/> object into an <see cref="XElement"/> object.
    /// </summary>
    /// <param name="element">The <see cref="LogixElement"/> object to serialize.</param>
    /// <returns>A <see cref="XElement"/> representing the serialized logic element object.</returns>
    // ReSharper disable once UnusedMember.Global
    // I know this is probably going unused, but I feel like I want it here just for completeness.
    public static XElement Serialize(LogixElement element) => element.Serialize();

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
    public static TElement Deserialize<TElement>(XElement element)
        where TElement : LogixElement => (TElement) Deserializer(typeof(TElement)).Invoke(element);

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static LogixElement Deserialize(Type type, XElement element) =>
        Deserializer(type).Invoke(element);

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the first matching <see cref="LogixElement"/> type found in the
    /// element hierarchy.
    /// </summary>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>If the element is or has a parent of a known deserializable type, then a new <see cref="LogixElement"/>
    /// of the first found type in the XML tree; Otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This method will traverse the XMl tree until it reaches a <c>XElement</c> with a name matching
    /// a known deserializable logic element type. Once it finds that type/element pair, it will deserialize it as that
    /// type and return the result. This is useful when we cross reference and find child elements that we need to
    /// deserialize by finding it's parent.
    /// </remarks>
    public static LogixElement Deserialize(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

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
    /// Handles getting the deserializer delegate for the specified type. If the type is not cached, this method will check
    /// if the type inherits <see cref="LogixElement"/> and has a valid constructor. If so, it will add to the
    /// global deserializer cache and return the deserializer delegate function.
    /// </summary>
    private static Func<XElement, LogixElement> Deserializer(Type type)
    {
        //We use the configured L5XType for the cache since a single type may have multiple supported element types.
        var typeName = type.L5XType();

        //If in cache then return.
        if (Deserializers.Value.TryGetValue(typeName, out var cached)) return cached;

        //If not then get the deserializer and cache it for all L5XTypes the given type supports.
        var deserializer = type.Deserializer<LogixElement>();
        foreach (var supportedType in type.L5XTypes())
            Deserializers.Value.TryAdd(supportedType, deserializer);

        return deserializer;
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
               type is {IsAbstract: false, IsPublic: true} &&
               type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] {typeof(XElement)},
                   null) is not null;
    }
}