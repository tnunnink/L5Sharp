using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// A global registry and factory for <see cref="LogixData"/> classes and their deriviatives. This class provides the
/// ability to create new default instances of data structures by name. It also has helpers for determining the type
/// name for a given <see cref="LogixData"/> type and if a given type name is an atomic type.
/// </summary>
/// <remarks>
/// This class will contain all registered <see cref="LogixData"/> implementation decorated with
/// the <see cref="LogixDataAttribute"/>. You can use the <see cref="Register{TData}"/> method manually if you prefer to
/// register a custom factory delegate.
/// </remarks>
public static class LogixType
{
    /// <summary>
    /// A dictionary mapping .NET <see cref="Type"/> instances to their corresponding Logix type names.
    /// Used internally to resolve type names for LogixData creation and management.
    /// </summary>
    private static readonly Dictionary<Type, string> Names = [];

    /// <summary>
    /// A collection of factory methods for creating instances of <see cref="LogixData"/>
    /// based on a registered type name.
    /// </summary>
    private static readonly Dictionary<string, Func<LogixData>> Factories = [];

    /// <summary>
    /// A private static list of strings representing atomic data types in the Logix system.
    /// It is used internally to store and manage predefined atomic type names for validation and type resolution.
    /// </summary>
    private static readonly List<string> Atomics = [];

    /// <summary>
    /// Returns the singleton null <see cref="LogixData"/> object.
    /// </summary>
    public static LogixData Null => NullData.Instance;

    /// <summary>
    /// Registers a new <see cref="LogixData"/> type with the specified name and factory method.
    /// </summary>
    /// <param name="name">The unique name associated with the <see cref="LogixData"/> type being registered.</param>
    /// <param name="factory">A factory method used to create instances of the specified <see cref="LogixData"/> type.</param>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> being registered.</typeparam>
    /// <exception cref="ArgumentNullException">Thrown when the name or factory parameter is null.</exception>
    public static void Register<TData>(string name, Func<TData> factory) where TData : LogixData
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.");

        if (factory is null)
            throw new ArgumentNullException(nameof(factory));

        if (Names.ContainsKey(typeof(TData)))
            throw new InvalidOperationException($"Type already registered: '{typeof(TData)}'");

        Names[typeof(TData)] = name;
        Factories[name] = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <summary>
    /// Registers a new <see cref="AtomicData"/> type with the specified name and factory method.
    /// </summary>
    /// <param name="name">The unique name associated with the <see cref="AtomicData"/> type being registered.</param>
    /// <param name="factory">A factory method used to create instances of the specified <see cref="AtomicData"/> type.</param>
    /// <typeparam name="TAtomic">The type of <see cref="AtomicData"/> being registered.</typeparam>
    /// <exception cref="ArgumentNullException">Thrown when the name or factory parameter is null.</exception>
    internal static void RegisterAtomic<TAtomic>(string name, Func<TAtomic> factory) where TAtomic : AtomicData
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty.");

        Names[typeof(TAtomic)] = name;
        Factories[name] = factory ?? throw new ArgumentNullException(nameof(factory));
        Atomics.Add(name);
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
    /// Creates an instance of the specified <see cref="LogixData"/> type using a registered factory method,
    /// or returns a default instance if no factory is found for the given data type.
    /// </summary>
    /// <param name="dataType">
    /// The name of the <see cref="LogixData"/> type to create. If null, the type name of <typeparamref name="TData"/> is used.
    /// </param>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> to create.</typeparam>
    /// <returns>
    /// An instance of <typeparamref name="TData"/> created by a factory method if registered, otherwise a default
    /// instance of <see cref="StructureData"/> cast to <typeparamref name="TData"/>
    /// .</returns>
    public static TData CreateOrDefault<TData>(string? dataType = null) where TData : LogixData
    {
        dataType ??= typeof(TData).Name;

        if (Factories.TryGetValue(dataType, out var factory))
        {
            return factory().As<TData>();
        }

        return new StructureData(dataType).As<TData>();
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
}