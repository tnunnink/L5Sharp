using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Common;

/// <summary>
/// 
/// </summary>
public class Params : IList<string>
{
    private readonly XAttribute _attribute;

    /// <summary>
    /// Creates a new <see cref="Params"/> collection with a default backing attribute to use as the store for
    /// parameter names.
    /// </summary>
    public Params()
    {
        _attribute = new XAttribute("Params", string.Empty);
    }

    /// <summary>
    /// Creates a new <see cref="Params"/> collection with the backing attribute having the specified name.
    /// </summary>
    /// <param name="name">The name or the params attribute to create.</param>
    /// <exception cref="ArgumentException"><c>name</c> is null or empty.</exception>
    public Params(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Can not create Params with empty or null name.", nameof(name));

        _attribute = new XAttribute(name, string.Empty);
    }

    /// <summary>
    /// Creates a new <see cref="Params"/> collection with the provided backing attribute.
    /// </summary>
    /// <param name="attribute">The backing attribute to use for storing the parameter collection.</param>
    /// <exception cref="ArgumentException"><c>attribute</c> is null.</exception>
    public Params(XAttribute attribute)
    {
        _attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static Params Pins(string parameters) => new Params(new XAttribute(L5XName.VisiblePins, parameters));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    public Params(IEnumerable<string> parameters)
    {
        _attribute = new XAttribute(nameof(Params), string.Join(" ", parameters));
    }

    /// <inheritdoc />
    public int Count => GetParams().Count;

    /// <inheritdoc />
    public string this[int index]
    {
        get => GetParams()[index];
        set
        {
            var parameters = GetParams();
            parameters[index] = value;
            SetParams(parameters);
        }
    }

    /// <inheritdoc />
    public void Add(string parameter)
    {
        _attribute.Value = string.Concat(_attribute.Value, " ", parameter).Trim();
    }

    /// <inheritdoc />
    public void Clear() => _attribute.SetValue(string.Empty);

    /// <inheritdoc />
    public bool Contains(string parameter) => GetParams().Contains(parameter);

    /// <inheritdoc />
    public void CopyTo(string[] array, int arrayIndex) => GetParams().CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(string parameter)
    {
        var parameters = GetParams();
        var result = parameters.Remove(parameter);
        SetParams(parameters);
        return result;
    }

    /// <inheritdoc />
    public int IndexOf(string parameter) => GetParams().IndexOf(parameter);

    /// <inheritdoc />
    public void Insert(int index, string parameter)
    {
        var parameters = GetParams();
        parameters.Insert(index, parameter);
        SetParams(parameters);
    }

    /// <inheritdoc />
    public void RemoveAt(int index) => GetParams().RemoveAt(index);

    /// <inheritdoc />
    public IEnumerator<string> GetEnumerator() => GetParams().GetEnumerator();

    #region Internal

    bool ICollection<string>.IsReadOnly => false;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    private List<string> GetParams() => _attribute.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
    private void SetParams(IEnumerable<string> parameters) => _attribute.SetValue(string.Join(" ", parameters));

    #endregion
}