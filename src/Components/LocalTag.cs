using System;
using System.Xml.Linq;

namespace L5Sharp.Components;

/// <summary>
/// 
/// </summary>
public class LocalTag : Tag
{
    /// <summary>
    /// Creates a new <see cref="LocalTag"/> with default values.
    /// </summary>
    public LocalTag()
    {
    }

    /// <summary>
    /// Creates a new <see cref="LocalTag"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public LocalTag(XElement element) : base(element)
    {
    }
}