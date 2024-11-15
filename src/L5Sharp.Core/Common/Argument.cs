using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents an argument to an instruction, which could be a tag name reference or an immediate atomic value.
/// </summary>
public class Argument : ILogixParsable<Argument>
{
    private readonly object _value;

    /// <summary>
    /// Creates a new <see cref="Argument"/> wrapping the object value.
    /// </summary>
    /// <param name="value">An object representing the argument.</param>
    /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
    private Argument(object value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Indicates whether the argument is an immediate atomic value.
    /// </summary>
    /// <value><c>true</c> if the underlying value is an <see cref="AtomicData"/> object; Otherwise, <c>false</c>.</value>
    public bool IsAtomic => _value is AtomicData;

    /// <summary>
    /// Indicates whether the argument is a literal string value with the single quote identifiers.
    /// </summary>
    /// <value><c>true</c> if the underlying value is an <see cref="string"/> object; Otherwise, <c>false</c>.</value>
    public bool IsString => _value is string;

    /// <summary>
    /// Indicates whether the argument is an immediate value, either string literal or atomic value.
    /// </summary>
    /// <value><c>true</c> if the underlying value is an <see cref="AtomicData"/> or <see cref="string"/> object;
    /// Otherwise, <c>false</c>.</value>
    public bool IsImmediate => _value is AtomicData or string;

    /// <summary>
    /// Indicates whether the argument is an tag name reference.
    /// </summary>
    /// <value><c>true</c> if the underlying value is a <see cref="TagName"/> object; Otherwise, <c>false</c>.</value>
    public bool IsTag => _value is TagName;

    /// <summary>
    /// Indicates whether the argument is an expression or combination of tag names, operators, and/or immediate values.
    /// </summary>
    /// <value><c>true</c> if the underlying value is a <see cref="NeutralText"/> instance wrapping a complex expression;
    /// Otherwise, <c>false</c>.</value>
    public bool IsExpression => _value is NeutralText;

    /// <summary>
    /// The collection of <see cref="TagName"/> values found in the argument.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values.</value>
    /// <remarks>
    /// Since an argument could represent a complex expression, it may contain more than one tag name value.
    /// We need a way to get all tag names from a single argument whether it's a single tag name or expression or
    /// multiple tag names.
    /// </remarks>
    public TagName[] Tags => _value switch
    {
        TagName tagName => [tagName],
        NeutralText text => text.Tags().ToArray(),
        _ => []
    };

    /// <summary>
    /// The collection of <see cref="TagName"/> values found in the argument.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values.</value>
    /// <remarks>
    /// Since an argument could represent a complex expression, it may contain more than one tag name value.
    /// We need a way to get all tag names from a single argument whether it's a single tag name or expression or
    /// multiple tag names.
    /// </remarks>
    public AtomicData[] Values => _value switch
    {
        AtomicData aomtic => [aomtic],
        _ => []
    };

    /// <summary>
    /// Represents an unknown argument that can be found in certain instruction text.
    /// </summary>
    /// <value>A <see cref="Argument"/> representing an unknown parameter.</value>
    /// <remarks>This is literally the '?' character, as often seen in the TIMER instruction arguments.</remarks>
    public static Argument Unknown => new("?");

    /// <summary>
    /// Represents an empty argument.
    /// </summary>
    /// <value>A <see cref="Argument"/> wrapping an empty string objet value.</value>
    /// <remarks>
    /// Some instruction have an empty/optional argument(s) (GSV) and therefore we need a way to represent
    /// that value.
    /// </remarks>
    public static Argument Empty => new(string.Empty);

    /// <summary>
    /// Parses the provided string into a <see cref="Argument"/> value.
    /// </summary>
    /// <param name="value">Teh string to parse.</param>
    /// <returns>An <see cref="Argument"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException"><c>value</c> is null or empty</exception>
    public static Argument Parse(string? value)
    {
        if (value is null || value.IsEmpty())
            throw new ArgumentException("Value can not be null or empty to parse.");

        //Unknown value - Can be found in TON instructions and probably others.
        if (value == "?") return Unknown;

        //Literal string value - We need to intercept this before the Atomic.TryParse method to prevent overflows.
        if (value.StartsWith('\'') && value.EndsWith('\'')) return new Argument(value);

        //Immediate atomic value
        var atomic = AtomicData.TryParse(value);
        if (atomic is not null) return new Argument(atomic);

        //TagName or Expression otherwise
        return TagName.IsTag(value) ? new Argument(new TagName(value)) : new Argument(new NeutralText(value));
    }

    /// <summary>
    /// Parses the provided string into a <see cref="Argument"/> value.
    /// </summary>
    /// <param name="value">Teh string to parse.</param>
    /// <returns>An <see cref="Argument"/> representing the parsed value if successful; Otherwise, <see cref="Empty"/>.</returns>
    public static Argument TryParse(string? value)
    {
        if (value is null || value.IsEmpty()) return Empty;

        //Unknown value - Can be found in TON instructions and probably others.
        if (value == "?") return Unknown;

        //Literal string value - We need to intercept this before the Atomic.TryParse method to prevent overflows.
        if (value.StartsWith('\'') && value.EndsWith('\'')) return new Argument(value);

        //Immediate atomic value
        var atomic = AtomicData.TryParse(value);
        if (atomic is not null) return new Argument(atomic);

        //TagName or Expression otherwise
        return TagName.IsTag(value) ? new Argument(new TagName(value)) : new Argument(new NeutralText(value));
    }

    /// <summary>
    /// Implicitly converts the provided <see cref="TagName"/> to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> object to convert.</param>
    /// <returns>A <see cref="Argument"/> object containing the value of the tag name.</returns>
    public static implicit operator Argument(TagName tagName) => new(tagName);

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>A <see cref="Argument"/> object containing the value of the tag name.</returns>
    public static implicit operator Argument(string value) => Parse(value);

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(bool value) => new(new BOOL(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(sbyte value) => new(new SINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(byte value) => new(new USINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(short value) => new(new INT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(ushort value) => new(new UINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(int value) => new(new DINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(uint value) => new(new UDINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(long value) => new(new LINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(ulong value) => new(new ULINT(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(float value) => new(new REAL(value));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(double value) => new(new LREAL(value));

    /// <summary>
    /// Explicitly converts the provided <see cref="Argument"/> to a <see cref="TagName"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="TagName"/> object representing the value of the argument.</returns>
    public static explicit operator TagName(Argument argument) => (TagName)argument._value;

    /// <summary>
    /// Explicitly converts the provided <see cref="Argument"/> to an <see cref="AtomicData"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="AtomicData"/> object representing the value of the argument.</returns>
    public static explicit operator AtomicData(Argument argument) => (AtomicData)argument._value;

    /// <summary>
    /// Explicitly converts the provided <see cref="Argument"/> to an <see cref="NeutralText"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="NeutralText"/> object representing the value of the argument.</returns>
    public static explicit operator NeutralText(Argument argument) => (NeutralText)argument._value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => _value.Equals(obj);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => _value.ToString()!;

    /// <summary>
    /// Determines whether two Argument objects are equal.
    /// </summary>
    /// <param name="left">The left Argument object.</param>
    /// <param name="right">The right Argument object.</param>
    /// <returns>Returns true if the two objects are equal, otherwise false.</returns>
    public static bool operator ==(Argument left, Argument right) => Equals(left, right);

    /// <summary>
    /// Defines the inequality operator for the Argument class.
    /// </summary>
    /// <param name="left">The left Argument object.</param>
    /// <param name="right">The right Argument object.</param>
    /// <returns>true if the left Argument is not equal to the right Argument; otherwise, false.</returns>
    public static bool operator !=(Argument left, Argument right) => Equals(left, right);
}