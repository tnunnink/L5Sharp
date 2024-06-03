using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable ReplaceSubstringWithRangeIndexer we want to keep SubString for NET Standard

namespace L5Sharp.Core;

/// <summary>
/// A string wrapper representing a Logix <c>TagName</c>.
/// </summary>
/// <remarks>
/// This value type class make working with string tag name easier by providing
/// methods fo analyzing and breaking the tag name into constituent parts (members).
/// </remarks>
public sealed class TagName : IComparable<TagName>, IEquatable<TagName>, ILogixParsable<TagName>
{
    private readonly string _tagName;

    private const char MemberSeparator = '.';
    private const char ArrayOpenSeparator = '[';
    private const char ArrayCloseSeparator = ']';

    /// <summary>
    /// A regex pattern for a Logix tag name with starting and ending anchors.
    /// Use this pattern to match a string and ensure it is only a tag name an nothing else.
    /// </summary>
    public const string AnchorPattern =
        @"^[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?$";

    /// <summary>
    /// The regex pattern for Logix tag names without starting and ending anchors.
    /// This pattern also includes a negative lookahead for removing text prior to parenthesis (i.e. instruction keys)
    /// Use this pattern for tag names within text, such as longer
    /// </summary>
    public const string SearchPattern =
        @"(?!\w*\()[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?";

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
    /// Gets the root portion of the <see cref="TagName"/> string.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The root portion of a given tag name is simply the beginning part of the tag name up to the first
    /// member separator character '.' or '['. For Module defined tags, this includes the colon separator.
    /// </para>
    /// <para>
    /// This value can be swapped out easily using <see cref="Rename"/> to return a new <see cref="TagName"/> with the
    /// newly specified root tag name value.
    /// </para>
    /// </remarks>
    /// <seealso cref="Operand"/>
    /// <seealso cref="Path"/>
    public string Root => GetRoot(_tagName);

    /// <summary>
    /// Gets the operand portion of the <see cref="TagName"/> value.
    /// </summary>
    /// <remarks>
    /// The <c>Operand</c> of a tag name represents the part of the name after <see cref="Root"/>. This value will always be
    /// the full tag name value without the leading root name. The operand will include the leading '.' character.
    /// </remarks>
    /// <seealso cref="Path"/>
    public string Operand => !Root.IsEmpty() ? _tagName.Remove(0, Root.Length) : string.Empty;

    /// <summary>
    /// Gets the member path of the tag name value.
    /// </summary>
    /// <remarks>
    /// The <c>Path</c> of a tag name represents a name relative to <see cref="Root"/>. The value will always be the full tag name
    /// without the leading root name. This is similar to <see cref="Operand"/>, except that is also removes any
    /// leading member separator character ('.'). 
    /// </remarks>
    /// <seealso cref="Operand"/>
    public string Path => Operand.StartsWith(".") ? Operand.Remove(0, 1) : Operand;

    /// <summary>
    /// Gets the member name, or the last member of <see cref="Members"/>, of the tag name value.
    /// </summary>
    /// <remarks>
    /// The <c>Member</c> of a tag name represents the last member name of the string. This is the string after the final
    /// member separator character.
    /// </remarks>
    public string Member => GetMember(_tagName);

    /// <summary>
    /// Returns a collection of string names representing each individual member of the full tag name value.
    /// </summary>
    /// <remarks>
    /// Each member of a tag name can be represented by a string, array bracket, or bit index value.
    /// For example, MyTag[1].MemberName.5 has 4 members.
    /// </remarks>
    //public IEnumerable<string> Members => Regex.Matches(_tagName, MembersPattern).Select(m => m.Value);
    public IEnumerable<string> Members => GetMembers(_tagName);

    /// <summary>
    /// A zero-based number representing the depth of the tag name value. In other words, the number of members
    /// between this tag name and the root.
    /// </summary>
    /// <remarks>
    /// This value represents the number of members between the root tag and last member name (i.e. one less than
    /// the number of members in the tag name). This is helpful for filtering tag descendents. Note that array
    /// indices are also considered a member. For example, 'MyTag[1].Value' has a depth of 2 since '[1]' and 'Value'
    /// are descendent member names of the root tag 'MyTag' member.
    /// </remarks>
    public int Depth => GetDepth(_tagName);

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> value is empty.
    /// </summary>
    public bool IsEmpty => _tagName.IsEmpty();

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> is a valid representation of a tag name.
    /// </summary>
    public bool IsQualified => IsQualifiedTagName(_tagName);

    /// <summary>
    /// Gets the static empty <see cref="TagName"/> value.
    /// </summary>
    public static TagName Empty => new(string.Empty);

    /// <summary>
    /// Parses the provided string into a <see cref="TagName"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="TagName"/> representing the parsed value.</returns>
    public static TagName Parse(string value) => new(value);

    /// <summary>
    /// Tries to parse the provided string into a <see cref="TagName"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="TagName"/> representing the parsed value if successful; Otherwise, <c>null</c>.</returns>
    public static TagName? TryParse(string? value) => value is not null ? new TagName(value) : null;

