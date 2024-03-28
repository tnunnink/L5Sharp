using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// The base class for all logix type classes, which represent the value or data structure of a logix tag component.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="LogixType"/> is a special type of <see cref="LogixElement"/> which models the tag data structure found
/// in L5X files. This class contains a <see cref="Name"/> and <see cref="Members"/> which comprise the hierarchy of
/// data for a given type. This class has built in conversion for implicitly converting .NET primitives and collections
/// (arrays and dictionaries) to the corresponding <see cref="LogixType"/> derived class.
/// </para>
/// <para>
/// The serialization implemented in the library will always attempt to instantiate the concrete type of a
/// given <see cref="LogixType"/>. For example, a TIMER L5X data structure is always deserialized as the concrete
/// <c>TIMER</c> class, so that the user can cast the type and manipulate the structure statically and compile time.
/// This applies for <see cref="AtomicType"/>, <see cref="ArrayType"/>, and all derived instance of <see cref="StructureType"/>.
/// </para>
/// </remarks>
/// <seealso cref="AtomicType"/>
/// <seealso cref="StructureType"/>
/// <seealso cref="ArrayType"/>
/// <seealso cref="StringType"/>
/// <seealso cref="NullType"/>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class LogixType : LogixElement
{
    /// <inheritdoc />
    protected LogixType(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The type name of the logix data.
    /// </summary>
    /// <value>A <see cref="string"/> name identifying the data type of the data object.</value>
    public virtual string Name => Element.DataType();

    /// <summary>
    /// The collection of <see cref="Core.Member"/> objects that make up the structure of the data.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="Core.Member"/> objects.</value>
    /// <remarks>
    /// All logix data, with the exception of a <c>BOOL</c> and <c>REAL/LREAL</c>, have what can be considered
    /// members. Every derived type must implement this property for which it returns a collection of members, forming
    /// the type/member hierarchy of the logix type.
    /// </remarks>
    public virtual IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Casts the <see cref="LogixType"/> to the type of the generic parameter.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast to.</typeparam>
    /// <exception cref="InvalidCastException">The object can not be casted to the specified type.</exception>
    /// <returns>The logix type object casted as the specified generic type parameter.</returns>
    public TLogixType As<TLogixType>() where TLogixType : LogixType => (TLogixType)this;
    
    /// <summary>
    /// Gets a <see cref="Core.Member"/> with the specified name if it exists for the <see cref="LogixType"/>;
    /// Otherwise, returns <c>null</c>.
    /// </summary>
    /// <param name="name">The name of the member to get.</param>
    /// <returns>A <see cref="Core.Member"/> with the specified name if found; Otherwise, <c>null</c>.</returns>
    /// <remarks>This performs a case insensitive comparison for the member name.</remarks>
    public Member? Member(string name) =>
        Members.SingleOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not LogixType type) return false;
        return Name == type.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Name.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => Name;

    /// <summary>
    /// Returns the singleton null <see cref="LogixType"/> object.
    /// </summary>
    public static LogixType Null => NullType.Instance;

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
    /// Converts the provided <see cref="double"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(double value) => new LREAL(value);

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
    public static implicit operator LogixType(LogixType[] value) => new ArrayType<LogixType>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(LogixType[,] value) => new ArrayType<LogixType>(value);

    /// <summary>
    /// Converts the provided <see cref="Array"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(LogixType[,,] value) => new ArrayType<LogixType>(value);

    /// <summary>
    /// Converts the provided <see cref="Dictionary{TKey,TValue}"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(Dictionary<string, LogixType> value) =>
        new ComplexType(nameof(ComplexType), value.Select(m => new Member(m.Key, m.Value)));
}