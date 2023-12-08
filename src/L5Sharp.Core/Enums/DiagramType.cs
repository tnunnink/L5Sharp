using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An enumeration object containing the possible types of a <c>Diagram</c> element.
/// </summary>
public  sealed class DiagramType : LogixEnum<DiagramType, string>
{
    private DiagramType(string name, string value) : base(name, value)
    {
    }
    
    /// <summary>
    /// Represents a Function Block Diagram <see cref="DiagramType"/>.
    /// </summary>
    public static readonly DiagramType Block = new(nameof(Block), nameof(Block));
    
    /// <summary>
    /// Represents a Sequential Function Chart <see cref="DiagramType"/>.
    /// </summary>
    public static readonly DiagramType Sequence = new(nameof(Sequence), nameof(Sequence));

    /// <summary>
    /// Determines the <see cref="DiagramType"/> from the provided element using the name of the element.
    /// </summary>
    /// <param name="element">The element to examine.</param>
    /// <returns>If the element name is <c>Sheet</c> then <see cref="Block"/>. If the element name is <c>SFCContent</c>
    /// then <see cref="Sequence"/>. If neither or null then throws an exception.</returns>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="NotSupportedException"><c>element</c> does not have one of the supported names.</exception>
    public static DiagramType FromElement(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        if (element.Name == L5XName.Sheet)
        {
            return Block;
        }
        
        if (element.Name == L5XName.SFCContent)
        {
            return Sequence;
        }

        throw new NotSupportedException($"The element '{element.Name}' is not a supported as a DiagramType.");
    }
}