using System;

namespace L5Sharp.Core;

/// <summary>
/// Provides functionality for the incremental construction of a <see cref="Tag"/> object.
/// </summary>
internal class TagBuilder(TagName tagName) : ITagBuilder
{
    private readonly Tag _instance = new() { Name = tagName.Base };

    public ITagBuilder WithValue(LogixData value)
    {
        _instance.Value = value;
        return this;
    }

    public ITagBuilder WithValue<TData>() where TData : LogixData, new()
    {
        _instance.Value = new TData();
        return this;
    }

    public ITagBuilder WithValue<TData>(Action<TData> config) where TData : LogixData, new()
    {
        var value = new TData();
        config(value);
        _instance.Value = value;
        return this;
    }

    public ITagBuilder WithDescription(string description)
    {
        _instance.Description = description;
        return this;
    }

    public ITagBuilder AliasFor(TagName alias)
    {
        _instance.AliasFor = alias;
        _instance.TagType = TagType.Alias;
        return this;
    }

    public ITagBuilder UsedAs(TagUsage usage)
    {
        _instance.Usage = usage;
        return this;
    }

    public ITagBuilder ReadWrite()
    {
        _instance.ExternalAccess = ExternalAccess.ReadWrite;
        return this;
    }

    public ITagBuilder ReadOnly()
    {
        _instance.ExternalAccess = ExternalAccess.ReadOnly;
        return this;
    }

    public ITagBuilder Private()
    {
        _instance.ExternalAccess = ExternalAccess.None;
        return this;
    }

    public ITagBuilder Constant()
    {
        _instance.Constant = true;
        return this;
    }

    public ITagBuilder Produces(Action<ITagProducerBuilder> config)
    {
        var builder = new TagProducerBuilder();
        config.Invoke(builder);
        var info = builder.Build();
        _instance.ProduceInfo = info;
        _instance.TagType = TagType.Produced;
        return this;
    }

    public ITagBuilder Consumes(Action<ITagConsumerBuilder> config)
    {
        var builder = new TagConsumerBuilder();
        config.Invoke(builder);
        var info = builder.Build();
        _instance.ConsumeInfo = info;
        _instance.TagType = TagType.Consumed;
        return this;
    }

    public Tag Build()
    {
        if (tagName.Scope.IsProgram)
        {
            var context = new Program(tagName.Scope.Container) { Use = Use.Context };
            context.Tags.Add(_instance);
        }

        return _instance;
    }
}