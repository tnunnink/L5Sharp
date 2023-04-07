using L5Sharp.Types;

namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all Logix <see cref="DataTypeFamily"/> options for a given <see cref="ILogixType"/>.
/// Valid options are None and String.
/// </summary>
public sealed class DataTypeFamily : LogixEnum<DataTypeFamily, string>
{
    private DataTypeFamily(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents no specific data type family.
    /// All <see cref="ILogixType"/> objects except <see cref="StringType"/> should have this option.
    /// </summary>
    public static readonly DataTypeFamily None = new(nameof(None), "NoFamily");
        
        
    /// <summary>
    /// Represents a string family. Only <see cref="StringType"/> objects should have this option.
    /// </summary>
    public static readonly DataTypeFamily String = new(nameof(String), "StringFamily");
}