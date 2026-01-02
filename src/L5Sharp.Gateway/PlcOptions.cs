using System.Collections.Generic;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents configuration options for interacting with a PLC (Programmable Logic Controller).
/// </summary>
public class PlcOptions
{
    /// <summary>
    /// Gets or sets the timeout duration, in milliseconds, for PLC operations.
    /// </summary>
    public int Timeout { get; set; } = 5000;

    /// <summary>
    /// 
    /// </summary>
    public int ReadInterval { get; set; } = 1000;

    /// <summary>
    /// Gets the set of statuses that the client should throw exceptions for.
    /// By default, no status codes are configured.
    /// </summary>
    public HashSet<TagResult> ExceptionCodes { get; } = [];
}