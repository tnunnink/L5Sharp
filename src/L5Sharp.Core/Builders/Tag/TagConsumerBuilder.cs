using System;

namespace L5Sharp.Core;

internal class TagConsumerBuilder : ITagConsumerBuilder
{
    private string _provider = string.Empty;
    private TagName _remoteTag = TagName.Empty;
    private double _rpi = 20.0;
    private bool _unicast;

    public ITagConsumerBuilder Provider(string provider)
    {
        if (string.IsNullOrEmpty(provider))
            throw new ArgumentException("Provider is required for tag consumer configuration.");

        _provider = provider;
        return this;
    }

    public ITagConsumerBuilder RemoteTag(TagName remoteTag)
    {
        if (string.IsNullOrEmpty(remoteTag))
            throw new ArgumentException("RemoteTag is required for tag consumer configuration.");

        _remoteTag = remoteTag;
        return this;
    }

    public ITagConsumerBuilder RPI(double rpi)
    {
        _rpi = rpi;
        return this;
    }

    public ITagConsumerBuilder Unicast()
    {
        _unicast = true;
        return this;
    }

    public ConsumeInfo Build()
    {
        return new ConsumeInfo
        {
            Producer = _provider,
            RemoteTag = _remoteTag,
            RPI = _rpi,
            Unicast = _unicast
        };
    }
}