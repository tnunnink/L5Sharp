using System;

namespace L5Sharp.Gateway.Abstractions;

/// <summary>
/// Defines the contract for building a PLC client with customizable configuration options.
/// </summary>
public interface IPlcClientBuilder
{
    /// <summary>
    /// Configures the client to target the specified slot on the PLC.
    /// </summary>
    /// <param name="slot">The slot number on the PLC to target.</param>
    /// <returns>A builder instance with the specified slot configuration applied.</returns>
    IPlcClientBuilder Slot(ushort slot);

    /// <summary>
    /// Configures the client with custom PLC options.
    /// </summary>
    /// <param name="config">An action delegate to configure the PLC options.</param>
    /// <returns>A builder instance with the specified PLC options applied.</returns>
    IPlcClientBuilder WithOptions(Action<PlcOptions> config);

    /// <summary>
    /// Configures the client builder to use a specified tag service for operations.
    /// If not specified, this builder will initialize the client with the native tag service.
    /// </summary>
    /// <param name="serviceFactory">A factory method that creates an instance of an <see cref="ITagService"/> implementation.</param>
    /// <returns>The client builder instance with the specified service configuration applied.</returns>
    IPlcClientBuilder UseTagService(Func<ITagService> serviceFactory);

    /// <summary>
    /// Builds and returns an instance of the PLC client based on the current configuration.
    /// </summary>
    /// <returns>An instance of <see cref="IPlcClient"/> with the applied configurations.</returns>
    IPlcClient Build();
}