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
        private const string TagNamePattern = @"^[\w\.[\],:]+$";
        private const string MemberPattern = @"^[\w:]+$|^\[[\d,]+\]$";
        private const string MembersPattern = @"[\w:]+|\[[\d,]+\]";
        private const string PartsPattern = @"\w+|\[[\d,]+\]";
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
            if (string.IsNullOrEmpty(tagName))
                throw new ArgumentException("TagName can not be null or empty.");

            if (!Regex.IsMatch(tagName, TagNamePattern, RegexOptions.Compiled))
                throw new FormatException($"The provided tag name '{tagName}' does not have a valid format.");

            _tagName = tagName;
        }

        /// <summary>
        /// Gets the root string  portion of the <see cref="TagName"/> value.
        /// </summary>
        /// <remarks>
        /// The root portion of a given tag name is simply the beginning part of the tag name up to the first '.'
        /// member separator character. For Module defined tags, this includes the colon separator.
        /// </remarks>
        public string Root => _tagName[..GetRootLength()];

        /// <summary>
        /// Gets the operand portion of the <see cref="TagName"/> value.
        /// </summary>
        /// <remarks>
        /// The operand of a tag name represents the part of the name after the base name. This value will always be
        /// the full tag name value without the leading base name. The operand will include any leading '.' character.
        /// </remarks>
        /// <seealso cref="Path"/>
        public string Operand => _tagName.Remove(0, Root.Length);

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
        /// This value represents the number of members between the root member and current member (i.e. on less than
        /// <see cref="Members"/> to exclude the root). This is helpful fo filtering tag descendents. Note that array
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
        public override bool Equals(object? obj) => Equals(obj as TagName);

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
                : string.Compare(_tagName, other._tagName, StringComparison.OrdinalIgnoreCase);
        }

        private int GetRootLength()
        {
            var index = _tagName.IndexOf(MemberSeparator, StringComparison.Ordinal);
            return index >= 0 ? index : _tagName.Length;
        }
    }
}