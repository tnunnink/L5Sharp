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
public sealed class ArrayData : LogixData, IEnumerable<LogixData>
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

    /// <summary>
    /// Gets the <see cref="LogixData"/> instance at the specified index.
    /// </summary>
    /// <param name="x">The index of the array element</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    public LogixData this[ushort x]
    {
        get => GetElement($"[{x}]");
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
        get => GetElement($"[{x},{y}]");
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
        get => GetElement($"[{x},{y},{z}]");
        set => SetElement($"[{x},{y},{z}]", value);
    }

    /// <inheritdoc />
    public override void Update(LogixData data)
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
            match.Target.Update(match.Source);
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> with the specified data type and dimensions.
    /// </summary>
    /// <param name="dataType">The data type of the array elements.</param>
    /// <param name="dimensions">The dimensions defining the size of the array. Must not be null.</param>
    /// <returns>A new instance of <see cref="ArrayData"/> representing the created array.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="dimensions"/> parameter is null.</exception>
    public static ArrayData New(string dataType, Dimensions dimensions)
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        var seed = LogixType.CreateOrDefault(dataType);

        return new ArrayData(CreateArray(seed, dimensions));
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ArrayData"/> class with the specified data type and dimensions.
    /// </summary>
    /// <param name="dataType">The <see cref="LogixData"/> that specifies the type of the array elements.</param>
    /// <param name="dimensions">The <see cref="Dimensions"/> that defines the structure of the array.</param>
    /// <returns>A new instance of <see cref="ArrayData"/> with the specified data type and dimensions.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="dataType"/> or <paramref name="dimensions"/> is null.</exception>
    public static ArrayData New(LogixData dataType, Dimensions dimensions)
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        if (dataType is null)
            throw new ArgumentNullException(nameof(dataType));

        return new ArrayData(CreateArray(dataType, dimensions));
    }

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> with the specified dimensions and element data type.
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> elements contained in the array.</typeparam>
    /// <param name="dimensions">The dimensions of the array to be created.</param>
    /// <returns>A new <see cref="ArrayData"/> instance with the specified dimensions and element type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="dimensions"/> parameter is null.</exception>
    public static ArrayData New<TData>(Dimensions dimensions) where TData : LogixData, new()
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        return new ArrayData(CreateArray(new TData(), dimensions));
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData"/> instance from the specified array of elements.
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> elements contained in the array.</typeparam>
    /// <param name="array">The array of <typeparamref name="TData"/> elements to create the <see cref="ArrayData"/> from.</param>
    /// <returns>A new <see cref="ArrayData"/> instance containing the specified elements.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="array"/> is null.</exception>
    public static ArrayData New<TData>(TData[] array) where TData : LogixData, new()
    {
        return new ArrayData(CreateArray<TData>(array));
    }

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> from a two-dimensional array of a specified data type.
    /// </summary>
    /// <typeparam name="TData">The type of elements in the array.</typeparam>
    /// <param name="array">The two-dimensional array of data to create the <see cref="ArrayData"/> instance from.</param>
    /// <returns>A new <see cref="ArrayData"/> instance containing the provided data.</returns>
    public static ArrayData New<TData>(TData[,] array) where TData : LogixData, new()
    {
        return new ArrayData(CreateArray<TData>(array));
    }

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> from the specified multidimensional array of LogixData.
    /// </summary>
    /// <typeparam name="TData">The type of elements in the array.</typeparam>
    /// <param name="array">A three-dimensional array of LogixData from which to create the ArrayData.</param>
    /// <returns>A new instance of <see cref="ArrayData"/> filled with the data from the specified array.</returns>
    public static ArrayData New<TData>(TData[,,] array) where TData : LogixData, new()
    {
        return new ArrayData(CreateArray<TData>(array));
    }

    /// <inheritdoc />
    public IEnumerator<LogixData> GetEnumerator() => Members.Select(m => m.Value).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public override string ToString() => $"{Name}{Dimensions.ToIndex()}";

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(BOOL[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(SINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(USINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(INT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(UINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(DINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(UDINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(LINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(ULINT[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(REAL[] value) => New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator ArrayData(LREAL[] value) => New(value);

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixData"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private LogixData GetElement(string index)
    {
        var element = Element.Elements().SingleOrDefault(m => m.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        return element.Deserialize<LogixData>();
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to set the type.</param>
    /// <param name="value">The logix type value to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private void SetElement(string index, LogixData value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var element = Element.Elements().SingleOrDefault(m => m.MemberName() == index);

        if (element is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        element.Deserialize<LogixData>().Update(value);
    }

    /// <summary>
    /// Creates a new array data element with the provided data type name and dimensions. This method will use the base
    /// static factory to create a seed <see cref="LogixData"/> instance, and use that along with the dimensions to
    /// generate a default array element. 
    /// </summary>
    private static XElement CreateArray(LogixData seed, Dimensions dimensions)
    {
        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, seed.Name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));
        if (seed is AtomicData atomic) element.Add(new XAttribute(L5XName.Radix, atomic.Radix));

        var elements = dimensions.Indices().Select(i => CreateArrayElement(i, seed));
        element.Add(elements);

        return element;
    }

    /// <summary>
    /// Creates a new array data structure element with the provided array object. This method will
    /// determine the dimensions from the provided array. It will also use the first item in the array
    /// to seed the data type name and radix if available. If this collection contains no items, we will resort
    /// to the type name of the array element type.
    /// </summary>
    private static XElement CreateArray<TData>(Array array) where TData : LogixData, new()
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
/// Provides extension methods for working with <see cref="ArrayData"/> objects, enabling streamlined
/// creation and manipulation of array-based Logix data structures.
/// </summary>
/// <remarks>
/// The methods within this class facilitate operations such as converting collections into
/// <see cref="ArrayData"/> instances and enhancing array-related functionalities.
/// </remarks>
public static class ArrayDataExtensions
{
    /// <summary>
    /// Converts an enumerable collection of items into an instance of <see cref="ArrayData"/>.
    /// </summary>
    /// <param name="items">The collection of items to be converted into an <see cref="ArrayData"/> object.
    /// Each item must be a type derived from <see cref="LogixData"/>.</param>
    /// <typeparam name="TData">The type of the items in the collection, which must inherit from <see cref="LogixData"/> and have a parameterless constructor.</typeparam>
    /// <returns>An instance of <see cref="ArrayData"/> that encapsulates the provided collection of items.</returns>
    public static ArrayData ToArrayData<TData>(this IEnumerable<TData> items) where TData : LogixData, new()
    {
        return ArrayData.New(items.ToArray());
    }

    /// <summary>
    /// Converts the specified one-dimensional array of items into a new <see cref="ArrayData"/> instance.
    /// </summary>
    /// <typeparam name="TData">The type of elements in the array. Must be a subclass of <see cref="LogixData"/> and have a parameterless constructor.</typeparam>
    /// <param name="items">The one-dimensional array of items to be converted into an <see cref="ArrayData"/> instance.</param>
    /// <returns>A new <see cref="ArrayData"/> instance that represents the provided array of items.</returns>
    public static ArrayData ToArrayData<TData>(this TData[] items) where TData : LogixData, new()
    {
        return ArrayData.New(items);
    }

    /// <summary>
    /// Converts a multidimensional array of <typeparamref name="TData"/> into a new instance of <see cref="ArrayData"/>.
    /// </summary>
    /// <param name="items">The multidimensional array containing elements of type <typeparamref name="TData"/> to convert.</param>
    /// <typeparam name="TData">The data type of elements contained in the array, which must derive from <see cref="LogixData"/>.</typeparam>
    /// <returns>A new <see cref="ArrayData"/> instance populated with the provided array elements.</returns>
    public static ArrayData ToArrayData<TData>(this TData[,] items) where TData : LogixData, new()
    {
        return ArrayData.New(items);
    }

    /// <summary>
    /// Converts a three-dimensional array of items into a new <see cref="ArrayData"/> instance.
    /// </summary>
    /// <param name="items">The three-dimensional array of <typeparamref name="TData"/> to be converted.</param>
    /// <typeparam name="TData">The type of the items in the array, inheriting from <see cref="LogixData"/>.</typeparam>
    /// <returns>A new <see cref="ArrayData"/> instance containing the provided items.</returns>
    public static ArrayData ToArrayData<TData>(this TData[,,] items) where TData : LogixData, new()
    {
        return ArrayData.New(items);
    }
}