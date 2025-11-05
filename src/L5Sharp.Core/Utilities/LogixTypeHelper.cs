using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace L5Sharp.Core;

/// <summary>
/// Provides helper methods for working with Logix-specific type attributes and metadata.
/// </summary>
internal static class LogixTypeHelper
{
    private static readonly Lazy<Dictionary<Type, L5XTypeAttribute[]>> Lookup = new(
        InitializeLookup,
        LazyThreadSafetyMode.ExecutionAndPublication
    );

    /// <summary>
    /// Gets the configured L5XType name for the specified type.
    /// </summary>
    /// <param name="type">The type to get the L5XType name for.</param>
    /// <returns>A <see cref="string"/> representing the type name configured by the <see cref="L5XTypeAttribute"/>, or the default name of the type if no attribute is found.</returns>
    /// <remarks>
    /// This method retrieves the first configured <see cref="L5XTypeAttribute"/> for the provided type.
    /// If no attribute is found, it defaults to returning type name property.
    /// </remarks>
    internal static string GetLogixTypeName(this Type type)
    {
        if (!Lookup.Value.TryGetValue(type, out var attributes))
            return type.Name;

        return attributes[0].TypeName;
    }

    /// <summary>
    /// Gets all configured L5XType names for the specified type.
    /// </summary>
    /// <param name="type">The type to get the L5XType names for.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="string"/> representing all type names configured by the <see cref="L5XTypeAttribute"/>, or a collection containing the default name of the type if no attributes are found.</returns>
    /// <remarks>
    /// This method retrieves all configured <see cref="L5XTypeAttribute"/> instances for the provided type.
    /// If no attribute is found, it defaults to returning type name property.
    /// </remarks>
    internal static IEnumerable<string> GetLogixTypeNames(this Type type)
    {
        if (!Lookup.Value.TryGetValue(type, out var attributes))
            return [type.Name];

        return attributes.Select(a => a.TypeName);
    }

    /// <summary>
    /// Gets the configured L5X container name for the specified type.
    /// </summary>
    /// <param name="type">The type to get the L5X container name for.</param>
    /// <returns>A <see cref="string"/> representing the name of the container configured by the <see cref="L5XTypeAttribute"/>, or a pluralized version of the default type name if no attribute is found.</returns>
    /// <remarks>
    /// This method retrieves the first configured <see cref="L5XTypeAttribute"/> for the provided type.
    /// If no attribute is found, it defaults to returning the pluralized version of the type's name.
    /// </remarks>
    internal static string GetLogixContainerName(this Type type)
    {
        if (!Lookup.Value.TryGetValue(type, out var attributes))
            return $"{type.Name}s";

        return attributes[0].ContainerName;
    }

    /// <summary>
    /// Constructs and initializes a lookup dictionary associating types with their corresponding
    /// <see cref="L5XTypeAttribute"/> configurations by scanning assemblies in the current application domain.
    /// </summary>
    /// <returns>A dictionary with types as keys and arrays of <see cref="L5XTypeAttribute"/> as values.</returns>
    /// <remarks>
    /// This method first scans the assembly containing the <see cref="LogixTypeHelper"/> class to identify
    /// configured attributes. It then scans other loaded assemblies in the application domain for user-defined
    /// types with <see cref="L5XTypeAttribute"/>, ensuring no duplicate type registrations occur.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a duplicate type is detected during attribute registration.
    /// </exception>
    private static Dictionary<Type, L5XTypeAttribute[]> InitializeLookup()
    {
        //Scan and add types from this assembly first.
        var sharp = typeof(LogixTypeHelper).Assembly;
        var lookup = GetAttributesFromAssembly(sharp);

        //Scan other loaded assemblies for use-defined types.
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a != sharp);

        foreach (var assembly in assemblies)
        {
            var custom = GetAttributesFromAssembly(assembly);
            foreach (var attribute in custom)
            {
                if (!lookup.TryAdd(attribute.Key, attribute.Value))
                    throw new InvalidOperationException($"Type {attribute.Key} already registered with library");
            }
        }

        return lookup;
    }

    /// <summary>
    /// Retrieves a dictionary mapping types in the specified assembly to their associated
    /// <see cref="L5XTypeAttribute"/> instances. Only types that meet the criteria defined in
    /// <see cref="HasConfiguredAttribute"/> are included.
    /// </summary>
    /// <param name="assembly">The assembly from which to retrieve types and their associated attributes.</param>
    /// <returns>A dictionary where the keys are types and the values are arrays of <see cref="L5XTypeAttribute"/>
    /// instances associated with those types.</returns>
    private static Dictionary<Type, L5XTypeAttribute[]> GetAttributesFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(HasConfiguredAttribute)
            .Select(t =>
            {
                var attributes = t.GetCustomAttributes<L5XTypeAttribute>().ToArray();
                return new KeyValuePair<Type, L5XTypeAttribute[]>(t, attributes);
            })
            .ToArray();

        return types.ToDictionary(t => t.Key, t => t.Value);
    }

    /// <summary>
    /// Determines whether a given type has been configured with the necessary attributes to be considered
    /// as a valid Logix type. The type must inherit from <see cref="LogixElement"/>, be public and non-abstract,
    /// and have at least one <see cref="L5XTypeAttribute"/> applied.
    /// </summary>
    /// <param name="type">The type to evaluate for configuration attributes.</param>
    /// <returns><c>true</c> if the type meets the configuration criteria; otherwise, <c>false</c>.</returns>
    private static bool HasConfiguredAttribute(Type type)
    {
        return typeof(LogixElement).IsAssignableFrom(type)
               && type is { IsAbstract: false, IsPublic: true }
               && type.GetCustomAttributes<L5XTypeAttribute>().Any();
    }
}