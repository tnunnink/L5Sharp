using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
    /// The underlying string representation of a Logix tag name encapsulated within the TagName class.
    /// This private field is used to store and manage the full path or name of a tag, ensuring that
    /// tag operations such as parsing, validation, and formatting are performed on a consistent data structure.
    /// </summary>
    private readonly string _tagName;

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
        _tagName = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Gets the full path of the Logix <c>TagName</c>, including all nested members and elements.
    /// This property represents the complete hierarchical representation of the tag name as a string and is used for
    /// equality and value comparison methods.
    /// </summary>
    // ReSharper disable once ConvertToAutoPropertyWhenPossible
    public string FullPath => _tagName;

    /// <summary>
    /// Gets the local portion of the tag name, excluding any program scope prefix.
    /// </summary>
    /// <remarks>
    /// If the tag name is program-scoped (begins with "Program:"), this property returns
    /// only the tag name portion after the program prefix and separator. For controller-scoped
    /// tags, this property returns the same value as <see cref="FullPath"/>.
    /// For example, "Program:MyProgram.MyTag" would return "MyTag".
    /// </remarks>
    public string LocalPath => GetLocalTagName();

    /// <summary>
    /// Represents the hierarchical portion of the tag name that excludes the base tag name and is stripped of the leading separator.
    /// </summary>
    /// <remarks>
    /// This property returns the remaining portion of the tag name, starting from the first member or array notation,
    /// if applicable, after the base tag. Commonly used for accessing specific levels of a tag's hierarchy
    /// within complex or structured tag definitions. Returns <c>null</c> if the tag has no member path.
    /// </remarks>
    public string? MemberPath => GetMemberPath();

    /// <summary>
    /// Represents the portion of the tag name that follows the base tag name, containing all members,
    /// elements, and indices if present, including the leading separator for context.
    /// The relative path provides a hierarchical breakdown of the tag's structure beyond its base name.
    /// Returns <c>null</c> if the tag has no relative path.
    /// </summary>
    public string? RelativePath => GetRelativePath();

    /// <summary>
    /// The base name of the tag represented by the <see cref="TagName"/> instance.
    /// This string corresponds to the root tag name, excluding any member accessor,
    /// indices, or hierarchy information. It is derived from the full path and is used
    /// in scenarios where only the top-level tag name is required.
    /// </summary>
    public string BaseName => GetBaseName();

    /// <summary>
    /// Gets the immediate member name of the tag represented by the current <see cref="TagName"/> instance.
    /// The member name refers to the most specific, non-hierarchical segment of the tag, typically
    /// the final component following any hierarchical path or indexing.
    /// If the tag does not include any hierarchical or member path, the value will be <c>null</c>.
    /// </summary>
    public string? MemberName => GetMemberName();

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
    public int Depth => GetDepth();

    /// <summary>
    /// Retrieves the scope level and container information of the tag name represented by this <see cref="TagName"/> instance.
    /// </summary>
    /// <remarks>
    /// The scope is evaluated based on whether the tag name path contains a program prefix. If a scope prefix is detected,
    /// the scope is identified as program-scoped, and the container is set to the program name. Otherwise, it is identified
    /// as controller-scoped with an empty container.
    /// </remarks>
    public Scope Scope => GetScope();

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> value is empty.
    /// </summary>
    public bool IsEmpty => _tagName.IsEmpty();

    /// <summary>
    /// Gets a value indicating whether the current <see cref="TagName"/> is a valid representation of a tag name.
    /// </summary>
    public bool IsQualified => IsQualifiedTagName();

    /// <summary>
    /// Indicates whether the tag name is relative.
    /// A tag name is considered relative if it begins with a separator or array-opening character,
    /// suggesting it is not a fully qualified path but instead references a member or element relative
    /// to a parent context.
    /// </summary>
    public bool IsRelative => _tagName.IndexOfAny([Separator, ArrayOpen]) == 0;

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
    public IEnumerable<string> Members() => GetMembers();

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
    public IEnumerable<string> Members(int count) => GetMembers(count);

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

        return _tagName.IndexOf(tagName._tagName, StringComparison.OrdinalIgnoreCase) >= 0;
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
    /// the structural path intact. For example, renaming "OldTag.Member[1].Value" with the base name "NewTag"
    /// would result in "NewTag.Member[1].Value".
    /// </remarks>
    public TagName Rename(string baseName)
    {
        return baseName.ToTagName().Append(MemberPath);
    }

    /// <summary>
    /// Appends a specified member to the current <see cref="TagName"/> and returns a new updated instance.
    /// </summary>
    /// <param name="member">The member string to append. This can be null or empty, in which case the current <see cref="TagName"/> is returned unchanged.</param>
    /// <returns>A new <see cref="TagName"/> instance with the specified member appended to the current tag name.</returns>
    /// <remarks>
    /// This method automatically inserts a dot (.) separator before the member name unless the member already begins
    /// with a separator character (either '.' for member access or '[' for array indexing). For example, appending
    /// "Value" to "MyTag" results in "MyTag.Value", while appending "[0]" results in "MyTag[0]" without an extra separator.
    /// </remarks>
    public TagName Append(string? member)
    {
        if (member is null || member.IsEmpty())
            return new TagName(_tagName);

        if (member[0] is '[' or '.')
            return new TagName(_tagName + member);

        return new TagName(_tagName + '.' + member);
    }

    /// <inheritdoc />
    public int CompareTo(TagName? other)
    {
        return ReferenceEquals(this, other) ? 0
            : ReferenceEquals(null, other) ? 1
            : StringComparer.OrdinalIgnoreCase.Compare(_tagName, other._tagName);
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
    public override string ToString() => _tagName;

    /// <summary>
    /// Determines if the provided string value is a valid tag name.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to test.</param>
    /// <returns><c>true</c> if the value is a valid and qualified tag name; Otherwise, <c>false</c>.</returns>
    public static bool IsTag(string? value) => value is not null && value.ToTagName().IsQualified;

    /// <summary>
    /// Combines a series of strings into a single <see cref="TagName"/> value, inserting member separator
    /// characters as needed.
    /// </summary>
    /// <param name="members">The series of strings that, in order, comprise the full tag name value.</param>
    /// <returns>A new <see cref="TagName"/>value that represents the combination of all provided member names.</returns>
    /// <exception cref="ArgumentException">If any provided member does not match the member pattern format.</exception>
    public static TagName Combine(params string?[] members) => new(ConcatenateMembers(members.AsEnumerable()));

    /// <summary>
    /// Combines a collection of member names into a single <see cref="TagName"/> value.
    /// </summary>
    /// <param name="members">The collection of strings that represent the member names of the tag name value.</param>
    /// <returns>A new <see cref="TagName"/>A new <see cref="TagName"/> value that is the combination of all provided member names.</returns>
    /// <exception cref="ArgumentException">If a provided name does not match the member pattern format.</exception>
    public static TagName Combine(IEnumerable<string?> members) => new(ConcatenateMembers(members));

    /// <summary>
    /// Parses a <see cref="NeutralText"/> object into a collection of <see cref="TagName"/> objects based on its tokenized content.
    /// </summary>
    /// <param name="text">The <see cref="NeutralText"/> object to parse into tag names.</param>
    /// <returns>A collection of <see cref="TagName"/> objects extracted from the given <see cref="NeutralText"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the provided <paramref name="text"/> is null.</exception>
    public static IEnumerable<TagName> Parse(NeutralText text)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text));

        using var stream = new NeutralStream(text);
        var tagNames = new List<TagName>();
        var tagCache = new Dictionary<int, TagName>();
        var depth = 0;

        while (stream.Read(out var token))
        {
            // This is the termination point. When we hit a non-tag token, we can add the constructed tag names and reset.
            // Advance until we find the beginning of a potentially new tag name string.
            if (!IsTagToken(token.Type))
            {
                // We need to skip any identifier that precedes an open parenthesis because this
                // represents an instruction or a function and not a tag name.
                if (tagCache.Count > 0 && token.Type != TokenType.OpenParen)
                    tagNames.AddRange(tagCache.Values);

                tagCache.Clear();
                depth = 0;
                continue;
            }

            // Handle array bracket depth to track nested tag name.
            // We also need to ignore any array brackets while not "inside" of a tag name,
            // as these likely represent rung branch tokens.
            if (tagCache.Count > 0 && token.Type == TokenType.OpenBracket) depth++;
            if (tagCache.Count > 0 && token.Type == TokenType.CloseBracket) depth--;

            // Build up a tag name path for each level/depth in the stream.
            for (var i = 0; i <= depth; i++)
            {
                if (tagCache.TryGetValue(i, out var current))
                {
                    tagCache[i] = current + token.Value;
                    continue;
                }

                // Only add nested tags if the token is an identifier (nested tag name).
                // We don't care about literal index or commas.
                if (token.Type == TokenType.Identifier)
                {
                    tagCache[i] = token.Value;
                }
            }
        }

        return tagNames;
    }

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
    public static implicit operator string(TagName tagName) => tagName._tagName;

    /// <summary>
    /// Converts a <see cref="string"/> to a <see cref="TagName"/> value.
    /// </summary>
    /// <param name="tagName">The <see cref="string"/> value to convert.</param>
    /// <returns>A new <see cref="TagName"/> value representing the value of the tag name.</returns>
    public static implicit operator TagName(string? tagName) => tagName is null ? Empty : new TagName(tagName);

    /// <summary>
    /// Retrieves the relative path portion of a tag name by parsing the associated token stream.
    /// </summary>
    /// <returns>
    /// A string representing the relative path of the tag name, or null if no relative path is found.
    /// </returns>
    private string? GetRelativePath()
    {
        var tagName = GetLocalTagName();
        var separator = tagName.IndexOfAny([Separator, ArrayOpen]);
        return separator >= 0 ? tagName.Substring(separator) : null;
    }

    /// <summary>
    /// Retrieves the member path portion of a tag name, excluding the base name and any leading separator.
    /// This represents the hierarchical structure after the base tag, including members, array indices, and elements.
    /// </summary>
    /// <returns>
    /// A string representing the member path of the tag name without the leading separator, or null if no member path exists.
    /// </returns>
    private string? GetMemberPath()
    {
        var tagName = GetLocalTagName();

        var separator = tagName.IndexOfAny([Separator, ArrayOpen]);
        if (separator < 0) return null;

        var startIndex = tagName[separator] is Separator ? separator + 1 : separator;
        return tagName.Substring(startIndex);
    }

    /// <summary>
    /// Retrieves the base portion of a tag name from the specified path.
    /// Start by extracting the localized tag name value.
    /// Returns an empty string if the tag name is empty or starts with a separator.
    /// Otherwise, returns the portion of the tag name up to the first separator.
    /// </summary>
    private string GetBaseName()
    {
        var tagName = GetLocalTagName();

        if (tagName.IsEmpty() || tagName.StartsWith(Separator) || tagName.StartsWith(ArrayOpen))
            return string.Empty;

        var end = tagName.IndexOfAny([Separator, ArrayOpen]);
        return end > 0 ? tagName.Substring(0, end) : tagName;
    }

    /// <summary>
    /// Gets the last member of the tag name path, or the portion of the string from the last member separator to the
    /// end of the string. We are calling this the element.
    /// </summary>
    private string? GetMemberName()
    {
        var tagName = GetLocalTagName();

        var lastSeparator = tagName.LastIndexOfAny([Separator, ArrayOpen]);
        if (lastSeparator < 0) return null;

        var startIndex = tagName[lastSeparator] is Separator ? lastSeparator + 1 : lastSeparator;
        return tagName.Substring(startIndex);
    }

    /// <summary>
    /// Gets each member by iterating the tag name string.
    /// We are no longer using regex to make this as efficient as possible since there could realistically be millions
    /// of tag names this can get called on.
    /// </summary>
    private IEnumerable<string> GetMembers(int count = 0)
    {
        var tagName = GetLocalTagName();
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
    /// Calculates the depth of the tag hierarchy represented by this instance.
    /// </summary>
    /// <returns>The number of hierarchical levels (or depth) in the tag name.</returns>
    private int GetDepth()
    {
        var tagName = GetLocalTagName();

        if (tagName.IsEmpty())
            return 0;

        //We can't count the first member if this tag name starts with a separator.
        return tagName.Substring(1).Count(c => c is Separator or ArrayOpen);
    }

    /// <summary>
    /// Determines if the tag name path contains a program prefix name and if so uses that to return a new
    /// <see cref="Scope"/> object to identify the scope of the tag name. If no program prefix is present, we always assume
    /// a controller-scoped tag name.
    /// </summary>
    private Scope GetScope()
    {
        if (!_tagName.StartsWith(ProgramPrefix, StringComparison.OrdinalIgnoreCase))
            return Scope.Controller;

        var memberIndex = _tagName.IndexOf(Separator);
        var endIndex = memberIndex > 0 ? memberIndex : _tagName.Length;
        var programName = _tagName.Substring(ProgramPrefix.Length, endIndex - ProgramPrefix.Length);
        return Scope.Program(programName);
    }

    /// <summary>
    /// Gets the portion of the tag name without the leading program prefix if present. This is needed to analyze
    /// the remaining portion of the actual localized tag name value.
    /// </summary>
    private string GetLocalTagName()
    {
        if (!_tagName.StartsWith(ProgramPrefix, StringComparison.OrdinalIgnoreCase))
            return _tagName;

        var memberIndex = _tagName.IndexOf(Separator);

        if (memberIndex == -1 || memberIndex == _tagName.Length - 1)
            return string.Empty;

        return _tagName.Substring(memberIndex + 1);
    }

    /// <summary>
    /// Concatenates the provided collection of tag members into a single string representation
    /// using appropriate delimiters.
    /// </summary>
    /// <param name="members">The collection of tag members to concatenate.</param>
    /// <returns>A string representing the concatenated tag members.</returns>
    private static string ConcatenateMembers(IEnumerable<string?> members)
    {
        var builder = new StringBuilder();

        foreach (var member in members)
        {
            if (member is null) continue;

            if (!(member.StartsWith('[') || member.StartsWith('.')) && builder.Length > 1)
                builder.Append('.');

            builder.Append(member);
        }

        return builder.ToString();
    }


    private bool IsQualifiedTagName()
    {
        if (IsEmpty) return false;
        var members = GetMembers().ToArray();
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

    /// <summary>
    /// Determines whether the specified token type represents a tag token.
    /// </summary>
    /// <param name="type">The <see cref="TokenType"/> to evaluate.</param>
    /// <returns>
    /// true if the token type is one of the defined tag-related tokens such as Identifier, Dot, OpenBracket, CloseBracket,
    /// Comma, or Colon; otherwise, false.
    /// </returns>
    private static bool IsTagToken(TokenType type)
    {
        return type == TokenType.Identifier
               || type == TokenType.Literal
               || type == TokenType.Dot
               || type == TokenType.OpenBracket
               || type == TokenType.CloseBracket
               || type == TokenType.Comma
               || type == TokenType.Colon;
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