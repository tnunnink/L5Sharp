namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of Ethernet/IP communication modes for a Logix controller or module.
/// </summary>
public class EthernetMode  : LogixEnum<EthernetMode, string>
{
    private EthernetMode(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents the Dual-IP <see cref="EthernetMode"/> value.
    /// </summary>
    public static readonly EthernetMode DualIp = new(nameof(DualIp), "A1/A2: Dual-IP");
        
    /// <summary>
    /// Represents the Linear/DLR (Device Level Ring) <see cref="EthernetMode"/> value.
    /// </summary>
    public static readonly EthernetMode LinearDlr = new(nameof(LinearDlr), "Linear/DLR");
}