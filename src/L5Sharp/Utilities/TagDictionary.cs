using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;

namespace L5Sharp.Utilities;

/// <summary>
/// Provides performant and concise access to tag objects within an L5X file.
/// </summary>
/// <remarks>
/// <para>
/// This class is optimized to quickly retrieve tag objects by name using nested dictionaries.
/// This is important simply for the fact that a single L5X file could reasonably contain millions of tag objects
/// when considering that even atomic members can have up to 64 member tags.
/// </para>
/// <para>
/// This class is quick to create and quick to retrieve, and should probably be used in place of
/// .NET <c>ToLookup()</c> of <c>ToDictionary()</c> as they will not be as performant upon creation,
/// especially with larger collections.
/// </para>
/// </remarks>
public class TagDictionary : IEnumerable<Tag>
{
    private static readonly List<string> TagElements = new()
    {
        L5XName.Tag,
        L5XName.ConfigTag,
        L5XName.InputTag,
        L5XName.OutputTag
    };

    private readonly Dictionary<string, Dictionary<TagName, XElement>> _tags = new()
    {
        {L5XName.Controller, new Dictionary<TagName, XElement>()}
    };

    /// <summary>
    /// Creates a new <see cref="TagDictionary"/> instance with the provided L5X content file.
    /// </summary>
    /// <param name="content">The <see cref="LogixContent"/> to initialize the dictionary with.</param>
    /// <exception cref="ArgumentNullException"><c>content</c> is null</exception>
    /// <exception cref="ArgumentException"><c></c> contains tag elements with
    /// no name property -or- contains elements with the same tag name.</exception>
    public TagDictionary(LogixContent content)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));

        var elements = content.L5X.Descendants().Where(d => TagElements.Any(e => e == d.Name));

        foreach (var element in elements)
        {
            var scope = GetScope(element);
            var tagName = GetTagName(element);

            if (!_tags.TryAdd(scope, new Dictionary<TagName, XElement> {{tagName, element}}))
                _tags[scope].Add(tagName, element);
        }
    }

    /// <summary>
    /// Returns the total number of tag objets in both controller and program scopes found in the L5X content file.
    /// </summary>
    /// <value>An <see cref="int"/> representing the number of tags in the L5X.</value>
    public int Count => _tags.Select(x => x.Value).SelectMany(t => t.Values).Count();

    /// <summary>
    /// Gets a <see cref="Tag"/> with the specified tag name value.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> identifying the tag to retrieve.
    /// For program tags, you must prefix with <i>Program:{ProgramName}</i>. Otherwise, the tag will be
    /// considered a controller scoped tag.
    /// </param>
    /// <exception cref="ArgumentException"><c>tagName</c> is null -or- no tag exists with the specified value.</exception>
    /// <remarks>
    /// <para>
    /// This performs a quick lookup of the tag object using a unique tag name. Given that program scoped tag names are
    /// not unique, <c>tagName</c> must be prefixed with the program name specifier in the format <i>Program:{ProgramName}</i>,
    /// where <i>ProgramName</i> is the name of the program the tag is contained in.
    /// </para>
    /// </remarks>
    public Tag this[TagName tagName]
    {
        get
        {
            if (tagName is null) throw new ArgumentNullException(nameof(tagName));

            var scope = GetScopeName(tagName);
            var local = GetLocalName(tagName);

            var element = _tags[scope][local.Root];
            var tag = new Tag(element);
            return tagName.Depth == 0 ? tag : tag[local.Path];
        }
    }

    /// <summary>
    /// Determines if the specified tag name exists in the L5X content file.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> value to search for.</param>
    /// <returns><c>true</c> if a tag with the specified name exists; Otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// If no program specifier is prepended to the tag name, then this will search all controller and
    /// program scoped tags for the specified tag name. If a program specifier is prepended, then this will
    /// narrow the search to the specific program.</remarks>
    public bool Contains(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var local = GetLocalName(tagName);

        if (tagName.Root.StartsWith("Program:"))
        {
            var scope = GetScopeName(tagName);
            return _tags.ContainsKey(scope) && _tags[scope].TryGetValue(local.Root, out var element) &&
                   (tagName.Depth == 0 || new Tag(element).Member(local.Path) is not null);
        }

        foreach (var container in _tags)
        {
            if (container.Value.TryGetValue(tagName.Root, out var element))
                return tagName.Depth == 0 || new Tag(element).Member(local.Path) is not null;
        }

        return false;
    }

    /// <summary>
    /// Finds a <see cref="Tag"/> with the specified tag name value.
    /// </summary>
    /// <param name="tagName">The <see cref="TagName"/> identifying the tag to retrieve.
    /// For program tags, you must prefix with <i>Program:{ProgramName}</i>. Otherwise, the tag will be
    /// considered a controller scoped tag.
    /// </param>
    /// <returns>A <see cref="Tag"/> with the specified name if it exists; Otherwise, <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
    /// <remarks>
    /// <para>
    /// This performs a quick lookup of the tag object using a unique tag name. Given that program scoped tag names are
    /// not unique, <c>tagName</c> must be prefixed with the program name specifier in the format <i>Program:{ProgramName}</i>,
    /// where <i>ProgramName</i> is the name of the program the tag is contained in.
    /// </para>
    /// </remarks>
    public Tag? Find(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var scope = GetScopeName(tagName);
        var local = GetLocalName(tagName);

        if (!_tags.TryGetValue(scope, out var container)) return default;
        if (!container.TryGetValue(local.Root, out var element)) return default;

        var tag = new Tag(element);
        return tagName.Depth == 0 ? tag : tag.Member(local.Path);
    }
    
    

    /// <summary>
    /// Finds the ir
    /// </summary>
    /// <param name="tagName"></param>
    /// <param name="program"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Tag? FindFirst(TagName tagName, string program)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        if (_tags.TryGetValue(program, out var container) && container.TryGetValue(tagName, out var programElement))
            return new Tag(programElement);

        return _tags[L5XName.Controller].TryGetValue(tagName, out var controllerElement)
            ? new Tag(controllerElement)
            : default;
    }

    /// <summary>
    /// Finds all tags with the specified tag name value.  
    /// </summary>
    /// <param name="tagName">The tag name to search.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all <see cref="Tag"/> objects found in the L5X.</returns>
    /// <remarks>
    /// <c>tagName</c> does not need to be prefixed with the program specifier for this method to
    /// return results. It will iterate each container and search for the specified tag name. Therefore, this returns
    /// both controller scoped and program scoped tags.
    /// </remarks>
    public IEnumerable<Tag> FindAll(TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        var results = new List<Tag>();

        foreach (var container in _tags)
        {
            if (container.Value.TryGetValue(tagName.Root, out var element))
                results.Add(new Tag(element));
        }

        return results;
    }

    /// <summary>
    /// Finds a tag in a single program with the specified tag name value.  
    /// </summary>
    /// <param name="program">The name of the program to search.</param>
    /// <param name="tagName">The name of the tag to find.</param>
    /// <returns>A <see cref="Tag"/> with the specified name if found; Otherwise, <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException"><c>tagName</c> is null.</exception>
    public Tag? In(string program, TagName tagName)
    {
        if (tagName is null) throw new ArgumentNullException(nameof(tagName));

        if (!_tags.TryGetValue(program, out var container)) return default;
        if (!container.TryGetValue(tagName, out var element)) return default;

        var tag = new Tag(element);
        return tagName.Depth == 0 ? tag : tag.Member(tagName.Path);
    }

    /// <summary>
    /// Returns all tags in a specified program container.
    /// </summary>
    /// <param name="program">The name of the program to search.</param>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Tag"/> contained in the program.</returns>
    public IEnumerable<Tag> In(string program) =>
        _tags.TryGetValue(program, out var container)
            ? container.Values.Select(e => new Tag(e))
            : Enumerable.Empty<Tag>();

    /// <inheritdoc />
    public IEnumerator<Tag> GetEnumerator() =>
        _tags.Select(x => x.Value).SelectMany(x => x.Values).Select(e => new Tag(e)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #region Internal

    private static string GetScopeName(TagName tagName) =>
        tagName.Root.StartsWith("Program:") ? tagName.Root.Replace("Program:", string.Empty) : L5XName.Controller;

    private static TagName GetLocalName(TagName tagName) =>
        tagName.Root.StartsWith("Program:") ? new TagName(tagName.Path) : tagName;

    private static string GetScope(XNode element) =>
        element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ?? L5XName.Controller;

    private static string GetTagName(XElement element)
    {
        if (element.Name == L5XName.ConfigTag || element.Name == L5XName.InputTag || element.Name == L5XName.OutputTag)
            return GetModuleTagName(element);

        return element.Attribute(L5XName.Name)?.Value ??
               throw new ArgumentException($"No name attribute exists for element '{element.Name}'.");
    }

    private static string GetModuleTagName(XElement element)
    {
        var suffix = DetermineModuleSuffix(element);

        var moduleName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.Name)?.Value;

        var parentName = element.Ancestors(L5XName.Module)
            .FirstOrDefault()?.Attribute(L5XName.ParentModule)?.Value;

        var slot = element
            .Ancestors(L5XName.Module)
            .Descendants(L5XName.Port)
            .Where(p => bool.TryParse(p.Attribute(L5XName.Upstream)?.Value, out var upstream) && upstream
                && p.Attribute(L5XName.Type)?.Value != "Ethernet"
                && int.TryParse(p.Attribute(L5XName.Address)?.Value, out _))
            .Select(p => p.Attribute(L5XName.Address)?.Value)
            .FirstOrDefault();

        return slot is not null ? $"{parentName}:{slot}:{suffix}" : $"{moduleName}:{suffix}";
    }

    private static string DetermineModuleSuffix(XElement element)
    {
        if (element.Name == L5XName.InputTag)
            return element.Parent?.Attribute(L5XName.InputTagSuffix)?.Value ?? "I";

        if (element.Name == L5XName.OutputTag)
            return element.Parent?.Attribute(L5XName.OutputTagSuffix)?.Value ?? "O";

        return "C";
    }

    #endregion
}