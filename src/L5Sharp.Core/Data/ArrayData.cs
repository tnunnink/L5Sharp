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
public sealed class ArrayData : LogixData, IEnumerable<LogixData>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    public ArrayData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new array data instance with the specified dimensions and data type name.
    /// </summary>
    /// <param name="dataType">The name of the data type for the array.</param>
    /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array.</param>
    /// <remarks>
    /// This constructor will use the <see cref="LogixType.Create(string)"/> factory to instantiate default
    /// data objects to fill the array at the specified dimensions. If the provided data type name is not a statically
    /// defined type in this or another assembly, then a default <see cref="StructureData"/> object will be used.
    /// </remarks>
    public ArrayData(string dataType, Dimensions dimensions) : base(CreateArray(LogixType.Create(dataType), dimensions))
    {
    }

    /// <summary>
    /// Creates a new array data instance with the specified dimensions and seed data object.
    /// </summary>
    /// <param name="dataType">A <see cref="LogixData"/> object to use as a seed to create the array elements.</param>
    /// <param name="dimensions">The <see cref="Core.Dimensions"/> of the array.</param>
    public ArrayData(LogixData dataType, Dimensions dimensions) : base(CreateArray(dataType, dimensions))
    {
    }

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Element.Elements().Select(e => new Member(e));

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static implicit operator ArrayData(LogixData[] array) => New(array);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static implicit operator ArrayData(LogixData[,] array) => New(array);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static implicit operator ArrayData(LogixData[,,] array) => New(array);

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> with the specified dimensions and element data type.
    /// </summary>
    /// <typeparam name="TData">The type of data for each element in the array, inheriting from <see cref="LogixData"/>.</typeparam>
    /// <param name="dimensions">The dimensions of the array to be created.</param>
    /// <returns>A new <see cref="ArrayData"/> instance with the specified dimensions and element type.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="dimensions"/> parameter is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="dimensions"/> parameter contains invalid values.</exception>
    public static ArrayData New<TData>(Dimensions dimensions) where TData : LogixData, new()
    {
        if (dimensions is null)
            throw new ArgumentNullException(nameof(dimensions));

        return new ArrayData(new TData(), dimensions);
    }

    /// <summary>
    /// Creates a new <see cref="ArrayData"/> instance from the specified array of elements.
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="LogixData"/> elements contained in the array.</typeparam>
    /// <param name="array">The array of <typeparamref name="TData"/> elements to create the <see cref="ArrayData"/> from.</param>
    /// <returns>A new <see cref="ArrayData"/> instance containing the specified elements.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="array"/> is null.</exception>
    public static ArrayData New<TData>(TData[] array) where TData : LogixData
    {
        return new ArrayData(CreateArray(array));
    }

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> from a two-dimensional array of a specified data type.
    /// </summary>
    /// <typeparam name="TData">The type of elements in the array. Must inherit from <see cref="LogixData"/> and have a parameterless constructor.</typeparam>
    /// <param name="array">The two-dimensional array of data to create the <see cref="ArrayData"/> instance from.</param>
    /// <returns>A new <see cref="ArrayData"/> instance containing the provided data.</returns>
    public static ArrayData New<TData>(TData[,] array) where TData : LogixData
    {
        return new ArrayData(CreateArray(array));
    }

    /// <summary>
    /// Creates a new instance of <see cref="ArrayData"/> from the specified multidimensional array of LogixData.
    /// </summary>
    /// <typeparam name="TData">The type of LogixData contained in the array.</typeparam>
    /// <param name="array">A three-dimensional array of LogixData from which to create the ArrayData.</param>
    /// <returns>A new instance of <see cref="ArrayData"/> filled with the data from the specified array.</returns>
    public static ArrayData New<TData>(TData[,,] array) where TData : LogixData
    {
        return new ArrayData(CreateArray(array));
    }

    /// <inheritdoc />
    public IEnumerator<LogixData> GetEnumerator() => Members.Select(m => m.Value).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public override string ToString() => $"{Name}{Dimensions.ToIndex()}";

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixData"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private LogixData GetElement(string index)
    {
        var member = Element.Elements().SingleOrDefault(m => m.MemberName() == index)?.ToMember();

        if (member is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        return member.Value;
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

        var member = Element.Elements().SingleOrDefault(m => m.MemberName() == index)?.ToMember();

        if (member is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        member.Value = value;
    }

    /// <summary>
    /// Creates a new array data element with the provided data type name and dimensions. This method will use the base
    /// static factory to create a seed <see cref="LogixData"/> instance, and use that along with the dimensions to
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
    /// to seed the data type name and radix if available. If this collection contains no items, we will resort
    /// to the type name of the array element type.
    /// </summary>
    private static XElement CreateArray(Array array)
    {
        var dimensions = Dimensions.FromArray(array);

        if (dimensions.Length == 0)
            throw new InvalidOperationException("Can not initialize ArrayData from empty array");

        var collection = array.Cast<LogixData>().ToArray();

        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, collection[0].Name));
        element.Add(new XAttribute(L5XName.Dimensions, dimensions));

        if (collection[0] is AtomicData atomic)
        {
            element.Add(new XAttribute(L5XName.Radix, atomic.Radix));
        }

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