    /// <summary>
    /// Determines if the provided string value is a valid tag name.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to test.</param>
    /// <returns><c>true</c> if the value is a valid qualified tag name; Otherwise, <c>false</c>.</returns>
    public static bool IsTag(string value) => IsQualifiedTagName(value);

    /// <summary>
    /// Concatenates two strings to produce a new <see cref="TagName"/> value. This method will also insert the '.'
    /// member separator character if not found at the beginning of <c>right</c>.
    /// </summary>
    /// <param name="left">The first or left side of the tag name to concatenate.</param>
    /// <param name="right">The second or right side of the tag name to concatenate.</param>
    /// <returns>A <see cref="TagName"/> representing the combination of <c>left</c> and <c>right</c>.</returns>
    /// <remarks>
    /// This method would be more performant than <see cref="Combine(string[])"/>, assuming there are just
    /// two strings to join together, as it does not iterate a collection and build a string with a string builder
    /// class. This method simply joins to strings using a string format syntax.
    /// </remarks>
    public static TagName Concat(string left, string right)
    {
        if (string.IsNullOrEmpty(right)) return left;

        if (right[0] == ArrayOpenSeparator || right[0] == MemberSeparator)
            return new TagName(left + right);

        return new TagName(left + MemberSeparator + right);
    }

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
    /// Determines whether the provided <paramref name="tagName"/> value is equivalent to the <see cref="Root"/> of
    /// the current instance.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to compare the Root with.</param>
    /// <returns>
    /// <c>true</c> if the root of the current instance is equivalent to the value of <paramref name="tagName"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This equivalency check uses a ordinal comparer and is case insensitive. Note that it is comparing the
    /// current Root value to the full value of the provided tag name.
    /// </remarks>
    public bool HasRoot(TagName tagName) => Root.IsEquivalent(tagName);

    /// <summary>
    /// Determines whether the provided <paramref name="tagName"/> value is equivalent to the <see cref="Operand"/> of
    /// the current instance.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to compare the Operand with.</param>
    /// <returns>
    /// <c>true</c> if the operand of the current instance is equivalent to the value of <paramref name="tagName"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This equivalency check uses a ordinal comparer and is case insensitive. Note that it is comparing the
    /// current Operand value to the full value of the provided tag name.
    /// </remarks>
    public bool HasOperand(TagName tagName) => Operand.IsEquivalent(tagName);

    /// <summary>
    /// Determines whether the provided <paramref name="tagName"/> value is equivalent to the <see cref="Path"/> of
    /// the current instance.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to compare the Path with.</param>
    /// <returns>
    /// <c>true</c> if the Path of the current instance is equivalent to the value of <paramref name="tagName"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This equivalency check uses a ordinal comparer and is case insensitive. Note that it is comparing the
    /// current Path value to the full value of the provided tag name.
    /// </remarks>
    public bool HasPath(TagName tagName) => Path.IsEquivalent(tagName);

    /// <summary>
    /// Determines whether the provided <paramref name="tagName"/> value is equivalent to the <see cref="Member"/> of
    /// the current instance.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to compare the Member with.</param>
    /// <returns>
    /// <c>true</c> if the Member of the current instance is equivalent to the value of <paramref name="tagName"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This equivalency check uses a ordinal comparer and is case insensitive. Note that it is comparing the
    /// current Member value to the full value of the provided tag name.
    /// </remarks>
    public bool HasMember(TagName tagName) => Member.IsEquivalent(tagName);
    
    /// <summary>
    /// Returns a new tag name with the root <see cref="Root"/> value replaced with the provided string tag.
    /// </summary>
    /// <param name="tag">The new root tag name value to replace.</param>
    /// <returns>A new <see cref="TagName"/> with the new root tag value.</returns>
    /// <remarks>Note that this doesn't change the current tag name value, rather, returns a new object with the changed
    /// value. This ensures the immutability of <see cref="TagName"/>.</remarks>
    public TagName Rename(string tag) => Combine(tag, Operand);

    /// <summary>
    /// Determines whether the specified <see cref="TagName"/> objects are equal using the specified <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="first">A tag name object to compare.</param>
    /// <param name="second">A tag name object to compare.</param>
    /// <param name="comparer">The equality comparer to use for comparison.</param>
    /// <returns><c>true</c> if the tag name are equal according too the provided comparer; otherwise, false.</returns>
    /// <remarks>Use the prebuilt <see cref="TagNameComparer"/> class for several predefined comparer objects.</remarks>
    public static bool Equals(TagName first, TagName second, IEqualityComparer<TagName> comparer) =>
        comparer.Equals(first, second);

    /// <inheritdoc />
    public bool Equals(TagName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || StringComparer.OrdinalIgnoreCase.Equals(_tagName, other._tagName);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            TagName other => StringComparer.OrdinalIgnoreCase.Equals(_tagName, other._tagName),
            string other => StringComparer.OrdinalIgnoreCase.Equals(_tagName, other),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_tagName);
    
