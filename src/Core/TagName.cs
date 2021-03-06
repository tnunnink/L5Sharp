using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a string tag name value used to identify members of a <see cref="ITag{TDataType}"/> component.
    /// </summary>
    public class TagName : IEquatable<TagName>, IComparable<TagName>
    {
        private const string ArrayBracket = "[";
        private const string MemberSeparator = ".";
        private const string TagNamePattern = @"^[A-Za-z_][\w\.[\],:]+$";
        private const string MemberPattern = @"^[A-Za-z_][\w:]+$|^\[\d+\]$|^\[\d+,\d+\]$|^\[\d+,\d+,\d+\]$";
        private const string MembersPattern = @"[\w:]+|\[[\d,]+\]";
        private const string PartsPattern = @"\w+|\[[\d,]+\]";
        private readonly string _tagName;

        /// <summary>
        /// Creates a new <see cref="TagName"/> object with the provided string tag name.
        /// </summary>
        /// <param name="tagName">The string that represents the tag name value.</param>
        /// <exception cref="ArgumentNullException">tagName is null.</exception>
        public TagName(string tagName)
        {
            _tagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
        }

        /// <summary>
        /// Gets the base portion of the <see cref="TagName"/> value.
        /// </summary>
        /// <remarks>
        /// The base portion of a given tag name is simply the beginning part of the tag name up to the first '.'
        /// member separator character. For Module defined tags, this includes the colon separator.
        /// </remarks>
        public string Base => _tagName[..GetRootLength()];

        /// <summary>
        /// Gets the operand portion of the <see cref="TagName"/> value.
        /// </summary>
        /// <remarks>
        /// The operand of a tag name represents the part of the name after the base name. This value will always be
        /// the full tag name value without the leading base name. The operand will include any leading '.' character.
        /// </remarks>
        /// <seealso cref="Path"/>
        public string Operand => !Base.IsEmpty() ? _tagName.Remove(0, Base.Length) : string.Empty;

        /// <summary>
        /// Gets the member path of the <see cref="TagName"/> value.
        /// </summary>
        /// <remarks>
        /// The path of a tag name represents a name relative to the base name. The value will always be the full tag name
        /// without the leading base name. This is similar to <see cref="Operand"/> except that is also removes any
        /// leading '.' character. 
        /// </remarks>
        /// <seealso cref="Operand"/>
        public string Path => !Operand.IsEmpty() ? Operand.Remove(0, 1) : string.Empty;

        /// <summary>
        /// Gets a number representing the distance or depth of the current member from the root tag name.
        /// </summary>
        /// <remarks>
        /// This value represents the number of members between the root member and current member (i.e. one less than
        /// <see cref="Members"/> to exclude the base name). This is helpful fo filtering tag descendents. Note that array
        /// indices are also considered a member. For example, 'MyTag[1].Value' has a depth of 2 since '[1]' and 'Value'
        /// are descendent members name of the root 'MyTag' member.
        /// </remarks>
        public int Depth => Members.Count() - 1;

        /// <summary>
        /// Gets the set of member names for the current <see cref="TagName"/>.
        /// </summary>
        /// <remarks>
        /// The members of a tag name represent all the individual constituent parts of the full name. This includes
        /// the index arrays if any exist. For Modules, the root is considered a single member.
        /// The root will include all character up to the first '.' member separator character.
        /// </remarks>
        public IEnumerable<string> Members =>
            Regex.Matches(_tagName, MembersPattern, RegexOptions.Compiled).Select(m => m.Value);

        /// <summary>
        /// Gets the each part of the tag name string separated by ':' or '.' characters.
        /// </summary>
        /// <remarks>
        /// This is similar to <see cref="Members"/>, except it splits the string by colons as well. If the tag name
        /// value is not a module tag, then <see cref="Parts"/> should be equivalent to <see cref="Members"/> 
        /// </remarks>
        public IEnumerable<string> Parts =>
            Regex.Matches(_tagName, PartsPattern, RegexOptions.Compiled).Select(m => m.Value);

        /// <summary>
        /// Gets a value indicating whether the current <see cref="TagName"/> value is empty.
        /// </summary>
        public bool IsEmpty => _tagName.IsEmpty();

        /// <summary>
        /// Gets a value indicating whether the current <see cref="TagName"/> test is a valid representation of a tag name.
        /// </summary>
        public bool IsValid => Regex.IsMatch(_tagName, TagNamePattern, RegexOptions.Compiled);

        /// <summary>
        /// Gets the static empty <see cref="TagName"/> value.
        /// </summary>
        public static TagName Empty => new(string.Empty);

        /// <summary>
        /// Combines two strings into a single <see cref="TagName"/> value.
        /// </summary>
        /// <param name="left">The string that represents the left part of the tag name value.</param>
        /// <param name="right">The string that represents the right part of the tag name value.</param>
        /// <returns>A new <see cref="TagName"/> value that is the combination of the provided names.</returns>
        /// <remarks>
        /// This method effectively concatenates the strings but inserts a '.' if right is not a index
        /// bracket (i.e. array member name).
        /// </remarks>
        public static TagName Combine(string left, string right) =>
            new(right.StartsWith(ArrayBracket) || right.StartsWith(MemberSeparator)
                ? $"{left}{right}"
                : $"{left}{MemberSeparator}{right}");

        /// <summary>
        /// Combines a collection of member names into a single <see cref="TagName"/> value.
        /// </summary>
        /// <param name="members">The collection of strings that represent the member names of the tag name value.</param>
        /// <returns>A new <see cref="TagName"/>A new <see cref="TagName"/> value that is the combination of all provided member names.</returns>
        /// <exception cref="ArgumentException">If a provided name does not match the member pattern format.</exception>
        public static TagName Combine(IEnumerable<string> members)
        {
            var builder = new StringBuilder();

            foreach (var name in members)
            {
                if (!Regex.IsMatch(name, MemberPattern))
                    throw new FormatException($"The provided name '{name}' is not a valid member name format.");

                if (!name.StartsWith(ArrayBracket) && builder.Length > 1)
                    builder.Append(MemberSeparator);

                builder.Append(name);
            }

            return new TagName(builder.ToString());
        }

        /// <summary>
        /// Converts a <see cref="TagName"/> to a <see cref="string"/> value.
        /// </summary>
        /// <param name="tagName">The <see cref="TagName"/> value to convert.</param>
        /// <returns>A new <see cref="string"/> value representing the value of the tag name.</returns>
        public static implicit operator string(TagName tagName) => tagName is not null
            ? tagName._tagName
            : throw new ArgumentNullException(nameof(tagName));

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="TagName"/> value.
        /// </summary>
        /// <param name="tagName">The <see cref="string"/> value to convert.</param>
        /// <returns>A new <see cref="TagName"/> value representing the value of the tag name.</returns>
        public static implicit operator TagName(string tagName) => new(tagName);

        /// <summary>
        /// Determines if the provided tagName is contained within the current value.
        /// </summary>
        /// <param name="tagName">The tag name to evaluate as a sub path or contained tag name path.</param>
        /// <returns>true if tagName is contained within the current value; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">tagName is null.</exception>
        public bool Contains(TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            return _tagName.Contains(tagName);
        }

        /// <summary>
        /// Creates a copy of the current <see cref="TagName"/> object with the same value.
        /// </summary>
        /// <returns>A new <see cref="TagName"/> instance with the value of the current tag name.</returns>
        public TagName Copy() => new(string.Copy(_tagName));

        /// <inheritdoc />
        public override string ToString() => _tagName;

        /// <inheritdoc /> 
        public bool Equals(TagName? other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) ||
                   StringComparer.OrdinalIgnoreCase.Equals(_tagName, other._tagName);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as TagName);

        /// <inheritdoc />
        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_tagName);

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
                : string.Compare(_tagName, other._tagName, StringComparison.OrdinalIgnoreCase);
        }

        private int GetRootLength()
        {
            var index = _tagName.IndexOf(MemberSeparator, StringComparison.Ordinal);
            return index >= 0 ? index : _tagName.Length;
        }
    }
}