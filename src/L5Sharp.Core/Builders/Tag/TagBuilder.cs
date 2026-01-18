using System;

namespace L5Sharp.Core;

/// <summary>
/// Provides functionality for the incremental construction of a <see cref="Tag"/> object.
/// </summary>
internal class TagBuilder(TagName tagName) : ITagBuilder
{
    private readonly Tag _instance = new() { Name = tagName.Base };

    public ITagBuilder AliasFor(TagName alias)
    {
        _instance.AliasFor = alias;
        _instance.TagType = TagType.Alias;
        return this;
    }

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

    public ITagBuilder WithAccess(Access access)
    {
        _instance.ExternalAccess = access;
        return this;
    }

    public ITagBuilder ReadWrite()
    {
        _instance.ExternalAccess = Access.ReadWrite;
        return this;
    }

    public ITagBuilder ReadOnly()
    {
        _instance.ExternalAccess = Access.ReadOnly;
        return this;
    }

    public ITagBuilder WithUsage(TagUsage usage)
    {
        _instance.Usage = usage;
        return this;
    }

    public ITagBuilder Normal()
    {
        _instance.Usage = TagUsage.Normal;
        return this;
    }

    public ITagBuilder Local()
    {
        _instance.Usage = TagUsage.Local;
        return this;
    }

    public ITagBuilder Public()
    {
        _instance.Usage = TagUsage.Public;
        return this;
    }

    public ITagBuilder Input()
    {
        _instance.Usage = TagUsage.Input;
        return this;
    }

    public ITagBuilder Output()
    {
        _instance.Usage = TagUsage.Output;
        return this;
    }

    public ITagBuilder InOut()
    {
        _instance.Usage = TagUsage.InOut;
        return this;
    }

    public ITagBuilder Static()
    {
        _instance.Usage = TagUsage.Static;
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