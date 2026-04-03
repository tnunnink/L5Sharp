using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Provides a mechanism for adding and retrieving custom metadata for a given <see cref="ILogixElement"/>.
/// </summary>
/// <remarks>
/// This class is a wrapper around the underlying <see cref="XElement"/> annotations, specifically
/// using a <see cref="Dictionary{TKey,TValue}"/> to store key-value pairs of metadata.
/// </remarks>
public class LogixMetadata : IEnumerable<KeyValuePair<string, object>>
{
    /// <summary>
    /// Represents the underlying XML element used to store and manage the metadata annotations.
    /// </summary>
    private readonly XElement _element;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogixMetadata"/> class with the specified XML element.
    /// </summary>
    /// <param name="element">The underlying XML element that will store the metadata annotations.</param>
    /// <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
    internal LogixMetadata(XElement element)
    {
        _element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// Gets the number of metadata items.
    /// </summary>
    public int Count => GetData().Count;

    /// <summary>
    /// Gets or sets the metadata value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the metadata to get or set.</param>
    /// <returns>The value associated with the specified key.</returns>
    public object this[string key]
    {
        get => GetData()[key];
        set => GetData()[key] = value;
    }

    /// <summary>
    /// Adds a metadata item with the specified key and value.
    /// </summary>
    /// <param name="key">The key of the metadata to add.</param>
    /// <param name="value">The value of the metadata to add.</param>
    public void Add(string key, object value) => GetData().Add(key, value);

    /// <summary>
    /// Removes the metadata item with the specified key.
    /// </summary>
    /// <param name="key">The key of the metadata to remove.</param>
    /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>.</returns>
    public bool Remove(string key) => GetData().Remove(key);

    /// <summary>
    /// Removes all metadata items.
    /// </summary>
    public void Clear() => GetData().Clear();

    /// <summary>
    /// Determines whether the metadata contains an item with the specified key.
    /// </summary>
    /// <param name="key">The key to locate.</param>
    /// <returns><c>true</c> if the metadata contains an item with the key; otherwise, <c>false</c>.</returns>
    public bool Contains(string key) => GetData().ContainsKey(key);

    /// <summary>
    /// Gets the metadata value associated with the specified key and casts it to the specified type.
    /// </summary>
    /// <param name="key">The key of the metadata to retrieve.</param>
    /// <typeparam name="T">The type to cast the metadata value to.</typeparam>
    /// <returns>The metadata value associated with the specified key, cast to type <typeparamref name="T"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the specified key is not found in the metadata storage.</exception>
    /// <exception cref="InvalidCastException">Thrown when the metadata value cannot be cast to the specified type <typeparamref name="T"/>.</exception>
    public T Get<T>(string key)
    {
        if (!GetData().TryGetValue(key, out var value))
            throw new KeyNotFoundException($"The key '{key}' was not found in metadata storage.");

        if (value is not T typed)
            throw new InvalidCastException(
                $"The metadata value for key '{key}' is of type '{value.GetType().Name}' and cannot be cast to type '{typeof(T).Name}'.");

        return typed;
    }

    /// <summary>
    /// Tries to get the metadata value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the metadata to retrieve.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the metadata contains an item with the specified key; otherwise, <c>false</c>.</returns>
    public bool TryGetValue(string key, out object value) => GetData().TryGetValue(key, out value);

    /// <summary>
    /// Tries to get the metadata value associated with the specified key and cast it to the specified type.
    /// </summary>
    /// <param name="key">The key of the metadata to retrieve.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key cast to type <typeparamref name="T"/>, if the key is found and the value can be cast; otherwise, the default value for type <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The type to cast the metadata value to.</typeparam>
    /// <returns><c>true</c> if the metadata contains an item with the specified key and the value can be cast to type <typeparamref name="T"/>; otherwise, <c>false</c>.</returns>
    public bool TryGetValue<T>(string key, out T value)
    {
        value = default!;

        if (!GetData().TryGetValue(key, out var match) || match is not T typed)
            return false;

        value = typed;
        return true;
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => GetData().GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Retrieves the annotation dictionary from the underlying XML element that stores metadata key-value pairs.
    /// If the annotation does not exist, a new dictionary is created and added to the element.
    /// </summary>
    /// <returns>A dictionary containing the metadata key-value pairs associated with the XML element.</returns>
    private Dictionary<string, object> GetData()
    {
        var data = _element.Annotation<Dictionary<string, object>>();

        if (data is null)
        {
            data = new Dictionary<string, object>();
            _element.AddAnnotation(data);
        }

        return data;
    }
}