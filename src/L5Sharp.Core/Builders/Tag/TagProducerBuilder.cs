namespace L5Sharp.Core;

internal class TagProducerBuilder : ITagProducerBuilder
{
    private int _maxCount = 1;
    private bool _sendEventTrigger;
    private bool _unicast;

    public ITagProducerBuilder WithMaxCount(int maximum)
    {
        _maxCount = maximum;
        return this;
    }

    public ITagProducerBuilder SendEventTrigger()
    {
        _sendEventTrigger = true;
        return this;
    }

    public ITagProducerBuilder Unicast()
    {
        _unicast = true;
        return this;
    }

    public ProduceInfo Build()
    {
        return new ProduceInfo
        {
            ProduceCount = _maxCount,
            ProgrammaticallySendEventTrigger = _sendEventTrigger,
            UnicastPermitted = _unicast
        };
    }
}