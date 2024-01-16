using System;
using System.Collections;
using System.Collections.Generic;
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
    public sealed override DataTypeFamily Family => DataTypeFamily.None;

    /// <inheritdoc />
    public sealed override DataTypeClass Class => DataTypeClass.Atomic;

    /// <inheritdoc />
    public override IEnumerable<LogixMember> Members
    {
        get
        {
            var bits = new BitArray(GetBytes());
            for (var i = 0; i < bits.Count; i++)
            {
                var member = new LogixMember(i.ToString(), new BOOL(bits[i]));
                member.DataChanged += OnMemberDataChanged;
                yield return member;
            }
        }
    }

    /// <summary>
    /// The radix format for the <see cref="AtomicType"/>.
    /// </summary>
    /// <value>A <see cref="Core.Radix"/> representing the format of the atomic type value.</value>
    public abstract Radix Radix { get; }
    
    /// <summary>
    /// Parses the provided string value into an atomic type value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>An <see cref="AtomicType"/> representing the parsed value and format of the provided string.</returns>
    /// <exception cref="FormatException"><c>value</c> does not have a valid Radix format to be parsed as an
    /// atomic type.</exception>
    public static AtomicType Parse(string value)
    {
        return value.IsEquivalent("true") ? new BOOL(true)
            : value.IsEquivalent("false") ? new BOOL()
            : Radix.Infer(value).ParseValue(value);
    }
    
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
    /// Tries to parse the provided string value into an atomic type value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>An <see cref="AtomicType"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static AtomicType? TryParse(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return default;

        if (value.IsEquivalent("true")) return new BOOL(true);
        if (value.IsEquivalent("false")) return new BOOL(false);

        return Radix.TryInfer(value, out var radix) ? radix.ParseValue(value) : default;
    }

    /// <summary>
    /// Returns the <see cref="AtomicType"/> value as an array of <see cref="byte"/> values.
    /// </summary>
    /// <returns>An array of <see cref="byte"/> representing the value of the type.</returns>
    public abstract byte[] GetBytes();

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
    /// Trigger the <see cref="LogixType.DataChanged"/> event when a atomic member data changed event is fired to forward
    /// the call up the type/member hierarchy.
    /// </summary>
    /// <param name="sender">The member sending the data changed event.</param>
    /// <param name="e">The event args.</param>
    protected virtual void OnMemberDataChanged(object? sender, EventArgs e) => RaiseDataChanged(sender);
}