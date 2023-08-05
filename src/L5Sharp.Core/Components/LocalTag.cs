using System;
using System.Xml.Linq;

namespace L5Sharp.Components;

/// <summary>
/// A <see cref="Tag"/> derivative that is a component of a <see cref="AddOnInstruction"/> component.
/// </summary>
/// <remarks>
/// This class doesn't add any new functionality. It is only necessary to solve the issue of tags in an AOI having a
/// different "Type Name" (element name).
/// Since <see cref="LogixContainer{TElement}"/> queries and adds items using the component specific type name, we
/// need a way to override "Tag" to "LocalTag", and we can easily achieve this using a derived type. This would also
/// allow this type to be queried specifically across the L5X.
/// </remarks>
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