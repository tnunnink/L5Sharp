using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A static deserialization class for <see cref="LogixElement{TElement}"/> objects and their derivatives.
/// Deserialization requires..
/// </summary>
public static class LogixSerializer
{
    private static readonly Lazy<Dictionary<Type, ConstructorInfo>> Constructors = new(() =>
    {
        var dictionary = new Dictionary<Type, ConstructorInfo>();

        var types = typeof(LogixSerializer).Assembly.GetTypes().Where(t =>
            t.IsDerivativeOf(typeof(LogixElement<>))
            && t is { IsAbstract: false, IsPublic: true }
            && t.GetConstructor(new[] { typeof(XElement) }) is not null);

        foreach (var type in types)
        {
            var constructor = type.GetConstructor(new[] { typeof(XElement) });
            dictionary.TryAdd(type, constructor);
        }

        return dictionary;
    });

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to deserialize.</param>
    /// <typeparam name="TElement">The return type of the deserialized element.</typeparam>
    /// <returns>A new object of the specified type representing the deserialized element.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static TElement Deserialize<TElement>(XElement element) where TElement : LogixElement<TElement> =>
        (TElement)Constructor(typeof(TElement)).Invoke(new object[] { element });

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static object Deserialize(Type type, XElement element) => Constructor(type).Invoke(new object[] { element });

    /// <summary>
    /// Handles getting the constructor for the specified type. If the type is not cached, this method will check
    /// if the type inherits <see cref="LogixElement{TElement}"/> and has a valid constructor. If so, it will add to the
    /// global constructor cache and return the <see cref="ConstructorInfo"/> object.
    /// </summary>
    private static ConstructorInfo Constructor(Type type)
    {
        if (Constructors.Value.TryGetValue(type, out var constructor))
            return constructor;

        if (!type.IsDerivativeOf(typeof(LogixElement<>)))
            throw new ArgumentException(
                $"LogixSerializer is only compatible for types inheriting from {typeof(LogixElement<>)}");

        var info = type.GetConstructor(new[] { typeof(XElement) });

        if (info is null)
            throw new InvalidOperationException(
                @$"No element constructor defined for type {type}.
                     Class must specify constructor accepting a single {typeof(XElement)} to be deserialized.");

        Constructors.Value.TryAdd(type, info);
        return info;
    }
}