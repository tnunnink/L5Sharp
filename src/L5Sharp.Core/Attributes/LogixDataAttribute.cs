using System;

namespace L5Sharp.Core;

/// <summary>
/// Represents an attribute used to decorate a class with metadata for Logix data type mapping.
/// </summary>
/// <remarks>
/// This attribute associates a class with a Logix data type by defining the type name and optionally
/// indicating if the type is atomic. The attribute is not inherited by derived classes.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class LogixDataAttribute : Attribute
{
    /// <summary>
    /// Instantiates new <see cref="LogixDataAttribute"/> instance with provided data type name.
    /// </summary>
    /// <param name="typeName">The name of the data type the class represents.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="typeName"/> is null.</exception>
    public LogixDataAttribute(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentException("TypeName is required for attribute");
        
        TypeName = typeName;
    }

    /// <summary>
    /// Instantiates new <see cref="LogixDataAttribute"/> instance with provided data type name.
    /// </summary>
    /// <param name="typeName">The name of the data type the class represents.</param>
    /// <param name="isAtomic"></param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="typeName"/> is null.</exception>
    internal LogixDataAttribute(string typeName, bool isAtomic)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentException("TypeName is required for attribute");
        
        TypeName = typeName;
        IsAtomic = isAtomic;
    }

    /// <summary>
    /// Gets the name of the Logix data type associated with the class.
    /// </summary>
    /// <remarks>
    /// This property holds the identifier for the data type used in Logix systems.
    /// It is specified when the <see cref="LogixDataAttribute"/> is applied to a class and
    /// provides the mapping between the class and the corresponding Logix data type.
    /// </remarks>
    public string TypeName { get; }

    /// <summary>
    /// Gets a value indicating whether the associated Logix data type is atomic.
    /// </summary>
    /// <remarks>
    /// This property determines if the Logix data type represented by the attribute
    /// is considered atomic, meaning it cannot be decomposed into smaller Logix types.
    /// Typically, atomic types are primitive data types within Logix systems.
    /// </remarks>
    public bool IsAtomic { get; }
}