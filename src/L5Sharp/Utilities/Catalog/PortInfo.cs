namespace L5Sharp.Utilities.Catalog;

/// <summary>
/// A entity class representing the data returned from a module catalog entry.
/// </summary>
public class PortInfo
{
    /// <summary>
    /// The unique identifying number of the port.
    /// </summary>
    /// <remarks>
    /// All Modules have at least one port which defines how it connects to other peripherals or devices.
    /// Each port is identified by the number. Default value is 1.
    /// </remarks>
    public int Number { get; set; } = 1;
    
    /// <summary>
    /// Gets the value that represents the port type.
    /// </summary>
    /// <remarks>
    /// This value appears to be specific to the product. Ports with IP will have 'Network' for their type.
    /// </remarks>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets a value indicating whether the port allows upstream connection, or if it is a downstream only port.
    /// </summary>
    public bool DownstreamOnly { get; set; }
}