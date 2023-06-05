using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp;

/// <summary>
/// The base class for all logix data type classes.
/// </summary>
/// <remarks>This class exposes the common data type properties, such as <see cref="Name"/>, <see cref="Family"/>,
/// <see cref="Class"/>, and <see cref="Members"/>. This class also provides common implicit conversions between .NET
/// base types and LogixType classes so that the tag values may be set in a concise way.</remarks>
/// <seealso cref="AtomicType"/>.
/// <seealso cref="StructureType"/>
/// <seealso cref="ArrayType"/>
/// <seealso cref="StringType"/>
/// <seealso cref="NullType"/>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class LogixType : LogixEntity<LogixType>
{
    /// <inheritdoc />
    protected LogixType()
    {
    }

    /// <inheritdoc />
    protected LogixType(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the <c>Logix</c> type.
    /// </summary>
    /// <value>A <see cref="string"/> name identifying the logix type.</value>
    public virtual string Name => Element.LogixType() ?? throw new L5XException(L5XName.DataType, Element);

    /// <summary>
    /// The family (string or none) of the type.
    /// </summary>
    /// <value>A <see cref="DataTypeFamily"/> option representing the family value.</value>
    public virtual DataTypeFamily Family => DataTypeFamily.None;

    /// <summary>
    /// The class (atomic, predefine, user defined) that the type belongs to.
    /// </summary>
    /// <value>A <see cref="DataTypeClass"/> option representing the class type.</value>
    public virtual DataTypeClass Class => DataTypeClass.Unknown;

    /// <summary>
    /// The collection of <see cref="Member"/> objects that make up the structure of the type.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects</value>
    public virtual IEnumerable<Member> Members => Enumerable.Empty<Member>();
    
    /// <summary>
    /// Performs a explicit cast of the current <see cref="LogixType"/> to the type of the generic argument.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast to.</typeparam>
    /// <returns>The instance casted as the specified generic type argument.</returns>
    /// <exception cref="InvalidCastException">The current type is not compatible with specified generic argument type.</exception>
    public TLogixType To<TLogixType>() where TLogixType : LogixType => (TLogixType)this;

    /// <summary>
    /// Performs a safe cast of the current <see cref="LogixType"/> to the type of the generic argument.
    /// </summary>
    /// <typeparam name="TLogixType">The logix type to cast to.</typeparam>
    /// <returns>The instance casted as the specified generic type argument.</returns>
    public TLogixType? As<TLogixType>() where TLogixType : LogixType => this as TLogixType;

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
    public static implicit operator LogixType(Array value) => new ArrayType<LogixType>(value);

    /// <summary>
    /// Converts the provided <see cref="Dictionary{TKey,TValue}"/> to a <see cref="LogixType"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="LogixType"/> representing the converted value.</returns>
    public static implicit operator LogixType(Dictionary<string, LogixType> value) => new StructureType(value);
}