using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// The base class for all logix type classes, which represent the value or data structure of a logix tag component.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="LogixData"/> is a special type of <see cref="LogixElement"/> which models the tag data structure found
/// in L5X files. This class contains a <see cref="Name"/> and <see cref="Members"/> which comprise the hierarchy of
/// data for a given type. This class has built-in conversion for implicitly converting .NET primitives and collections
/// (arrays and dictionaries) to the corresponding <see cref="LogixData"/> derived class.
/// </para>
/// <para>
/// The serialization implemented in the library will always attempt to instantiate the concrete type of
/// given <see cref="LogixData"/>. For example, a TIMER L5X data structure is always deserialized as the concrete
/// <c>TIMER</c> class, so that the user can cast the type and manipulate the structure statically and compile time.
/// This applies for <see cref="AtomicData"/>, <see cref="ArrayData"/>, and all derived instance of <see cref="StructureData"/>.
/// </para>
/// <para>
/// If you wish to create in memory complex data structures, use the <see cref="StructureData"/> class which exposes
/// methods for adding, removing, replacing, and inserting <see cref="LogixMember"/> objects for the data structure.
/// </para>
/// </remarks>
/// <seealso cref="AtomicData"/>
/// <seealso cref="StructureData"/>
/// <seealso cref="ArrayData"/>
/// <seealso cref="StringData"/>
/// <seealso cref="NullData"/>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class LogixData : LogixElement
{
    /// <inheritdoc />
    protected LogixData(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The type name of the logix data.
    /// </summary>
    /// <value>A <see cref="string"/> name identifying the data type of the data object.</value>
    public virtual string Name => Element.DataType() ?? throw Element.L5XError(L5XName.DataType);

    /// <summary>
    /// The collection of <see cref="LogixMember"/> objects that make up the structure of the data.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="LogixMember"/> objects.</value>
    /// <remarks>
    /// Complex data structures such as <see cref="StructureData"/> and <see cref="ArrayData"/> will return
    /// members. <see cref="AtomicData"/> will not return the bit members since they are not present in the underlying
    /// XML and having them would exponentially increase the number of members given tags have.
    /// </remarks>
    public virtual IEnumerable<LogixMember> Members => [];

    /// <summary>
    /// The memory size of the logix data element in bytes.
    /// </summary>
    /// <value>An <see cref="int"/> representing the allocated size of the logix data element.</value>
    public virtual int Size => 0;

    /// <summary>
    /// Returns the value of the <see cref="AtomicData"/> as a byte array.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the binary value of the atomic type.</returns>
    /// <remarks>This is needed for formatting the atomic value in the proper radix format.</remarks>
    public virtual byte[] ToBytes()
    {
        var data = new byte[Size];
        var offset = 0;

        foreach (var member in Members)
        {
            var block = member.Value.ToBytes();
            Buffer.BlockCopy(block, 0, data, offset, block.Length);
            offset += block.Length;
        }

        return data;
    }

    /// <summary>
    /// Updates the current instance of the <see cref="LogixData"/> with the data from the specified instance.
    /// </summary>
    /// <param name="data">The <see cref="LogixData"/> instance containing the data to update from.</param>
    /// <remarks>
    /// This method can be used to update data without changing the type structure for complex types. This is the primary
    /// means through which we mutate data values once they are created. Each deriving type is responsible for implementing
    /// this method as necessary.
    /// </remarks>
    public abstract void Update(LogixData data);

    /// <summary>
    /// Updates the current object state using the provided data starting at the specified offset.
    /// </summary>
    /// <param name="data">The byte array containing the data to update the object with.</param>
    /// <param name="offset">The starting index in the byte array from which data will be read.</param>
    /// <returns>The number of bytes processed during the update operation.</returns>
    public virtual int Update(byte[] data, int offset)
    {
        var bitNumber = 0;

        foreach (var member in Members)
        {
            // Logix packs contiguous booleans into byte chunks, which this code will handle by default.
            // Any non-standard formatting/layout will need to be handled in derived classes.
            if (member.Value is BOOL bit)
            {
                if (bitNumber == 0) offset++;
                var bitValue = (data[offset - 1] & (1 << bitNumber)) != 0;
                bit.Update(bitValue);
                bitNumber = (bitNumber + 1) % 8;
                continue;
            }

            //Reset the bit block when we hit a non-BOOL member.
            bitNumber = 0;

            // Recurse and update offset to the next position in the array.
            offset = member.Value.Update(data, offset);
        }

        return offset;
    }

    /// <summary>
    /// Gets a <see cref="LogixMember"/> with the specified name if it exists for the <see cref="LogixData"/>;
    /// Otherwise, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the member to get.</param>
    /// <returns>A <see cref="LogixMember"/> with the specified name if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This performs a case-insensitive comparison for the member name.</remarks>
    public LogixMember? Member(string name)
    {
        return Members.SingleOrDefault(m => m.Name.IsEquivalent(name));
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not LogixData type) return false;
        return Name == type.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Name.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => Name;

    /// <summary>
    /// Determines whether the <see cref="LogixData"/> values are equal.
    /// </summary>
    /// <param name="left">The logix type to compare.</param>
    /// <param name="right">The logix type to compare.</param>
    /// <returns><c>true</c> if the objects are equal, otherwise, <c>false</c>.</returns>
    public static bool operator ==(LogixData left, LogixData right) => Equals(left, right);

    /// <summary>
    /// Determines whether the <see cref="LogixData"/> values are not equal.
    /// </summary>
    /// <param name="left">The logix type to compare.</param>
    /// <param name="right">The logix type to compare.</param>
    /// <returns><c>true</c> if the objects are not equal, otherwise, <c>false</c>.</returns>
    public static bool operator !=(LogixData left, LogixData right) => !Equals(left, right);

    /// <summary>
    /// Compares two objects and determines if 'a' is greater than 'b'.
    /// </summary>
    /// <param name="a">The logix type to compare.</param>
    /// <param name="b">The logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) > 0;
    }

    /// <summary>
    /// Compares two objects and determines if 'a' is less than 'b'.
    /// </summary>
    /// <param name="a">The logix type to compare.</param>
    /// <param name="b">The logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) < 0;
    }

    /// <summary>
    /// Compares two objects and determines if <paramref name="a"/> is greater or equal to than <paramref name="b"/>.
    /// </summary>
    /// <param name="a">The logix type to compare.</param>
    /// <param name="b">The logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >=(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) >= 0;
    }

    /// <summary>
    /// Compares two objects and determines if <paramref name="a"/> is less than or equal to <paramref name="b"/>.
    /// </summary>
    /// <param name="a">The logix type to compare.</param>
    /// <param name="b">The logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <=(LogixData a, LogixData b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) <= 0;
    }

    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(bool value) => new BOOL(value);

    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(sbyte value) => new SINT(value);

    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(short value) => new INT(value);

    /// <summary>
    /// Converts the provided <see cref="int"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(int value) => new DINT(value);

    /// <summary>
    /// Converts the provided <see cref="long"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(long value) => new LINT(value);

    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(float value) => new REAL(value);

    /// <summary>
    /// Converts the provided <see cref="double"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(double value) => new LREAL(value);

    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(byte value) => new USINT(value);

    /// <summary>
    /// Converts the provided <see cref="ushort"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(ushort value) => new UINT(value);

    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(uint value) => new UDINT(value);

    /// <summary>
    /// Converts the provided <see cref="ulong"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(ulong value) => new ULINT(value);

    /// <summary>
    /// Converts the provided <see cref="string"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(string value) => new STRING(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(BOOL[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(SINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(USINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(INT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(UINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(DINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(UDINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(ULINT[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(REAL[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LREAL[] value) => ArrayData.New(value);

    /// <summary>
    /// Converts the provided <see cref="Dictionary{TKey,TValue}"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(Dictionary<string, LogixData> value)
    {
        return new StructureData(nameof(StructureData), value.Select(m => new LogixMember(m.Key, m.Value)));
    }
}