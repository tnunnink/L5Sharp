using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace L5Sharp.Core;

internal class LogixLookup(XElement content) : ILogixLookup
{
    public bool Contains(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        var element = content.XPathSelectElement(reference);
        return element is not null;
    }

    public bool Contains(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);
        var element = content.XPathSelectElement(reference);
        return element is not null;
    }

    public LogixEntity Get(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        var element = content.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided scope was found: {reference}");

        var result = element.Deserialize<LogixEntity>();
        return result is Tag tag ? tag[reference.Location.ToTagName().Path] : result;
    }

    public TComponent Get<TComponent>(string name, string? program = null) where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name, program);

        var element = content.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = element.Deserialize<TComponent>();
        return result is Tag tag ? (TComponent)(LogixObject)tag[reference.Location.ToTagName().Path] : result;
    }

    public TCode Get<TCode>(int number, string program, string routine) where TCode : LogixCode
    {
        var reference = Reference.To<TCode>(number, program, routine);

        var element = content.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        return element.Deserialize<TCode>();
    }

    public LogixEntity Get(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);
        var element = content.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = element.Deserialize<LogixEntity>();
        return result is Tag tag ? tag[reference.Location.ToTagName().Path] : result;
    }

    public TEntity Get<TEntity>(Action<IReferenceLocationBuilder> action) where TEntity : LogixEntity
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var builder = new ReferenceBuilder();
        builder.Type(ReferenceType.Parse(typeof(TEntity).Name));
        action.Invoke(builder);
        var reference = builder.Build();

        var element = content.XPathSelectElement(reference);

        if (element is null)
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = element.Deserialize<TEntity>();
        return result is Tag tag ? (TEntity)(LogixObject)tag[reference.Location.ToTagName().Path] : result;
    }

    public bool TryGet(Reference reference, out LogixEntity entity)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        var result = content.XPathSelectElement(reference)?.Deserialize<LogixEntity>();

        if (result is null)
        {
            entity = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path) : result;
        return target.IsNull(out entity);
    }

    bool ILogixLookup.TryGet<TComponent>(string name, out TComponent compoenent)
    {
        var reference = Reference.To<TComponent>(name);

        var result = content.XPathSelectElement(reference)?.Deserialize<TComponent>();

        if (result is null)
        {
            compoenent = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
        return (target as TComponent).IsNull(out compoenent);
    }

    public bool TryGet<TComponent>(string name, string program, out TComponent component)
        where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name, program);

        var result = content.XPathSelectElement(reference)?.Deserialize<TComponent>();

        if (result is null)
        {
            component = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
        return (target as TComponent).IsNull(out component);
    }

    public bool TryGet<TCode>(int number, string program, string routine, out TCode code) where TCode : LogixCode
    {
        var reference = Reference.To<TCode>(number, program, routine);

        var result = content.XPathSelectElement(reference)?.Deserialize<TCode>();

        if (result is null)
        {
            code = null!;
            return false;
        }

        return result.IsNull(out code);
    }

    public bool TryGet(Action<IReferenceTypeBuilder> action, out LogixEntity entity)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);
        var element = content.XPathSelectElement(reference);
        var result = element?.Deserialize<LogixEntity>();

        if (result is null)
        {
            entity = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path) : result;
        return target.IsNull(out entity);
    }

    public bool TryGet<TEntity>(Action<IReferenceLocationBuilder> action, out TEntity entity)
        where TEntity : LogixEntity
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var builder = new ReferenceBuilder();
        builder.Type(ReferenceType.Parse(typeof(TEntity).Name));
        action.Invoke(builder);
        var reference = builder.Build();

        var element = content.XPathSelectElement(reference);
        var result = element?.Deserialize<LogixEntity>();

        if (result is null)
        {
            entity = null!;
            return false;
        }

        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
        return (target as TEntity).IsNull(out entity);
    }
}