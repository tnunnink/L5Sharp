using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An object that contains reference information for a given named component element.
/// A reference is defined by the <see cref="Name"/> of the conpomonet being referenced, and the <see cref="Scope"/>
/// of the element that references it. Optionally, code references can include a specific <see cref="Instrucion"/>
/// that contains the reference. This is meant to be somewhat generic so that it works for both code and tag references.  
/// </summary>
public class CrossReference
{
    /// <summary>
    /// Creates a new <see cref="CrossReference"/> with the provided parameters.
    /// </summary>
    /// <param name="scope">The <see cref="Scope"/> of the element that references the component.</param>
    /// <param name="name">The <see cref="TagName"/> of the component that is referenced.
    /// This might be any component type, not just Tag. Using TagName helps break down the parts of the references easier.</param>
    /// <param name="instrucion">The optional instruction name that contains the reference. Only applies to code
    /// based references.</param>
    public CrossReference(Scope scope, TagName name, string? instrucion = null)
    {
        Scope = scope;
        Name = name;
        Instrucion = instrucion ?? string.Empty;
    }

    /// <summary>
    /// The name of the component being referenced by <see cref="Scope"/>.
    /// </summary>
    /// <remarks>
    /// Even though this is a <see cref="TagName"/> object, it doesn't necessarily mean it's a tag component being referenced.
    /// We are using <see cref="TagName"/> since it allows us to parse the parts of the name.
    /// For references other than tag, this will be a single "root" name without any member parts.
    /// </remarks>
    public TagName Name { get; }

    /// <summary>
    /// The <see cref="Core.Scope"/> of the element containing the component reference.
    /// </summary>
    /// <remarks>
    /// This can really be any element, but in reality should be the code (Rung/Line/Sheet) or component (Tag) that
    /// contains the reference to <see cref="Name"/>.
    /// </remarks>
    public Scope Scope { get; }

    /// <summary>
    /// The name of the instruction containing the component reference if found.
    /// </summary>
    /// <remarks>
    /// This will only be set for references to code, and not references to tags (i.e. DataType references).
    /// </remarks>
    public string Instrucion { get; }

    /// <summary>
    /// Produces a collection of all references found in the provided <see cref="XElement"/> object.
    /// </summary>
    /// <param name="element">The element in which to find references.</param>
    /// <returns>A collection of <see cref="CrossReference"/> contained by the provided element.</returns>
    /// <remarks>
    /// It's expected that the provided element is a code (Rung/Text/Sheet) or Tag element.
    /// If not then it shouldn't produce any results.
    /// This code will basically pull out all the named parts we specifically consider a component reference and return
    /// a <see cref="CrossReference"/> for each one.
    /// </remarks>
    public static IEnumerable<CrossReference> In(XElement element)
    {
        if (element.L5XType() is L5XName.Tag)
            return GetTypeReferences(element);

        if (element.L5XType() is L5XName.Rung)
            return GetRungReferences(element);

        if (element.L5XType() is L5XName.Line)
            return GetLineReferences(element);

        if (element.L5XType() is L5XName.Sheet)
            return GetSheetReferences(element);

        return [];
    }

    private static IEnumerable<CrossReference> GetTypeReferences(XElement element)
    {
        HashSet<string> tags = [L5XName.Tag, L5XName.LocalTag, L5XName.ConfigTag, L5XName.InputTag, L5XName.ConfigTag];

        var type = element.Attribute(L5XName.DataType)?.Value;
        var tag = element.AncestorsAndSelf().FirstOrDefault(e => tags.Contains(e.L5XType()));

        if (type is null || tag is null) return [];

        var scope = Scope.Of(tag);
        return [new CrossReference(scope, type)];
    }

    private static IEnumerable<CrossReference> GetRungReferences(XElement element)
    {
        var text = element.Element(L5XName.Text)?.Value.Parse<NeutralText>();
        if (text is null) return [];

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);

        foreach (var instruction in text.Instructions())
        {
            references.AddRange(instruction.Tags().Select(t => new CrossReference(scope, t, instruction.Key)));
        }

        return references;
    }

    private static IEnumerable<CrossReference> GetLineReferences(XElement element)
    {
        var text = element.Value.Parse<NeutralText>();

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);

        foreach (var instruction in text.Instructions())
        {
            references.AddRange(instruction.Tags().Select(t => new CrossReference(scope, t, instruction.Key)));
        }

        return references;
    }

    private static IEnumerable<CrossReference> GetSheetReferences(XElement element)
    {
        return [];
    }
}