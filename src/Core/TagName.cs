using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using L5Sharp.Extensions;

namespace L5Sharp.Core;

/// <summary>
/// A string wrapper representing a Logix <c>TagName</c>.
/// </summary>
/// <remarks>
/// This value type class make working with string tag name easier by providing
/// methods fo analyzing and breaking the tag name into constituent parts (members).
/// </remarks>
public class TagName : IEquatable<TagName>, IComparable<TagName>
{
    private readonly string _tagName;

    private const string ArrayBracketStart = "[";
    private const string MemberSeparator = ".";

    /// <summary>
    /// A regex pattern for getting the base tag of the tag name string.
    /// Use this patter to capture the root or base of a tag name value.
    /// </summary>
    private const string TagPattern = @"^[A-Za-z_][\w+:]{1,39}";

    /// <summary>
    /// A regex pattern for a Logix tag name with starting and ending anchors.
    /// Use this pattern to match a string and ensure it is only a tag name an nothing else.
    /// </summary>
    private const string TagNamePattern =
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$";

    /// <summary>
    /// A regex pattern for matching each individual possible member portion of a full tag name.
    /// Used to split incoming string into member parts. 
    /// </summary>
    private const string MembersPattern =
        @"[A-Za-z_][\w:]{1,39}|(?<=\.)[A-Za-z_][\w]{0,39}|\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\]|[0-9][0-9]?$";

    /// <summary>
    /// A regex pattern for matching each individual possible member portion of a full tag name.
    /// Used to split incoming string into member parts. 
    /// </summary>
    private const string MemberPattern =
        @"^(?:[A-Za-z_][\w+:]{0,39})?(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:[0-9][0-9]?)?$";

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
    /// Gets the tag or root portion of the <see cref="TagName"/> string.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The root portion of a given tag name is simply the beginning part of the tag name up to the first
    /// member separator character ('.'). For Module defined tags, this includes the colon separator. This represent
    /// the actual user configured tag name. The remaining tag name string (if any) represents the data type structure member.
    /// </para>
    /// <para>
    /// This value can be swapped out easily using <see cref="Rename"/> to return a new <see cref="TagName"/> with the
    /// newly specified root tag name value.
    /// </para>
    /// </remarks>
    /// <seealso cref="Operand"/>
    /// <seealso cref="Path"/>
    public string Root => Regex.Match(_tagName, TagPattern).Value;

    /// <summary>
    /// Gets the operand portion of the <see cref="TagName"/> value.
    /// </summary>
    /// <remarks>
    /// The operand of a tag name represents the part of the name after <see cref="Root"/>. This value will always be
    /// the full tag name value without the leading root name. The operand will include the leading '.' character.
    /// </remarks>
    /// <seealso cref="Path"/>
    public string Operand => !Root.IsEmpty() ? _tagName.Remove(0, Root.Length) : string.Empty;

    /// <summary>
    /// Gets the member path of the tag name value.
    /// </summary>
    /// <remarks>
    /// The path of a tag name represents a name relative to <see cref="Root"/>. The value will always be the full tag name
    /// without the leading root name. This is similar to <see cref="Operand"/>, except that is also removes any
    /// leading member separator character ('.'). 
    /// </remarks>
    /// <seealso cref="Operand"/>
    public string Path => Operand.StartsWith(".") ? Operand.Remove(0, 1) : Operand;

    /// <summary>
    /// Gets the final member name of the full tag name path.
    /// </summary>
    /// <remarks>
    /// This is 
    /// </remarks>
    public string Member => Members.Last();

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<string> Members => Regex.Matches(_tagName, MembersPattern).Select(m => m.Value);

    /// <summary>
    /// A number representing the depth, or number of members from the root tag name, of the current tag name value.
    /// </summary>
    /// <remarks>
    /// This value represents the number of members between the root tag and last member name (i.e. one less than
    /// the number of members in the tag name). This is helpful for filtering tag descendents. Note that array
    /// indices are also considered a member. For example, 'MyTag[1].Value' has a depth of 2 since '[1]' and 'Value'
    /// are descendent member names of the root tag 'MyTag' member.
    /// </remarks>
    public int Depth => Members.Count() - 1;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> value is empty.
    /// </summary>
    public bool IsEmpty => _tagName.IsEmpty();

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> is a valid representation of a tag name.
    /// </summary>
    public bool IsQualified => Regex.IsMatch(_tagName, TagNamePattern);

