using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types;

/// <summary>
/// A <see cref="L5Sharp.LogixType"/> that represents value type object.
/// </summary>
/// <remarks>
/// Logix atomic types are types that have value (i.e. BOOL, SINT, INT, DINT, REAL, etc.).
/// These type are synonymous with value types in .NET. This is the common abstract class for all atomic types.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class AtomicType : LogixType
{
    /// <inheritdoc />
    public sealed override DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public sealed override DataTypeClass Class => DataTypeClass.Atomic;

    /// <inheritdoc />
    public override IEnumerable<Member> Members
    {
        get
        {
            var bits = new BitArray(GetBytes());
            for (var i = 0; i < bits.Count; i++)
            {
                var member = new Member(i.ToString(), new BOOL(bits[i]));
                member.DataType.DataChanged += OnMemberDataChanged;
                yield return member;
            }
        }
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> representing the format of the atomic type value.</value>
    public abstract Radix Radix { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not AtomicType atomic) return false;

        var a = GetBytes();
        var b = atomic.GetBytes();

        var max = Math.Max(a.Length, b.Length);

        for (var i = 0; i < max; i++)
        {
            var left = i < a.Length ? a[i] : (byte)0;
            var right = i < b.Length ? b[i] : (byte)0;
            if (left != right) return false;
        }

        return true;
    }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as an array of <see cref="byte"/> values.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the value of the type.</returns>
    public abstract byte[] GetBytes();

    /// <inheritdoc />
    public override int GetHashCode() => GetBytes().Aggregate(0, (i, b) => i ^ b.GetHashCode());

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Radix.Format(this);

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Enums.Radix"/> format.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => radix.Format(this);

    /// <summary>
    /// Serialized the atomic type as the DataValue <see cref="XElement"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> containing the data for the atomic type.</returns>
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, Name));
        element.Add(new XAttribute(L5XName.Radix, Radix));
        element.Add(new XAttribute(L5XName.Value, ToString()));
        return element;
    }

    /// <summary>
    /// Returns a new byte array updated with the provided byte array data.
    /// </summary>
    /// <param name="other">The array of <see cref="byte"/> to update the underlying value with.</param>
    /// <returns>A new array of <see cref="byte"/> containing the updated data.</returns>
    /// <remarks>
    /// This method can be used when setting data between mismatched atomic types (i.e. type conversion).
    /// Obviously data loss or overflow may occur depending on the length and values of the byte arrays.
    /// This will always return a byte array of the same length as the byte array for the current value to
    /// ensure it can be converted via <see cref="BitConverter"/> back into the .NET integral type.
    /// </remarks>
    protected byte[] SetBytes(byte[] other)
    {
        var value = GetBytes();

        for (var i = 0; i < value.Length; i++)
            value[i] = i < other.Length ? other[i] : default;

        return value;
    }

    private void OnMemberDataChanged(object sender, EventArgs e)
    {
        RaiseDataChanged();
    }
}