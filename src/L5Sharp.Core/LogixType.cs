using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A global registry and factory for <see cref="LogixData"/> classes and their deriviatives. This class provides the
/// ability to create new default instances of data structures by name and deserialize contrete instances from a given XElement.
/// It also has helpers for determining the type name for a given <see cref="LogixData"/> type and if a given type name is an atomic type.
/// </summary>
public static class LogixType
{
    /// <summary>
    /// The map of all registered logix data types to their corresponding name. The name is the value of the type
    /// defined in the L5X.
    /// </summary>
    private static readonly Dictionary<Type, string> Names = [];

    /// <summary>
    /// Collection of registered atomic data type names. This lets us check if a given type is a special case atomic
    /// data instance.
    /// </summary>
    private static readonly List<string> Atomics = [];

    /// <summary>
    /// A collection of factory methods for creating default instances of logix data types by name.
    /// </summary>
    private static readonly Dictionary<string, Func<LogixData>> Factories = [];

    /// <summary>
    /// A collection of factory methods for deserializing a logix data type from a given XElement.
    /// </summary>
    private static readonly Dictionary<string, Func<XElement, LogixData>> Deserializers = [];

    /// <summary>
    /// Returns the singleton null <see cref="LogixData"/> object.
    /// </summary>
    public static LogixData Null => NullData.Instance;

    /// <summary>
    /// Registers a new Logix data type with the specified name.
    /// The data type must inherit from <see cref="LogixData"/> and have a parameterless constructor.
    /// </summary>
    /// <typeparam name="TData">The type of the Logix data to register, inheriting from <see cref="LogixData"/>.</typeparam>
    /// <param name="name">The name associated with the Logix data type to be registered.</param>
    /// <exception cref="ArgumentException">Thrown when the provided name is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the data type has already been registered.</exception>
    public static void Register<TData>(string name) where TData : LogixData, new()
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.");

        var type = typeof(TData);

        if (Names.ContainsKey(type)) return;

