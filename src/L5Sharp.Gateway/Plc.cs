using L5Sharp.Gateway.Abstractions;
using L5Sharp.Gateway.Internal;

namespace L5Sharp.Gateway;

/// <summary>
/// Provides static methods to establish a connection to a PLC and configure its client builder.
/// Serves as the entry point for initiating interactions with PLCs.
/// </summary>
public static class Plc
{
    /// <summary>
    /// Establishes a connection to a PLC at the specified IP address and initializes
    /// a client builder for further configuration.
    /// </summary>
    /// <param name="ip">The IP address of the PLC to connect to.</param>
    /// <returns>An instance of <see cref="IPlcClientBuilder"/> for configuring and building the PLC client.</returns>
    public static IPlcClientBuilder ConnectTo(string ip)
    {
        return new PlcClientBuilder(ip);
    }
}