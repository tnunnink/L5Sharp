using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace L5Sharp.Core;

/// <summary>
/// Represents a revision number that is expressed by as {Major}.{Minor}.
/// </summary>
public sealed class Revision : IComparable, ILogixParsable<Revision>
{
    private const char RevisionSeparator = '.';
    private readonly string _value;
    private static readonly Regex RevisionPattern = new("^[0-9]+\\.[0-9]+$");

    /// <summary>
    /// Creates a new default <see cref="Revision"/> with value 1.0.
    /// </summary>
    public Revision()
    {
        _value = "1.0";
    }

    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified value.
    /// </summary>
    /// <param name="value">The Major.Minor revision value to initialize the type with.</param>
    /// <exception cref="ArgumentException"><paramref name="value"/> is null or empty.</exception>
    /// <exception cref="FormatException"><paramref name="value"/> is not in a valid format Major.Minor.</exception>
    public Revision(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Revision value can not be null or empty.");

        if (!RevisionPattern.IsMatch(value))
            throw new FormatException(
                $"Value '{value}' is invalid revision format. Revision format must be Major.Minor.");

        _value = value;
    }

    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified major and minor values.
    /// </summary>
    /// <param name="major">The value of the major revision.</param>
    /// <param name="minor">The value of the minor revision. Will default to 0 if not provided.</param>
    public Revision(ushort major, ushort minor = 0)
    {
        _value = $"{major}.{minor}";
    }

    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified value.
    /// </summary>
    /// <param name="value">The double value representing the revision number.</param>
    public Revision(double value)
    {
        _value = value.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// The major revision of the <see cref="Revision"/> value.
    /// </summary>
    public string Major => _value.Split(RevisionSeparator)[0];

    /// <summary>
    /// The minor revision of the <see cref="Revision"/> value.
    /// </summary>
    public string Minor => _value.Split(RevisionSeparator)[1];

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            Revision other => string.Compare(_value, other._value, StringComparison.Ordinal),
            string value => string.Compare(_value, value, StringComparison.Ordinal),
            _ => throw new ArgumentException($"Can not compare {obj.GetType()} with Revision value.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Revision other => string.Equals(_value, other._value, StringComparison.Ordinal),
            string value => string.Equals(_value, value, StringComparison.Ordinal),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => _value;
    
    /// <summary>
    /// Parses the provided string into a <see cref="Revision"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="Revision"/> representing the parsed value.</returns>
    public static Revision Parse(string value) => new(value);

    /// <summary>
    /// Tries to parse the provided string into a <see cref="Revision"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="Revision"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static Revision? TryParse(string? value)
    {
        if (value is null || value.IsEmpty()) 
            return default;

        return RevisionPattern.IsMatch(value) ? new Revision(value) : default;
    }

    /// <summary>
    /// Determines if the provided objects are equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are equal; otherwise, false.</returns>
    public static bool operator ==(Revision left, Revision right) => Equals(left, right);

    /// <summary>
    /// Determines if the provided objects are not equal.
    /// </summary>
    /// <param name="left">An object to compare.</param>
    /// <param name="right">An object to compare.</param>
    /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
    public static bool operator !=(Revision left, Revision right) => !Equals(left, right);

    /// <summary>
    /// Compares two objects and determines if left is greater than right.
    /// </summary>
    /// <param name="left">A <see cref="Revision"/> to compare.</param>
    /// <param name="right">A <see cref="Revision"/> to compare.</param>
    /// <returns><c>true</c> if <c>left</c> is greater than <c>right</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >(Revision left, Revision right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Compares two objects and determines if left is less than right.
    /// </summary>
    /// <param name="left">A <see cref="Revision"/> to compare.</param>
    /// <param name="right">A <see cref="Revision"/> to compare.</param>
    /// <returns><c>true</c> if <c>left</c> is less than <c>right</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <(Revision left, Revision right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Compares two objects and determines if left is greater than or equal to right.
    /// </summary>
    /// <param name="left">A <see cref="Revision"/> to compare.</param>
    /// <param name="right">A <see cref="Revision"/> to compare.</param>
    /// <returns><c>true</c> if <c>left</c> is greater than or equal to <c>right</c>, otherwise, <c>false</c>.</returns>
    public static bool operator >=(Revision left, Revision right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Compares two objects and determines if left is less than or equal to right.
    /// </summary>
    /// <param name="left">A <see cref="Revision"/> to compare.</param>
    /// <param name="right">A <see cref="Revision"/> to compare.</param>
    /// <returns><c>true</c> if <c>left</c> is less or equal to than <c>right</c>, otherwise, <c>false</c>.</returns>
    public static bool operator <=(Revision left, Revision right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Converts a <see cref="Revision"/> to a <see cref="string"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="string"/> value representing a major and minor revision.</returns>
    public static implicit operator string(Revision revision) => revision.ToString();

    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="Revision"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="Revision"/> value representing a major and minor revision.</returns>
    public static implicit operator Revision(string revision) => new(revision);
    
    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="Revision"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="Revision"/> value representing a major and minor revision.</returns>
    public static implicit operator Revision(double revision) => new(revision);
}