using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// The internal implementation that indexes elements and references and stores them in the local dictionaries.
/// This class is then used by <see cref="L5X"/> to find components, tags, and references quickly.
/// </summary>
internal class LogixIndex : ILogixLookup
{
    /// <summary>
    /// An index of all logix elements (that we care to index) in the L5X file for fast lookups.
    /// </summary>
    private readonly Dictionary<Reference, XElement> _index;

    public LogixIndex(XElement content, bool trackChanges = false)
    {
        _index = content.Descendants()
            .Where(e => e.IsReferenceElement() && e.IsIdentifiable())
            .ToDictionary(Reference.To);

        if (trackChanges)
        {
            content.Changed += ContentOnChanged;
        }
    }

    public bool Contains(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        return _index.ContainsKey(reference);
    }

    public bool Contains(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);

        return _index.ContainsKey(reference);
    }

    public LogixEntity Get(Reference reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        if (!_index.TryGetValue(reference, out var value))
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = value.Deserialize<LogixEntity>();
        return result is Tag tag ? tag[reference.Location.ToTagName().Path] : result;
    }

    public TComponent Get<TComponent>(string name, string? program = null) where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name, program);

        if (!_index.TryGetValue(reference, out var value))
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var result = value.Deserialize<TComponent>();
        return result is Tag tag ? (TComponent)(LogixObject)tag[reference.Location.ToTagName().Path] : result;
    }

    public TCode Get<TCode>(int number, string program, string routine) where TCode : LogixCode
    {
        var reference = Reference.To<TCode>(number, program, routine);

        if (!_index.TryGetValue(reference, out var value))
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        return value.Deserialize<TCode>();
    }

    public LogixEntity Get(Action<IReferenceTypeBuilder> action)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);

        if (!_index.TryGetValue(reference, out var value))
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var element = value.Deserialize<LogixEntity>();
        return element is Tag tag ? tag[reference.Location.ToTagName().Path] : element;
    }

    public TEntity Get<TEntity>(Action<IReferenceLocationBuilder> action) where TEntity : LogixEntity
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var builder = new ReferenceBuilder();
        builder.Type(ReferenceType.Parse(typeof(TEntity).Name));
        action.Invoke(builder);
        var reference = builder.Build();

        if (!_index.TryGetValue(reference, out var value))
            throw new KeyNotFoundException($"No element with the provided reference was found: {reference}");

        var element = value.Deserialize<TEntity>();
        return element is Tag tag ? (TEntity)(LogixObject)tag[reference.Location.ToTagName().Path] : element;
    }

    public bool TryGet(Reference reference, out LogixEntity entity)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        if (!_index.TryGetValue(reference, out var value))
        {
            entity = null!;
            return false;
        }

        var result = value.Deserialize<LogixEntity>();
        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path) : result;
        return target.IsNull(out entity);
    }

    public bool TryGet<TComponent>(string name, out TComponent component) where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name);

        if (!_index.TryGetValue(reference, out var value))
        {
            component = null!;
            return false;
        }

        var result = value.Deserialize<TComponent>();
        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixObject>() : result;
        return (target as TComponent).IsNull(out component);
    }

    public bool TryGet<TComponent>(string name, string program, out TComponent component)
        where TComponent : LogixComponent
    {
        var reference = Reference.To<TComponent>(name, program);

        if (!_index.TryGetValue(reference, out var value))
        {
            component = null!;
            return false;
        }

        var result = value.Deserialize<TComponent>();
        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixObject>() : result;
        return (target as TComponent).IsNull(out component);
    }

    public bool TryGet<TCode>(int number, string program, string routine, out TCode code) where TCode : LogixCode
    {
        var reference = Reference.To<TCode>(number, program, routine);

        if (!_index.TryGetValue(reference, out var value))
        {
            code = null!;
            return false;
        }

        var result = value.Deserialize<TCode>();
        return result.IsNull(out code);
    }

    public bool TryGet(Action<IReferenceTypeBuilder> action, out LogixEntity entity)
    {
        if (action is null)
            throw new ArgumentNullException(nameof(action));

        var reference = Reference.Build(action);

        if (!_index.TryGetValue(reference, out var value))
        {
            entity = null!;
            return false;
        }

        var result = value.Deserialize<LogixEntity>();
        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixEntity>() : result;
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

        if (!_index.TryGetValue(reference, out var value))
        {
            entity = null!;
            return false;
        }

        var result = value.Deserialize<TEntity>();
        var target = result is Tag tag ? tag.Member(reference.Location.ToTagName().Path)?.As<LogixObject>() : result;
        return (target as TEntity).IsNull(out entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void ContentOnChanged(object sender, XObjectChangeEventArgs e)
    {
        //todo we could have option to track changes made but the only issue is reference collisions.
        //does that throw an exception? how does that work.
        throw new NotImplementedException();
    }
}