using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A abstract <see cref="LogixData"/> that represents an array of other data elements (either atomic or structure).
/// </summary>
/// <remarks>
/// This class implements some of the base array type functionality for the generic derived class
/// <see cref="ArrayData{TLogixType}"/>, but is abstract to force instantiation of the generic class.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class ArrayData : LogixData, IEnumerable
{
    /// <summary>
    /// Creates a new array data instance with the specified dimensions and data type name.
    /// </summary>
    /// <param name="dataType">The name of the data type for the array.</param>
    /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array.</param>
    /// <remarks>
    /// This constructor will use the <see cref="LogixData.Create(string)"/> factory to instantiate default
    /// data objects to fill the array at the specified dimensions. If the provided data type name is not a statically
    /// defined type in this or another assembly, then a default <see cref="ComplexData"/> object will be used.
    /// </remarks>
    protected internal ArrayData(string dataType, Dimensions dimensions)
        : base(CreateArray(Create(dataType), dimensions))
    {
    }

    /// <summary>
    /// Creates a new array data instance with the specified dimensions and seed data object.
    /// </summary>
    /// <param name="dataType">A <see cref="LogixData"/> object to use as a seed to create the array elements.</param>
    /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array.</param>
    /// <remarks>
    /// This constructor will use the provided <paramref name="dataType"/> as the seed to fill the array. It will just
    /// call serialize to generate the data elments of the array at the specified dimension length.
    /// </remarks>
    protected internal ArrayData(LogixData dataType, Dimensions dimensions)
        : base(CreateArray(dataType, dimensions))
    {
    }

    /// <summary>
    /// Creates a new array type from the provided array object.
    /// </summary>
    /// <param name="array">An array of logix types. Array can not be empty, contain null items,
    /// or objects of different logix type.
    /// </param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> contains no elements, <c>null</c> or <c>NullType</c> elements,
    /// or objects with different logix type names.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><c>array</c> rank is greater than 3 dimensions.</exception>
    protected internal ArrayData(Array array) : base(CreateArray(array))
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException"><c>element</c> does not have required attributes or child elements.</exception>
    protected internal ArrayData(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Element.Elements().Select(e => new Member(e));

    /// <summary>
    /// The dimensions of the array, which define the length and rank of the array's elements.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    public Dimensions Dimensions => GetRequiredValue<Dimensions>();

    /// <summary>
    /// Gets the radix format of the array type elements.
    /// </summary>
    /// <value>A <see cref="Core.Radix"/> format if the array is an atomic type array;
    /// otherwise, the radix <see cref="L5Sharp.Core.Radix.Null"/> format.</value>
    public Radix Radix => GetValue<Radix>() ?? Radix.Null;

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixData this[ushort x]
    {
        get => GetIndex<LogixData>($"[{x}]");
        set => SetIndex($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixData this[ushort x, ushort y]
    {
        get => GetIndex<LogixData>($"[{x},{y}]");
        set => SetIndex($"[{x},{y}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixData this[ushort x, ushort y, ushort z]
    {
        get => GetIndex<LogixData>($"[{x},{y},{z}]");
        set => SetIndex($"[{x},{y},{z}]", value);
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData{TLogixType}"/> of the specified type with the length of the provided dimensions.
    /// </summary>
    /// <param name="dimensions">The dimensions of the array to create.</param>
    /// <typeparam name="TLogixType">The logix type for which to create.
    /// Must have a default parameterless constructor in order to generate instances.</typeparam>
    /// <returns>A <see cref="ArrayData{TLogixType}"/> of the specified dimensions containing new objects of the specified type.</returns>
    public static ArrayData<TLogixType> New<TLogixType>(Dimensions dimensions) where TLogixType : LogixData, new()
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        switch (dimensions.Rank)
        {
            case 1:
            {
                var array = new TLogixType[dimensions.X];
                for (var i = 0; i < array.GetLength(0); i++)
                    array[i] = new TLogixType();
                return new ArrayData<TLogixType>(array);
            }
            case 2:
            {
                var array = new TLogixType[dimensions.X, dimensions.Y];
                for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                    array[i, j] = new TLogixType();
                return new ArrayData<TLogixType>(array);
            }
            case 3:
            {
                var array = new TLogixType[dimensions.X, dimensions.Y, dimensions.Z];
                for (var i = 0; i < array.GetLength(0); i++)
                for (var j = 0; j < array.GetLength(1); j++)
                for (var k = 0; k < array.GetLength(2); k++)
                    array[i, j, k] = new TLogixType();
                return new ArrayData<TLogixType>(array);
            }
            default:
                throw new ArgumentException($"The Rank {dimensions.Rank} is not valid.");
        }
    }

    /// <summary>
    /// Casts the current <see cref="ArrayData"/> to an <see cref="ArrayData{TLogixType}"/> of the specified logix
    /// type generic parameter.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast.</typeparam>
    /// <returns>A <see cref="ArrayData{TLogixType}"/> of the specified.</returns>
    public ArrayData<TLogixType> Cast<TLogixType>() where TLogixType : LogixData => new(Element);

    /// <inheritdoc />
    public override string ToString() => $"{Name}{Dimensions.ToIndex()}";

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixData"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    protected TLogixType GetIndex<TLogixType>(string index) where TLogixType : LogixData
    {
        var member = Element.Elements().SingleOrDefault(m => m.MemberName() == index)?.ToMember();

        if (member is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        return member.Value.As<TLogixType>();
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to set the type.</param>
    /// <param name="value">The logix type value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    protected void SetIndex<TLogixType>(string index, TLogixType value) where TLogixType : LogixData
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var member = Element.Elements().SingleOrDefault(m => m.MemberName() == index)?.ToMember();

        if (member is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        member.Value = value;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => Members.Select(m => m.Value).GetEnumerator();

    /// <summary>
    /// Cretaes a new array data element with the provided data type name and dimensions. This method will use the base
    /// static factory to create a seed <see cref="LogixData"/> instance, and use that alond with the dimensions to
    /// generate a default array element. 
    /// </summary>
    private static XElement CreateArray(LogixData dataType, Dimensions dimensions)
    {
        if (dataType is null)
            throw new ArgumentException("Can not create array with null or empty data type name");

        if (dimensions is null || dimensions.IsEmpty)
            throw new ArgumentException("Can not create array with null or empty dimensions");

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, dataType.Name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));
        if (dataType is AtomicData atomic) element.Add(new XAttribute(L5XName.Radix, atomic.Radix));

        var elements = dimensions.Indices().Select(i => CreateArrayElement(i, dataType));
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates a new array data structure element with the provided array object. This method will
    /// determine the dimensions from the provided array. It will also use the first item in the array
    /// to seed the data type name and radix if available. If this collection contains no items we will resort
    /// to the type name of the array element type.
    /// </summary>
    private static XElement CreateArray(Array array)
    {
        var dimensions = Dimensions.FromArray(array);

        var collection = array.Cast<LogixData>().ToArray();
        if (collection.Any(t => t is null or NullData))
            throw new ArgumentException("Array can not be initialized with null items.", nameof(array));

        var first = collection.Length > 0 ? collection[0] : null;
        var dataType = first?.Name ?? (array.GetType().GetElementType()?.Name ?? Null);

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, dataType));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));
        if (first is AtomicData atomic) element.Add(new XAttribute(L5XName.Radix, atomic.Radix));

        var elements = dimensions.Indices().Zip(collection, CreateArrayElement);
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates an array index element with the provided index string and logix type data object.
    /// The format of the element will depend on the logix type provided, atomic, string, or structure.
    /// </summary>
    private static XElement CreateArrayElement(string index, LogixData data)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, index));

        switch (data)
        {
            case AtomicData atomicType:
                element.Add(new XAttribute(L5XName.Value, atomicType));
                break;
            case StringData stringType:
                element.Add(stringType.SerializeStructure());
                break;
            case StructureData structureType:
                element.Add(structureType.Serialize());
                break;
        }

        return element;
    }
}

/// <summary>
/// A <see cref="LogixData"/> that represents an array of other logix types (either atomic or structure).
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public sealed class ArrayData<TData> : ArrayData, IEnumerable<TData> where TData : LogixData
{
    /// <inheritdoc />
    public ArrayData(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public ArrayData(string dataType, Dimensions dimensions) : base(dataType, dimensions)
    {
    }

    /// <inheritdoc />
    public ArrayData(LogixData dataType, Dimensions dimensions) : base(dataType, dimensions)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData{TLogixType}"/> initialized with the provided one dimensional array.
    /// </summary>
    /// <param name="array">The one dimensional array to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> is empty, contains <c>null</c> or <c>NullType</c> elements,
    /// or is an array of more than one logix type.</exception>
    public ArrayData(TData[] array) : base(array)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData{TLogixType}"/> initialized with the provided two dimensional array.
    /// </summary>
    /// <param name="array">The two dimensional array to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> is empty, contains <c>null</c> or <c>NullType</c> elements,
    /// or is an array of more than one logix type.</exception>
    public ArrayData(TData[,] array) : base(array)
    {
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData{TLogixType}"/> initialized with the provided three dimensional array.
    /// </summary>
    /// <param name="array">The three dimensional array to initialize the array type with.</param>
    /// <exception cref="ArgumentNullException"><c>array</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>array</c> is empty, contains <c>null</c> or <c>NullType</c> elements,
    /// or is an array of more than one logix type.</exception>
    public ArrayData(TData[,,] array) : base(array)
    {
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public new TData this[ushort x]
    {
        get => GetIndex<TData>($"[{x}]");
        set => SetIndex($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public new TData this[ushort x, ushort y]
    {
        get => GetIndex<TData>($"[{x},{y}]");
        set => SetIndex($"[{x},{y}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <param name="z">The z index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public new TData this[ushort x, ushort y, ushort z]
    {
        get => GetIndex<TData>($"[{x},{y},{z}]");
        set => SetIndex($"[{x},{y},{z}]", value);
    }

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayData{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayData{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayData<TData>(TData[] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayData{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayData{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayData<TData>(TData[,] array) => new(array);

    /// <summary>
    /// Implicitly converts the provided array of logix type objects to an <see cref="ArrayData{TLogixType}"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <returns>A new <see cref="ArrayData{TLogixType}"/> containing the elements of the provided array.</returns>
    public static implicit operator ArrayData<TData>(TData[,,] array) => new(array);

    /// <inheritdoc />
    public IEnumerator<TData> GetEnumerator() => Members.Select(m => m.Value.As<TData>()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}