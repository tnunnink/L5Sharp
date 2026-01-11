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
/// This value type class makes working with a string tag name easier by providing
/// methods for analyzing and breaking the tag name into constituent parts (members).
/// </remarks>
public sealed class TagName : IComparable<TagName>
{
    private const string ProgramPrefix = "Program:";
    private const char Separator = '.';
    private const char ArrayOpen = '[';
    private const char ArrayClose = ']';

    /// <summary>
    /// The internal storage for the full tag path represented by the <see cref="TagName"/> instance.
    /// This string encapsulates the hierarchical representation of a Logix tag, including its members,
    /// elements, and indices if applicable. Used for operations and methods related to tag analysis,
    /// validation, and manipulation.
    /// </summary>
    private readonly string _path;

    /// <summary>
    /// A regular expression pattern used for validating and analyzing Logix tag names.
    /// This pattern ensures compliance with specific naming conventions, including formats for
    /// base names, indexed elements, members, and operands within the hierarchical structure
    /// of Logix tag definitions.
    /// </summary>
    private static readonly Regex TagNamePattern = new(
        @"(?!\w*\()[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?",
        RegexOptions.Compiled
    );

    /// <summary>
    /// A regular expression pattern used to validate the base name of a Logix tag.
    /// The base name must start with an alphabetic character or underscore followed
    /// by up to 39 alphanumeric characters or colons. This ensures compliance with
    /// Logix naming conventions for base tag identifiers.
    /// </summary>
    private static readonly Regex BaseNamePattern =
        new(@"^[A-Za-z_][\w:]{0,39}$", RegexOptions.Compiled);

    /// <summary>
    /// 
    /// </summary>
    private static readonly Regex MemberNamePattern =
        new(@"^[A-Za-z_][\w]{0,39}$", RegexOptions.Compiled);

    /// <summary>
    /// 
    /// </summary>
    private static readonly Regex ReferenceIndexPattern =
        new(@"^\[[A-Za-z_][\w:]{0,39}\]$", RegexOptions.Compiled);

    /// <summary>
    /// 
    /// </summary>
    private static readonly Regex NumericIndexPattern =
        new(@"^\[[0-9]+(?:\,[0-9]+)?(?:\,[0-9]+)?\]$", RegexOptions.Compiled);

    /// <summary>
    /// Creates a new <see cref="TagName"/> object with the provided string tag name.
    /// </summary>
    /// <param name="name">The string that represents the tag name value.</param>
    /// <exception cref="ArgumentNullException">tagName is null.</exception>
    public TagName(string name)
    {
        _path = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Gets the full path of the Logix <c>TagName</c>, including all nested members and elements.
    /// This property represents the complete hierarchical representation of the tag name as a string and is used for
    /// equality and value comparison methods.
    /// </summary>
    // ReSharper disable once ConvertToAutoPropertyWhenPossible
    public string Path => _path;

    /// <summary>
    /// Gets the base portion of the tag name or an empty string if not defined.
    /// </summary>
    /// <remarks>
    /// The <c>Base</c> part of a tag name is the beginning part of the tag name up to the first member
    /// separator character (e.g., '.' or '['). For Module-defined tags, this includes the colon separator.
    /// </remarks>
    /// <seealso cref="Operand"/>
    /// <seealso cref="Member"/>
    public string Base => GetBase(_path);

    /// <summary>
    /// Gets the operand portion of the <see cref="TagName"/> value.
    /// </summary>
    /// <remarks>
    /// The <c>Operand</c> of a tag name represents the part of the name after <see cref="Base"/>. This value will always be
    /// the full tag name value without the leading root name. The operand will include the leading '.' character.
    /// </remarks>
    /// <seealso cref="Member"/>
    public string Operand => GetOperand(_path);

    /// <summary>
    /// Gets the member portion of the tag name if it contains hierarchical segments.
    /// </summary>
    /// <remarks>
    /// The <c>Member</c> is the part of the tag name following its <see cref="Base"/> and any leading separator (e.g., '.').
    /// This is similar to <see cref="Operand"/>, except that is also removes any leading member separator character ('.'). 
    /// </remarks>
    /// <seealso cref="Operand"/>
    public string Member => GetOperand(_path).TrimStart(Separator);

    /// <summary>
    /// Gets the member name, or the last member of <see cref="Members"/>, of the tag name value.
    /// </summary>
    /// <remarks>
    /// The <c>Member</c> of a tag name represents the last member name of the string. This is the string after the final
    /// member separator character.
    /// </remarks>
    public string Element => GetElement(_path);

    /// <summary>
    /// Returns a collection of string names representing each member of the full tag name value.
    /// </summary>
    /// <remarks>
    /// Each member of a tag name can be represented by a string, array bracket, or bit index value.
    /// For example, MyTag[1].MemberName.5 has 4 members.
    /// </remarks>
    public IEnumerable<string> Members => GetMembers(_path);

    /// <summary>
    /// A zero-based number representing the depth of the tag name. In other words, the number of members
    /// after the root portion of the tag name.
    /// </summary>
    /// <remarks>
    /// This value represents the number of members between the root name and the last member name (i.e., one less than
    /// the number of members in the tag name). This is helpful for filtering tag descendents. Note that array
    /// indices are also considered a member name. For example, 'MyTag[1].Value' has a depth of 2 since '[1]' and 'Value'
    /// are descendent member names of the root tag 'MyTag' member.
    /// </remarks>
    public int Depth => GetDepth(_path);

    /// <summary>
    /// Retrieves the scope level and container information of the tag name represented by this <see cref="TagName"/> instance.
    /// </summary>
    /// <remarks>
    /// The scope is evaluated based on whether the tag name path contains a program prefix. If a scope prefix is detected,
    /// the scope is identified as program-scoped, and the container is set to the program name. Otherwise, it is identified
    /// as controller-scoped with an empty container.
    /// </remarks>
    public Scope Scope => GetScope(_path);

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> value is empty.
    /// </summary>
    public bool IsEmpty => _path.IsEmpty();

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> is a valid representation of a tag name.
    /// </summary>
    public bool IsQualified => IsQualifiedTagName(_path);

    /// <summary>
    /// Gets the static empty <see cref="TagName"/> value.
    /// </summary>
    public static TagName Empty => new(string.Empty);

    /// <summary>
    /// Extracts all <see cref="TagName"/> instances from the specified text based on the predefined tag name pattern.
    /// </summary>
    /// <param name="text">
    /// The text from which tag names are to be extracted. This is typically an expression or
    /// rung of neutral text in which multiple tag names are embedded.
    /// </param>
    /// <returns>A collection of <see cref="TagName"/> objects representing the tags found within the input text.</returns>
    public static IEnumerable<TagName> Scrape(string text)
    {
        return TagNamePattern.Matches(text).Cast<Match>().Select(m => new TagName(m.Value));
    }

    /// <summary>
    /// Parses the provided string into a <see cref="TagName"/> value.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>A <see cref="TagName"/> representing the parsed value.</returns>
    public static TagName Parse(string value) => new(value);

    /// <summary>
    /// Determines if the provided string value is a valid tag name.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to test.</param>
    /// <returns><c>true</c> if the value is a valid and qualified tag name; Otherwise, <c>false</c>.</returns>
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

        if (right[0] == ArrayOpen || right[0] == Separator)
            return new TagName(left + right);

        return new TagName(left + Separator + right);
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

        return _path.Contains(tagName);
    }

