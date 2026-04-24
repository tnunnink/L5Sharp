namespace L5Sharp.Core;

/// <summary>
/// Represents an enumeration of Ethernet/IP communication modes for a Logix controller or module.
/// </summary>
public class EthernetIpMode  : LogixEnum<EthernetIpMode, string>
{
    private EthernetIpMode(string name, string value) : base(name, value)
    {
    }

    /// <summary>
    /// Represents the Dual-IP <see cref="EthernetIpMode"/> value.
    /// </summary>
    public static readonly EthernetIpMode DualIp = new(nameof(DualIp), "A1/A2: Dual-IP");
        
    /// <summary>
    /// Represents the Linear/DLR (Device Level Ring) <see cref="EthernetIpMode"/> value.
    /// </summary>
    public static readonly EthernetIpMode LinearDlr = new(nameof(LinearDlr), "Linear/DLR");
}