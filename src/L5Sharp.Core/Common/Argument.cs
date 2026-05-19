using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a textual argument value that can be parsed and analyzed. Arguments are part of an instruction and can
/// be either a tag name reference, immediate atomic or string value, or even complex expressions. This class provides
/// members for inspecting and parsing/extracting specific data from an argument value.
/// </summary>
public class Argument
{
    /// <summary>
    /// A cached array of all known Logix operator symbols used for splitting and parsing expression arguments.
    /// </summary>
    private static readonly string[] Operators = Operator.All().Select(x => x.Value).ToArray();

    /// <summary>
    /// The value typically found in Studio for undefined argument values in certain instructions.
    /// </summary>
    private const string UnknownValue = "?";

    /// <summary>
    /// Represents the underlying value of an <see cref="Argument"/> instance.
    /// </summary>
    private readonly string _value;

    /// <summary>
    /// Creates a new <see cref="Argument"/> wrapping the object value.
    /// </summary>
    /// <param name="value">An object representing the argument.</param>
    /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
    public Argument(string value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Gets the interpreted type of the argument value.
    /// </summary>
    /// <remarks>
    /// This is determined by analyzing the structure or format of the argument's textual representation
    /// and returning a corresponding predefined <see cref="ArgumentType"/> enumeration value.
    /// </remarks>
    public ArgumentType Type => ArgumentType.Of(_value);

    /// <summary>
    /// Gets a value indicating whether this argument is invalid (either empty or unknown).
    /// </summary>
    /// <value>
    /// <c>true</c> if the argument type is <see cref="ArgumentType.Empty"/> or <see cref="ArgumentType.Unknown"/>; otherwise, <c>false</c>.
    /// </value>
    public bool IsInvalid => Type == ArgumentType.Empty || Type == ArgumentType.Unknown;

    /// <summary>
    /// Gets a value indicating whether this argument represents an immediate value (atomic or string).
    /// </summary>
    /// <value>
    /// <c>true</c> if the argument type is <see cref="ArgumentType.Atomic"/> or <see cref="ArgumentType.String"/>; otherwise, <c>false</c>.
    /// </value>
    public bool IsLiteral => Type == ArgumentType.Atomic || this == ArgumentType.String;

    /// <summary>
    /// Gets a value indicating whether this argument represents a tag name reference.
    /// </summary>
    /// <value>
    /// <c>true</c> if the argument type is <see cref="ArgumentType.Tag"/>; otherwise, <c>false</c>.
    /// </value>
    public bool IsTag => Type == ArgumentType.Tag;

    /// <summary>
    /// Gets a value indicating whether this argument represents an atomic value.
    /// </summary>
    /// <value>
    /// <c>true</c> if the argument type is <see cref="ArgumentType.Atomic"/>; otherwise, <c>false</c>.
    /// </value>
    public bool IsAtomic => Type == ArgumentType.Atomic;

    /// <summary>
    /// Gets a value indicating whether this argument represents an expression containing operators.
    /// </summary>
    /// <value>
    /// <c>true</c> if the argument type is <see cref="ArgumentType.Expression"/>; otherwise, <c>false</c>.
    /// </value>
    public bool IsExpression => Type == ArgumentType.Expression;

    /// <summary>
    /// Retrieves a read-only collection of arguments derived from the current argument string value.
    /// Useful for parsing and analyzing composite argument structures within expressions.
    /// </summary>
    /// <remarks>
    /// If the argument type is <see cref="ArgumentType.Expression"/>, this property returns a collection of
    /// individual component arguments extracted by splitting the expression on known Logix operators.
    /// If the argument is not an expression (e.g., a tag name, atomic value, or string literal),
    /// this property returns a single-item collection containing the argument itself.
    /// </remarks>
    public IReadOnlyList<Argument> Arguments => ExtractArguments();

    /// <summary>
    /// Represents an unknown argument that can be found in certain instruction text.
    /// </summary>
    /// <remarks>This is literally the '?' character, as often seen in the Timer and Counter instructions.</remarks>
    public static Argument Unknown => new(UnknownValue);

    /// <summary>
    /// Represents an empty argument.
    /// </summary>
    /// <remarks>
    /// Some instruction has an empty/optional argument(s) (GSV), and therefore we will support empty arguments instances.
    /// </remarks>
    public static Argument Empty => new(string.Empty);

    /// <summary>
    /// Converts this argument to a <see cref="TagName"/> instance.
    /// </summary>
    /// <returns>A <see cref="TagName"/> representing the tag reference in this argument.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the argument type is not <see cref="ArgumentType.Tag"/>.</exception>
    public TagName ToTag()
    {
        if (Type != ArgumentType.Tag)
            throw new InvalidOperationException(
                $"Cannot convert argument '{_value}' to TagName. The argument type is {Type}, but expected {ArgumentType.Tag}.");

        return new TagName(_value);
    }

    /// <summary>
    /// Converts this argument to an <see cref="AtomicData"/> instance by parsing its immediate atomic value.
    /// </summary>
    /// <returns>An <see cref="AtomicData"/> representing the parsed atomic value from this argument.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the argument type is not <see cref="ArgumentType.Atomic"/>.</exception>
    public AtomicData ToAtomic()
    {
        if (Type != ArgumentType.Atomic)
            throw new InvalidOperationException(
                $"Cannot convert argument '{_value}' to AtomicData. The argument type is {Type}, but expected {ArgumentType.Atomic}.");

        return AtomicData.Parse(_value);
    }

    #region Equality

    /// <inheritdoc />
    public override bool Equals(object? obj) => _value.Equals(obj?.ToString());

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => _value;

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

    #endregion

    #region Operators

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
    public static implicit operator Argument(string value) => new(value);

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(bool value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(sbyte value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(byte value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(short value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(ushort value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(int value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(uint value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(long value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(ulong value) => new(value.ToString());

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(float value) => new(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Implicitly converts the provided value to an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The object value to convert.</param>
    /// <returns>An <see cref="Argument"/> containing the value of the provided object.</returns>
    public static implicit operator Argument(double value) => new(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Explicitly converts the provided <see cref="Argument"/> to a <see cref="string"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="string"/> object representing the value of the argument.</returns>
    public static implicit operator string(Argument argument) => argument._value;

    #endregion

    /// <summary>
    /// Extracts individual component arguments from an expression by splitting on known Logix operators.
    /// </summary>
    /// <returns>An array of <see cref="Argument"/> objects representing each component of the expression,
    /// or an empty array if not an expression.</returns>
    private Argument[] ExtractArguments()
    {
        if (!IsExpression) return [this];

        return _value
            .Split(Operators, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new Argument(x))
            .ToArray();
    }
}