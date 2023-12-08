namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all Logix <see cref="RungType"/> values for a given Rung.
/// </summary>
public class RungType : LogixEnum<RungType, string>
{
    private RungType(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents a Normal <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType Normal = new(nameof(Normal), "N");

    /// <summary>
    /// Represents a Insert <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType Insert = new(nameof(Insert), "I");
        
    /// <summary>
    /// Represents a Delete <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType Delete = new(nameof(Delete), "D");
        
    /// <summary>
    /// Represents a Replace <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType Replace = new(nameof(Replace), "R");
        
    /// <summary>
    /// Represents a InsertReplace <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType InsertReplace = new(nameof(InsertReplace), "IR");
        
    /// <summary>
    /// Represents a PendingReplace <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType PendingReplace = new(nameof(PendingReplace), "rR");
        
    /// <summary>
    /// Represents a PendingReplaceInsert <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType PendingReplaceInsert = new(nameof(PendingReplaceInsert), "rI");
        
    /// <summary>
    /// Represents a PendingReplaceNormal <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType PendingReplaceNormal = new(nameof(PendingReplaceNormal), "rN");
        
    /// <summary>
    /// Represents a PendingInsertRung <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType PendingInsertRung = new(nameof(PendingInsertRung), "e");
        
    /// <summary>
    /// Represents a PendingReplaceRung <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType PendingReplaceRung = new(nameof(PendingReplaceRung), "er");
        
    /// <summary>
    /// Represents a PendingDeleteRung <see cref="RungType"/> value.
    /// </summary>
    public static readonly RungType PendingDeleteRung = new(nameof(PendingDeleteRung), "dN");
}