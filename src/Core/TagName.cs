using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a string value of a tag member name...
    /// </summary>
    public class TagName : IEquatable<TagName>, IComparable<TagName>
    {
        private const string ArrayBracket = "[";
        private const string MemberSeparator = ".";
        private const string MembersPattern = @"\w+|\[[\d,]+\]";
        private readonly string _tagName;

        /// <summary>
        /// Creates a new <see cref="TagName"/> object with the provided string tag name.
        /// </summary>
        /// <param name="tagName">The string that represents the tag name value.</param>
        /// <exception cref="ArgumentNullException">tagName is null.</exception>
        /// <exception cref="ArgumentException">tagName is empty.</exception>
        /// <exception cref="FormatException">tagName does not have a valid format.</exception>
        public TagName(string tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            if (tagName.IsEmpty())
                throw new ArgumentException();

            if (!Regex.IsMatch(tagName, @"^[\w\.[\],]+$", RegexOptions.Compiled))
                throw new FormatException();

            _tagName = tagName;
        }

        /// <summary>
        /// Gets the base tag portion of the <see cref="TagName"/> value.
        /// </summary>
        public string Base =>
            Regex.Matches(_tagName, MembersPattern, RegexOptions.Compiled).Select(m => m.Value).First();

        /// <summary>
        /// Gets the operand portion of the <see cref="TagName"/> value.
        /// </summary>
        public string Operand => _tagName.Remove(0, Base.Length);

        /// <summary>
        /// Gets the set of member names for the current <see cref="TagName"/>.
        /// </summary>
        public IEnumerable<string> Members =>
            Regex.Matches(_tagName, MembersPattern, RegexOptions.Compiled).Select(m => m.Value);

        /// <summary>
        /// Combines two strings, as base and member names, into a single <see cref="TagName"/> value.
        /// </summary>
        /// <param name="baseName">The string that represents the base tag name value.</param>
        /// <param name="memberName">The string that represents a member tag name value.</param>
        /// <returns>A new <see cref="TagName"/> value that is the combination of the provided member names.</returns>
        public static TagName Combine(string baseName, string memberName) =>
            new(memberName.StartsWith(ArrayBracket)
                ? $"{baseName}{memberName}"
                : $"{baseName}{MemberSeparator}{memberName}");

        /// <summary>
        /// Combines a collection of member names into a single <see cref="TagName"/> value.
        /// </summary>
        /// <param name="members">The collection of strings that represent the members name of the tag name value.</param>
        /// <returns>A new <see cref="TagName"/> value that is the combination of the provided collection.</returns>
        /// <exception cref="ArgumentException">If a provided name does not match the member pattern format.</exception>
        public static TagName Combine(IEnumerable<string> members)
        {
            var builder = new StringBuilder();

            foreach (var name in members)
            {
                if (!Regex.IsMatch(name, MembersPattern))
                    throw new ArgumentException();

                if (!name.StartsWith(ArrayBracket) && builder.Length > 1)
                    builder.Append(MemberSeparator);
                
                builder.Append(name);
            }

            return new TagName(builder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static implicit operator string(TagName tagName) => tagName._tagName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static implicit operator TagName(string tagName) => new(tagName);

        /// <inheritdoc />
        public override string ToString() => _tagName;

        /// <inheritdoc /> 
        public bool Equals(TagName? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _tagName == other._tagName;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is TagName tagName && Equals(tagName);

        /// <inheritdoc />
        public override int GetHashCode() => _tagName.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(TagName? left, TagName? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(TagName? left, TagName? right) => !Equals(left, right);

        /// <inheritdoc />
        public int CompareTo(TagName? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other)
                ? 1
                : string.Compare(_tagName, other._tagName, StringComparison.Ordinal);
        }
    }
}