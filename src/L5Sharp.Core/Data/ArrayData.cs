using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents an array of Logix data elements with defined dimensions, allowing access
/// to individual elements via indexed properties. Provides functionality to define the data
/// type and dimensional structure of a ControlLogix array.
/// </summary>
/// <remarks>
/// Ensure the array dimensions and data type are valid for the system's requirements.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixElement(L5XName.Array)]
[LogixElement(L5XName.ArrayMember)]
public class ArrayData : LogixData
{
    /// <inheritdoc />
    public ArrayData(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => Element.Elements().Select(e => new LogixMember(e));

    /// <summary>
    /// The dimensions of the array, which define the length and rank of the array's elements.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    public Dimensions Dimensions => GetRequiredValue(Dimensions.Parse);

    /// <summary>
    /// Gets the radix format of the array type elements.
    /// </summary>
    /// <value>A <see cref="Core.Radix"/> format if the array is an atomic type array;
    /// otherwise, the radix <see cref="L5Sharp.Core.Radix.Null"/> format.</value>
    public Radix Radix => GetValue(Radix.Parse) ?? Radix.Null;

    /// <inheritdoc />
    public override int GetSize() => Members.Sum(m => m.Value.GetSize());

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixData this[ushort x]
    {
        get => GetElement<LogixData>($"[{x}]");
        set => SetElement($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixData this[ushort x, ushort y]
    {
        get => GetElement<LogixData>($"[{x},{y}]");
        set => SetElement($"[{x},{y}]", value);
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
        get => GetElement<LogixData>($"[{x},{y},{z}]");
        set => SetElement($"[{x},{y},{z}]", value);
    }

    /// <inheritdoc />
    public override void UpdateData(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not ArrayData array)
            throw new ArgumentException($"Can not update array with data of type '{data.GetType()}'");

        var matches = Members.Join(array.Members,
            m => m.Name,
            m => m.Name,
            (x, y) => new { Target = x.Value, Source = y.Value },
            StringComparer.OrdinalIgnoreCase
        );

        foreach (var match in matches)
        {
            match.Target.UpdateData(match.Source);
        }
    }

    /// <inheritdoc />
    public override int UpdateData(byte[] data, int offset)
    {
        foreach (var member in Members)
        {
            offset = member.Value.UpdateData(data, offset);
        }

        return offset;
    }

    /// <inheritdoc />
    public override string ToString() => $"{Name}{Dimensions.ToIndex()}";

    /// <summary>
    /// Creates a new instance of the <see cref="ArrayData"/> class with the specified seed data type and dimensions.
    /// </summary>
    /// <param name="dataType">The <see cref="LogixData"/> that specifies the type of the array elements.</param>
    /// <param name="dimensions">The <see cref="Dimensions"/> that defines the structure of the array.</param>
    /// <returns>A new instance of <see cref="ArrayData"/> with the specified data type and dimensions.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="dataType"/> or <paramref name="dimensions"/> is null.</exception>
    public static ArrayData New(LogixData dataType, Dimensions dimensions)
    {
        if (dataType is null) throw new ArgumentNullException(nameof(dataType));
        if (dimensions is null) throw new ArgumentNullException(nameof(dimensions));

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, dataType.Name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));

        if (dataType is AtomicData atomic)
            element.Add(new XAttribute(L5XName.Radix, atomic.Radix));

        var elements = dimensions.Indices().Select(i => CreateArrayElement(i, (LogixData)dataType.Clone()));
        element.Add(elements);

        return new ArrayData(element);
    }

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixData"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    protected TData GetElement<TData>(string index) where TData : LogixData
    {
        var element = Element.Elements().SingleOrDefault(m => m.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        return element.Deserialize<TData>();
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to set the type.</param>
    /// <param name="value">The logix type value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    protected void SetElement<TData>(string index, TData value) where TData : LogixData
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var element = Element.Elements().SingleOrDefault(m => m.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        element.Deserialize<TData>().UpdateData(value);
    }

    /// <summary>
    /// Creates an array index element with the provided index string and logix type data object.
    /// The format of the element will depend on the logix type provided, atomic, string, or structure.
    /// </summary>
    protected static XElement CreateArrayElement(string index, LogixData data)
    {
        var element = new XElement(L5XName.Element, new XAttribute(L5XName.Index, index));

        switch (data)
        {
            case AtomicData atomicType:
                element.Add(new XAttribute(L5XName.Value, atomicType));
                break;
            case StringData stringType:
                element.Add(stringType.ToStructureElement());
                break;
            case StructureData structureType:
                element.Add(structureType.Serialize());
                break;
        }

        return element;
    }
}

/// <summary>
/// Represents a Logix array of data elements, supporting multidimensional indexing and implicit
/// conversions from standard array types. Provides functionality to define and handle Logix data
/// structures with specific dimensions and types.
/// </summary>
/// <remarks>
/// This class allows for flexible initialization of array data, including one-dimensional,
/// two-dimensional, and three-dimensional structures. Indexed properties provide access to elements
/// of the array, and enumerable functionality enables iteration over data elements.
/// </remarks>
public sealed class ArrayData<TData> : ArrayData, IEnumerable<TData> where TData : LogixData, new()
{
    /// <inheritdoc />
    public ArrayData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayData{TData}"/> class with the specified dimensions.
    /// </summary>
    /// <param name="dimensions">The dimensions that define the structure of the array.</param>
    /// <remarks>
    /// Creates an array of <typeparamref name="TData"/> elements with the specified dimensions,
    /// where each element is initialized to a new instance of <typeparamref name="TData"/>.
    /// </remarks>
    public ArrayData(Dimensions dimensions) : base(CreateArray(dimensions))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayData{TData}"/> class with a one-dimensional array.
    /// </summary>
    /// <param name="array">A one-dimensional array of <typeparamref name="TData"/> elements to initialize the array data.</param>
    /// <remarks>
    /// Creates an <see cref="ArrayData{TData}"/> from the provided one-dimensional array,
    /// preserving the array's length and element values.
    /// </remarks>
    public ArrayData(TData[] array) : base(CreateArray(array))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayData{TData}"/> class with a two-dimensional array.
    /// </summary>
    /// <param name="array">A two-dimensional array of <typeparamref name="TData"/> elements to initialize the array data.</param>
    /// <remarks>
    /// Creates an <see cref="ArrayData{TData}"/> from the provided two-dimensional array,
    /// preserving the array's dimensions and element values.
    /// </remarks>
    public ArrayData(TData[,] array) : base(CreateArray(array))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayData{TData}"/> class with a three-dimensional array.
    /// </summary>
    /// <param name="array">A three-dimensional array of <typeparamref name="TData"/> elements to initialize the array data.</param>
    /// <remarks>
    /// Creates an <see cref="ArrayData{TData}"/> from the provided three-dimensional array,
    /// preserving the array's dimensions and element values.
    /// </remarks>
    public ArrayData(TData[,,] array) : base(CreateArray(array))
    {
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public new TData this[ushort x]
    {
        get => GetElement<TData>($"[{x}]");
        set => SetElement($"[{x}]", value);
    }

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The x index of the array element</param>
    /// <param name="y">The y index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public new TData this[ushort x, ushort y]
    {
        get => GetElement<TData>($"[{x},{y}]");
        set => SetElement($"[{x},{y}]", value);
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
        get => GetElement<TData>($"[{x},{y},{z}]");
        set => SetElement($"[{x},{y},{z}]", value);
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

    /// <summary>
    /// Creates an XML element representing an array structure with the specified dimensions
    /// and type info specified by the generic type parameter.
    /// </summary>
    private static XElement CreateArray(Dimensions dimensions)
    {
        var name = LogixType.NameFor(typeof(TData));
        var radix = Radix.Default(typeof(TData));

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));

        if (radix != Radix.Null)
            element.Add(new XAttribute(L5XName.Radix, radix));

        var elements = dimensions.Indices().Select(i => CreateArrayElement(i, new TData()));
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates an XML representation of the specified array, including its data type, dimensions,
    /// and optionally its radix, and populates it with elements derived from the array data.
    /// </summary>
    private static XElement CreateArray(Array array)
    {
        var name = LogixType.NameFor(typeof(TData));
        var dimensions = Dimensions.FromArray(array);
        var radix = Radix.Default(typeof(TData));

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));

        if (radix != Radix.Null)
            element.Add(new XAttribute(L5XName.Radix, radix));

        var elements = dimensions.Indices().Zip(array.Cast<TData>(), (i, d) => CreateArrayElement(i, d ?? new TData()));
        element.Add(elements);

        return element;
    }
}