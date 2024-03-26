using System;
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
    protected internal AtomicType(XElement element) : base(element)
    {
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
    public Radix Radix => Radix.Infer(Value);
    
    /// <summary>
    /// The actual value of the <see cref="AtomicType"/> as the formatted string representation.
    /// </summary>
    /// <remarks>
    /// This value is parsed in derived atomic types and used to give the class value semantics. The intention
    /// is that the atomic types should be immutable. This way the radix and value attributes don't become invalid,
    /// and also we can't have mutable data for the properties used in equality checks.
    /// </remarks>
    protected string Value => GetRequiredValue<string>();

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
            bool typed => new BOOL(typed, radix),
            sbyte typed => new INT(typed, radix),
            short typed => new INT(typed, radix),
            int typed => new DINT(typed, radix),
            long typed => new LINT(typed, radix),
            float typed => new REAL(typed, radix),
            byte typed => new USINT(typed, radix),
            ushort typed => new UINT(typed, radix),
            uint typed => new UDINT(typed, radix),
            ulong typed => new ULINT(typed, radix),
            double typed => new LREAL(typed, radix),
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
            bool typed => new BOOL(typed, radix),
            sbyte typed => new INT(typed, radix),
            short typed => new INT(typed, radix),
            int typed => new DINT(typed, radix),
            long typed => new LINT(typed, radix),
            float typed => new REAL(typed, radix),
            byte typed => new USINT(typed, radix),
            ushort typed => new UINT(typed, radix),
            uint typed => new UDINT(typed, radix),
            ulong typed => new ULINT(typed, radix),
            double typed => new LREAL(typed, radix),
            _ => throw new FormatException($"The value '{value}' cannot be parsed as an atomic type.")
        };
    }

    /// <summary>
    /// Return the atomic value formatted using the current <see cref="Radix"/> format.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Returns the atomic value formatted in the specified <see cref="Core.Radix"/> format.
    /// </summary>
    /// <param name="radix">The radix format.</param>
    /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
    public string ToString(Radix radix) => Format(radix);

    /// <summary>
    /// Creates a new <see cref="XElement"/> containing the data for the atomic type object using the provided name,
    /// radix, and formatted value.
    /// </summary>
    protected static XElement CreateElement(string name, Radix radix, ValueType value)
    {
        if (radix is null)
            throw new ArgumentNullException(nameof(radix), "Can not create atomic with null radix.");

        //Convert the value type to the correct format.
        var atomic = radix.FormatValue(value);
            
        var element = new XElement(L5XName.DataValue);
        element.Add(new XAttribute(L5XName.DataType, name));
        element.Add(new XAttribute(L5XName.Radix, radix));
        element.Add(new XAttribute(L5XName.Value, atomic));
        return element;
    }
    
    /// <summary>
    /// Converts this atomic type to the value type and uses the provided radix to return the formatted string value. 
    /// </summary>
    private string Format(Radix radix)
    {
        return this switch
        {
            BOOL atomic => radix.FormatValue((bool)atomic),
            SINT atomic => radix.FormatValue((sbyte)atomic),
            INT atomic => radix.FormatValue((short)atomic),
            DINT atomic => radix.FormatValue((int)atomic),
            LINT atomic => radix.FormatValue((long)atomic),
            REAL atomic => radix.FormatValue((float)atomic),
            USINT atomic => radix.FormatValue((byte)atomic),
            UINT atomic => radix.FormatValue((ushort)atomic),
            UDINT atomic => radix.FormatValue((uint)atomic),
            ULINT atomic => radix.FormatValue((ulong)atomic),
            LREAL atomic => radix.FormatValue((double)atomic),
            _ => throw new NotSupportedException($"The atomic type {GetType()} is not supported.")
        };
    }
}