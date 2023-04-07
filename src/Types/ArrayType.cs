using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;
using L5Sharp.Utilities;

namespace L5Sharp.Types;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TLogixType"></typeparam>
[LogixSerializer(typeof(ArraySerializer))]
public sealed class ArrayType<TLogixType> : ILogixArray<TLogixType> where TLogixType : ILogixType
{
    private readonly Dictionary<string, TLogixType> _elements;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dimensions"></param>
    /// <param name="types"></param>
    /// <exception cref="ArgumentNullException"><c>dimensions</c> or <c>types</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>dimensions</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>types</c> is empty or greater than the specified dimensional length.</exception>
    public ArrayType(Dimensions dimensions, ICollection<TLogixType> types)
    {
        Check.NotNull(dimensions);
        Check.NotNull(types);

        if (dimensions.IsEmpty)
            throw new ArgumentException("Dimensions can not be empty to generate array type.");

        if (dimensions.Length > types.Count || types.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(types),
                "Types collection must be non empty and less than or equal to the specified dimensional length.");

        Dimensions = dimensions;

        if (types.Any(t => t is null))
            throw new ArgumentException("The provided types collection can not have any null instance members");

        _elements = Dimensions.Indices()
            .Zip(types, (index, type) => new { index, type })
            .ToDictionary(i => i.index, i => i.type);
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType{TLogixType}"/> collection using the provided array of <see cref="ILogixType"/> objects.
    /// </summary>
    /// <param name="elements">The array of element to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>elements</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>elements</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Any <c>elements</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
    public ArrayType(TLogixType[] elements)
    {
        Dimensions = Dimensions.FromArray(elements);
            
        if (elements.Any(t => t is null))
            throw new ArgumentException("Elements array can not have any null instances");

        _elements = Dimensions.Indices()
            .Zip(elements, (index, type) => new { index, type })
            .ToDictionary(i => i.index, i => i.type);
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType{TLogixType}"/> collection using the provided array of <see cref="ILogixType"/> objects.
    /// </summary>
    /// <param name="elements">The array of element to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>elements</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>elements</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Any <c>elements</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
    public ArrayType(TLogixType[,] elements)
    {
        Dimensions = Dimensions.FromArray(elements);

        if (elements.Cast<TLogixType>().Any(e => e is null))
            throw new ArgumentException("Elements array can not have any null instances");

        _elements = Dimensions.Indices()
            .Zip(elements.Cast<TLogixType>(), (index, type) => new { index, type })
            .ToDictionary(i => i.index, i => i.type);
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType{TLogixType}"/> collection using the provided array of <see cref="ILogixType"/> objects.
    /// </summary>
    /// <param name="elements">The array of element to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>elements</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>elements</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Any <c>elements</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
    public ArrayType(TLogixType[,,] elements)
    {
        Dimensions = Dimensions.FromArray(elements);
            
        if (elements.Cast<TLogixType>().Any(e => e is null))
            throw new ArgumentException("Elements array can not have any null instances");

        _elements = Dimensions.Indices()
            .Zip(elements.Cast<TLogixType>(), (index, type) => new { index, type })
            .ToDictionary(i => i.index, i => i.type);
    }

    /// <inheritdoc />
    public string Name => _elements.First().Value.Name;

    /// <inheritdoc />
    public DataTypeFamily Family => _elements.First().Value.Family;

    /// <inheritdoc />
    public DataTypeClass Class => _elements.First().Value.Class;

    /// <summary>
    /// The dimensions value of the <see cref="ArrayType{TLogixType}"/>, indicating the length of the array.
    /// </summary>
    public Dimensions Dimensions { get; }

    /// <summary>
    /// Gets the <see cref="ILogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    public TLogixType this[ushort x] => _elements[$"[{x}]"];


    /// <summary>
    /// Gets the <see cref="ILogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    public TLogixType this[ushort x, ushort y] => _elements[$"[{x},{y}]"];

    /// <summary>
    /// Gets the <see cref="ILogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    public TLogixType this[ushort x, ushort y, ushort z] => _elements[$"[{x},{y},{z}]"];

    /// <summary>
    /// Gets a collection of <see cref="Member"/> that represent the elements of the array.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> object.</returns>
    public IEnumerable<Member> Members => _elements.Select(e => new Member(e.Key, e.Value));

    /// <inheritdoc />
    public override string ToString() => Name;

    /// <inheritdoc />
    public IEnumerator<TLogixType> GetEnumerator() => _elements.Values.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}