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
    /// Radix may not always represent the actual value format, so use this property with caution. When we parse the atomic value,
    /// we are inferring the radix from the actual string value. This Radix property is read from the underlying XElement
    /// for the data object. 
    /// </remarks>
    public Radix Radix => GetValue(Radix.Parse) ?? Radix.Default(GetType());

    /// <inheritdoc />
    public override void Update(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not AtomicData atomic)
            throw new ArgumentException($"Can not update atomic with data of type '{data.GetType()}'.");

        var converted = (AtomicData)Convert.ChangeType(atomic, GetType());
        var formatted = converted.ToString(Radix);
        Element.SetAttributeValue(L5XName.Value, formatted);
    }

    /// <summary>
    /// Gets bit member's data type value at the specified bit index. 
    /// </summary>
    /// <param name="bit">The zero-based bit index of the value to get.</param>
    /// <returns>A <see cref="BOOL"/> representing the value of the specified bit value (0/1).</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public bool this[int bit] => new BitArray(ToBytes())[bit];

    /// <summary>
    /// Returns a string representation of the current <see cref="AtomicData"/> object in the specified
    /// <paramref name="radix"/> format.
    /// </summary>
    /// <param name="radix">The <see cref="Radix"/> format in which the data should be represented.</param>
    /// <returns>A string representation of the <see cref="AtomicData"/> object in the specified radix format.</returns>
    public abstract string ToString(Radix radix);

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
    /// Parses the specified string value and returns an instance of <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="value">The string representation of the atomic data to parse.</param>
    /// <returns>An instance of <see cref="AtomicData"/> corresponding to the parsed value.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided value is null or empty.</exception>
    public static AtomicData Parse(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Can not parse null or empty value");

        //Radix can't handle true/false values, but we could parse those as BOOL. 
        if (bool.TryParse(value, out var bit))
            return new BOOL(bit);

        //Floating point values can have NAN value which we want to intercept since no Radix will match that value.
        if (value.Contains("QNAN"))
            return new LREAL(double.NaN);

        //All other formats should be detectable.
        var radix = Radix.Infer(value);

        if (radix == Radix.Float || radix == Radix.Exponential)
            return new LREAL(radix.Parse<double>(value));

        return new LINT(radix.Parse<long>(value));
    }

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

    /// <summary>
    /// Creates a new XML data element with the specified name, radix, and value.
    /// </summary>
    /// <param name="name">The name of the data type to associate with the element.</param>
    /// <param name="radix">The radix of the data, representing its numerical base or format.</param>
    /// <param name="value">The value to assign to the created data element.</param>
    /// <returns>A new instance of <see cref="XElement"/> representing the created data element.</returns>
    protected static XElement CreateDataElement(string name, Radix radix, string value)
    {
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(new XAttribute(L5XName.Radix, radix));
        element.Add(new XAttribute(L5XName.Value, value));
        return element;
    }
}

/// <summary>
/// 
/// </summary>
public static class AtomicDataExtensions
{
    /// <summary>
    /// Retrieves the underlying value of the provided <see cref="AtomicData"/> instance.
    /// </summary>
    /// <param name="data">The <see cref="AtomicData"/> instance from which to retrieve the value.</param>
    /// <returns>The underlying value of the specified <see cref="AtomicData"/>, or throws an exception if the data type is not supported.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the provided <see cref="AtomicData"/> is of an unsupported type.</exception>
    public static object GetValue(this AtomicData data)
    {
        return data switch
        {
            IAtomicValue<sbyte> x => x.Value,
            IAtomicValue<byte> x => x.Value,
            IAtomicValue<short> x => x.Value,
            IAtomicValue<ushort> x => x.Value,
            IAtomicValue<int> x => x.Value,
            IAtomicValue<uint> x => x.Value,
            IAtomicValue<long> x => x.Value,
            IAtomicValue<ulong> x => x.Value,
            IAtomicValue<float> x => x.Value,
            IAtomicValue<double> x => x.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(data), data, null)
        };
    }
}