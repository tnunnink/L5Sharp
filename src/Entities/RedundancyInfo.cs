using L5Sharp.Components;

namespace L5Sharp.Entities;

/// <summary>
/// A sub component of the <see cref="Controller"/> component that contains properties or configuration
/// related to the controller redundancy.
/// </summary>
public class RedundancyInfo : LogixEntity<RedundancyInfo>
{
    /// <summary>
    /// Specify whether redundancy is used.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>. Default id <c>false</c>.</value>
    public bool Enabled { get; set; } = false;
    
    /// <summary>
    /// Specify whether to keep test edits on when a switchover occurs in a redundant system. 
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>. Default id <c>false</c>.</value>
    public bool KeepTestEditsOnSwitchOver { get; set; } = false;
    
    /// <summary>
    /// Specify the percentage (0...100) of I/O memory that is available to the system after the download
    /// when configured for redundancy.
    /// </summary>
    /// <value>A <see cref="float"/> indicating the percentage 0-100.</value>
    public float IOMemoryPadPercentage { get; set; } = 0;
    
    /// <summary>
    /// Specify the percentage (0...100) of the data table to reserve.
    /// </summary>
    /// <value>A <see cref="float"/> indicating the percentage 0-100.</value>
    /// <remarks>
    /// <b>From docs</b>: If redundancy is not enabled, type 0.
    /// If redundancy is enabled, type 50.
    /// </remarks>
    public float DataTablePadPercentage { get; set; } = 0;
}