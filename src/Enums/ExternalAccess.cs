namespace L5Sharp.Enums;

/// <summary>
/// An enumeration of all Logix <see cref="ExternalAccess"/> options.
/// <remarks>
/// <see cref="ExternalAccess"/> is a Logix setting that determines the ability to read from or write to a given component.
/// </remarks>
/// </summary>
public sealed class ExternalAccess : LogixEnum<ExternalAccess, string>
{
    private ExternalAccess(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents no read or write <see cref="ExternalAccess"/>.
    /// </summary>
    public static readonly ExternalAccess None = new(nameof(None), nameof(None));

    /// <summary>
    /// Represents read but not write <see cref="ExternalAccess"/>.
    /// </summary>
    public static readonly ExternalAccess ReadOnly = new(nameof(ReadOnly), "Read Only");

    /// <summary>
    /// Represents read and write <see cref="ExternalAccess"/>.
    /// </summary>
    public static readonly ExternalAccess ReadWrite = new(nameof(ReadWrite), "Read/Write");
}