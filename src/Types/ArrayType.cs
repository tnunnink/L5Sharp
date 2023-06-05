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
public class ArrayType : LogixType, IEnumerable<LogixType>
{
    /// <inheritdoc />
    public ArrayType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dimensions"></param>
    public ArrayType(Dimensions dimensions)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType{TLogixType}"/> collection using the provided array of <see cref="LogixType"/> objects.
    /// </summary>
    /// <param name="array">The array of element to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Any <c>array</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
    public ArrayType(Array array) : base(GenerateElement(array))
    {
    }

    /// <inheritdoc />
    public override DataTypeFamily Family => this.First().Family;

    /// <inheritdoc />
    public override DataTypeClass Class => this.First().Class;

    /// <summary>
    /// Gets the dimensions of the the array.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    public Dimensions Dimensions => GetValue<Dimensions>() ?? throw new L5XException(Element);

    /// <summary>
    /// Gets a collection of <see cref="Member"/> that represent the elements of the array.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> object.</returns>
    public override IEnumerable<Member> Members => Element.Elements().Select(e => new Member(e));

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
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
    /// Created a new <see cref="ArrayType{TDataType}"/> of the specified type with the length of the provided dimensions.
    /// </summary>
    /// <param name="dimensions">The dimensions of the array to create.</param>
    /// <typeparam name="TDataType">The logix type for which to create.
    /// Must have a default parameterless constructor in order to generate instances.</typeparam>
    /// <returns>A <see cref="ArrayType{TDataType}"/> of the specified dimensions containing new objects of the specified type.</returns>
    public static ArrayType<TDataType> New<TDataType>(Dimensions dimensions) where TDataType : LogixType, new()
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        //todo technically not right
        var elements = Enumerable.Range(0, dimensions.Length).Select(_ => new TDataType());

        return new ArrayType<TDataType>(elements.ToArray());
    }

    /// <inheritdoc />
    public override string ToString() => Name;

    /// <inheritdoc />
    public IEnumerator<LogixType> GetEnumerator() => Element.Elements().Select(LogixData.Deserialize).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region Internal

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying element. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixType"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private LogixType GetIndex(string index)
    {
        var element = Element.Elements().SingleOrDefault(e => e.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException($"The index '{index}' is outside the bound of the array.");

        return LogixData.Deserialize(element);
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

        var collection = array.Cast<LogixType>().ToList();

        var seed = collection.FirstOrDefault();

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, seed?.Name ?? string.Empty));
        element.Add(new XAttribute(L5XName.Dimensions, dimension));

        if (seed is AtomicType atomicType)
            element.Add(new XAttribute(L5XName.Radix, atomicType.Radix));

        var elements = dimension.Indices().Zip(collection, GenerateIndex);
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates a new array index <see cref="XElement"/> using the provided index and logix type.
    /// </summary>
    /// <param name="index">The array index of the element.</param>
    /// <param name="type">The logix type of the element.</param>
    /// <returns>A new <see cref="XElement"/> representing the serialized index of the array.</returns>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    private static XElement GenerateIndex(string index, LogixType type)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, index));

        switch (type)
        {
            case AtomicType atomicType:
                element.Add(new XAttribute(L5XName.Value, atomicType.ToString()));
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

    /// <summary>
    /// Builds a string structure element needed to generate a string member index element.
    /// </summary>
    /// <param name="type">The string type instance.</param>
    /// <returns>A <see cref="XElement"/> representing the serialized structure of the type.</returns>
    /// <remarks>This is really the only place (other than Member) that string types act or serialize string structure types.</remarks>
    private static XElement GenerateStringStructure(StringType type)
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal.Value));
        len.Add(new XAttribute(L5XName.Value, type.LEN.ToString()));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
        data.Add(new XCData(type.ToString()));
        element.Add(data);

        return element;
    }

    #endregion
}

/// <summary>
/// A <see cref="LogixType"/> that represents an array of logix types. 
/// </summary>
/// <typeparam name="TLogixType">The logic type the array contains.</typeparam>
public class ArrayType<TLogixType> : LogixType, IEnumerable<TLogixType> where TLogixType : LogixType
{
    /// <inheritdoc />
    public ArrayType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType{TLogixType}"/> collection using the provided array of <see cref="LogixType"/> objects.
    /// </summary>
    /// <param name="array">The array of element to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Any <c>array</c> dimensional length is greater than <see cref="ushort.MaxValue"/>.</exception>
    public ArrayType(Array array) : base(GenerateElement(array))
    {
    }

