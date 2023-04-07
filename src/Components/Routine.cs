using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Routine</c> component. Contains the properties for a generic Routine element. This type does not
/// include content property. More specific routine types are derived from this base class.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixSerializer(typeof(RoutineSerializer))]
public class Routine : ILogixRoutine
{
    private readonly List<ILogixCode> _content;

    /// <summary>
    /// 
    /// </summary>
    public Routine()
    {
        _content = new List<ILogixCode>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="capacity"></param>
    public Routine(int capacity)
    {
        _content = new List<ILogixCode>(capacity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    public Routine(IEnumerable<ILogixCode> content)
    {
        _content = new List<ILogixCode>(content);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="content"></param>
    /// <param name="description"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Routine(string name, IEnumerable<ILogixCode> content, string? description = null)
    {
        _content = new List<ILogixCode>(content);
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? string.Empty;
    }


    /// <inheritdoc />
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The type of the <see cref="Routine"/> component.
    /// </summary>
    /// <value>A <see cref="Enums.RoutineType"/> enum specifying the type content the routine contains.</value>
    public RoutineType Type { get; set; } = RoutineType.Typeless;

    /// <inheritdoc />
    public Scope Scope { get; set; } = Scope.Null;

    /// <inheritdoc />
    public string Container { get; set; } = string.Empty;

    /// <inheritdoc />
    public int Count => _content.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public ILogixCode this[int index]
    {
        get => _content[index];
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            ValidateType(value);
            AssignScope(value);
            
            _content[index] = value;
            
            UpdateNumbers();
        }
    }

    /// <inheritdoc />
    public void Add(ILogixCode item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));
            
        ValidateType(item);
        AssignScope(item);

        _content.Add(item);
        
        UpdateNumbers();
    }

    /// <inheritdoc />
    public void AddRange(IEnumerable<ILogixCode> content)
    {
        if (content is null)
            throw new ArgumentNullException(nameof(content));

        var items = content.ToList();

        foreach (var item in items)
        {
            ValidateType(item);
            AssignScope(item);
        }
            
        _content.AddRange(items);
        
        UpdateNumbers();
    }

    /// <inheritdoc />
    public void Clear() => _content.Clear();

    /// <inheritdoc />
    public bool Contains(ILogixCode item) => _content.Contains(item);

    /// <inheritdoc />
    public void CopyTo(ILogixCode[] array, int arrayIndex) => _content.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(ILogixCode item) => _content.Remove(item);

    /// <inheritdoc />
    public int IndexOf(ILogixCode item) => _content.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, ILogixCode item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));
            
        ValidateType(item);
        AssignScope(item);
        
        _content.Insert(index, item);

        UpdateNumbers();
    }

    /// <inheritdoc />
    public void RemoveAt(int index) => _content.RemoveAt(index);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    /// <returns></returns>
    public IEnumerable<TContent> As<TContent>() => _content.Cast<TContent>();

    /// <inheritdoc />
    public IEnumerator<ILogixCode> GetEnumerator() => _content.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void UpdateNumbers()
    {
        for (var i = 0; i < _content.Count; i++)
            _content[i].Number = i;
    }
    
    private void AssignScope(ILogixCode item)
    {
        item.Container = Container;
        item.Routine = Name;
    }

    private void ValidateType(ILogixCode item)
    {
        if (Type == RoutineType.Rll && item is not Rung)
            throw new ArgumentException($"The provided item type {item.GetType()} is not valid for an Rll Routine");

        if (Type == RoutineType.St && item is not Line)
            throw new ArgumentException($"The provided item type {item.GetType()} is not valid for an St Routine");
    }

    private static RoutineType DetermineType(IEnumerable<ILogixCode> content)
    {
        var type = content.FirstOrDefault() ?? throw new ArgumentException();
        return RoutineType.FromCode(type);
    }
}