using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A static serialization class that handles serializing and deserializing <see cref="LogixElement{TElement}"/>
/// and <see cref="LogixType"/> objects and their derivatives.
/// </summary>
public static class LogixSerializer
{
    private static readonly Lazy<Dictionary<Type, ConstructorInfo>> Constructors = new(() =>
    {
        var dictionary = new Dictionary<Type, ConstructorInfo>();
        if (Mode == ScanMode.None) return dictionary;

        var assemblies = Mode == ScanMode.Internal
            ? new List<Assembly> { typeof(LogixSerializer).Assembly }
            : AppDomain.CurrentDomain.GetAssemblies().ToList();

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes().Where(t =>
                (IsDerivativeOf(t, typeof(LogixElement<>)) || IsDerivativeOf(t, typeof(LogixType)))
                && t is { IsAbstract: false, IsPublic: true }
                && t.GetConstructor(new[] { typeof(XElement) }) is not null);

            foreach (var type in types)
            {
                var constructor = type.GetConstructor(new[] { typeof(XElement) });
                dictionary.TryAdd(type, constructor);
            }
        }

        return dictionary;
    });

    /// <summary>
    /// The <see cref="ScanMode"/> specifying how <see cref="LogixSerializer"/> should perform reflection scanning
    /// for types to pre-register for deserialization.
    /// </summary>
    /// <remarks>
    /// The serializer class will perform reflection based scanning for types matching the all the following criteria:
    /// 
    /// non-abstract classes that derive from <see cref="LogixElement{TElement}"/> or <see cref="LogixType"/> and have public
    /// constructors accepting a <see cref="XElement"/> object. Types match this criteria will be cached and constructed
    /// at runtime. 
    /// </remarks>
    public static ScanMode Mode { get; set; } = ScanMode.All;

    /// <summary>
    /// Serializes the provided object to an <see cref="XElement"/>.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <typeparam name="TSerializable">The type of the object being serialized. Type must implement <see cref="ILogixSerializable"/>.</typeparam>
    /// <returns>An <see cref="XElement"/> representing the serialized object.</returns>
    public static XElement Serialize<TSerializable>(TSerializable obj)
        where TSerializable : ILogixSerializable => obj.Serialize();

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to deserialize.</param>
    /// <typeparam name="T">The return type of the deserialized element.</typeparam>
    /// <returns>A new object of the specified type representing the deserialized element.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static T Deserialize<T>(XElement element) =>
        (T)Constructor(typeof(T)).Invoke(new object[] { element });

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static object Deserialize(Type type, XElement element) =>
        Constructor(type.Name).Invoke(new object[] { element });

    /// <summary>
    /// Deserializes a <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="name">The name of the type to deserialize</param>
    /// <param name="element">The <see cref="XElement"/> to deserialize.</param>
    /// <returns>An object of the specified type name representing the deserialized element.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static object Deserialize(string name, XElement element) =>
        Constructor(name).Invoke(new object[] { element });

    /// <summary>
    /// Register a custom type so that it may be instantiated during deserialization of a L5X. The type must implement
    /// a constructor accepting a single <see cref="XElement"/> argument. The type can either parse the element or
    /// derive from <see cref="LogixElement{TElement}"/> and pas the element to the base constructor. 
    /// </summary>
    /// <typeparam name="T">The type of the logix type.</typeparam>
    public static void Register<T>() where T : class
    {
        var type = typeof(T);
        var constructor = type.GetConstructor(new[] { typeof(XElement) });

        if (constructor is null)
            throw new InvalidOperationException(
                @$"No element constructor defined for type {type}.
                     Class must specify constructor accepting a single {typeof(XElement)} to be registered.");

        if (!Constructors.Value.TryAdd(type, constructor))
            throw new InvalidOperationException($"The type {type} is already registered.");
    }

    /// <summary>
    /// Determines if a type with the specified type name is registered for deserialization.
    /// </summary>
    /// <param name="name">The type name to check.</param>
    /// <returns><c>true</c> if the type is registered; otherwise, <c>false</c>.</returns>
    public static bool IsRegistered(string name) => Constructors.Value.Keys.Any(t => t.Name == name);

    /// <summary>
    /// Determines if a type with the specified generic type parameter is registered for deserialization.
    /// </summary>
    /// <typeparam name="T">The type to check.</typeparam>
    /// <returns><c>true</c> if the type is registered; otherwise, <c>false</c>.</returns>
    public static bool IsRegistered<T>() where T : class => Constructors.Value.ContainsKey(typeof(T));

    /// <summary>
    /// 
    /// </summary>
    private static ConstructorInfo Constructor(Type type)
    {
        if (Constructors.Value.TryGetValue(type, out var constructor))
            return constructor;

        var arguments = new[] { typeof(XElement) };
        var ctor = type.GetConstructor(arguments);

        if (ctor is null)
            throw new InvalidOperationException(
                @$"No element constructor defined for type {type}.
                     Class must specify constructor accepting a single {typeof(XElement)} to be deserialized.");

        Constructors.Value.TryAdd(type, ctor);
        return ctor;
    }

    /// <summary>
    /// Gets a constructor info object provided the name of the type.
    /// </summary>
    private static ConstructorInfo Constructor(string typeName)
    {
        var type = Constructors.Value.Keys.SingleOrDefault(t => t.Name == typeName);

        if (type is null)
            throw new InvalidOperationException(
                $"Type '{typeName}' is not registered. Either register type or have LogixSerializer perform assembly scanning. " +
                $"Only public, non-abstract classes having a constructor accepting a single {typeof(XElement)} argument are supported.");

        return Constructors.Value[type];
    }

    /// <summary>
    /// Determines if a type is derived from the base type, even if the base type is a generic type.
    /// </summary>
    private static bool IsDerivativeOf(Type type, Type baseType)
    {
        if (type == baseType) return false;

        var current = type.BaseType;

        while (current != typeof(object) && current != null)
        {
            var definition = current.IsGenericType ? current.GetGenericTypeDefinition() : current;
            if (definition == baseType) return true;
            current = current.BaseType;
        }

        return false;
    }
}

/// <summary>
/// An enum option specifying how <see cref="LogixSerializer"/> should scan for types.
/// </summary>
public enum ScanMode
{
    /// <summary>
    /// Indicates that <see cref="LogixSerializer"/> should scan internal and external assemblies for types matching the
    /// deserialization criteria.
    /// </summary>
    All,

    /// <summary>
    /// Indicates that <see cref="LogixSerializer"/> should scan only internal assemblies for types matching the
    /// deserialization criteria.
    /// </summary>
    Internal,

    /// <summary>
    /// Indicates that <see cref="LogixSerializer"/> should not perform reflection scanning of types to automatically
    /// register for deserialization.
    /// </summary>
    None
}