using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An object that contains reference information for a given named component element.
/// A reference is defined by the <see cref="Reference"/> (name of the conpomonet being referenced),
/// and the <see cref="Scope"/> location of the element containing the reference).
/// Optionally, code references can include a specific <see cref="Element"/> which is a test representation of the
/// element that co contains the referencing. This is meant to be somewhat generic so that it works for
/// all different kind of references.  
/// </summary>
public class CrossReference
{
    /// <summary>
    /// Creates a new <see cref="CrossReference"/> with the provided parameters.
    /// </summary>
    /// /// <param name="type">The <see cref="Core.ReferenceType"/> the reference represents.</param>
    /// /// <param name="reference">The <see cref="TagName"/> of the component that is referenced.</param>
    /// <param name="scope">The <see cref="Scope"/> of the element that references the component.</param>
    /// <param name="element">The element the reference is found in.</param>
    private CrossReference(ReferenceType type, TagName reference, Scope scope, string element)
    {
        Reference = reference;
        Scope = scope;
        Type = type;
        Element = element;
    }

    /// <summary>
    /// The type of the component that is being referenced by <see cref="Scope"/>. This can be essentially any common
    /// component type such as Tag, DataType, Routine, etc.
    /// </summary>
    public ReferenceType Type { get; }

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
    /// The element containing the component reference if found.
    /// </summary>
    /// <remarks>
    /// This will either be the instruction text (code reference), tag name (data type reference), or a component
    /// property name of the referencing element that is found.
    /// This gives a little more context as to what in the target <see cref="Scope"/> is referencing the component.
    /// </remarks>
    public string Element { get; }

    /// <summary>
    /// The <see cref="Core.Instruction"/> that contains this reference. This will only be availble for
    /// references that are found in rung or line elements. 
    /// </summary>
    public Instruction? Instruction => Scope.Type == ScopeType.Rung || Scope.Type == ScopeType.Line
        ? Instruction.Parse(Element)
        : default;

    /// <summary>
    /// Indicates that element containing the reference is a tag type component.
    /// </summary>
    public bool IsTag => Scope.Type == ScopeType.Tag;

    /// <summary>
    /// Indicates that element containing the reference is a logic (Rung, Line, Sheet) type component.
    /// </summary>
    public bool IsLogic => Scope.Type.InRoutine;

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

        if (element.L5XType() is L5XName.Program)
            return GetRoutineReferences(element);

        if (element.L5XType() is L5XName.Rung)
            return GetRungReferences(element);

        if (element.L5XType() is L5XName.Line)
            return GetLineReferences(element);

        if (element.L5XType() is L5XName.Sheet)
            return GetSheetReferences(element);

        return [];
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
            references.Add(new CrossReference(ReferenceType.DataType, dataType, scope, tagName));
        }

        return references;
    }

    private static List<CrossReference> GetRoutineReferences(XElement element)
    {
        var scope = Scope.Of(element);

        var references = element.Attributes()
            .Where(a => a.Name.LocalName.Contains("RoutineName"))
            .Select(a => new CrossReference(ReferenceType.Routine, a.Value, scope, a.Name.LocalName));

        return references.ToList();
    }

    private static List<CrossReference> GetRungReferences(XElement element)
    {
        var text = element.Element(L5XName.Text)?.Value.Parse<NeutralText>();
        if (text is null) return [];

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);

        foreach (var instruction in text.Instructions())
        {
            references.Add(new CrossReference(ReferenceType.Instruction, instruction.Key, scope, instruction.Text));

            var referenced = instruction.References
                .Select(r => new CrossReference(ReferenceType.Parse(r.Type.Name), r.Name, scope, instruction.Text));

            references.AddRange(referenced);
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
            references.Add(new CrossReference(ReferenceType.Instruction, instruction.Key, scope, instruction.Text));

            var referenced = instruction.References
                .Select(r => new CrossReference(ReferenceType.Parse(r.Type.Name), r.Name, scope, instruction.Text));

            references.AddRange(referenced);
        }

        return references;
    }

    private static IEnumerable<CrossReference> GetSheetReferences(XElement element)
    {
        var blocks = element.Elements()
            .Where(e => e.L5XType() is not L5XName.Wire and not L5XName.TextBox and not L5XName.Attachment)
            .Select(e => e.Deserialize<Block>());

        var references = new List<CrossReference>();
        var scope = Scope.Of(element);

        foreach (var block in blocks)
        {
            references.Add(new CrossReference(ReferenceType.Instruction, block.Type, scope, block.ToString()));

            if (block.Operand is null || !block.Operand.IsTag)
                return references;

            references.Add(new CrossReference(ReferenceType.Tag, block.Operand.ToString(), scope, block.ToString()));
            references.AddRange(
                block.Tags.Select(t => new CrossReference(ReferenceType.Tag, t, scope, block.ToString())));
        }

        return references;
    }

    #endregion
}