using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace L5Sharp.Core;

/// <summary>
/// Represents a textual argument value that can be parsed and analyzed. Arguments are part of an instruction and can
/// be either a tag name reference, immediate atomic or string value, or even complex expressions. This class provides
/// members for inspecting and parsing/extracting specific data from an argument value.
/// </summary>
public class Argument
{
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
    private Argument(string value)
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
    /// The collection of <see cref="TagName"/> values found in the argument.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values.</value>
    /// <remarks>
    /// Since an argument could represent a complex expression, it may contain more than one tag name value.
    /// We need a way to get all tag names from a single argument, whether it's a single tag name or expression or
    /// multiple tag names.
    /// </remarks>
    public IReadOnlyList<TagName> Tags => ExtractTags(_value).ToArray();

    /// <summary>
    /// The collection of <see cref="TagName"/> values found in the argument.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values.</value>
    /// <remarks>
    /// Since an argument could represent a complex expression, it may contain more than one tag name value.
    /// We need a way to get all tag names from a single argument, whether it's a single tag name or expression or
    /// multiple tag names.
    /// </remarks>
    public IReadOnlyList<AtomicData> Values => ExtractValues(_value).ToArray();

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
    /// Parses the provided string into a <see cref="Argument"/> value.
    /// </summary>
    /// <param name="value">Teh string to parse.</param>
    /// <returns>An <see cref="Argument"/> representing the parsed value.</returns>
    /// <exception cref="ArgumentException"><c>value</c> is null.</exception>
    public static Argument Parse(string? value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        return new Argument(value);
    }

    /// <summary>
    /// Attempts to parse the specified string into an <see cref="Argument"/>.
    /// </summary>
    /// <param name="value">The string value to be parsed into an <see cref="Argument"/>.</param>
    /// <returns>An <see cref="Argument"/> instance created from the input value.</returns>
    public static Argument TryParse(string? value)
    {
        return new Argument(value ?? string.Empty);
    }

    #region Equality

    /// <inheritdoc />
    public override bool Equals(object? obj) => _value.Equals(obj);

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
    /// Explicitly converts the provided <see cref="Argument"/> to a <see cref="TagName"/>.
    /// </summary>
    /// <param name="argument">The <see cref="Argument"/> object to convert.</param>
    /// <returns>A <see cref="TagName"/> object representing the value of the argument.</returns>
    public static implicit operator string(Argument argument) => argument._value;

    #endregion

    #region Internal

    /// <summary>
    /// Extracts all tag names from the provided text based on a predefined search pattern.
    /// </summary>
    private static IEnumerable<TagName> ExtractTags(string text)
    {
        var matches = Regex.Matches(text, TagName.SearchPattern);

        foreach (Match match in matches)
        {
            yield return new TagName(match.Value);
        }
    }

    /// <summary>
    /// Extracts a collection of <see cref="AtomicData"/> from the given argument string.
    /// </summary>
    private static IEnumerable<AtomicData> ExtractValues(string argument)
    {
        var type = ArgumentType.Of(argument);

        if (type == ArgumentType.Atomic)
            return [Radix.ParseAtomic(argument)];

        if (type == ArgumentType.Expression)
            //todo handle nested values in expression
            throw new NotImplementedException();

        return [];
    }

    #endregion
}