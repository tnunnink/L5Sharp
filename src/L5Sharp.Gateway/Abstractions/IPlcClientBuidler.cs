using System;

namespace L5Sharp.Gateway.Abstractions;

/// <summary>
/// Defines the contract for building a PLC client with customizable configuration options.
/// </summary>
public interface IPlcClientBuilder
{
    /// <summary>
    /// Configures the client with the specified IP address.
    /// </summary>
    /// <param name="ip">The IP address used to configure the client.</param>
    /// <returns>A builder instance with the specified IP address configuration applied.</returns>
    IPlcClientBuilder At(string ip);

    /// <summary>
    /// Configures the client to target the specified slot on the PLC.
    /// </summary>
    /// <param name="slot">The slot number on the PLC to target.</param>
    /// <returns>A builder instance with the specified slot configuration applied.</returns>
    IPlcClientBuilder Slot(short slot);

    /// <summary>
    /// Configures the client with custom PLC options.
    /// </summary>
    /// <param name="config">An action delegate to configure the PLC options.</param>
    /// <returns>A builder instance with the specified PLC options applied.</returns>
    IPlcClientBuilder WithOptions(Action<PlcOptions> config);

    /// <summary>
    /// Configures the client to use the native service for PLC communication.
    /// </summary>
    /// <returns>A builder instance configured to use the native service.</returns>
    IPlcClientBuilder UseNativeService();

    /// <summary>
    /// Configures the client to use an in-memory service for PLC communication.
    /// </summary>
    /// <returns>A builder instance configured to use the in-memory service.</returns>
    IPlcClientBuilder UseInMemoryService();

    /// <summary>
    /// Configures the client builder to use a specific service for tag operations.
    /// </summary>
    /// <param name="serviceFactory">A factory method that creates an instance of an <see cref="ITagService"/> implementation.</param>
    /// <returns>The client builder instance with the specified service configuration applied.</returns>
    IPlcClientBuilder UseService(Func<ITagService> serviceFactory);

    /// <summary>
    /// Builds and returns an instance of the PLC client based on the current configuration.
    /// </summary>
    /// <returns>An instance of <see cref="IPlcClient"/> with the applied configurations.</returns>
    IPlcClient Build();
}

/// <summary>
/// 
/// </summary>
public static class Plc
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static IPlcClientBuilder ConnectTo(string ip)
    {
        throw new NotImplementedException();
    }
}