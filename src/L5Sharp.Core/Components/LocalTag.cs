using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="Tag"/> that is defined locally to an <see cref="AddOnInstruction"/> component.
/// </summary>
/// <remarks>
/// This class does not add any new functionality but simply overrides the default L5XType to LocalTag. Logix
/// uses a different element name for tags in an AOI so to match this our L5XTypeAttribute implementation, we are
/// deriving a new specific class.
/// </remarks>
[L5XType(L5XName.LocalTag)]
public sealed class LocalTag : Tag
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.DefaultData,
    ];
    
    /// <summary>
    /// Creates a new <see cref="LocalTag"/> with default values.
    /// </summary>
    public LocalTag() : base(L5XName.LocalTag)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="Tag"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public LocalTag(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="LocalTag"/> initialized with the provided name and value.
    /// </summary>
    /// <param name="name">The name of the LocalTag.</param>
    /// <param name="value">The <see cref="LogixData"/> value of the LocalTag.</param>
    /// <param name="description">the optional description of the LocalTag.</param>
    public LocalTag(string name, LogixData value, string? description = default) : base(L5XName.LocalTag)
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Value = value;
        SetDescription(description);
    }
}