namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all Logix <see cref="DataTypeFamily"/> options for a given <see cref="LogixData"/>.
/// Valid options are None and String.
/// </summary>
public sealed class DataTypeFamily : LogixEnum<DataTypeFamily, string>
{
    private DataTypeFamily(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents no specific data type family.
    /// All <see cref="LogixData"/> objects except <see cref="StringData"/> should have this option.
    /// </summary>
    public static readonly DataTypeFamily None = new(nameof(None), "NoFamily");
        
        
    /// <summary>
    /// Represents a string family. Only <see cref="StringData"/> objects should have this option.
    /// </summary>
    public static readonly DataTypeFamily String = new(nameof(String), "StringFamily");
}