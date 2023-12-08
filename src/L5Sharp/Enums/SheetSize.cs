namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of all <see cref="SheetSize"/> options for a given <see cref="Routine"/>.
/// </summary>
public class SheetSize : LogixEnum<SheetSize, string>
{
    private SheetSize(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a 'Letter - 8.5 x 11 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize Letter = new(nameof(Letter), "Letter - 8.5 x 11 in");
    
    /// <summary>
    /// Represents a 'Legal - 8.5 x 14 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize Legal = new(nameof(Legal), "Legal - 8.5 x 14 in");
    
    /// <summary>
    /// Represents a 'Tabloid - 11 x 17 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize Tabloid = new(nameof(Tabloid), "Tabloid - 11 x 17 in");
    
    /// <summary>
    /// Represents a 'A - 8.5 x 11 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize A = new(nameof(A), "A - 8.5 x 11 in");
    
    /// <summary>
    /// Represents a 'B - 11 x 17 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize B = new(nameof(B), "B - 11 x 17 in");
    
    /// <summary>
    /// Represents a 'A - C - 17 x 22 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize C = new(nameof(C), "C - 17 x 22 in");
    
    /// <summary>
    /// Represents a 'B - D - 22 x 34 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize D = new(nameof(D), "D - 22 x 34 in");
    
    /// <summary>
    /// Represents a 'B - E - 34 x 44 in' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize E = new(nameof(E), "E - 34 x 44 in");
    
    /// <summary>
    /// Represents a 'B - A4 - 210 x 297 mm' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize A4 = new(nameof(A4), "A4 - 210 x 297 mm");
    
    /// <summary>
    /// Represents a 'B - A3 - 297 x 420 mm' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize A3 = new(nameof(A3), "A3 - 297 x 420 mm");
    
    /// <summary>
    /// Represents a 'B - A2 - 420 x 594 mm' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize A2 = new(nameof(A2), "A2 - 420 x 594 mm");
    
    /// <summary>
    /// Represents a 'B - A1 - 594 x 841 mm' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize A1 = new(nameof(A1), "A1 - 594 x 841 mm");
    
    /// <summary>
    /// Represents a 'B - A0 - 841 x 1189 mm' <see cref="SheetSize"/> value.
    /// </summary>
    public static readonly SheetSize A0 = new(nameof(A0), "A0 - 841 x 1189 mm");
}