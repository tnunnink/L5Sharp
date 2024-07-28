namespace L5Sharp.Core;

/// <summary>
/// An enumeration of all Logix <see cref="OpcUAAccess"/> options.
/// <remarks>
/// <see cref="OpcUAAccess"/> is a Logix setting that determines the ability to read from or write to a given component from OPC UA.
/// </remarks>
/// </summary>
public sealed class OpcUAAccess : LogixEnum<OpcUAAccess, string>
{
    private OpcUAAccess(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents no read or write <see cref="OpcUAAccess"/>.
    /// </summary>
    public static readonly OpcUAAccess None = new(nameof(None), nameof(None));

    /// <summary>
    /// Represents read but not write <see cref="OpcUAAccess"/>.
    /// </summary>
    public static readonly OpcUAAccess ReadOnly = new(nameof(ReadOnly), "Read Only");

    /// <summary>
    /// Represents read and write <see cref="OpcUAAccess"/>.
    /// </summary>
    public static readonly OpcUAAccess ReadWrite = new(nameof(ReadWrite), "Read/Write");
}