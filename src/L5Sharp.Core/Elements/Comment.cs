using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An element wrapping the Logix <c>Comment</c> element and providing access to its attributes and values.
/// </summary>
/// <remarks>
/// A comment contains the <see cref="Operand"/> representing the nested tag member the comment is associated with,
/// as well as the actual comment <see cref="Value"/>.
/// </remarks>
[L5XType(L5XName.Comment)]
public class Comment : LogixObject
{
    /// <summary>
    /// Creates a new <see cref="Comment"/> element with the provided operand tag and comment text.
    /// </summary>
    public Comment(TagName operand, string? value = default) : base(L5XName.Comment)
    {
        Operand = operand;
        Value = value ?? string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Comment"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Comment(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The operand tag name that identifies which member tag this comment is attached to. 
    /// </summary>
    /// <value>
    /// A <see cref="TagName"/> that starts with the '.' and contains the full dot down path to the nested tag member
    /// for which this comment is associated with.
    /// </value>
    public TagName Operand
    {
        get => GetRequiredValue<TagName>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The text containting the comment.
    /// </summary>
    /// <value>A <see cref="string"/> containsing the comment text.</value>
    public string Value
    {
        get => Element.Value;
        set => Element.ReplaceNodes(new XCData(value));
    }
}