using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A static serialization class that handles serializing and deserializing Logix entity objects.
/// </summary>
public static class LogixSerializer
{
    /// <summary>
    /// Controls whether the static factory class will scan all assemblies for objects 
    /// </summary>
    public static bool AutoScan { get; set; } = true;
    
    /// <summary>
    /// 
    /// </summary>
    public static bool ScanExternal { get; set; } = true;

    /*/// <summary>
    /// Cached collection of entity constructors to that we can avoid reflection to get constructor each time we want
    /// to deserialize a Logix entity.
    /// </summary>
    private static readonly Dictionary<Type, ConstructorInfo> Constructors =
        AutoScan ? Initialize() : new Dictionary<Type, ConstructorInfo>();*/
    
    private static readonly Lazy<Dictionary<Type, ConstructorInfo>> Constructors = new(() =>
    {
        var dictionary = new Dictionary<Type, ConstructorInfo>();
        if (!AutoScan) return dictionary;
        
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var types = assembly.GetTypes().Where(t =>
                IsDerivativeOf(t, typeof(LogixEntity<>))
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
    /// Serializes the provided object to an <see cref="XElement"/>.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <typeparam name="TSerializable">The type of the object being serialized. Type must implement <see cref="ILogixSerializable"/>.</typeparam>
    /// <returns>An <see cref="XElement"/> representing the serialized object.</returns>
    public static XElement Serialize<TSerializable>(TSerializable obj)
        where TSerializable : ILogixSerializable => obj.Serialize();

    /// <summary>
    /// Deserializes the provided <see cref="XElement"/> into the specified object type.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to deserialize.</param>
    /// <typeparam name="T">The return type of the deserialized element.</typeparam>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    /// <remarks>
    /// The return object must specify a constructor accepting a single <see cref="XElement"/> for deserialization to work.
    /// </remarks>
    public static T Deserialize<T>(XElement element) =>
        (T)Constructor(typeof(T)).Invoke(new object[] { element });

    /// <summary>
    /// </summary>
    /// <param name="type">The type to deserialize.</param>
    /// <param name="element">The XML element to deserialize.</param>
    /// <returns>A new object of the specified type representing the deserialized object.</returns>
    public static object Deserialize(Type type, XElement element) =>
        Constructor(type.Name).Invoke(new object[] { element });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static object Deserialize(string name, XElement element) =>
        Constructor(name).Invoke(new object[] { element });

    /// <summary>
    /// Register a custom type with the static <see cref="LogixType"/> factory to be used for created of the type during
    /// deserialization of a L5X. 
    /// </summary>
    /// <typeparam name="T">The type of the logix type.</typeparam>
    /// <returns><c>true</c> if the type was registered successfully; otherwise, false.</returns>
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

    private static ConstructorInfo Constructor(Type type)
    {
        if (Constructors.Value.TryGetValue(type, out var constructor))
            return constructor;

        var arguments = new[] { typeof(XElement) };
        var constructorInfo = type.GetConstructor(arguments);

        if (constructorInfo is null)
            throw new InvalidOperationException(
                @$"No element constructor defined for type {type}.
                     Class must specify constructor accepting a single {typeof(XElement)} to be deserialized.");

        Constructors.Value.TryAdd(type, constructorInfo);

        return constructorInfo;
    }

    private static ConstructorInfo Constructor(string typeName)
    {
        var type = Constructors.Value.Keys.SingleOrDefault(t => t.Name == typeName);

        if (type is null)
            throw new InvalidOperationException(
                $"Type '{typeName}' is not registered. LogixSerializer only supports public, non-abstract classes inheriting from {typeof(LogixEntity<>)} which have a public constructor accepting a single {typeof(XElement)} argument.");

        return Constructors.Value[type];
    }

    /// <summary>
    /// Determines if a type is derived from the base type, even if the base type is a generic type.
    /// </summary>
    /// <param name="type">The type to test.</param>
    /// <param name="baseType">The base type</param>
    /// <returns></returns>
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