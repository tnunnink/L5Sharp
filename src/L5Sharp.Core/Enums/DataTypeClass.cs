namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all Logix <see cref="DataTypeClass"/> for a given data type component.
/// </summary>
/// <remarks>Only user,</remarks>
public sealed class DataTypeClass : LogixEnum<DataTypeClass, string>
{
    private DataTypeClass(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents an <b>Unknown</b> <see cref="DataTypeClass"/>.
    /// </summary>
    public static readonly DataTypeClass Unknown = new(nameof(Unknown), nameof(Unknown));

    /// <summary>
    /// Represents an <b>UserDefined</b> <see cref="DataTypeClass"/>.
    /// </summary>
    public static readonly DataTypeClass User = new(nameof(User), nameof(User));

    /// <summary>
    /// Represents an <b>IO</b> <see cref="DataTypeClass"/>.
    /// </summary>
    public static readonly DataTypeClass IO = new(nameof(IO), nameof(IO));
}