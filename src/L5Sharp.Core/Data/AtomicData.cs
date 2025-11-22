using System;
using System.Collections;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a contract for atomic value types that encapsulate a strongly typed value.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value represented by the atomic type. This must be a value type (struct).
/// </typeparam>
/// <remarks>
/// Implementations of this interface are used to encapsulate single, immutable value types with predefined semantics.
/// These types generally support operations like comparisons, format transformations, and value extraction.
/// </remarks>
public interface IAtomicValue<out TValue> where TValue : struct
{
    /// <summary>
    /// Retrieves the value associated with the current instance.
    /// </summary>
    /// <remarks>
    /// This method is used to get the internal value stored within the instance. The returned value
    /// will depend on the specific implementation of the method in the derived or base class.
    /// </remarks>
    /// <returns>
    /// The value associated with the current instance.
    /// </returns>
    public TValue Value { get; }
}

/// <summary>
/// A <see cref="LogixData"/> that represents a value type object.
/// </summary>
/// <remarks>
/// <para>
/// Logix atomic types are types that have value (e.g., BOOL, SINT, INT, DINT, REAL, etc.).
/// These types are synonymous with value types in .NET and in fact wrap the .NET value types internally while adding
/// the common <see cref="LogixData"/> API. Atomic types also add <see cref="Radix"/> to indicate the format of the current
/// type value.
/// </para>
/// <para>
/// All derived atomic types will implement value equality and comparison semantics to allow common operations to be performed.
/// They will also implement the <see cref="IConvertible"/> interface explicitly so to allow conversion between types. 
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public abstract class AtomicData : LogixData
{
    /// <inheritdoc />
    protected internal AtomicData(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="AtomicData"/> instance with the provided name and default value and radix for the type.
    /// </summary>
    /// <param name="name">The name of the atomic data type.</param>
    /// <param name="value">The default value for the atomic data type.</param>
    protected internal AtomicData(string name, string value = "0") : this(new XElement(L5XName.DataValue))
    {
        Element.SetAttributeValue(L5XName.DataType, name);
        Element.SetAttributeValue(L5XName.Radix, Radix.Default(GetType()));
        Element.SetAttributeValue(L5XName.Value, value);
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicData"/>.
    /// </summary>
    /// <value>A <see cref="Core.Radix"/> representing the format of the atomic type value.</value>
    /// <remarks>
    /// This value is not read from the underlying XML element but instead inferred from the value
    /// of the underlying data element. This is because some elements were observed to show the incorrect format
    /// which could cause runtime errors when trying to parse the string value. The Radix will however be written to the
    /// element upon creation of an <see cref="AtomicData"/> and should never be changed. Radix and Value are immutable
    /// properties.
    /// </remarks>
    public Radix Radix => GetValue(Radix.Parse) ?? Radix.Default(GetType());

    /// <summary>
    /// Gets bit member's data type value at the specified bit index. 
    /// </summary>
    /// <param name="bit">The zero-based bit index of the value to get.</param>
    /// <returns>A <see cref="BOOL"/> representing the value of the specified bit value (0/1).</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public bool this[int bit] => new BitArray(ToBytes())[bit];

    /// <summary>
    /// Returns the value of the <see cref="AtomicData"/> as a byte array.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the binary value of the atomic type.</returns>
    /// <remarks>This is needed for formatting the atomic value in the proper radix format.</remarks>
    public abstract byte[] ToBytes();

    /// <summary>
    /// Gets the array of bit representing the value of the atomic type.
    /// </summary>
    /// <returns>A <see cref="BitArray"/> containing the array bit values</returns>
    // ReSharper disable once ReturnTypeCanBeEnumerable.Global no I want an array
    public BitArray ToBitArray() => new(ToBytes());

    /// <summary>
    /// Converts the provided <see cref="bool"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(bool value) => new BOOL(value);

    /// <summary>
    /// Converts the provided <see cref="sbyte"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(sbyte value) => new SINT(value);

    /// <summary>
    /// Converts the provided <see cref="short"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(short value) => new INT(value);

    /// <summary>
    /// Converts the provided <see cref="int"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(int value) => new DINT(value);

    /// <summary>
    /// Converts the provided <see cref="long"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(long value) => new LINT(value);

    /// <summary>
    /// Converts the provided <see cref="float"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(float value) => new REAL(value);

    /// <summary>
    /// Converts the provided <see cref="double"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(double value) => new LREAL(value);

    /// <summary>
    /// Converts the provided <see cref="byte"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(byte value) => new USINT(value);

    /// <summary>
    /// Converts the provided <see cref="ushort"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(ushort value) => new UINT(value);

    /// <summary>
    /// Converts the provided <see cref="uint"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(uint value) => new UDINT(value);

    /// <summary>
    /// Converts the provided <see cref="ulong"/> to a <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A <see cref="AtomicData"/> representing the converted value.</returns>
    public static implicit operator AtomicData(ulong value) => new ULINT(value);

    /// <summary>
    /// Retrieves the atomic value of the current instance of <see cref="AtomicData"/>.
    /// </summary>
    /// <typeparam name="TValue">The value type to parse and return. Must be a value type.</typeparam>
    /// <returns>The parsed value of type <typeparamref name="TValue"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the atomic value cannot be retrieved or parsed.</exception>
    protected TValue GetAtomicValue<TValue>() where TValue : struct
    {
        var value = Element.Attribute(L5XName.Value)?.Value ?? throw Element.L5XError(L5XName.Value);
        var radix = Radix.Infer(value);
        return radix.Parse<TValue>(value);
    }
}