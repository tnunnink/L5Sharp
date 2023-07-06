using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp;

/// <summary>
/// The base class for all logix data type classes. Also contains static members for registration and deserialization
/// of logix types.
/// </summary>
/// <remarks>
/// <para>
/// This class exposes the common data type properties, such as <see cref="Name"/>, <see cref="Family"/>,
/// <see cref="Class"/>, and <see cref="Members"/>. This class also provides common implicit conversions between .NET
/// base types and LogixType classes so that the tag values may be set in a concise way.
/// </para>
/// </remarks>
/// <seealso cref="AtomicType"/>.
/// <seealso cref="StructureType"/>
/// <seealso cref="ArrayType"/>
/// <seealso cref="StringType"/>
/// <seealso cref="NullType"/>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class LogixType : ILogixSerializable
{
    /// <summary>
    /// The name of the logix type.
    /// </summary>
    /// <value>A <see cref="string"/> name identifying the logix type.</value>
    public abstract string Name { get; }

    /// <summary>
    /// The family (string or none) of the type.
    /// </summary>
    /// <value>A <see cref="DataTypeFamily"/> option representing the family value.</value>
    public abstract DataTypeFamily Family { get; }

    /// <summary>
    /// The class (atomic, predefined, user-defined) that the type belongs to.
    /// </summary>
    /// <value>A <see cref="DataTypeClass"/> option representing the class type.</value>
    public abstract DataTypeClass Class { get; }

    /// <summary>
    /// The collection of <see cref="Member"/> objects that make up the structure of the type.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects</value>
    public abstract IEnumerable<Member> Members { get; }
    
    /// <summary>
    /// An event that triggers when the logix type data changes.
    /// </summary>
    /// <remarks>
    /// This event is allowing us to detect when the value of a 
    /// </remarks>
    public event EventHandler? DataChanged;
    
    /// <summary>
    /// Handles raising the <see cref="DataChanged"/> event for the type.
    /// </summary>
    protected void RaiseDataChanged() => DataChanged?.Invoke(this, EventArgs.Empty);
    
    protected void RaiseDataChanged(string member) => DataChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Casts the <see cref="LogixType"/> to the type of the generic argument.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast to.</typeparam>
    /// <exception cref="InvalidCastException">The object can not be casted to the specified type.</exception>
    /// <returns>The instance casted as the specified generic type argument.</returns>
    public TLogixType As<TLogixType>() where TLogixType : LogixType => (TLogixType)this;

    /// <summary>
    /// Returns a new deep cloned instance of the current type.
    /// </summary>
    /// <returns>A new instance of the specified logix type with the same property values.</returns>
    /// <exception cref="InvalidOperationException">The object being cloned does not have a constructor accepting a single <see cref="XElement"/> argument.</exception>
    /// <remarks>This method will simply deserialize a new instance using the current underlying element data.</remarks>
    public LogixType Clone() => (LogixType)LogixSerializer.Deserialize(GetType(), new XElement(Serialize()));

    /// <inheritdoc />
    public override string ToString() => Name;

    /// <inheritdoc />
    public abstract XElement Serialize();

    /// <summary>
    /// Sets the data of the current <see cref="LogixType"/> with the provided logix type data. 
    /// </summary>
    /// <param name="type">The type to set this type with.</param>
    /// <exception cref="ArgumentException">The provided type can not set this type. (e.g., Structure can not set Atomic).</exception>
    /// <returns>A <see cref="LogixType"/> with data updated from the provided logix type.</returns>
    /// <remarks>
    /// This method is the basis for how underlying values are set in memory.
    /// Atomic and String type objects will set underlying values, whereas Structure and Array types will join members and
    /// delegate the set down the hierarchical type structure.
    /// </remarks>
    public abstract LogixType Set(LogixType type);

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not LogixType type) return false;
        return Name == type.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Name.GetHashCode();

    /// <summary>
    /// Gets a <see cref="Types.Member"/> with the specified name if it exists for the <see cref="LogixType"/>;
    /// Otherwise, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the member to get.</param>
    /// <returns>A <see cref="Types.Member"/> with the specified name if found; Otherwise, <c>null</c>.</returns>
     public Member? Member(string name) =>
        Members.SingleOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Determines whether the <see cref="LogixType"/> values are equal.
    /// </summary>
    /// <param name="left">An logix type to compare.</param>
    /// <param name="right">An logix type to compare.</param>
    /// <returns><c>true</c> if the objects are equal, otherwise, <c>false</c>.</returns>
    public static bool operator ==(LogixType left, LogixType right) => Equals(left, right);

    /// <summary>
    /// Determines whether the <see cref="LogixType"/> values are not equal.
    /// </summary>
    /// <param name="left">An logix type to compare.</param>
    /// <param name="right">An logix type to compare.</param>
    /// <returns><c>true</c> if the objects are not equal, otherwise, <c>false</c>.</returns>
    public static bool operator !=(LogixType left, LogixType right) => !Equals(left, right);

    /// <summary>
    /// Compares two objects and determines if a is greater than b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >(LogixType a, LogixType b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) > 0;
    }

    /// <summary>
    /// Compares two objects and determines if a is less than b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <(LogixType a, LogixType b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) < 0;
    }

    /// <summary>
    /// Compares two objects and determines if a is greater or equal to than b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >=(LogixType a, LogixType b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) >= 0;
    }

    /// <summary>
    /// Compares two objects and determines if a is less than or equal to b.
    /// </summary>
    /// <param name="a">An logix type to compare.</param>
    /// <param name="b">An logix type to compare.</param>
    /// <returns><c>true</c> if <c>a</c> is less than or equal to <c>b</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <=(LogixType a, LogixType b)
    {
        if (a is not IComparable comparable)
            throw new ArgumentException($"Type {a.GetType()} does not implement {typeof(IComparable)}.");

        return comparable.CompareTo(b) <= 0;
    }

    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(bool value) => new BOOL(value);

    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(sbyte value) => new SINT(value);

    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(short value) => new INT(value);

    /// <summary>
    /// Converts the provided <see cref="int"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(int value) => new DINT(value);

    /// <summary>
    /// Converts the provided <see cref="long"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(long value) => new LINT(value);

    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(float value) => new REAL(value);

    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(byte value) => new USINT(value);

    /// <summary>
    /// Converts the provided <see cref="ushort"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(ushort value) => new UINT(value);

    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(uint value) => new UDINT(value);

    /// <summary>
    /// Converts the provided <see cref="ulong"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(ulong value) => new ULINT(value);

    /// <summary>
    /// Converts the provided <see cref="string"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(string value) => new STRING(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(LogixType[] value) => new ArrayType(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(LogixType[,] value) => new ArrayType(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(LogixType[,,] value) => new ArrayType(value);

    /// <summary>
    /// Converts the provided <see cref="Dictionary{TKey,TValue}"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(Dictionary<string, LogixType> value) =>
        new ComplexType(nameof(ComplexType), value.Select(m => new Member(m.Key, m.Value)));
}