    /// <inheritdoc />
    public override DataTypeFamily Family => this.First().Family;

    /// <inheritdoc />
    public override DataTypeClass Class => this.First().Class;

    /// <summary>
    /// Gets the dimensions of the the array.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    public Dimensions Dimensions => GetValue<Dimensions>() ?? throw new L5XException(Element);

    /// <summary>
    /// Gets a collection of <see cref="Member"/> that represent the elements of the array.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> object.</returns>
    public override IEnumerable<Member> Members => Element.Elements().Select(e => new Member(e));

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    public TLogixType this[ushort x]
    {
        get => GetIndex($"[{x}]");
        set => SetIndex($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixType"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    public TLogixType this[ushort x, ushort y]
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
    public TLogixType this[ushort x, ushort y, ushort z]
    {
        get => GetIndex($"[{x},{y},{z}]");
        set => SetIndex($"[{x},{y},{z}]", value);
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
    public override string ToString() => Name;

    /// <inheritdoc />
    public IEnumerator<TLogixType> GetEnumerator() =>
        Element.Elements().Select(e => (TLogixType)LogixData.Deserialize(e)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region Internal

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying element. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixType"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private TLogixType GetIndex(string index)
    {
        var element = Element.Elements().SingleOrDefault(e => e.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException($"The index '{index}' is outside the bound of the array.");

        return (TLogixType)LogixData.Deserialize(element);
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the current underlying array element. 
    /// </summary>
    /// <param name="index">The index at which to set the type.</param>
    /// <param name="value">The logix type value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private void SetIndex(string index, TLogixType value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

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

        var collection = array.Cast<TLogixType>().ToList();

        var seed = collection.FirstOrDefault();

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, seed?.Name ?? string.Empty));
        element.Add(new XAttribute(L5XName.Dimensions, dimension));

        if (seed is AtomicType atomicType)
            element.Add(new XAttribute(L5XName.Radix, atomicType.Radix));

        var elements = dimension.Indices().Zip(collection, GenerateIndex);
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates a new array index <see cref="XElement"/> using the provided index and logix type.
    /// </summary>
    /// <param name="index">The array index of the element.</param>
    /// <param name="type">The logix type of the element.</param>
    /// <returns>A new <see cref="XElement"/> representing the serialized index of the array.</returns>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    private static XElement GenerateIndex(string index, TLogixType type)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, index));

        switch (type)
        {
            case AtomicType atomicType:
                element.Add(new XAttribute(L5XName.Value, atomicType.ToString()));
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

    /// <summary>
    /// Builds a string structure element needed to generate a string member index element.
    /// </summary>
    /// <param name="type">The string type instance.</param>
    /// <returns>A <see cref="XElement"/> representing the serialized structure of the type.</returns>
    /// <remarks>This is really the only place (other than Member) that string types act or serialize string structure types.</remarks>
    private static XElement GenerateStringStructure(StringType type)
    {
        var element = new XElement(L5XName.Structure);
        element.Add(new XAttribute(L5XName.DataType, type.Name));

        var len = new XElement(L5XName.DataValueMember);
        len.Add(new XAttribute(L5XName.Name, nameof(type.LEN)));
        len.Add(new XAttribute(L5XName.DataType, type.LEN.Name));
        len.Add(new XAttribute(L5XName.Radix, Radix.Decimal.Value));
        len.Add(new XAttribute(L5XName.Value, type.LEN.ToString()));
        element.Add(len);

        var data = new XElement(L5XName.DataValueMember);
        data.Add(new XAttribute(L5XName.Name, nameof(type.DATA)));
        data.Add(new XAttribute(L5XName.DataType, type.Name));
        data.Add(new XAttribute(L5XName.Radix, Radix.Ascii.Value));
        data.Add(new XCData(type.ToString()));
        element.Add(data);

        return element;
    }

    #endregion
}