using System;
using System.Collections.Generic;
using System.Net;
using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Common;

namespace L5Sharp.Gateway;

/// <summary>
/// Represents configuration options for interacting with a PLC (Programmable Logic Controller).
/// </summary>
public class PlcOptions
{
    /// <summary>
    /// Gets or sets the IP address of the PLC (Programmable Logic Controller) to connect to.
    /// Specifies the network address used for communication with the PLC.
    /// </summary>
    public string IP { get; set; } = IPAddress.Loopback.ToString();

    /// <summary>
    /// Gets or sets the slot number of the PLC (Programmable Logic Controller) to connect to.
    /// Specifies the position of the controller within the chassis.
    /// </summary>
    public ushort Slot { get; set; }

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
    public HashSet<TagStatus> ThrowOn { get; } = [];
}