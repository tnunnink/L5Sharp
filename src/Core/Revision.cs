using System;
using System.Globalization;
using System.Xml.Serialization;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a revision or a number that is expressed by a Major and Minor revision.
    /// </summary>
    public sealed class Revision : IEquatable<Revision>, IComparable<Revision>
    {
        private const string RevisionSeparator = ".";

        /// <summary>
        /// Creates a new instance of a <c>Revision</c> withe the optional major and minor versions.
        /// </summary>
        /// <param name="major">The value of the major revision.</param>
        /// <param name="minor">The value of the minor revision.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Revision(ushort major = 1, ushort minor = 0)
        {
            Major = major;
            Minor = minor;
        }

        /// <summary>
        /// Gets the value of the Major revision number.
        /// </summary>
        public ushort Major { get; }

        /// <summary>
        /// Gets the value of the Minor revision number.
        /// </summary>
        public ushort Minor { get; }


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
                throw new ArgumentException("Value must only have a major and minor revision number.");

            if (!ushort.TryParse(revisions[0], out var major))
                throw new ArgumentException("Major revision could not be parsed. Make sure the value is a ushort.");

            if (!ushort.TryParse(revisions[1], out var minor))
                throw new ArgumentException("Minor revision could not be parsed. Make sure the value is a ushort.");

            return new Revision(major, minor);
        }

        public static implicit operator double(Revision revision) =>
            float.Parse($"{revision.Major}{RevisionSeparator}{revision.Minor}");

        public static implicit operator Revision(double revision) =>
            Parse(revision.ToString(CultureInfo.InvariantCulture));

        /// <inheritdoc />
        public override string ToString() => $"{Major}{RevisionSeparator}{Minor}";

        /// <inheritdoc />
        public bool Equals(Revision? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Major == other.Major && Minor == other.Minor;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Revision);

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Major, Minor);

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

        /// <inheritdoc />
        public int CompareTo(Revision? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var majorComparison = Major.CompareTo(other.Major);
            return majorComparison != 0 ? majorComparison : Minor.CompareTo(other.Minor);
        }
    }
}