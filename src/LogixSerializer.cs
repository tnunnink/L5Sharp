using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A static serialization class that provides the ability to retrieve <see cref="ILogixSerializer{T}"/> instances
/// for specified type, as well as perform serialization and deserialization of the type.
/// </summary>
public static class LogixSerializer
{
    /// <summary>
    /// Cached collection of entity constructors to that we can avoid reflection to get constructor each time we want
    /// to deserialize a Logix entity.
    /// </summary>
    private static Dictionary<Type, ConstructorInfo> Constructors => new();

    /// <summary>
    /// Serializes the provided object using the preconfigured <see cref="ILogixSerializer{T}"/> for the type.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <typeparam name="TSerializable">The type of the object being serialized. Type must have configured
    /// <see cref="LogixSerializerAttribute"/> so that the serializer can retrieve the correct implementation
    /// to use for serialization.</typeparam>
    /// <returns>An <see cref="XElement"/> representing the serialized object.</returns>
    public static XElement Serialize<TSerializable>(TSerializable obj) where TSerializable : ILogixSerializable =>
        obj.Serialize();

    /// <summary>
    /// Deserialized the provided element into the specified object type using the preconfigured
    /// <see cref="ILogixSerializer{T}"/> for the type.
    /// </summary>
    /// <param name="element">The XML element to deserialize.</param>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    public static T Deserialize<T>(XElement element) =>
        (T)GetConstructorInfo(typeof(T)).Invoke(new object[] { element });

    /// <summary>
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    public static object Deserialize(Type type, XElement element) =>
        GetConstructorInfo(type).Invoke(new object[] { element });

    private static ConstructorInfo GetConstructorInfo(Type type)
    {
        //Check cache first
        if (Constructors.TryGetValue(type, out var ctor))
            return ctor;

        //Get via reflection
        var arguments = new[] { typeof(XElement) };
        var constructor = type.GetConstructor(arguments);

        if (constructor is null)
            throw new InvalidOperationException(
                @$"No element constructor defined for type {type}.
                     Class must specify constructor accepting a single {typeof(XElement)} to be deserialized.");

        //Cache for reuse yeah?
        Constructors.TryAdd(type, constructor);

        return constructor;
    }
}