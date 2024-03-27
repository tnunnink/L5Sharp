using System;
using System.Collections;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A <see cref="LogixType"/> that represents value type object.
/// </summary>
/// <remarks>
/// <para>
/// Logix atomic types are types that have value (e.g., BOOL, SINT, INT, DINT, REAL, etc.).
/// These type are synonymous with value types in .NET and in fact wrap the .NET value types internally while adding
/// the common <see cref="LogixType"/> API. Atomic types also add <see cref="Radix"/> to indicate the format of the current
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
public abstract class AtomicType : LogixType, ILogixParsable<AtomicType>
{
    /// <inheritdoc />
    /// <remarks>
    /// We are passing a dummy XElement to the base class because we don't want it to use reflection to
    /// generate the default element.
    /// </remarks>
    protected internal AtomicType() : base(new XElement(L5XName.DefaultData))
    {
        Radix = Radix.Default(this);
    }

    /// <inheritdoc />
    /// <remarks>
    /// We are passing a dummy XElement to the base class because we don't want it to use reflection to
    /// generate the default element.
    /// </remarks>
    protected internal AtomicType(Radix radix) : base(new XElement(L5XName.DefaultData))
    {
        if (radix is null)
            throw new ArgumentNullException(nameof(radix));

        if (!radix.SupportsType(this))
            throw new ArgumentException($"{GetType()} is not supported by {radix.Name} Radix.");

        Radix = radix;
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Core.Radix"/> representing the format of the atomic type value.</value>
    /// <remarks>
    /// This value is ont actually read from the underlying XML element but instead inferred from the value
    /// of the underlying data element. This is because some elements were observed to show the incorrect format
    /// which could cause runtime errors when trying to parse the string value. The Radix will however be written to the
    /// element upon creation of an <see cref="AtomicType"/> and should never be changed. Radix and Value are immutable
    /// properties.
    /// </remarks>
    public Radix Radix { get; }

    /// <summary>
    /// Gets bit member's data type value at the specified bit index. 
    /// </summary>
    /// <param name="bit">The zero based bit index of the value to get.</param>
    /// <returns>A <see cref="BOOL"/> representing the value of the specified bit value (0/1).</returns>
    /// <exception cref="ArgumentOutOfRangeException"><c>bit</c> is out of range of the atomic type bit length.</exception>
    public BOOL this[int bit] => new BitArray(GetBytes())[bit];

    /// <summary>
    /// Parses the provided string value into the atomic type value specified by name.
    /// </summary>
    /// <param name="name">The name of the atomic type.</param>
    /// <param name="value">The string value to parse.</param>
    /// <returns>An <see cref="AtomicType"/> representing the parsed value and format of the provided string.</returns>
    /// <exception cref="ArgumentException"><c>name</c> does not represent a valid atomic type.</exception>
    /// <exception cref="FormatException"><c>value</c> does not have a valid format to be parsed as the specified atomic type.</exception>
    public static AtomicType Parse(string name, string value)
    {
        return name switch
        {
            nameof(BOOL) => BOOL.Parse(value),
            "BIT" => BOOL.Parse(value),
            nameof(SINT) => SINT.Parse(value),
            nameof(INT) => INT.Parse(value),
            nameof(DINT) => DINT.Parse(value),
            nameof(LINT) => LINT.Parse(value),
            nameof(REAL) => REAL.Parse(value),
            nameof(USINT) => USINT.Parse(value),
            nameof(UINT) => UINT.Parse(value),
            nameof(UDINT) => UDINT.Parse(value),
            nameof(ULINT) => ULINT.Parse(value),
            nameof(LREAL) => LREAL.Parse(value),
            _ => throw new ArgumentException($"The type name '{name}' is not a valid {typeof(AtomicType)}")
        };
    }

    /// <summary>
    /// Parses the provided string value into an atomic type value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>An <see cref="AtomicType"/> representing the parsed value and format of the provided string.</returns>
    /// <exception cref="FormatException"><c>value</c> does not have a valid Radix format to be parsed as an
    /// atomic type.</exception>
    public static AtomicType Parse(string value)
    {
        if (value.IsEquivalent("true")) return new BOOL(true);
        if (value.IsEquivalent("false")) return new BOOL(false);

        var radix = Radix.Infer(value);
        var parsed = radix.ParseValue(value);

        return parsed switch
        {
            BOOL typed => new BOOL(typed, radix),
            SINT typed => new SINT(typed, radix),
            INT typed => new INT(typed, radix),
            DINT typed => new DINT(typed, radix),
            LINT typed => new LINT(typed, radix),
            REAL typed => new REAL(typed, radix),
            USINT typed => new USINT(typed, radix),
            UINT typed => new UINT(typed, radix),
            UDINT typed => new UDINT(typed, radix),
            ULINT typed => new ULINT(typed, radix),
            LREAL typed => new LREAL(typed, radix),
            _ => throw new FormatException($"The value '{value}' cannot be parsed as an atomic type.")
        };
    }

    /// <summary>
    /// Tries to parse the provided string value into an atomic type value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>An <see cref="AtomicType"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static AtomicType? TryParse(string? value)
    {
        if (string.IsNullOrEmpty(value)) return default;
        if (value.IsEquivalent("true")) return new BOOL(true);
        if (value.IsEquivalent("false")) return new BOOL(false);
        if (!Radix.TryInfer(value, out var radix)) return default;

        var parsed = radix.ParseValue(value);

        return parsed switch
        {
            BOOL typed => new BOOL(typed, radix),
            SINT typed => new INT(typed, radix),
            INT typed => new INT(typed, radix),
            DINT typed => new DINT(typed, radix),
            LINT typed => new LINT(typed, radix),
            REAL typed => new REAL(typed, radix),
            USINT typed => new USINT(typed, radix),
            UINT typed => new UINT(typed, radix),
            UDINT typed => new UDINT(typed, radix),
            ULINT typed => new ULINT(typed, radix),
            LREAL typed => new LREAL(typed, radix),
            _ => throw new FormatException($"The value '{value}' cannot be parsed as an atomic type.")
        };
    }

    /// <summary>
    /// Returns the value of the <see cref="AtomicType"/> as a byte array.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the binary value of the atomic type.</returns>
    /// <remarks>This is needed for formatting the atomic value in the proper radix format.</remarks>
    public byte[] GetBytes()
    {
        return this switch
        {
            BOOL v => BitConverter.GetBytes((bool)v),
            SINT v => [(byte)(sbyte)v],
            USINT v => [v],
            INT v => BitConverter.GetBytes(v),
            UINT v => BitConverter.GetBytes(v),
            DINT v => BitConverter.GetBytes(v),
            UDINT v => BitConverter.GetBytes(v),
            LINT v => BitConverter.GetBytes(v),
            ULINT v => BitConverter.GetBytes(v),
            REAL v => BitConverter.GetBytes(v),
            LREAL v => BitConverter.GetBytes(v),
            _ => []
        };
    }

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Radix.FormatValue(this);

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Core.Radix"/> format.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => radix.FormatValue(this);

    /// <inheritdoc />
    /// <remarks>
    /// An <see cref="AtomicType"/> is special in that it does not use the base Element for storing it's
    /// information. All atomic types values are set in the derived classes. This method will generate a new
    /// <see cref="XElement"/> when called. This is because atomics need the value types to be performant. We don't
    /// want to have to box the atomic value as an object, but rather let it wrap a simple .NET primitive type.
    /// </remarks>
    public override XElement Serialize()
    {
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, Name));
        element.Add(new XAttribute(L5XName.Radix, Radix));
        element.Add(new XAttribute(L5XName.Value, this));
        return element;
    }
}