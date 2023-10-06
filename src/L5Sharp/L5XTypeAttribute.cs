using System;

namespace L5Sharp;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class L5XTypeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="L5XTypeAttribute"/> instance with provided configuration parameters.
    /// </summary>
    /// <param name="typeName">The primary L5X type name for the class.</param>
    /// <param name="isPrimaryType"></param>
    /// <exception cref="ArgumentNullException"><c>typeName</c></exception>
    public L5XTypeAttribute(string typeName, bool isPrimaryType = true)
    {
        TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        ContainerName = $"{TypeName}s";
        IsPrimaryType = isPrimaryType;
    }

    /// <summary>
    /// Creates a new <see cref="L5XTypeAttribute"/> instance with provided configuration parameters.
    /// </summary>
    /// <param name="typeName">The primary L5X type name for the class.</param>
    /// <param name="containerName"></param>
    /// <param name="isPrimaryType"></param>
    /// <exception cref="ArgumentNullException"><c>typeName</c></exception>
    public L5XTypeAttribute(string typeName, string containerName, bool isPrimaryType = true)
    {
        TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        ContainerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
        IsPrimaryType = isPrimaryType;
    }
    
    /// <summary>
    /// The primary name of the L5X type. This name corresponds to the element name of the L5X/XML element, and is
    /// used to retrieve the type's elements from an L5X file.
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// 
    /// </summary>
    public string ContainerName { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsPrimaryType { get; }
}