using System.Collections.Generic;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents configuration options for interacting with a PLC (Programmable Logic Controller).
/// </summary>
public class PlcOptions
{
    /// <summary>
    /// Gets or sets the timeout duration, in milliseconds, for operations interacting with the PLC.
    /// Determines the maximum time to wait for a response before timing out.
    /// </summary>
    public int Timeout { get; set; } = 5000;

    /// <summary>
    /// Gets or sets the interval, in milliseconds, at which data is read from the PLC.
    /// Specifies the frequency of read operations to ensure up-to-date data retrieval.
    /// </summary>
    public int ReadInterval { get; set; } = 1000;

    /// <summary>
    /// Gets the set of statuses that the client should throw exceptions for.
    /// By default, no status codes are configured and error codes are returned with the tag response.
    /// </summary>
    public HashSet<TagResult> ThrowOn { get; } = [];
}