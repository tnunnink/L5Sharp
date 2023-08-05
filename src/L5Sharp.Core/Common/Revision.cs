using System;
using System.Globalization;

namespace L5Sharp.Common;

/// <summary>
/// Represents a revision number that is expressed by as {Major}.{Minor}.
/// </summary>
public sealed class Revision : IComparable
{
    private const string RevisionSeparator = ".";

    /// <summary>
    /// Creates a new default <see cref="Revision"/> with value 1.0.
    /// </summary>
    public Revision()
    {
        Major = 1;
        Minor = 0;
    }

    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified major and minor values.
    /// </summary>
    /// <param name="major">The value of the major revision.</param>
    /// <param name="minor">The value of the minor revision. Will default to 0 if not provided.</param>
    public Revision(ushort major, ushort minor = 0)
    {
        Major = major;
        Minor = minor;
    }
    
    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified value.
    /// </summary>
    /// <param name="value">The double value representing the revision number.</param>
    public Revision(double value)
    {
        Revision revision = value;
        Major = revision.Major;
        Minor = revision.Minor;
    }

    /// <summary>
    /// The major revision of the <see cref="Revision"/> value.
    /// </summary>
    public ushort Major { get; }

    /// <summary>
    /// The minor revision of the <see cref="Revision"/> value.
    /// </summary>
    public ushort Minor { get; }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            Revision other => Major.CompareTo(other.Major) != 0
                ? Major.CompareTo(other.Major)
                : Minor.CompareTo(other.Minor),
            double other => other.CompareTo(this),
            string other => string.Compare(ToString(), other, StringComparison.Ordinal),
            _ => throw new ArgumentException($"Can not compare {obj.GetType()} with Revision value.")
        };
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Revision other => Major == other.Major && Minor == other.Minor,
            double value => value == this,
            string value => value == ToString(),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Major.GetHashCode() ^ Minor.GetHashCode();

    /// <summary>
    /// Parses the string input into a new <see cref="Revision"/> value.
    /// </summary>
    /// <param name="value">The string revision value to parse.</param>
    /// <returns>A new Revision value that represents the value of the provided string revision.</returns>
    /// <exception cref="ArgumentException">value is empty, null, does not have the character '.', has more or less
    /// than 2 revision numbers, or can not be parsed to a byte.</exception>
    public static Revision Parse(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Value can not be null or empty");

        if (!value.Contains(RevisionSeparator))
            throw new ArgumentException($"Value must have character '{RevisionSeparator}'.");

        var revisions = value.Split('.');

        if (revisions.Length != 2)
            throw new ArgumentException(
                $"Value {value} does not have Major.Minor pattern. Must only have a major and minor revision number to correctly parse.");

        if (!ushort.TryParse(revisions[0], out var major))
            throw new ArgumentException($"Major revision could not be parsed to a {typeof(ushort)}.");

        if (!ushort.TryParse(revisions[1], out var minor))
            throw new ArgumentException($"Minor revision could not be parsed to a {typeof(ushort)}.");

        return new Revision(major, minor);
    }

    /// <inheritdoc />
    public override string ToString() => $"{Major}{RevisionSeparator}{Minor}";

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
    /// Converts a <see cref="Revision"/> to a <see cref="double"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="double"/> value representing a major and minor revision.</returns>
    public static implicit operator double(Revision revision) =>
        double.Parse($"{revision.Major}{RevisionSeparator}{revision.Minor}");

    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="Revision"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="Revision"/> value representing a major and minor revision.</returns>
    public static implicit operator Revision(double revision) =>
        Parse(revision.ToString(CultureInfo.InvariantCulture));
    
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
    public static implicit operator Revision(string revision) => Parse(revision);
}