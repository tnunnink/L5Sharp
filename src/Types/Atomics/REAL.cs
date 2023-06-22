using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics.Converters;

namespace L5Sharp.Types.Atomics;

/// <summary>
/// Represents a <b>REAL</b> Logix atomic data type, or a type analogous to a <see cref="float"/>.
/// </summary>
[TypeConverter(typeof(RealConverter))]
public sealed class REAL : AtomicType, IEquatable<REAL>, IComparable<REAL>
{
    private float Local => BitConverter.ToSingle(ToBytes());
    
    /// <summary>
    /// Creates a new default <see cref="REAL"/> type.
    /// </summary>
    public REAL() : base(nameof(REAL), Radix.Float, BitConverter.GetBytes(default(float)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="REAL"/> value with the provided radix format.
    /// </summary>
    /// <param name="radix">The <see cref="Enums.Radix"/> number format of the value.</param>
    public REAL(Radix radix) : base(nameof(REAL), radix, BitConverter.GetBytes(default(float)))
    {
    }

    /// <summary>
    /// Creates a new <see cref="REAL"/> with the provided value.
    /// </summary>
    /// <param name="value">The value to initialize the type with.</param>
    /// <param name="radix">The optional radix format of the value.</param>
    public REAL(float value, Radix? radix = null) : base(nameof(REAL), radix ?? Radix.Float,
        BitConverter.GetBytes(value))
    {
    }

    /// <summary>
    /// Represents the largest possible value of <see cref="REAL"/>.
    /// </summary>
    public const float MaxValue = float.MaxValue;

    /// <summary>
    /// Represents the smallest possible value of <see cref="REAL"/>.
    /// </summary>
    public const float MinValue = float.MinValue;

    /// <inheritdoc />
    public override IEnumerable<Member> Members => Enumerable.Empty<Member>();

    /// <summary>
    /// Parses the provided string value to a new <see cref="REAL"/>.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <returns>A <see cref="REAL"/> representing the parsed value.</returns>
    /// <exception cref="FormatException">The <see cref="Radix"/> format can not be inferred from <c>value</c>.</exception>
    public static REAL Parse(string value)
    {
        //todo need to handle NAN
        //NOT really sure how to handle this better yet. Do we just throw an exception?
        if (value == "1.#QNAN") return new REAL();
        
        if (float.TryParse(value, out var result))
            return new REAL(result);

        var radix = Radix.Infer(value);
        var atomic = (REAL)radix.Parse(value);
        return new REAL(atomic, radix);
    }

    /// <inheritdoc />
    public bool Equals(REAL? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Math.Abs(Local - other.Local) < float.Epsilon;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as REAL);

    /// <inheritdoc />
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    // NOT sure how else to handle since it needs to be settable and used for equality.
    // This would only be a problem if you created a hash table of atomic types.
    // NOT sure anyone would need to do that.
    public override int GetHashCode() => Local.GetHashCode();

    /// <summary>
    /// Determines whether the objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are equal, otherwise, false.</returns>
    public static bool operator ==(REAL left, REAL right) => Equals(left, right);

    /// <summary>
    /// Determines whether the objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the objects are not equal, otherwise, false.</returns>
    public static bool operator !=(REAL left, REAL right) => !Equals(left, right);

    /// <inheritdoc />
    public int CompareTo(REAL? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return ReferenceEquals(null, other) ? 1 : Local.CompareTo(other.Local);
    }
    
    #region Conversions

    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="REAL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="REAL"/> value.</returns>
    public static implicit operator REAL(float value) => new(value);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="float"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="float"/> type value.</returns>
    public static implicit operator float(REAL atomic) => atomic.Local;

    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> value.</returns>
    public static implicit operator REAL(string value) => Parse(value);

    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> value.</returns>
    public static implicit operator string(REAL value) => value.ToString();

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="BOOL"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="BOOL"/> type value.</returns>
    public static explicit operator BOOL(REAL atomic) => new(atomic.Local != 0);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="SINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="SINT"/> type value.</returns>
    public static explicit operator SINT(REAL atomic) => new((sbyte)atomic.Local);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="USINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="USINT"/> type value.</returns>
    public static explicit operator USINT(REAL atomic) => new((byte)atomic.Local);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="INT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="INT"/> type value.</returns>
    public static explicit operator INT(REAL atomic) => new((short)atomic.Local);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="UINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UINT"/> type value.</returns>
    public static explicit operator UINT(REAL atomic) => new((ushort)atomic.Local);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="DINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="DINT"/> type value.</returns>
    public static explicit operator DINT(REAL atomic) => new((int)atomic.Local);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="UDINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="UDINT"/> type value.</returns>
    public static explicit operator UDINT(REAL atomic) => new((uint)atomic.Local);

    /// <summary>
    /// Converts the provided <see cref="REAL"/> to a <see cref="LINT"/> value.
    /// </summary>
    /// <param name="atomic">The value to convert.</param>
    /// <returns>A <see cref="LINT"/> type value.</returns>
    public static explicit operator LINT(REAL atomic) => new((long)atomic.Local);

    #endregion
}