        Names[type] = name;
        Factories[name] = () => new TData();
        TryRegisterDeserializer(type, name);
    }

    /// <summary>
    /// Registers a new atomic data type with the specified name.
    /// The atomic type must derive from <see cref="AtomicData"/> and have a parameterless constructor.
    /// </summary>
    /// <typeparam name="TAtomic">The type of the atomic data to register, inheriting from <see cref="AtomicData"/>.</typeparam>
    /// <param name="name">The name associated with the atomic data type to be registered.</param>
    /// <exception cref="ArgumentException">Thrown when the provided name is null or empty.</exception>
    internal static void RegisterAtomic<TAtomic>(string name) where TAtomic : AtomicData, new()
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.");

        var type = typeof(TAtomic);

        Names[type] = name;
        Atomics.Add(name);
        Factories[name] = () => new TAtomic();
        TryRegisterDeserializer(type, name);
    }

    /// <summary>
    /// Creates an instance of <see cref="LogixData"/> based on the specified type name.
    /// Throws an exception if no registered factory exists for the provided type name.
    /// </summary>
    /// <param name="dataType">The name of the data type for which to create an instance.</param>
    /// <returns>An instance of <see cref="LogixData"/> corresponding to the provided type name.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified type name does not have a registered factory.</exception>
    public static LogixData Create(string dataType)
    {
        if (Factories.TryGetValue(dataType, out var factory))
        {
            return factory();
        }

        throw new InvalidOperationException($"No registered type found for '{dataType}'");
    }

    /// <summary>
    /// Creates a new instance of the specified Logix data type.
    /// </summary>
    /// <typeparam name="TData">The type of Logix data to create. Must inherit from <see cref="LogixData"/>.</typeparam>
    /// <param name="dataType">The name of the type to create.</param>
    /// <returns>A new instance of the specified Logix data type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the specified type name has not been registered in the system.
    /// </exception>
    public static TData Create<TData>(string? dataType = null) where TData : LogixData
    {
        dataType ??= typeof(TData).Name;

        if (Factories.TryGetValue(dataType, out var factory))
        {
            return factory().As<TData>();
        }

        throw new InvalidOperationException($"No registered type found for '{dataType}'");
    }

    /// <summary>
    /// Attempts to create an instance of <see cref="LogixData"/> based on the specified type name.
    /// </summary>
    /// <param name="dataType">The name of the type for which a <see cref="LogixData"/> instance should be created.</param>
    /// <param name="data">
    /// When this method returns, contains the created <see cref="LogixData"/> instance if the type name exists,
    /// or null if the creation fails.
    /// </param>
    /// <returns>
    /// <c>true</c> if an instance of <see cref="LogixData"/> could be created successfully; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryCreate(string dataType, out LogixData data)
    {
        data = null!;

        if (Factories.TryGetValue(dataType, out var factory))
        {
            data = factory();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to create an instance of the specified <see cref="LogixData"/> type using the provided data type name.
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> to create.</typeparam>
    /// <param name="data">
    /// When this method returns, contains the created instance of <typeparamref name="TData"/>, if successful;
    /// otherwise, contains the default value for the type.
    /// </param>
    /// <param name="dataType">
    /// The name of the data type to create, or null to use the type name of <typeparamref name="TData"/>.
    /// </param>
    /// <returns><c>true</c> if the instance was successfully created; otherwise, <c>false</c>.</returns>
    public static bool TryCreate<TData>(out TData data, string? dataType = null) where TData : LogixData
    {
        dataType ??= typeof(TData).Name;
        data = null!;

        if (Factories.TryGetValue(dataType, out var factory))
        {
            data = factory().As<TData>();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="LogixData"/> type based on the specified data type name,
    /// or returns a default <see cref="StructureData"/> if the type is not registered.
    /// </summary>
    /// <param name="dataType">The name of the data type to create an instance for.</param>
    /// <returns>
    /// A new instance of the <see cref="LogixData"/> type if the data type is found,
    /// otherwise a default instance of <see cref="StructureData"/> having the specified type name.
    /// </returns>
    public static LogixData CreateOrDefault(string dataType)
    {
        if (Factories.TryGetValue(dataType, out var factory))
        {
            return factory();
        }

        return new StructureData(dataType);
    }

    /// <summary>
    /// Deserializes an <see cref="XElement"/> into a <see cref="LogixData"/> object based on its data type.
    /// The data type must be registered with an associated deserializer function.
    /// </summary>
    /// <param name="element">
    /// The XML element to deserialize, containing the data type information. It's expected that this element is
    /// some tag data structure, array, or data value element.</param>
    /// <returns>A <see cref="LogixData"/> instance representing the deserialized data.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided element is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the data type is not registered or a matching deserializer function is not found.
    /// </exception>
    public static LogixData Deserialize(XElement element)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);

        if (Deserializers.TryGetValue(dataType, out var deserializer))
        {
            return deserializer(element);
        }

        throw new InvalidOperationException($"No registered type found for '{dataType}'");
    }

    /// <summary>
    /// Attempts to deserialize an <see cref="XElement"/> into a <see cref="LogixData"/> instance.
    /// </summary>
    /// <param name="element">The XML element containing the data to be deserialized.</param>
    /// <param name="data">
    /// When this method returns, contains the deserialized <see cref="LogixData"/> instance if the operation succeeds,
    /// or <c>null</c> if the operation fails.
    /// </param>
    /// <returns>
    /// <c>true</c> if the deserialization is successful and a matching deserializer is found; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a required attribute, such as the data type, is missing or invalid in the provided element.
    /// </exception>
    public static bool TryDeserialize(XElement element, out LogixData data)
    {
        var dataType = element.DataType() ?? throw element.L5XError(L5XName.DataType);

        if (Deserializers.TryGetValue(dataType, out var deserializer))
        {
            data = deserializer(element);
            return true;
        }

        data = null!;
        return false;
    }

    /// <summary>
    /// Retrieves the Logix type name associated with a specified .NET <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The .NET type for which to retrieve the corresponding Logix type name.</param>
    /// <returns>The Logix type name associated with the specified .NET type.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified type does not have a registered Logix type name.</exception>
    public static string NameFor(Type type)
    {
        if (Names.TryGetValue(type, out var name))
        {
            return name;
        }

        throw new InvalidOperationException($"No registered data type found for name '{type}'");
    }

    /// <summary>
    /// Determines whether the specified type name represents an atomic data type in the Logix system.
    /// </summary>
    /// <param name="typeName">The name of the type to check for atomicity.</param>
    /// <returns>
    /// <c>true</c> if the specified type name is an atomic type; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAtomic(string typeName)
    {
        return Atomics.Contains(typeName.ToUpper());
    }

    /// <summary>
    /// Determines whether a specified .NET <see cref="Type"/> is registered within the Logix system.
    /// </summary>
    /// <param name="type">The .NET <see cref="Type"/> to check for registration in the Logix system.</param>
    /// <returns>True if the specified type is registered; otherwise, false.</returns>
    public static bool IsRegistered(Type type)
    {
        return Names.ContainsKey(type);
    }

    /// <summary>
    /// Determines whether the specified Logix type name is registered in the system.
    /// </summary>
    /// <param name="typeName">The Logix type name to check for registration.</param>
    /// <returns><c>true</c> if the specified type name is registered; otherwise, <c>false</c>.</returns>
    public static bool IsRegistered(string typeName)
    {
        return Factories.ContainsKey(typeName);
    }

    /// <summary>
    /// Attempts to register a deserializer for the specified type and name.
    /// A deserializer is only registered if the type has a public constructor
    /// that accepts a single parameter of type <see cref="XElement"/> and is not abstract.
    /// </summary>
    /// <param name="type">The type for which the deserializer is being registered.</param>
    /// <param name="name">The name associated with the deserializer to be registered.</param>
    private static void TryRegisterDeserializer(Type type, string name)
    {
        var constructor = type.GetConstructor([typeof(XElement)]);

        if (constructor is null || !constructor.IsPublic || type.IsAbstract)
            return;

        var parameter = Expression.Parameter(typeof(XElement), "e");
        var expression = Expression.New(constructor, parameter);
        var deserializer = Expression.Lambda<Func<XElement, LogixData>>(expression, parameter).Compile();
        Deserializers[name] = deserializer;
    }
}