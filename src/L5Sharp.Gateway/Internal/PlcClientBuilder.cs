using System;
using L5Sharp.Gateway.Abstractions;

namespace L5Sharp.Gateway.Internal;

/// <summary>
/// A builder class designed to construct and configure instances of <see cref="IPlcClient"/>.
/// Provides methods for setting the PLC slot, configuring PLC options, and defining a custom tag service.
/// </summary>
internal class PlcClientBuilder(string ip) : IPlcClientBuilder
{
    private ushort _slot;
    private readonly PlcOptions _options = new();
    private Func<ITagService>? _serviceFactory;

    /// <inheritdoc />
    public IPlcClientBuilder Slot(ushort slot)
    {
        _slot = slot;
        return this;
    }

    /// <inheritdoc />
    public IPlcClientBuilder WithOptions(Action<PlcOptions> config)
    {
        if (config is null) throw new ArgumentNullException(nameof(config));
        config.Invoke(_options);
        return this;
    }

    /// <inheritdoc />
    public IPlcClientBuilder UseTagService(Func<ITagService> serviceFactory)
    {
        _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        return this;
    }

    /// <inheritdoc />
    public IPlcClient Build()
    {
        return _serviceFactory is not null
            ? new PlcClient(ip, _slot, _options, _serviceFactory())
            : new PlcClient(ip, _slot, _options);
    }
}