    /// <summary>
    /// Determines whether the provided <see cref="TagName"/> object has the same base as the current <see cref="TagName"/>.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to compare with the base of the current instance.</param>
    /// <returns>
    /// <c>true</c> if the base of the current instance is equivalent to the base of the provided <see cref="TagName"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool HasBase(TagName tagName) => Base.IsEquivalent(tagName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public bool HasOperand(TagName tagName) => Operand.IsEquivalent(tagName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public bool HasMember(TagName tagName) => Member.IsEquivalent(tagName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    /// <returns></returns>
    public bool HasElement(TagName tagName) => Element.IsEquivalent(tagName);

    /// <inheritdoc />
    public int CompareTo(TagName? other)
    {
        return ReferenceEquals(this, other) ? 0
            : ReferenceEquals(null, other) ? 1
            : StringComparer.OrdinalIgnoreCase.Compare(_path, other._path);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            TagName other => StringComparer.OrdinalIgnoreCase.Equals(_path, other._path),
            string other => StringComparer.OrdinalIgnoreCase.Equals(_path, other),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_path);

    /// <inheritdoc />
    public override string ToString() => _path;

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
    public static implicit operator string(TagName tagName) => tagName._path;

    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="TagName"/> value.
    /// </summary>
    /// <param name="tagName">The <see cref="string"/> value to convert.</param>
    /// <returns>A new <see cref="TagName"/> value representing the value of the tag name.</returns>
    public static implicit operator TagName(string tagName) => new(tagName);

    /// <summary>
    /// Retrieves the base portion of a tag name from the specified path.
    /// Start by extracting the localized tag name value.
    /// Returns an empty string if the tag name is empty or starts with a separator.
    /// Otherwise, returns the portion of the tag name up to the first separator.
    /// </summary>
    private static string GetBase(string path)
    {
        var tagName = GetLocalTagName(path);

        if (tagName.IsEmpty() || tagName.StartsWith(Separator) || tagName.StartsWith(ArrayOpen))
            return string.Empty;

        var separator = tagName.IndexOfAny([Separator, ArrayOpen]);
        return separator > 0 ? tagName.Substring(0, separator) : tagName;
    }

    /// <summary>
    /// Retrieves the operand portion of a tag name from the provided path string.
    /// </summary>
    private static string GetOperand(string path)
    {
        var tagName = GetLocalTagName(path);
        var separator = tagName.IndexOfAny([Separator, ArrayOpen]);
        return separator >= 0 ? tagName.Substring(separator) : string.Empty;
    }

