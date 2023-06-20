using System;
using System.Collections;
using System.Collections.Generic;
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
    protected internal AtomicType(string name, Radix radix, byte[] bytes) : base(GenerateElement(name, radix))
    {
        if (!radix.SupportsType(this))
            throw new ArgumentException($"The radix {radix} is not supported for atomic type {typeof(AtomicType)}");

        Radix = radix;
        Value = bytes;
        Element.SetAttributeValue(L5XName.Value, ToString(radix));
    }

    /// <inheritdoc />
    public sealed override DataTypeClass Class => DataTypeClass.Atomic;

    /// <inheritdoc />
    public override IEnumerable<Member> Members
    {
        get
        {
            for (var i = 0; i < ToBitArray().Count; i++)
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
    private byte[] Value { get; }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as an array of <see cref="byte"/>.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the underlying value of the type.</returns>
    public byte[] GetBytes() => Value;

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

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as a <see cref="BitArray"/>.  
    /// </summary>
    /// <returns>A <see cref="BitArray"/> representing the underlying value of the type.</returns>
    public BitArray ToBitArray() => new(Value);

    /*{
        var bytes = new byte[(Value.Length - 1) / 8 + 1];
        Value.CopyTo(bytes, 0);
        return bytes;
    }*/

    private static XElement GenerateElement(string name, Radix radix)
    {
        if (name is null) throw new ArgumentNullException(nameof(name));
        if (radix is null) throw new ArgumentNullException(nameof(radix));

        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(new XAttribute(L5XName.Radix, radix));
        return element;
    }
}