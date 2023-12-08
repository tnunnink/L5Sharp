using System;
using System.Collections;
using System.Collections.Generic;

namespace L5Sharp.Core;

public class TagMap : IEnumerable<KeyValuePair<TagName, TagName>>
{
    private readonly List<KeyValuePair<TagName, TagName>> _map = new();

    /// <summary>
    /// Represents a mapping class for tags.
    /// </summary>
    public TagMap()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void Map(TagName from, TagName to)
    {
        if (from is null) throw new ArgumentNullException(nameof(from));
        if (to is null) throw new ArgumentNullException(nameof(to));
        if (from.IsEmpty) throw new ArgumentException("Can not create mapping for empty tag.");
        if (to.IsEmpty) throw new ArgumentException("Can not create mapping for empty tag.");
        _map.Add(new KeyValuePair<TagName, TagName>(from, to));
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<TagName, TagName>> GetEnumerator() => _map.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}