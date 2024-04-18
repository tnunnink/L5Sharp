using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A component of the <see cref="SafetyInfo"/> element which contains the list of tags configured for the
/// safety tag map.
/// </summary>
public class SafetyTagMap : LogixElement, IList<TagName>
{
    /// <summary>
    /// Creates a new <see cref="SafetyTagMap"/> with default values.
    /// </summary>
    public SafetyTagMap() : base(L5XName.SafetyTagMap)
    {
    }

    /// <summary>
    /// Creates a new <see cref="SafetyTagMap"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public SafetyTagMap(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the number of elements in the SafetyTagMap.
    /// </summary>
    public int Count => GetMap().Count;

    /// <summary>
    /// Represents an indexer for accessing elements in a collection of TagName objects.
    /// </summary>
    /// <param name="index">The index of the element to get or set.</param>
    /// <returns>The TagName object at the specified index.</returns>
    public TagName this[int index]
    {
        get => GetMap()[index];
        set
        {
            var map = GetMap();
            map[index] = value;
            SetMap(map);
        }
    }

    /// <summary>
    /// Adds a <see cref="TagName"/> to the SafetyTagMap.
    /// </summary>
    /// <param name="tag">The <see cref="TagName"/> to be added.</param>
    public void Add(TagName tag)
    {
        var map = GetMap();
        map.Add(tag);
        SetMap(map);
    }

    /// <summary>
    /// Adds a collection of TagNames to the SafetyTagMap.
    /// </summary>
    /// <param name="tags">The collection of TagNames to add.</param>
    public void AddRange(IEnumerable<TagName> tags)
    {
        var map = GetMap();
        map.AddRange(tags);
        SetMap(map);
    }

    /// <summary>
    /// Clears the tag map.
    /// </summary>
    public void Clear()
    {
        var map = GetMap();
        map.Clear();
        SetMap(map);
    }

    /// <summary>
    /// Checks whether the SafetyTagMap contains the specified tag.
    /// </summary>
    /// <param name="tag">The tag to check.</param>
    /// <returns>true if the SafetyTagMap contains the specified tag; otherwise, false.</returns>
    public bool Contains(TagName tag)
    {
        return GetMap().Contains(tag);
    }

    /// <summary>
    /// Copies the elements of the SafetyTagMap to an array, starting at the specified array index.
    /// </summary>
    /// <param name="array">The destination array.</param>
    /// <param name="arrayIndex">The starting index of the destination array.</param>
    public void CopyTo(TagName[] array, int arrayIndex)
    {
        var map = GetMap();
        map.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the specified tag from the SafetyTagMap.
    /// </summary>
    /// <param name="tag">The tag to remove.</param>
    /// <returns>true if the tag is successfully removed; otherwise, false.</returns>
    public bool Remove(TagName tag)
    {
        var map = GetMap();
        var result = map.Remove(tag);
        SetMap(map);
        return result;
    }

    /// <summary>
    /// Searches for the specified tag in the tag map.
    /// </summary>
    /// <param name="tag">The tag to search for.</param>
    /// <returns>The index of the first occurrence of the specified tag in the tag map; or -1 if the tag is not found.</returns>
    public int IndexOf(TagName tag)
    {
        return GetMap().IndexOf(tag);
    }

    /// <summary>
    /// Inserts a tag at the specified index in the SafetyTagMap.
    /// </summary>
    /// <param name="index">The zero-based index at which the tag should be inserted.</param>
    /// <param name="tag">The tag to insert.</param>
    public void Insert(int index, TagName tag)
    {
        var map = GetMap();
        map.Insert(index, tag);
        SetMap(map);
    }

    /// <summary>
    /// Removes the element at the specified index from the SafetyTagMap.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    public void RemoveAt(int index)
    {
        var map = GetMap();
        map.RemoveAt(index);
        SetMap(map);
    }

    /// <inheritdoc />
    public IEnumerator<TagName> GetEnumerator() => GetMap().GetEnumerator();

    bool ICollection<TagName>.IsReadOnly => false;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private List<TagName> GetMap()
    {
        return Element.Value.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => new TagName(s))
            .ToList();
    }

    private void SetMap(IEnumerable<TagName> map)
    {
        var value = map.Combine(',').Trim();
        Element.Value = value;
    }
}