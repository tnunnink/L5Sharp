﻿using System;

namespace L5Sharp.Core;

/// <summary>
/// A custom attribute that defines the L5X type name for a logic type class in order to match XML
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
    /// <exception cref="ArgumentNullException"><c>typeName</c> is null.</exception>
    public L5XTypeAttribute(string typeName)
    {
        TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
    }

    /// <summary>
    /// The L5X type name. This name corresponds to the name of the L5X/XML element, and is
    /// used to retrieve elements from an L5X file for the type.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the element for the type. (e.g. DataType, Tag, etc.)</value>
    public string TypeName { get; }
}