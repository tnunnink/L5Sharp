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
/// Internally the atomic type
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class AtomicType : LogixType
{
    /// <summary>
    /// Creates a new <see cref="AtomicType"/> instance with the provided name.
    /// </summary>
    /// <param name="name">The name of the atomic type.</param>
    /// <param name="radix">The default <see cref="Enums.Radix"/> format of the type.</param>
    /// <param name="bytes">An array of bytes that represent the value of the type.</param>
    /// <exception cref="ArgumentNullException">name is null.</exception>
    protected internal AtomicType(string name, Radix radix, byte[] bytes)
    {
        if (radix is null) throw new ArgumentNullException(nameof(radix));
        if (!radix.SupportsType(this))
            throw new ArgumentException($"The radix {radix} is not supported for atomic type {typeof(AtomicType)}");

        Name = name;
        Radix = radix;
        Value = new BitArray(bytes);
    }

    /// <inheritdoc />
    public override string Name { get; }

    /// <inheritdoc />
    public sealed override DataTypeClass Class => DataTypeClass.Atomic;

    /// <inheritdoc />
    public override IEnumerable<Member> Members
    {
        get
        {
            for (var i = 0; i < Value.Count; i++)
                yield return new Member(i.ToString(), new BOOL(Value[i]));
        }
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Enums.Radix"/> representing the format of the atomic type value.</value>
    public Radix Radix { get; }

    /// <summary>
    /// The underlying value of the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="BitArray"/> representing the value of the type.</value>
    protected BitArray Value { get; }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as an array of <see cref="byte"/>.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the underlying value of the type.</returns>
    public byte[] ToBytes()
    {
        var bytes = new byte[(Value.Length - 1) / 8 + 1];
        Value.CopyTo(bytes, 0);
        return bytes;
    }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as a <see cref="BitArray"/>.
    /// </summary>
    /// <returns>A <see cref="BitArray"/> representing the underlying value of the type.</returns>
    public BitArray ToBits() => new(Value);

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Radix.Format(this);

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Enums.Radix"/>.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => radix.Format(this);


    /// <inheritdoc />
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, Name));
        element.Add(new XAttribute(L5XName.Radix, Radix));
        element.Add(new XAttribute(L5XName.Value, ToString()));
        return element;
    }

    /// <inheritdoc />
    public override void Set(LogixType type)
    {
        if (type is not AtomicType atomicType)
            throw new ArgumentException($"Can not update {GetType().Name} with {type.GetType().Name}");

        //Updating the underlying bit array will work between different types so long as the incoming value is not larger
        //than what can be represented by the length of the this type's bit array. Otherwise, data loss will occur.
        for (var i = 0; i < Value.Length; i++)
            Value[i] = i < atomicType.Value.Length ? atomicType.Value[i] : default;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not AtomicType atomic) return false;

        var max = Math.Max(Value.Length, atomic.Value.Length);

        for (var i = 0; i < max; i++)
        {
            var left = i < Value.Length && Value[i];
            var right = i < atomic.Value.Length && atomic.Value[i];
            if (!left.Equals(right))
                return false;
        }

        return true;
    }

    /// <inheritdoc />
    public override int GetHashCode() => ToBytes().Aggregate(0, (i, b) => i ^ b.GetHashCode());
}