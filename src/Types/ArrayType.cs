using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types;

/// <summary>
/// 
/// </summary>
public class ArrayType : LogixType, IEnumerable<LogixType>
{
    /// <inheritdoc />
    public ArrayType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new array type from the provided logix array object.
    /// </summary>
    /// <param name="array">An array of logix types</param>
    /// <exception cref="ArgumentNullException"><c>array</c> or any element of <c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"></exception>
    public ArrayType(Array array) : base(GenerateElement(array.Cast<LogixType>().ToList(), Dimensions.FromArray(array)))
    {
    }

    /// <inheritdoc />
    public override DataTypeFamily Family => this.FirstOrDefault(t => t is not NullType)?.Family ?? DataTypeFamily.None;

    /// <inheritdoc />
    public override DataTypeClass Class => this.FirstOrDefault(t => t is not NullType)?.Class ?? DataTypeClass.Unknown;

    /// <summary>
    /// Gets the dimensions of the the array.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    public Dimensions Dimensions => GetValue<Dimensions>() ?? throw new L5XException(Element);

    /// <summary>
    /// Gets the radix format of the the atomic type array.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> format if the array is an atomic type array;
    /// otherwise, <see cref="Enums.Radix.Null"/>.</value>
    public Radix Radix => GetValue<Radix>() ?? Radix.Null;

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixType this[ushort x]
    {
        get => GetIndex($"[{x}]");
        set => SetIndex($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixType this[ushort x, ushort y]
    {
        get => GetIndex($"[{x},{y}]");
        set => SetIndex($"[{x},{y}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixType this[ushort x, ushort y, ushort z]
    {
        get => GetIndex($"[{x},{y},{z}]");
        set => SetIndex($"[{x},{y},{z}]", value);
    }

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType(LogixType[] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType(LogixType[,] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType(LogixType[,,] array) => new(array);

    /// <summary>
    /// Creates a new <see cref="ArrayType{TDataType}"/> of the specified type with the length of the provided dimensions.
    /// </summary>
    /// <param name="dimensions">The dimensions of the array to create.</param>
    /// <typeparam name="TDataType">The logix type for which to create.
    /// Must have a default parameterless constructor in order to generate instances.</typeparam>
    /// <returns>A <see cref="ArrayType{TDataType}"/> of the specified dimensions containing new objects of the specified type.</returns>
    public static ArrayType<TDataType> New<TDataType>(Dimensions dimensions) where TDataType : LogixType, new()
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        switch (dimensions.Rank)
        {
            case 1:
            {
                var array = new TDataType[dimensions.X];
                for (var i = 0; i < array.GetLength(0); i++)
                    array[i] = new TDataType();
                return array;
            }
            case 2:
            {
                var array = new TDataType[dimensions.X, dimensions.Y];
                for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    array[i, j] = new TDataType();
                return array;
            }
            case 3:
            {
                var array = new TDataType[dimensions.X, dimensions.Y, dimensions.Z];
                for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                for (var k = 0; k < array.GetLength(2); k++)
                    array[i, j, k] = new TDataType();
                return array;
            }
            default:
                throw new ArgumentException($"The Rank {dimensions.Rank} is not valid.");
        }
    }

    /// <inheritdoc />
    public IEnumerator<LogixType> GetEnumerator() => Members.Select(m => m.DataType).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Casts the current <see cref="ArrayType"/> to an <see cref="ArrayType{TLogixType}"/> of the specified logix
    /// type generic parameter.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast.</typeparam>
    /// <returns>A <see cref="ArrayType{TLogixType}"/> of the specified.</returns>
    public ArrayType<TLogixType> AsArray<TLogixType>() where TLogixType : LogixType =>
        new(this.Cast<TLogixType>().ToArray());

    #region Internal

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying element. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixType"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private LogixType GetIndex(string index)
    {
        var member = Members.SingleOrDefault(m => m.Name == index);

        if (member is null)
            throw new ArgumentOutOfRangeException($"The index '{index}' is outside the bound of the array.");

        return member.DataType;
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the current underlying array element. 
    /// </summary>
    /// <param name="index">The index at which to set the type.</param>
    /// <param name="value">The logix type value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private void SetIndex(string index, LogixType value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var member = Members.SingleOrDefault(m => m.Name == index);

        if (member is null)
            throw new ArgumentOutOfRangeException($"The index '{index}' is outside the bound of the array.");

        member.DataType = value;
    }

    /// <summary>
    /// Creates a default <see cref="XElement"/> representing the backing data for the array type.
    /// </summary>
    /// <param name="collection">The array to serialize.</param>
    /// <param name="dimensions"></param>
    /// <returns>A new <see cref="XElement"/> representing the array structure.</returns>
    /// <exception cref="ArgumentException"></exception>
    private static XElement GenerateElement(List<LogixType> collection, Dimensions dimensions)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (collection.Any(t => t is null)) throw new ArgumentNullException(nameof(collection));
        if (collection.Select(t => t.Name).Distinct().Count() != 1)
            throw new ArgumentException("Array type must be initialized with a single type.");

        var seed = collection.First();

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, seed.Name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));

        Radix? radix = null;
        if (seed is AtomicType atomicType)
        {
            radix = atomicType.Radix;
            element.Add(new XAttribute(L5XName.Radix, radix));
        }

        var elements = dimensions.Indices().Zip(collection, (s, t) => GenerateIndex(s, t, radix));
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates a new array index <see cref="XElement"/> using the provided index and logix type.
    /// </summary>
    /// <param name="index">The array index of the element.</param>
    /// <param name="type">The logix type of the element.</param>
    /// <param name="radix">The optional radix format of the array.</param>
    /// <returns>A new <see cref="XElement"/> representing the serialized index of the array.</returns>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    private static XElement GenerateIndex(string index, LogixType type, Radix? radix = null)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, index));

        switch (type)
        {
            case AtomicType atomicType:
                var value = radix is not null ? atomicType.ToString(radix) : atomicType.ToString();
                element.Add(new XAttribute(L5XName.Value, value));
                break;
            case StringType stringType:
                element.Add(GenerateStringStructure(stringType));
                break;
            case StructureType structureType:
                element.Add(structureType.Serialize());
                break;
        }

        return element;
    }

    private static XElement GenerateStringStructure(StringType type)
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, type.Name));
        element.Add(GenerateStringMembers(type));
        return element;
    }

    private static IEnumerable<XElement> GenerateStringMembers(StringType type)
    {
        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal));
        len.Add(new XAttribute(L5XName.Value, type.LEN));
        yield return len;

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii));
        data.Add(new XCData(type.ToString()));
        yield return data;
    }

    #endregion
}

/// <summary>
/// A generic <see cref="ArrayType"/> that provides strongly type element access to indices of the array.
/// </summary>
/// <typeparam name="TLogixType">The logic type the array contains.</typeparam>
public sealed class ArrayType<TLogixType> : ArrayType, IEnumerable<TLogixType> where TLogixType : LogixType
{
    /// <inheritdoc />
    public ArrayType(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public ArrayType(Array array) : base(array)
    {
    }

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    public new TLogixType this[ushort x]
    {
        get => (TLogixType)base[x];
        set => base[x] = value;
    }

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    public new TLogixType this[ushort x, ushort y]
    {
        get => (TLogixType)base[x, y];
        set => base[x, y] = value;
    }

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    public new TLogixType this[ushort x, ushort y, ushort z]
    {
        get => (TLogixType)base[x, y, z];
        set => base[x, y, z] = value;
    }

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType<TLogixType>(TLogixType[] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType<TLogixType>(TLogixType[,] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType<TLogixType>(TLogixType[,,] array) => new(array);

    /// <inheritdoc />
    public new IEnumerator<TLogixType> GetEnumerator() => Members.Select(m => (TLogixType)m.DataType).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}