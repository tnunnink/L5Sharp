using L5Sharp.CLI.Abstractions;
using L5Sharp.Core;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace L5Sharp.CLI.Rendering;

/// <summary>
/// 
/// </summary>
///  todo Not sure how we want to handle XML with field selection (if at all) and if there is a better way to render it.
public class XmlRenderer : IElementRenderer
{
    /// <inheritdoc />
    public IRenderable Render<TElement>(TElement element, params string[] fields) where TElement : ILogixElement
    {
        if (element is null) throw new ArgumentNullException(nameof(element));
        var xml = element.Serialize().ToString();
        return new Text(xml);
    }

    /// <inheritdoc />
    public IRenderable Render<TElement>(IEnumerable<TElement> elements, params string[] fields)
        where TElement : ILogixElement
    {
        if (elements is null) throw new ArgumentNullException(nameof(elements));
        var xml = elements.Select(e => e.Serialize().ToString());
        var text = string.Join(Environment.NewLine, xml);
        return new Text(text);
    }
}