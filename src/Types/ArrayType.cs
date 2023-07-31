using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="L5Sharp.LogixType"/> that represents an array of other logix types (either atomic or structure).
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class ArrayType : LogixType, IEnumerable<LogixType>
{
    private readonly List<LogixMember> _members;

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
    public ArrayType(Array array)
    {
        if (array is null) throw new ArgumentNullException(nameof(array));

        var collection = array.Cast<LogixType>().ToList();
        if (collection.Count == 0)
            throw new ArgumentException("Array can not be initialized with no items.", nameof(array));
        if (collection.Any(t => t is null or NullType))
            throw new ArgumentException("Array can not be initialized with null items.", nameof(array));
        if (collection.Select(t => t.Name).Distinct().Count() != 1)
            throw new ArgumentException("Array can not be initialized with different logix type items.", nameof(array));

        Dimensions = Dimensions.FromArray(array);
        
        _members = Dimensions.Indices().Zip(collection, (i, t) =>
        {
            var member = new LogixMember(i, t);
            member.DataChanged += OnMemberDataChanged;
            return member;
        }).ToList();
    }

    /// <summary>
    /// Creates a new <see cref="ArrayType"/> initialized from the provided <see cref="XElement"/> data.
    /// </summary>
    /// <param name="element">The element to parse.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="L5XException"><c>element</c> does not have required attributes or elements.</exception>
    public ArrayType(XElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        Dimensions = element.Get<Dimensions>(L5XName.Dimensions);
        
        _members = element.Elements().Select(e =>
        {
            var member = new LogixMember(e);
            member.DataChanged += OnMemberDataChanged;
            return member;
        }).ToList();
    }

    /// <summary>
    /// The name of the logix type the array contains.
    /// </summary>
    /// <value>A <see cref="string"/> containing the text name of the logix type for which the array contains.</value>
    /// <remarks>This is used to delineate from the <see cref="Name"/> property of the array type which is the type name and
    /// the dimensions index concatenated. The type name is used for serialization.</remarks>
    public string TypeName => this.First().Name;

    /// <inheritdoc />
    /// <remarks>The name of an array type will be the name of it's contained element types with the dimensions index
    /// text appended. This helps differentiate types when querying so we don't return arrays and type</remarks>
    public override string Name => $"{this.First().Name}{Dimensions.ToIndex()}";

    /// <inheritdoc />
    public override DataTypeFamily Family => this.First().Family;

    /// <inheritdoc />
    public override DataTypeClass Class => this.First().Class;

    /// <summary>
    /// The dimensions of the the array, which defined the length and rank of the array's elements.
    /// </summary>
    /// <value>A <see cref="Core.Dimensions"/> value representing the array dimensions.</value>
    public Dimensions Dimensions { get; }

    /// <summary>
    /// Gets the radix format of the the array type elements.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> format if the array is an atomic type array;
    /// otherwise, <see cref="Enums.Radix.Null"/>.</value>
    public Radix Radix => this.First() is AtomicType atomicType ? atomicType.Radix : Radix.Null;

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members => _members.AsEnumerable();

    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.Array);
        element.Add(new XAttribute(L5XName.DataType, TypeName));
        element.Add(new XAttribute(L5XName.Dimensions, Dimensions));
        if (Radix != Radix.Null) element.Add(new XAttribute(L5XName.Radix, Radix));
        element.Add(_members.Select(m =>
        {
            var index = new XElement(L5XName.Element, new XAttribute(L5XName.Index, m.Name));

            switch (m.DataType)
            {
                case AtomicType atomicType:
                    var value = Radix != Radix.Null ? atomicType.ToString(Radix) : atomicType.ToString();
                    index.Add(new XAttribute(L5XName.Value, value));
                    break;
                case StringType stringType:
                    index.Add(stringType.SerializeStructure());
                    break;
                case StructureType structureType:
                    index.Add(structureType.Serialize());
                    break;
            }

            return index;
        }));

        return element;
    }

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
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static implicit operator LogixType[](ArrayType array) => array.ToArray();

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
    public ArrayType<TLogixType> Of<TLogixType>() where TLogixType : LogixType => this.Cast<TLogixType>().ToArray();

    /// <summary>
    /// Handles getting the logix type at the specified index of the current array from the underlying member collection. 
    /// </summary>
    /// <param name="index">The index at which to get the type.</param>
    /// <returns>A <see cref="LogixType"/> at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range of the array.</exception>
    private LogixType GetIndex(string index)
    {
        var member = Members.SingleOrDefault(m => m.Name == index);

        if (member is null)
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        return member.DataType;
    }

    /// <summary>
    /// Handles setting the logix type at the specified index of the underlying member collection. 
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
            throw new ArgumentOutOfRangeException(nameof(index),
                $"The index '{index}' is outside the bound of the array.");

        member.DataType = value;
    }

    /// <summary>
    /// This method needs to be attached to each member of the type to enable the bubbling up of nested member data changed events.
    /// </summary>
    private void OnMemberDataChanged(object sender, EventArgs e) => RaiseDataChanged(sender);
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