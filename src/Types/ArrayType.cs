using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Types;

/// <summary>
/// 
/// </summary>
public class ArrayType : L5Sharp.LogixType, IEnumerable<L5Sharp.LogixType>
{
    /// <inheritdoc />
    public ArrayType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dimensions"></param>
    public ArrayType(Dimensions dimensions) : base(GenerateElement(dimensions))
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType{TLogixType}"/> collection using the provided array of <see cref="L5Sharp.LogixType"/> objects.
    /// </summary>
    /// <param name="array">The array of element to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Any <c>array</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
    public ArrayType(Array array) : base(GenerateElement(array))
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
    /// Gets the <see cref="L5Sharp.LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    public L5Sharp.LogixType this[ushort x]
    {
        get => GetIndex($"[{x}]");
        set => SetIndex($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="L5Sharp.LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    public L5Sharp.LogixType this[ushort x, ushort y]
    {
        get => GetIndex($"[{x},{y}]");
        set => SetIndex($"[{x},{y}]", value);
    }

    /// <summary>
    /// Gets the <see cref="L5Sharp.LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    public L5Sharp.LogixType this[ushort x, ushort y, ushort z]
    {
        get => GetIndex($"[{x},{y},{z}]");
        set => SetIndex($"[{x},{y},{z}]", value);
    }

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType(L5Sharp.LogixType[] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType(L5Sharp.LogixType[,] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayType{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayType{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayType(L5Sharp.LogixType[,,] array) => new(array);

    /// <summary>
    /// Created a new <see cref="ArrayType{TDataType}"/> of the specified type with the length of the provided dimensions.
    /// </summary>
    /// <param name="dimensions">The dimensions of the array to create.</param>
    /// <typeparam name="TDataType">The logix type for which to create.
    /// Must have a default parameterless constructor in order to generate instances.</typeparam>
    /// <returns>A <see cref="ArrayType{TDataType}"/> of the specified dimensions containing new objects of the specified type.</returns>
    public static ArrayType<TDataType> New<TDataType>(Dimensions dimensions) where TDataType : L5Sharp.LogixType, new()
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        //todo technically not right
        var elements = Enumerable.Range(0, dimensions.Length).Select(_ => new TDataType());

        return new ArrayType<TDataType>(elements.ToArray());
    }

    /// <inheritdoc />
    public IEnumerator<L5Sharp.LogixType> GetEnumerator() => Element.Elements().Select(LogixType.Deserialize).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Casts the current <see cref="ArrayType"/> to an <see cref="ArrayType{TLogixType}"/> of the specified logix
    /// type generic parameter.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast.</typeparam>
    /// <returns>A <see cref="ArrayType{TLogixType}"/> of the specified.</returns>
    public ArrayType<TLogixType> ToType<TLogixType>() where TLogixType : L5Sharp.LogixType
    {
        var seed = this.FirstOrDefault(t => t is not null);

        if (seed is not null && seed is not TLogixType)
            throw new InvalidCastException(
                $"The current array type {seed.GetType()} can not be case to type {typeof(TLogixType)}.");

        return new ArrayType<TLogixType>(Element);
    }


    #region Internal

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying element. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="L5Sharp.LogixType"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private L5Sharp.LogixType GetIndex(string index)
    {
        var element = Element.Elements().SingleOrDefault(e => e.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException($"The index '{index}' is outside the bound of the array.");

        return LogixType.Deserialize(element);
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the current underlying array element. 
    /// </summary>
    /// <param name="index">The index at which to set the type.</param>
    /// <param name="value">The logix type value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private void SetIndex(string index, L5Sharp.LogixType value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        //If the array was uninitialized, set the type and radix.
        if (string.IsNullOrEmpty(Element.Attribute(L5XName.DataType)?.Value))
        {
            Element.SetAttributeValue(L5XName.DataType, value.Name);
            if (value is AtomicType atomicType)
                Element.SetAttributeValue(L5XName.Radix, atomicType.Radix);
        }

        var element = Element.Elements().SingleOrDefault(e => e.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException($"The index '{index}' is outside the bound of the array.");

        element.ReplaceWith(GenerateIndex(index, value));
    }

    /// <summary>
    /// Creates a default <see cref="XElement"/> representing the backing data for the array type.
    /// </summary>
    /// <param name="array">The array to serialize.</param>
    /// <returns>A new <see cref="XElement"/> representing the array structure.</returns>
    /// <exception cref="ArgumentException"></exception>
    private static XElement GenerateElement(Array array)
    {
        var dimension = Dimensions.FromArray(array);

        var collection = array.Cast<L5Sharp.LogixType>().ToList();

        var seed = collection.FirstOrDefault();

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, seed?.Name ?? string.Empty));
        element.Add(new XAttribute(L5XName.Dimensions, dimension));

        Radix? radix = null;
        if (seed is AtomicType atomicType)
        {
            radix = atomicType.Radix;
            element.Add(new XAttribute(L5XName.Radix, radix));
        }

        var elements = dimension.Indices().Zip(collection, (s, t) => GenerateIndex(s, t, radix));
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates a default array <see cref="XElement"/> with the provided dimensions.
    /// </summary>
    /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array.</param>
    /// <returns>A new <see cref="XElement"/> representing the array structure.</returns>
    private static XElement GenerateElement(Dimensions dimensions)
    {
        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, string.Empty));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));

        var elements = dimensions.Indices()
            .Select(i => new XElement(L5XName.Element, new XAttribute(L5XName.Index, i)));
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
    private static XElement GenerateIndex(string index, L5Sharp.LogixType type, Radix? radix = null)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, index));

        switch (type)
        {
            case AtomicType atomicType:
                var value = radix is not null ? atomicType.ToString(radix) : atomicType.ToString();
                element.Add(new XAttribute(L5XName.Value, value));
                break;
            case StringType stringType:
                element.Add(stringType.Serialize(L5XName.Structure));
                break;
            case StructureType structureType:
                element.Add(structureType.Serialize());
                break;
        }

        return element;
    }

    #endregion
}

/// <summary>
/// A generic <see cref="ArrayType"/> that provides strongly type element access to indices of the array.
/// </summary>
/// <typeparam name="TLogixType">The logic type the array contains.</typeparam>
public sealed class ArrayType<TLogixType> : ArrayType, IEnumerable<TLogixType> where TLogixType : L5Sharp.LogixType
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
    /// Gets the <see cref="L5Sharp.LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    public new TLogixType this[ushort x]
    {
        get => (TLogixType)base[x];
        set => base[x] = value;
    }

    /// <summary>
    /// Gets the <see cref="L5Sharp.LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    public new TLogixType this[ushort x, ushort y]
    {
        get => (TLogixType)base[x, y];
        set => base[x, y] = value;
    }

    /// <summary>
    /// Gets the <see cref="L5Sharp.LogixType"/> instance at the specified index.
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
    public new IEnumerator<TLogixType> GetEnumerator() =>
        Element.Elements().Select(e => (TLogixType)LogixType.Deserialize(e)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}