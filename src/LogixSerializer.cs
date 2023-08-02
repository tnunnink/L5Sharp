using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A static deserialization class for <see cref="LogixElement"/> objects and their derivatives.
/// </summary>
public static class LogixSerializer
{
    /// <summary>
    /// The global cache for all <see cref="LogixElement"/> object deserializer delegate functions.
    /// </summary>
    private static readonly Lazy<Dictionary<Type, Func<XElement, LogixElement>>> Deserializers = new(() =>
        Introspect(typeof(LogixSerializer).Assembly).ToDictionary(k => k.Key, v => v.Value));

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
    public static TElement Deserialize<TElement>(XElement element) where TElement : LogixElement =>
        (TElement)Deserializer(typeof(TElement)).Invoke(element);

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static object Deserialize(Type type, XElement element) => Deserializer(type).Invoke(element);

    /// <summary>
    /// Handles getting the deserializer delegate for the specified type. If the type is not cached, this method will check
    /// if the type inherits <see cref="LogixElement"/> and has a valid constructor. If so, it will add to the
    /// global deserializer cache and return the deserializer delegate function.
    /// </summary>
    private static Func<XElement, LogixElement> Deserializer(Type type)
    {
        if (Deserializers.Value.TryGetValue(type, out var cached))
            return cached;

        var deserializer = type.Deserializer<LogixElement>();
        if (!Deserializers.Value.TryAdd(type, deserializer))
            throw new InvalidOperationException($"The type {type.Name} is already registered.");    
        return deserializer;
    }

    /// <summary>
    /// Performs reflection scanning of provided <see cref="Assembly"/> to get all public non abstract types
    /// inheriting from <see cref="LogixElement"/> that have the supported deserialization constructor,
    /// and returns the <c>L5XType</c> and compiled deserialization delegate pair. This is used to initialize the
    /// set of concrete deserializer functions for all known logix element objects.
    /// </summary>
    private static IEnumerable<KeyValuePair<Type, Func<XElement, LogixElement>>> Introspect(Assembly assembly)
    {
        var types = assembly.GetTypes().Where(t =>
            typeof(LogixElement).IsAssignableFrom(t)
            && t is { IsAbstract: false, IsPublic: true }
            && t.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(XElement) },
                null) is not null);

        foreach (var type in types)
        {
            var deserializer = type.Deserializer<LogixElement>();
            yield return new KeyValuePair<Type, Func<XElement, LogixElement>>(type, deserializer);
        }
    }
}