    /// <inheritdoc />
    public int CompareTo(TagName? other)
    {
        return ReferenceEquals(this, other) ? 0
            : ReferenceEquals(null, other) ? 1
            : StringComparer.OrdinalIgnoreCase.Compare(_tagName, other._tagName);
    }

    /// <inheritdoc />
    public override string ToString() => _tagName;

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

    /// <summary>
    /// Converts a <see cref="TagName"/> to a <see cref="string"/> value.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> value to convert.</param>
    /// <returns>A new <see cref="string"/> value representing the value of the tag name.</returns>
    public static implicit operator string(TagName? tagName) => tagName is not null ? tagName._tagName : string.Empty;

    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="TagName"/> value.
    /// </summary>
    /// <param name="tagName">The <see cref="string"/> value to convert.</param>
    /// <returns>A new <see cref="TagName"/> value representing the value of the tag name.</returns>
    public static implicit operator TagName(string? tagName) => tagName is not null ? new TagName(tagName) : Empty;

    /// <summary>
    /// Gets the first member of the tag name, or the portion of the string up to the first/next member separator.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static string GetRoot(string tagName)
    {
        if (tagName.IsEmpty() || tagName.StartsWith(MemberSeparator)) return string.Empty;
        if (tagName.StartsWith(ArrayOpenSeparator))
        {
            return tagName.Substring(0, tagName.IndexOf(ArrayCloseSeparator));
        }

        var index = tagName.IndexOfAny([MemberSeparator, ArrayOpenSeparator]);
        return index > 0 ? tagName.Substring(0, index) : tagName;
    }

    /// <summary>
    /// Gets the last member of the tag name, or the portion of the string from the end to the last member separator.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static string GetMember(string tagName)
    {
        var index = tagName.LastIndexOfAny([MemberSeparator, ArrayOpenSeparator]);
        var length = tagName.Length - index;
        return index >= 0 ? tagName.Substring(index, length).TrimStart(MemberSeparator) : tagName;
    }

    /// <summary>
    /// Gets each member by iterating the tag name string.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static IEnumerable<string> GetMembers(string tagName) =>
        NormalizeDelimiter(tagName).Split(MemberSeparator, StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    /// Gets the zero-based depth or number of members between this member and the root.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static int GetDepth(string tagName) => tagName.Count(c => c is MemberSeparator or ArrayOpenSeparator);

    /// <summary>
    /// Handles combining an enumerable containing string member names into a single <see cref="TagName"/> value.
    /// </summary>
    private static string ConcatenateMembers(IEnumerable<string> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            if (!(member.StartsWith(ArrayOpenSeparator) || member.StartsWith(MemberSeparator)) && builder.Length > 1)
                builder.Append(MemberSeparator);

            builder.Append(member);
        }

        return builder.ToString();
    }

    private static bool IsQualifiedTagName(string value)
    {
        if (value.IsEmpty()) return false;

        var normalized = NormalizeDelimiter(value);
        var members = normalized.Split(MemberSeparator).ToList();

        for (var i = 0; i < members.Count; i++)
        {
            switch (i)
            {
                case 0 when !IsValidRoot(members[i]):
                case > 0 when members[i].StartsWith(ArrayOpenSeparator) && !IsValidIndex(members[i]):
                case > 0 when char.IsLetter(members[i][0]) && !IsValidMember(members[i]):
                    return false;
            }

            if (i == members.Count - 1 && char.IsDigit(members[i][0]) && !IsValidBit(members[i])) return false;
        }

        return true;

        bool IsValidRoot(string member) => Regex.IsMatch(member, @"^[A-Za-a_][\w:]{0,39}$");

        bool IsValidMember(string member) => Regex.IsMatch(member, @"^[A-Za-a_][\w]{0,39}$");

        bool IsValidIndex(string member) => Regex.IsMatch(member, @"^\[[0-9]+(?:\,[0-9]+)?(?:\,[0-9]+)?\]$");

        bool IsValidBit(string member) =>
            member.All(char.IsDigit) && int.TryParse(member, out var bit) && bit is >= 0 and <= 63;
    }

    private static string NormalizeDelimiter(string value)
    {
        var builder = new StringBuilder();

        foreach (var character in value)
        {
            if (character == ArrayOpenSeparator)
                builder.Append(MemberSeparator);
            builder.Append(character);
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
    /// An <see cref="IEqualityComparer{T}"/> that compares the full qualified <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer Qualified { get; } = new();

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Root"/> property of the
    /// <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer Root { get; } = new RootTagNameComparer();

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Path"/> property of the
    /// <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer Path { get; } = new PathTagNameComparer();

    /// <summary>
    /// An <see cref="IEqualityComparer{T}"/> that compares the <see cref="TagName.Member"/> property of the
    /// <see cref="TagName"/> value.
    /// </summary>
    public static TagNameComparer Member { get; } = new MemberTagNameComparer();

    /// <inheritdoc />
    public virtual bool Equals(TagName? x, TagName? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
        return string.Equals(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public virtual int GetHashCode(TagName obj) => obj.GetHashCode();


    private class RootTagNameComparer : TagNameComparer
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
            return string.Equals(x.Member, y.Member, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode(TagName obj) => StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Member);
    }
}