    /// <summary>
    /// Gets the static empty <see cref="TagName"/> value.
    /// </summary>
    public static TagName Empty => new(string.Empty);

    /// <summary>
    /// Combines a series of strings into a single <see cref="TagName"/> value, inserting member separator
    /// characters as needed.
    /// </summary>
    /// <param name="members">The series of strings that, in order, comprise the full tag name value.</param>
    /// <returns>A new <see cref="TagName"/>value that represents the combination of all provided member names.</returns>
    /// <exception cref="ArgumentException">If any provided member does not match the member pattern format.</exception>
    public static TagName Combine(params string[] members) => new(ConcatenateMembers(members.AsEnumerable()));

    /// <summary>
    /// Combines a collection of member names into a single <see cref="TagName"/> value.
    /// </summary>
    /// <param name="members">The collection of strings that represent the member names of the tag name value.</param>
    /// <returns>A new <see cref="TagName"/>A new <see cref="TagName"/> value that is the combination of all provided member names.</returns>
    /// <exception cref="ArgumentException">If a provided name does not match the member pattern format.</exception>
    public static TagName Combine(IEnumerable<string> members) => new(ConcatenateMembers(members));

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
    /// Returns a new tag name with the root <see cref="Root"/> value replaced with the provided string tag.
    /// </summary>
    /// <param name="tag">The new root tag name value to replace.</param>
    /// <returns>A new <see cref="TagName"/> with the new root tag value.</returns>
    /// <remarks>Note that this doesn't change the current tag name value, rather, returns a new object with the changed
    /// value. This ensures the immutability of <see cref="TagName"/>.</remarks>
    public TagName Rename(string tag) => Combine(tag, Operand);

    /// <inheritdoc />
    public override string ToString() => _tagName;

    /// <summary>
    /// Determines whether the specified <see cref="TagName"/> objects are equal using the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="first">A tag name object to compare.</param>
    /// <param name="second">A tag name object to compare.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns><c>true</c> if the tag name are equal according too the provided comparer; otherwise, false.</returns>
    public static bool Equals(TagName first, TagName second, IEqualityComparer<TagName> comparer) =>
        comparer.Equals(first, second);

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

    private static string ConcatenateMembers(IEnumerable<string> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            if (!(member.StartsWith(ArrayBracketStart) || member.StartsWith(MemberSeparator)) && builder.Length > 1)
                builder.Append(MemberSeparator);

            builder.Append(member);
        }

        return builder.ToString();
    }
}

/// <summary>
/// A <see cref="IEqualityComparer{T}"/> for the <see cref="TagName"/> object.
/// </summary>
public class TagNameComparer : IEqualityComparer<TagName>
{
    private TagNameComparer()
    {
    }

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the full <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer FullName { get; } = new();

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Root"/> property of the
    /// <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer BaseName { get; } = new BaseTagNameComparer();

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Path"/> property of the
    /// <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer PathName { get; } = new PathTagNameComparer();

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the last member of the <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer MemberName { get; } = new MemberTagNameComparer();

    /// <inheritdoc />
    public virtual bool Equals(TagName? x, TagName? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
        return x.Equals(y);
    }

    /// <inheritdoc />
    public virtual int GetHashCode(TagName obj) => obj.GetHashCode();


    private class BaseTagNameComparer : TagNameComparer
    {
        public override bool Equals(TagName? x, TagName? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return string.Equals(x.Root, y.Root, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode(TagName obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Root);
    }

    private class PathTagNameComparer : TagNameComparer
    {
        public override bool Equals(TagName? x, TagName? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return string.Equals(x.Path, y.Path, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode(TagName obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Path);
    }

    private class MemberTagNameComparer : TagNameComparer
    {
        public override bool Equals(TagName? x, TagName? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return string.Equals(x.Members.Last(), y.Members.Last(), StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode(TagName obj) =>
            StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Members.Last());
    }
}