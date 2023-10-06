using L5Sharp.Components;
using L5Sharp.Elements;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all <see cref="PenType"/> options for a given <see cref="Pen"/>.
/// </summary>
public class PenType : LogixEnum<PenType, string>
{
    private PenType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'Analog' <see cref="PenType"/> value.
    /// </summary>
    public static readonly PenType Analog = new(nameof(Analog), nameof(Analog));
    
    /// <summary>
    /// Represents a 'Digital' <see cref="PenType"/> value.
    /// </summary>
    public static readonly PenType Digital = new(nameof(Digital), nameof(Digital));
    
    /// <summary>
    /// Represents a 'Full-Width' <see cref="PenType"/> value.
    /// </summary>
    public static readonly PenType FullWidth = new(nameof(FullWidth), "Full-Width");
}