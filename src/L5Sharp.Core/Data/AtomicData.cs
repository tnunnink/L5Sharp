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
public interface IAtomicValue<TValue> where TValue : struct
{
    /// <summary>
    /// Gets or sets the strongly typed value encapsulated by the atomic type.
    /// </summary>
    /// <value>The underlying value of type <typeparamref name="TValue"/>.</value>
    public TValue Value { get; set; }
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
    /// This will retrieve the value from the underlying element or parent element. If not found, the value will
    /// attempt to infer from the underlying value 
    /// </remarks>
    public Radix Radix => GetRadix();

    /// <inheritdoc />
    public override void UpdateData(LogixData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        if (data is not AtomicData atomic)
            throw new ArgumentException($"Can not update atomic with data of type '{data.GetType()}'.");

        var converted = (AtomicData)Convert.ChangeType(atomic, GetType());
        var formatted = converted.ToString(Radix);
        Element.SetAttributeValue(L5XName.Value, formatted);

        //Check if the underlying element contains an attribute annotation which can be injected as a "backing field" for the value.
        //This is to support updating underlying XAttribute of custom data formats (ALARM_ANALOG and ALARM_DIGITAL)
        Element.Annotation<XAttribute>()?.SetValue(formatted);
    }

    /// <inheritdoc />
    public override void ResetData()
    {
        // Reset the value for atomic data to zero.
        var value = Radix.Format(0);
        Element.SetAttributeValue(L5XName.Value, value);

        //Check if the underlying element contains an attribute annotation which can be injected as a "backing field" for the value.
        //This is to support updating underlying XAttribute of custom data formats (ALARM_ANALOG and ALARM_DIGITAL)
        Element.Annotation<XAttribute>()?.SetValue(value);
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
        var radix = Radix.Infer(value);

        if (radix == Radix.Float || radix == Radix.Exponential)
            return new LREAL(radix.Parse<double>(value));
        if (radix == Radix.DateTime)
            return new DT(radix.Parse<long>(value));
        if (radix == Radix.DateTimeNs)
            return new LDT(radix.Parse<long>(value));
        if (radix == Radix.Time)
            return new TIME(radix.Parse<long>(value));
        if (radix == Radix.Time32)
            return new TIME32(radix.Parse<int>(value));
        if (radix == Radix.TimeNs)
            return new LTIME(radix.Parse<long>(value));

        return new DINT(radix.Parse<int>(value));
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

        // Sometimes Logix, for whatever reason, serializes the value in a format different from the one specified,
        // so we have to check the format first. If it is valid, we can take the happy/faster path.
        if (Radix.IsValid(value))
        {
            return Radix.Parse<TValue>(value);
        }

        var radix = Radix.Infer(value);
        return radix.Parse<TValue>(value);
    }

    /// <summary>
    /// Sets the atomic value of the current data element, formatting the value according to the current radix.
    /// </summary>
    /// <typeparam name="TValue">The type of the value being set. It must be a value type.</typeparam>
    /// <param name="value">The value to set, which will be formatted and applied to the current element.</param>
    protected void SetAtomicValue<TValue>(TValue value) where TValue : struct
    {
        // Format to the current radix of the value type.
        var formatted = Radix.Format(value);

        // Update the underlying data element.
        Element.SetAttributeValue(L5XName.Value, formatted);

        // Check if the underlying element contains an attribute annotation which can be injected as a "backing field" for the value.
        // This is to support updating underlying XAttribute of custom data formats (ALARM_ANALOG and ALARM_DIGITAL)
        Element.Annotation<XAttribute>()?.SetValue(formatted);
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

    /// <summary>
    /// Attempt to get the radix from the underlying element. For normal data value elements this is a local attribute.
    /// For array elements, the attribute is on the parent array element. If non exists, we could attempt to infer the
    /// radix from the value, and if all else fails, just return the default radix for the atomic type.
    /// </summary>
    private Radix GetRadix()
    {
        if (Element.TryGetAttribute(L5XName.Radix, out var local))
            return Radix.Parse(local);

        if (Element.Parent?.TryGetAttribute(L5XName.Radix, out var parent) is true)
            return Radix.Parse(parent);

        return Element.TryGetAttribute(L5XName.Value, out var value)
            ? Radix.Infer(value)
            : Radix.Default(GetType());
    }
}

/// <summary>
/// Provides a set of extension methods for working with <see cref="AtomicData"/> instances.
/// </summary>
/// <remarks>
/// This static class defines utility methods that enhance the functionality of <see cref="AtomicData"/> objects.
/// These methods are designed to simplify common tasks such as value extraction while ensuring type safety and
/// error handling for unsupported data types.
/// </remarks>
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
            IAtomicValue<bool> x => x.Value,
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