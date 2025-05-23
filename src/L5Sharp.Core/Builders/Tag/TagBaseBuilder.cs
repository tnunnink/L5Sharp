using System;

namespace L5Sharp.Core;

internal abstract class TagBaseBuilder<TBuilder>(Tag instance) : ITagBaseBuilder<TBuilder>
{
    protected readonly Tag Instance = instance;

    public Tag Build()
    {
        return Instance;
    }

    public TBuilder WithDescription(string description)
    {
        Instance.Description = description;
        return GetBuilder();
    }

    public TBuilder WithAccess(ExternalAccess access)
    {
        Instance.ExternalAccess = access;
        return GetBuilder();
    }

    public TBuilder Constant()
    {
        Instance.Constant = true;
        return GetBuilder();
    }

    public TBuilder Produces(Action<ITagProducerBuilder> action)
    {
        var builder = new TagProducerBuilder();
        action.Invoke(builder);
        var info = builder.Build();
        Instance.ProduceInfo = info;
        Instance.TagType = TagType.Produced;
        return GetBuilder();
    }

    public TBuilder Consumes(Action<ITagConsumerBuilder> action)
    {
        var builder = new TagConsumerBuilder();
        action.Invoke(builder);
        var info = builder.Build();
        Instance.ConsumeInfo = info;
        Instance.TagType = TagType.Consumed;
        return GetBuilder();
    }

    protected abstract TBuilder GetBuilder();
}