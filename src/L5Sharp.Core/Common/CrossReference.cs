using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An object that contains reference information for a given named component element.
/// A reference is defined by the <see cref="Reference"/> (name of the conpomonet being referenced),
/// and the <see cref="Scope"/> 9location of the element containing the reference).
/// Optionally, code references can include a specific <see cref="Element"/>
/// that contains the reference. This is meant to be somewhat generic so that it works for both code and tag references.  
/// </summary>
public class CrossReference
{
    /// <summary>
    /// Creates a new <see cref="CrossReference"/> with the provided parameters.
    /// </summary>
    /// <param name="scope">The <see cref="Scope"/> of the element that references the component.</param>
    /// <param name="reference">The <see cref="TagName"/> of the component that is referenced.
    /// This might be any component type, not just Tag. Using TagName helps break down the parts of the references easier.</param>
    /// <param name="type">The <see cref="ReferenceType"/> the reference represents.</param>
    /// <param name="element">The element the reference is found in.</param>
    private CrossReference(TagName reference, Scope scope, ReferenceType type, string element)
    {
        Reference = reference;
        Scope = scope;
        Type = type;
        Element = element;
    }

    /// <summary>
    /// The name of the component being referenced by <see cref="Scope"/>.
    /// </summary>
    /// <remarks>
    /// Even though this is a <see cref="TagName"/> object, it doesn't necessarily mean it's a tag component being referenced.
    /// We are using <see cref="TagName"/> since it allows us to parse the parts of the name.
    /// For references other than tag, this will be a single "root" name without any member parts.
    /// </remarks>
    public TagName Reference { get; }

    /// <summary>
    /// The <see cref="Core.Scope"/> of the element containing the component reference.
    /// </summary>
    /// <remarks>
    /// This can really be any element, but in reality should be the code (Rung/Line/Sheet) or component (Tag) that
    /// contains the reference to <see cref="Reference"/>.
    /// </remarks>
    public Scope Scope { get; }

    /// <summary>
    /// Indicates whether the referening item is a tag or logic type reference.
    /// All references are tags (which reference data types, or potentially other tags) or logic
    /// (components/tag found in rungs/lines/sheets) type references.
    /// </summary>
    public ReferenceType Type { get; }

    /// <summary>
    /// The element name of this item containing the component reference if found.
    /// </summary>
    /// <remarks>
    /// This will either be the instruction name (code reference) or tag name (data type reference) of the referencing
    /// element that is found. This gives a little more helpful context as to what in the target <see cref="Scope"/>
    /// is referencing the component.
    /// </remarks>
    public string Element { get; }

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
        if (element.IsTagElement())
            return GetDataTypeReferences(element);

        if (element.L5XType() is L5XName.Rung)
            return GetRungReferences(element);

        if (element.L5XType() is L5XName.Line)
            return GetLineReferences(element);

        if (element.L5XType() is L5XName.Sheet)
            return GetSheetReferences(element);

        return [];
    }

    /// <summary>
    /// Determines whether the current reference is the same as another reference,
    /// meaning it is referring to the same component in the same scope (equal <see cref="Reference"/> and <see cref="Scope"/>).
    /// </summary>
    /// <param name="other">The <see cref="CrossReference"/> instance to compare with the current instance.</param>
    /// <returns>
    /// <c>true</c> if the current instance is the same as the <paramref name="other"/> instance; otherwise, <c>false</c>.
    /// </returns>
    public bool IsSameAs(CrossReference other)
    {
        return Reference == other.Reference && Scope == other.Scope;
    }


    #region Internal

    private static List<CrossReference> GetDataTypeReferences(XElement element)
    {
        if (!element.IsTagElement()) return [];

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);
        
        var members = element.DescendantsAndSelf()
            .Where(d => d.Parent?.Name.LocalName != L5XName.Data && d.Attribute(L5XName.DataType) is not null);

        foreach (var member in members)
        {
            var dataType = member.DataType();
            var tagName = member.TagName();
            references.Add(new CrossReference(dataType, scope, ReferenceType.Tag, tagName));
        }

        return references;
    }

    private static List<CrossReference> GetRungReferences(XElement element)
    {
        var text = element.Element(L5XName.Text)?.Value.Parse<NeutralText>();
        if (text is null) return [];

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);

        foreach (var instruction in text.Instructions())
        {
            references.Add(new CrossReference(instruction.Key, scope, ReferenceType.Logic, instruction.Text));

            references.AddRange(instruction.Tags().Select(t =>
                new CrossReference(t, scope, ReferenceType.Logic, instruction.Key)));
        }

        return references;
    }

    private static List<CrossReference> GetLineReferences(XElement element)
    {
        var text = element.Value.Parse<NeutralText>();

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);

        foreach (var instruction in text.Instructions())
        {
            references.Add(new CrossReference(instruction.Key, scope, ReferenceType.Logic, instruction.Text));

            references.AddRange(instruction.Tags().Select(t =>
                new CrossReference(t, scope, ReferenceType.Logic, instruction.Key)));
        }

        return references;
    }

    //todo implement this similar to old code.
    private static IEnumerable<CrossReference> GetSheetReferences(XElement element)
    {
        return [];
    }

    #endregion
}

/// <summary>
/// Represents the type of reference in the software system.
/// The reference type can be either Tag or Logic.
/// </summary>
public enum ReferenceType
{
    /// <summary>
    /// Indicates taht the reference is a tag type reference.
    /// </summary>
    Tag,

    /// <summary>
    /// Indicates taht the reference is a logic type reference.
    /// </summary>
    Logic
}