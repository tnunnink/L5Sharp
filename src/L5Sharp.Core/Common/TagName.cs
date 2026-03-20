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
    /// A compiled regular expression used to define the naming rules for Logix tag members.
    /// Validates that a member name starts with an alphabetic character or an underscore,
    /// followed by up to 39 alphanumeric characters or underscores.
    /// Ensures compliance with naming conventions for tag components within a Logix environment.
    /// </summary>
    private static readonly Regex MemberNamePattern =
        new(@"^[A-Za-z_][\w]{0,39}$", RegexOptions.Compiled);

    /// <summary>
    /// A regular expression pattern used to validate and match reference-style index notations
    /// in Logix tag names. The pattern ensures the string starts and ends with square brackets
    /// and contains valid characters, including alphabetic characters, underscores, colons,
    /// and alphanumeric values, with a maximum length of 41 characters (excluding brackets).
    /// This is important for parsing and verifying valid reference indices in tag operations.
    /// </summary>
    private static readonly Regex ReferenceIndexPattern =
        new(@"^\[[A-Za-z_][\w:]{0,39}\]$", RegexOptions.Compiled);

    /// <summary>
    /// A regular expression pattern used to match numeric index notations within a Logix tag name.
    /// The pattern is designed to identify indices enclosed in square brackets, such as single-dimensional
    /// (e.g., <c>[0]</c>), multidimensional (e.g., <c>[0,1]</c>, <c>[0,1,2]</c>), and properly formatted numeric indices.
    /// Used internally to validate and extract numeric indices during tag operations.
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
    /// Gets the local portion of the tag name, excluding any program scope prefix.
    /// </summary>
    /// <remarks>
    /// If the tag name is program-scoped (begins with "Program:"), this property returns
    /// only the tag name portion after the program prefix and separator. For controller-scoped
    /// tags, this property returns the same value as <see cref="Path"/>.
    /// For example, "Program:MyProgram.MyTag" would return "MyTag".
    /// </remarks>
    public string LocalPath => GetLocalTagName(_path);

    /// <summary>
    /// Gets the base portion of the tag name or an empty string if not defined.
    /// </summary>
    /// <remarks>
    /// The <c>Base</c> part of a tag name is the beginning part of the tag name up to the first member
    /// separator character (e.g., '.' or '['). For Module-defined tags, this includes the colon separator.
    /// </remarks>
    public string Base => GetBase(_path);

    /// <summary>
    /// Represents the operand portion of the tag path, including all members, elements, and indices,
    /// excluding the base portion of the tag path. This string begins with the separator used between
    /// the base and later members, providing a detailed representation of the tag structure beyond the base.
    /// Used for operations requiring deeper levels of the tag hierarchy.
    /// </summary>
    public string Operand => GetOperand(_path);

    /// <summary>
    /// Gets the portion of the tag name that represents the member path.
    /// </summary>
    /// /// <remarks>
    /// The member is derived by stripping the base tag name and any leading separators
    /// from the operand. This property encapsulates the hierarchical component
    /// beneath the base, including nested structures or indices when applicable.
    /// </remarks>
    public string Member => GetOperand(_path).TrimStart(Separator);

    /// <summary>
    /// Retrieves the terminal component or final segment of the tag path represented by the instance.
    /// </summary>
    /// <remarks>
    /// This property isolates the most specific portion of the logical reference, such as the last member,
    /// submember, or indexed element in a hierarchical path structure.
    /// It is particularly useful for identifying the immediate target or endpoint in a tag hierarchy.
    /// </remarks>
    public string Element => GetElement(_path);

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
    /// Retrieves all member components of the tag name path in hierarchical order.
    /// </summary>
    /// <returns>
    /// An enumerable collection of strings representing each member component of the tag name,
    /// including base name, member names, array indices, and bit operands.
    /// </returns>
    /// <remarks>
    /// This method breaks down the complete tag path into its constituent parts, making it useful
    /// for analyzing tag structure and hierarchy. For example, "MyTag[1].Value.12" would return
    /// ["MyTag", "[1]", "Value", "12"].
    /// </remarks>
    public IEnumerable<string> Members() => GetMembers(_path);

    /// <summary>
    /// Retrieves member components of the tag name path up to a specified depth.
    /// </summary>
    /// <param name="count">
    /// The maximum number of member components to retrieve. A value of 0 retrieves all members.
    /// </param>
    /// <returns>
    /// An enumerable collection of strings representing each member component up to the specified depth.
    /// </returns>
    /// <remarks>
    /// This method is useful for limiting the scope of tag name analysis to a specific hierarchical level.
    /// For instance, calling Members(2) on "MyTag[1].Value.12" would return ["MyTag", "[1]"].
    /// The depth parameter allows for efficient filtering of tag descendants without processing the entire path.
    /// </remarks>
    public IEnumerable<string> Members(int count) => GetMembers(_path, count);

    /// <summary>
    /// Determines whether the current tag name is a direct member (child) of the specified parent tag name.
    /// A tag is considered a member of a parent if all the parent's members match the beginning of this tag's members,
    /// and this tag has at least one additional member beyond the parent's path.
    /// </summary>
    /// <param name="parent">The parent tag name to check against.</param>
    /// <returns>
    /// <c>true</c> if the current tag name is a direct or nested member of the specified parent and has additional members beyond the parent's path;
    /// otherwise, <c>false</c>. Returns <c>false</c> if either tag name is empty.
    /// </returns>
    /// <remarks>
    /// This method performs a case-insensitive comparison of tag name members. For example, if the parent is "MyTag"
    /// and this instance is "MyTag.Member", the method returns <c>true</c>. However, if this instance is "MyTag",
    /// the method returns <c>false</c> because there are no additional members.
    /// </remarks>
    public bool IsMemberOf(TagName parent)
    {
        if (IsEmpty || parent.IsEmpty) return false;

        using var eThis = Members().GetEnumerator();
        using var eParent = parent.Members().GetEnumerator();

        while (eParent.MoveNext())
        {
            if (!eThis.MoveNext()) return false;
            if (!StringComparer.OrdinalIgnoreCase.Equals(eThis.Current, eParent.Current)) return false;
        }

        // Has to have at least one more member after the parent.
        return eThis.MoveNext();
    }

    /// <summary>
    /// Determines whether the current tag name is a member of the specified parent tag name or is equal to it.
    /// A tag is considered a member or self if all the parent's members match the beginning of this tag's members.
    /// Unlike <see cref="IsMemberOf"/>, this method returns <c>true</c> even when the tag names are identical.
    /// </summary>
    /// <param name="parent">The parent tag name to check against.</param>
    /// <returns>
    /// <c>true</c> if the current tag name is a member of the specified parent or is equal to it;
    /// otherwise, <c>false</c>. Returns <c>false</c> if either tag name is empty.
    /// </returns>
    /// <remarks>
    /// This method performs a case-insensitive comparison of tag name members. For example, if the parent is "MyTag"
    /// and this instance is "MyTag", the method returns <c>true</c>. If this instance is "MyTag.Member", it also
    /// returns <c>true</c>. This is useful for hierarchical filtering where you want to include the parent itself
    /// in addition to its descendants.
    /// </remarks>
    public bool IsMemberOrSelf(TagName parent)
    {
        if (IsEmpty || parent.IsEmpty) return false;

        using var eThis = Members().GetEnumerator();
        using var eParent = parent.Members().GetEnumerator();

        while (eParent.MoveNext())
        {
            if (!eThis.MoveNext()) return false;
            if (!StringComparer.OrdinalIgnoreCase.Equals(eThis.Current, eParent.Current)) return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether the current <see cref="TagName"/> contains the specified <paramref name="tagName"/>.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> to check for containment.</param>
    /// <returns>
    /// <c>true</c> if the current <see cref="TagName"/> contains the specified <paramref name="tagName"/>;
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="tagName"/> is <c>null</c>.</exception>
    public bool Contains(TagName tagName)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        return _path.IndexOf(tagName._path, StringComparison.OrdinalIgnoreCase) >= 0;
    }
    
    /// <summary>
    /// Creates a new <see cref="TagName"/> by replacing the base portion of the current tag name
    /// with the specified base name while preserving the operand (member path, indices, and elements).
    /// </summary>
    /// <param name="baseName">The new base name to use for the tag. This will replace the current base portion
    /// while maintaining all members, array indices, and other path components that follow.</param>
    /// <returns>A new <see cref="TagName"/> instance with the updated base name and the original operand.</returns>
    /// <remarks>
    /// This method is useful when you need to change the root portion of a tag reference while keeping
    /// the structural path intact. For example, renaming "OldTag.Member[1].Value" with base name "NewTag"
    /// would result in "NewTag.Member[1].Value".
    /// </remarks>
    public TagName Rename(string baseName) => Combine(baseName, Operand);

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
    /// two strings to join together, as it does not iterate a collection or use a string builder class.
    /// This method simply concatenates to strings.
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

        if (tagName.IsEmpty() || tagName.StartsWith(Separator))
            return string.Empty;

        var end = tagName.StartsWith(ArrayOpen)
            ? tagName.IndexOf(ArrayClose) + 1
            : tagName.IndexOfAny([Separator, ArrayOpen]);

        return end > 0 ? tagName.Substring(0, end) : tagName;
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
    private static IEnumerable<string> GetMembers(string path, int count = 0)
    {
        // Only parse the local tag name string.
        var tagName = GetLocalTagName(path);
        var start = 0;
        var depth = 0;

        for (var i = 0; i < tagName.Length; i++)
        {
            var current = tagName[i];

            switch (current)
            {
                case Separator or ArrayOpen when i > start:
                    yield return tagName.Substring(start, i - start);
                    depth++;
                    start = current is ArrayOpen ? i : i + 1;
                    break;
                case ArrayClose when i > start:
                    yield return tagName.Substring(start, i - start + 1);
                    start = i + 2;
                    break;
            }

            if (count > 0 && depth == count)
                yield break;
        }

        if (start < tagName.Length)
            yield return tagName.Substring(start);
    }

    /// <summary>
    /// Gets the zero-based depth or number of members between this member and the root.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private static int GetDepth(string path)
    {
        var tagName = GetLocalTagName(path);

        if (tagName.IsEmpty())
            return 0;

        //We can't count the first member if this tag name starts with a separator.
        return tagName.Substring(1).Count(c => c is Separator or ArrayOpen);
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
public static class TagNameExtensions
{
    /// <summary>
    /// Converts the specified string value to a <see cref="TagName"/> object.
    /// </summary>
    /// <param name="value">The string value to convert to a <see cref="TagName"/>.</param>
    /// <returns>A <see cref="TagName"/> object created from the specified string value.</returns>
    public static TagName ToTagName(this string value) => new(value);
}