    /// <summary>
    /// Gets the last member of the tag name path, or the portion of the string from the last member separator to the
    /// end of the string. We are calling this the element.
    /// </summary>
    private static string GetElement(string path)
    {
        var tagName = GetLocalTagName(path);

        var lastSeparator = tagName.LastIndexOfAny([Separator, ArrayOpen]);
        if (lastSeparator < 0) return string.Empty;

        var length = tagName.Length - lastSeparator;
        return tagName.Substring(lastSeparator, length).TrimStart(Separator);
    }

    /// <summary>
    /// Gets each member by iterating the tag name string.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static IEnumerable<string> GetMembers(string path)
    {
        var tagName = GetLocalTagName(path);

        var start = 0;

        for (var i = 0; i < tagName.Length; i++)
        {
            if (tagName[i] == Separator || tagName[i] == ArrayOpen)
            {
                if (i > start) yield return tagName.Substring(start, i - start);
                start = i;
            }

            if (tagName[i] == ArrayClose)
            {
                yield return tagName.Substring(start, i - start + 1);
                start = i + 1;
            }
        }

        if (start < tagName.Length)
        {
            var remaining = tagName.Substring(start).TrimStart(Separator);
            if (!remaining.IsEmpty()) yield return remaining;
        }
    }

    /// <summary>
    /// Gets the zero-based depth or number of members between this member and the root.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static int GetDepth(string path)
    {
        var tagName = GetLocalTagName(path);
        return tagName.Count(c => c is Separator or ArrayOpen);
    }

    /// <summary>
    /// Determines if the tag name path contains a program prefix name and if so uses that to return a new
    /// <see cref="Scope"/> object to identify the scope of the tag name. If no program prefix is present, we always assume
    /// a controller scoped tag name.
    /// </summary>
    private static Scope GetScope(string path)
    {
        if (!path.StartsWith(ProgramPrefix, StringComparison.OrdinalIgnoreCase))
            return Scope.Controller;

        var memberIndex = path.IndexOf(Separator);
        var endIndex = memberIndex > 0 ? memberIndex : path.Length;
        var programName = path.Substring(ProgramPrefix.Length, endIndex - ProgramPrefix.Length);
        return Scope.Program(programName);
    }

    /// <summary>
    /// Gets the portion of the tag name without the leading program prefix if present. This is needed to analyze
    /// the remaining portion of the actual localized tag name value.
    /// </summary>
    private static string GetLocalTagName(string path)
    {
        if (!path.StartsWith(ProgramPrefix, StringComparison.OrdinalIgnoreCase))
            return path;

        var memberIndex = path.IndexOf(Separator);

        if (memberIndex == -1 || memberIndex == path.Length - 1)
            return string.Empty;

        return path.Substring(memberIndex + 1);
    }

    /// <summary>
    /// Handles combining an enumerable containing string member names into a single <see cref="TagName"/> value.
    /// </summary>
    private static string ConcatenateMembers(IEnumerable<string> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            if (!(member.StartsWith(ArrayOpen) || member.StartsWith(Separator)) && builder.Length > 1)
                builder.Append(Separator);

            builder.Append(member);
        }

        return builder.ToString();
    }

    private static bool IsQualifiedTagName(string value)
    {
        if (value.IsEmpty()) return false;

        var members = GetMembers(value).ToArray();
        if (members.Length == 0) return false;

        for (var i = 0; i < members.Length; i++)
        {
            var member = members[i];

            switch (i)
            {
                case 0 when !IsValidBase(member):
                case > 0 when member.StartsWith(ArrayOpen) && !IsValidIndex(member):
                case > 0 when char.IsLetter(member[0]) && !IsValidMember(member):
                    return false;
            }

            if (i == members.Length - 1 && char.IsDigit(member[0]) && !IsValidBitNumber(members[i]))
                return false;
        }

        return true;

        bool IsValidBase(string member) =>
            BaseNamePattern.IsMatch(member);

        bool IsValidMember(string member) =>
            MemberNamePattern.IsMatch(member);

        bool IsValidIndex(string member) =>
            NumericIndexPattern.IsMatch(member) || ReferenceIndexPattern.IsMatch(member);

        bool IsValidBitNumber(string member) =>
            int.TryParse(member, out var bit) && bit is >= 0 and <= 63;
    }
}

/// <summary>
/// Provides extension methods for working with <c>TagName</c> objects.
/// </summary>
/// <remarks>
/// This static class contains methods that extend the functionality of <c>TagName</c>,
/// offering convenience for handling tag name conversions and operations.
/// </remarks>
public static class TagNameExtensions
{
    /// <summary>
    /// Converts the specified string value to a <see cref="TagName"/> object.
    /// </summary>
    /// <param name="value">The string value to convert to a <see cref="TagName"/>.</param>
    /// <returns>A <see cref="TagName"/> object created from the specified string value.</returns>
    public static TagName ToTagName(this string value) => new(value);
}