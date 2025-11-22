using System;
using System.Collections.Generic;

namespace L5Sharp.Core;

/// <summary>
/// Provides functionality to create, retrieve, and use Logix data types within the system.
/// This class offers mechanisms to interact with Logix-specific data structures and type information.
/// </summary>
public static partial class LogixType
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
    /// Calls static registration for logix types. Registration methods are implemented using generators that
    /// scan for types implementing <see cref="LogixDataAttribute"/>.
    /// </summary>
    static LogixType()
    {
        RegisterTypes();
    }

    /// <summary>
    /// Executes registration of all <see cref="LogixData"/> elements using source generation. This method will call
    /// <see cref="Register"/> to initialize local collections with type name mappings and factory methods for creating
    /// default instances of strongly typed data elements.
    /// </summary>
    static partial void RegisterTypes();

    /// <summary>
    /// Returns the singleton null <see cref="LogixData"/> object.
    /// </summary>
    public static LogixData Null => NullData.Instance;

    /// <summary>
    /// Creates an instance of <see cref="LogixData"/> based on the specified type name.
    /// Throws an exception if no registered factory exists for the provided type name.
    /// </summary>
    /// <param name="typeName">The name of the data type for which to create an instance.</param>
    /// <returns>An instance of <see cref="LogixData"/> corresponding to the provided type name.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified type name does not have a registered factory.</exception>
    public static LogixData Create(string typeName)
    {
        if (Factories.TryGetValue(typeName, out var factory))
        {
            return factory();
        }

        throw new InvalidOperationException($"No registered type found for '{typeName}'");
    }

    /// <summary>
    /// Creates a new instance of the specified Logix data type.
    /// </summary>
    /// <typeparam name="TData">The type of Logix data to create. Must inherit from <see cref="LogixData"/>.</typeparam>
    /// <param name="typeName">The name of the type to create.</param>
    /// <returns>A new instance of the specified Logix data type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the specified type name has not been registered in the system.
    /// </exception>
    public static TData Create<TData>(string? typeName = null) where TData : LogixData
    {
        typeName ??= typeof(TData).Name;

        if (Factories.TryGetValue(typeName, out var factory))
        {
            return factory().As<TData>();
        }

        throw new InvalidOperationException($"No registered type found for '{typeName}'");
    }

    /// <summary>
    /// Attempts to create an instance of <see cref="LogixData"/> based on the specified type name.
    /// </summary>
    /// <param name="typeName">The name of the type for which a <see cref="LogixData"/> instance should be created.</param>
    /// <param name="data">
    /// When this method returns, contains the created <see cref="LogixData"/> instance if the type name exists,
    /// or null if the creation fails.
    /// </param>
    /// <returns>
    /// <c>true</c> if an instance of <see cref="LogixData"/> could be created successfully; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryCreate(string typeName, out LogixData data)
    {
        data = null!;

        if (Factories.TryGetValue(typeName, out var factory))
        {
            data = factory();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Tries to create an instance of the specified type name as a strongly-typed <see cref="LogixData"/> object.
    /// The type name must match a registered LogixData type.
    /// </summary>
    /// <typeparam name="TData">The desired type of the <see cref="LogixData"/> instance.</typeparam>
    /// <param name="typeName">The name of the type to be created.</param>
    /// <param name="data">The output parameter that will hold the created instance if the creation is successful; otherwise, it will be null.</param>
    /// <returns>True if the creation was successful; otherwise, false.</returns>
    public static bool TryCreate<TData>(string typeName, out TData data) where TData : LogixData
    {
        data = null!;

        if (Factories.TryGetValue(typeName, out var factory))
        {
            data = factory().As<TData>();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Attempts to create an instance of the specified <typeparamref name="TData"/> type using a registered factory.
    /// </summary>
    /// <param name="data">The output parameter that will contain the created <typeparamref name="TData"/> instance if successful, or default if not.</param>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> to be created.</typeparam>
    /// <returns>True if the specified type was successfully created; otherwise, false.</returns>
    public static bool TryCreate<TData>(out TData data) where TData : LogixData
    {
        var typeName = typeof(TData).Name;
        data = null!;

        if (Factories.TryGetValue(typeName, out var factory))
        {
            data = factory().As<TData>();
            return true;
        }

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
    /// Registers a specified type with its associated name, atomicity, and factory method.
    /// This method updates internal collections to map types to their string representations,
    /// determine atomic data types, and associate a factory method for type instantiation.
    /// </summary>
    /// <param name="type">The type to be registered.</param>
    /// <param name="name">The name associated with the type.</param>
    /// <param name="isAtomic">A boolean indicating whether the type is considered atomic.</param>
    /// <param name="factory">A factory method for creating instances of the specified type.</param>
    private static void Register(Type type, string name, bool isAtomic, Func<LogixData> factory)
    {
        Names[type] = name;
        Factories[name] = factory;
        if (isAtomic) Atomics.Add(name);
    }
}