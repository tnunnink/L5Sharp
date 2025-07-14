using System;

namespace L5Sharp.Core;

/// <summary>
/// A custom attribute that defines the L5X type name for a logic type class to match XML
/// elements to a given class. This attribute is mostly needed for class types whose names do not match the L5X element type
/// found in the L5X, or for classes that support multiple L5X element types.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class L5XTypeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="L5XTypeAttribute"/> instance with type name.
    /// </summary>
    /// <param name="typeName">The L5X type name for the class.</param>
    /// <param name="containerName">The optional name of the container element this type belongs to. If not provided,
    /// defaults to <paramref name="typeName"/> with 's' appended (as this is the container for most types).</param>
    public L5XTypeAttribute(string typeName, string? containerName = null)
    {
        TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        ContainerName = containerName ?? $"{typeName}s";
    }

    /// <summary>
    /// Gets the L5X type name associated with the attribute. This property represents the name of the L5X element
    /// in the XML that the class is mapped to. It is specified when the <see cref="L5XTypeAttribute"/> is applied to a class.
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// Gets the name of the container element associated with the L5X type.
    /// This property represents the XML container in which the corresponding L5X
    /// elements are grouped. If not explicitly defined, it defaults to the type
    /// name with an appended 's'.
    /// </summary>
    public string ContainerName { get; }
}