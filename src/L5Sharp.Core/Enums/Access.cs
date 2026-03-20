namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all Logix <see cref="Access"/> options.
/// <remarks>
/// <see cref="Access"/> is a Logix setting that determines the ability to read from or write to a given component.
/// </remarks>
/// </summary>
public sealed class Access : LogixEnum<Access, string>
{
    private Access(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents no read or write <see cref="Access"/>.
    /// </summary>
    public static readonly Access None = new(nameof(None), nameof(None));

    /// <summary>
    /// Represents read but not write <see cref="Access"/>.
    /// </summary>
    public static readonly Access ReadOnly = new(nameof(ReadOnly), "Read Only");

    /// <summary>
    /// Represents read and write <see cref="Access"/>.
    /// </summary>
    public static readonly Access ReadWrite = new(nameof(ReadWrite), "Read/Write");
}