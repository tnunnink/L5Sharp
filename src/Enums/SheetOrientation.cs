using L5Sharp.Components;

namespace L5Sharp.Enums;

/// <summary>
/// Represents an enumeration of all <see cref="SheetOrientation"/> options for a given <see cref="Routine"/>.
/// </summary>
public class SheetOrientation : LogixEnum<SheetOrientation, string>
{
    private SheetOrientation(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'Landscape' <see cref="SheetOrientation"/> value.
    /// </summary>
    public static readonly SheetOrientation Landscape = new(nameof(Landscape), nameof(Landscape));
    
    /// <summary>
    /// Represents a 'Portrait' <see cref="SheetOrientation"/> value.
    /// </summary>
    public static readonly SheetOrientation Portrait = new(nameof(Portrait), nameof(Portrait));
}