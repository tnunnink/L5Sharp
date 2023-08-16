using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;

namespace L5Sharp.Extensions;

public class TagDictionary
{
    private static readonly List<string> Elements = new()
    {
        L5XName.Tag,
        L5XName.ConfigTag,
        L5XName.InputTag,
        L5XName.OutputTag
    };

    private readonly Dictionary<string, List<XElement>> _dictionary = new();
    
    public TagDictionary(LogixContent content)
    {
        if (content is null) throw new ArgumentNullException(nameof(content));

        var elements = content.L5X.Descendants().Where(d => Elements.Any(e => e == d.Name));

        foreach (var element in elements)
        {
            var key = GetTagName(element);

            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key].Add(element);
                continue;
            }

            _dictionary.Add(key, new List<XElement> { element });
        }
    }

    public Tag this[TagName tagName]
    {
        get
        {
            if (!_dictionary.TryGetValue(tagName.Root, out var elements))
                throw new InvalidOperationException();
        
            var tag = new Tag(elements[0]);
            return tagName.Depth == 0 ? tag : tag[tagName.Path];
        }
    }
    
    public IEnumerable<Tag> All(TagName tagName)
    {
        return _dictionary.TryGetValue(tagName.Root, out var elements)
            ? elements.Select(e => new Tag(e)).Where(t => t.TagName == tagName)
            : Enumerable.Empty<Tag>();
    }
    
    public Tag? Find(TagName tagName)
    {
        if (!_dictionary.TryGetValue(tagName.Root, out var elements)) return default;

        if (elements.Count > 1)
            throw new InvalidOperationException();

        var tag = new Tag(elements[0]);
        return tagName.Depth == 0 ? tag : tag.Member(tagName.Path);
    }

    private static string GetTagName(XElement element)
    {
        if (element.Name == L5XName.ConfigTag || element.Name == L5XName.InputTag || element.Name == L5XName.OutputTag)
            return ModuleTagName(element);

        return element.Attribute(L5XName.Name)?.Value ?? string.Empty;
    }

    private static string ModuleTagName(XElement element)
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
}