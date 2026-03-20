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
    /// Gets a <see cref="LogixMember"/> with the specified name if it exists for the <see cref="LogixData"/>;
    /// Otherwise, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the member to get.</param>
    /// <returns>A <see cref="LogixMember"/> with the specified name if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This performs a case-insensitive comparison for the member name.</remarks>
    public LogixMember? GetMember(string name) => Members.SingleOrDefault(m => m.Name.IsEquivalent(name));

    /// <summary>
    /// Updates the current instance of the <see cref="LogixData"/> with the data from the specified instance.
    /// </summary>
    /// <param name="data">The <see cref="LogixData"/> instance containing the data to update from.</param>
    /// <remarks>
    /// This method can be used to update data without changing the type structure for complex types. This is the primary
    /// means through which we mutate data values once they are created. Each deriving type is responsible for implementing
    /// this method as necessary.
    /// </remarks>
    public abstract void UpdateData(LogixData data);

    /// <summary>
    /// Updates the current data structure or value using the provided byte array and offset.
    /// </summary>
    /// <param name="data">The byte array containing the data to update the object with.</param>
    /// <param name="offset">The starting index in the byte array from which data will be read.</param>
    /// <returns>The number of bytes processed during the update operation.</returns>
    /// <remarks>
    /// The default implementation will naively delegate to all members recursively.
    /// Eventually each atomic member will update the underlying value at the current byte segment.
    /// Note that Logix packs booleans for user-defined types, so if you use this as a means to read/write
    /// data to/from real PLC, it might fail or incorrectly update tag buffers. Deriving classes can
    /// override the implementation to correctly map the byte array, or you can use the source generator project
    /// which will automatically override this method based on the type definition.
    /// </remarks>
    public virtual int UpdateData(byte[] data, int offset)
    {
        foreach (var member in Members)
        {
            offset = member.Value.UpdateData(data, offset);
        }

        return offset;
    }

    /// <summary>
    /// Resets all member data within the current <see cref="LogixData"/> structure to their default values.
    /// </summary>
    /// <remarks>
    /// This method recursively resets all members to their initial state (typically zero for numeric types,
    /// false for booleans, empty for strings, etc.). This is useful for clearing data structures before
    /// reusing them or returning them to a known initial state.
    /// </remarks>
    public virtual void ClearData()
    {
        foreach (var member in Members)
        {
            member.Value.ClearData();
        }
    }

    /// <summary>
    /// Determines whether the current LogixData instance represents an atomic data type.
    /// </summary>
    /// <returns>True if the data type is atomic; otherwise, false.</returns>
    public bool IsAtomic()
    {
        return LogixType.IsAtomic(Name);
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

    /// <summary>
    /// The size, in bytes, of the LogixData representation.
    /// </summary>
    /// <returns>An <see cref="int"/> value representing the total memory size of the Logix data object.</returns>
    public virtual int GetSize() => Members.Sum(m => m.Value.GetSize());

    /// <summary>
    /// Serializes the current <see cref="LogixData"/> instance into a byte array representation.
    /// </summary>
    /// <returns>
    /// A byte array containing the serialized data from all members of this <see cref="LogixData"/> instance.
    /// </returns>
    /// <remarks>
    /// The default implementation will naively delegate to all nested members recursively to aggregate the bytes for the data type.
    /// Eventually each atomic type will return the realized byte data. In most use cases this will work.
    /// However, Logix will pack contiguous bits (boolean members) into bytes for user-defined types, so if you use this
    /// method as a means to read/write serialized data to/from real PLC, it might fail or incorrectly update tag buffers.
    /// My primary use case for this functionality is the internal services and not PLC communication. 
    /// </remarks>
    public virtual byte[] ToBytes() => Members.SelectMany(m => m.Value.ToBytes()).ToArray();

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
    public static implicit operator LogixData(BOOL[] value) => new ArrayData<BOOL>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(SINT[] value) => new ArrayData<SINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(USINT[] value) => new ArrayData<USINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(INT[] value) => new ArrayData<INT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(UINT[] value) => new ArrayData<UINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(DINT[] value) => new ArrayData<DINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(UDINT[] value) => new ArrayData<UDINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LINT[] value) => new ArrayData<LINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(ULINT[] value) => new ArrayData<ULINT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(REAL[] value) => new ArrayData<REAL>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LREAL[] value) => new ArrayData<LREAL>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(DT[] value) => new ArrayData<DT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LDT[] value) => new ArrayData<LDT>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(TIME[] value) => new ArrayData<TIME>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(TIME32[] value) => new ArrayData<TIME32>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(LTIME[] value) => new ArrayData<LTIME>(value);

    /// <summary>
    /// Converts the provided <see cref="Dictionary{TKey,TValue}"/> to a <see cref="LogixData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixData"/> representing the converted value.</returns>
    public static implicit operator LogixData(Dictionary<string, LogixData> value) =>
        new StructureData(nameof(StructureData), value.Select(m => new LogixMember(m.Key, m.Value)));
}