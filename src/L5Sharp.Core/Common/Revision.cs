using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace L5Sharp.Core;

/// <summary>
/// Represents a revision number that is expressed by as {Major}.{Minor}.
/// </summary>
public sealed class Revision : IComparable, ILogixParsable<Revision>
{
    private const string FormatMessage = "Revision format must be {Major}.{Minor}.{Build} where Build is optional.";
    private static readonly Regex RevisionPattern = new(@"^(\d+)(?:\.(\d+))(?:\.(\d+))?$");

    /// <summary>
    /// Creates a new default <see cref="Revision"/> with value 1.0.
    /// </summary>
    public Revision()
    {
    }

    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified major and minor values.
    /// </summary>
    /// <param name="major">The value of the major revision.</param>
    /// <param name="minor">The value of the minor revision. Will default to 0 if not provided.</param>
    /// <param name="build"></param>
    public Revision(ushort major, ushort minor = 0, ushort build = 0)
    {
        Major = major;
        Minor = minor;
        Build = build;
    }

    /// <summary>
    /// Creates a new <see cref="Revision"/> with the specified value.
    /// </summary>
    /// <param name="value">The double value representing the revision number.</param>
    public Revision(double value)
    {
        var parsed = Parse(value.ToString(CultureInfo.InvariantCulture));
        Major = parsed.Major;
        Minor = parsed.Minor;
    }

    /// <summary>
    /// The major version of the <see cref="Revision"/> value.
    /// </summary>
    public ushort Major { get; } = 1;

    /// <summary>
    /// The minor version of the <see cref="Revision"/> value.
    /// </summary>
    /// <remarks>This is the second value of the revision and should represent a </remarks>
    public ushort Minor { get; }

    /// <summary>
    /// The build number of the <see cref="Revision"/> value.
    /// </summary>
    /// <remarks>
    /// Build is often and optional number and will only be considered if greater than zero.
    /// </remarks>
    public ushort Build { get; }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            Revision other => CompareRevisions(other),
            double number => CompareRevisions(number),
            string text => CompareRevisions(text),
            _ => throw new ArgumentException($"Can not compare {obj.GetType()} with Revision value.")
        };

        //Compares the major/minor/build for the provided revision.
        int CompareRevisions(Revision r)
        {
            var major = Major.CompareTo(r.Major);
            if (major != 0) return major;

            var minor = Minor.CompareTo(r.Minor);
            if (minor != 0) return minor;

            return Build.CompareTo(r.Build);
        }
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Revision other => IsEqual(other),
            double number => IsEqual(number),
            string text => IsEqual(text),
            _ => false
        };

        //Equate the major/minor/build for the provided revision.
        bool IsEqual(Revision r)
        {
            return Major == r.Major && Minor == r.Minor && Build == r.Build;
        }
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = 17;
        hash = hash * 31 + Major.GetHashCode();
        hash = hash * 31 + Minor.GetHashCode();
        hash = hash * 31 + Build.GetHashCode();
        return hash;
    }

    /// <inheritdoc />
    public override string ToString() => Build > 0 ? $"{Major}.{Minor}.{Build}" : $"{Major}.{Minor}";

    /// <summary>
    /// Parses the provided string into a <see cref="Revision"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="Revision"/> representing the parsed value.</returns>
    public static Revision Parse(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Revision can not be null or empty.");

        var match = RevisionPattern.Match(value);

        if (!match.Success)
            throw new FormatException($"'{value}' is an invalid revision format. {FormatMessage}");

        if (!ushort.TryParse(match.Groups[1].Value, out var major))
            throw new ArgumentException($"Unable to parse major version number from '{value}'");

        if (!ushort.TryParse(match.Groups[2].Value, out var minor))
            throw new ArgumentException($"Unable to parse minor version number from '{value}'");

        ushort build = 0;
        if (!match.Groups[3].Value.IsEmpty() && !ushort.TryParse(match.Groups[3].Value, out build))
            throw new ArgumentException($"Unable to parse build number from '{value}'");

        return new Revision(major, minor, build);
    }

    /// <summary>
    /// Tries to parse the provided string into a <see cref="Revision"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="Revision"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static Revision? TryParse(string? value)
    {
        if (value is null || value.IsEmpty())
            return default;

        var match = RevisionPattern.Match(value);

        if (!match.Success)
            return default;

        if (!ushort.TryParse(match.Groups[1].Value, out var major))
            return default;

        ushort minor = 0;
        if (!match.Groups[2].Value.IsEmpty() && !ushort.TryParse(match.Groups[2].Value, out minor))
            return default;

        ushort build = 0;
        if (!match.Groups[3].Value.IsEmpty() && !ushort.TryParse(match.Groups[3].Value, out build))
            return default;

        return new Revision(major, minor, build);
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
    public static implicit operator Revision(string revision) => Parse(revision);

    /// <summary>
    /// Converts a <see cref="Revision"/> to a <see cref="string"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="string"/> value representing a major and minor revision.</returns>
    public static explicit operator double(Revision revision) => double.Parse($"{revision.Major}.{revision.Minor}");

    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="Revision"/>.
    /// </summary>
    /// <param name="revision">The revision value to convert.</param>
    /// <returns>A new <see cref="Revision"/> value representing a major and minor revision.</returns>
    public static implicit operator Revision(double revision) => Parse(revision.ToString(CultureInfo.InvariantCulture));
}