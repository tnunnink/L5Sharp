using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An element wrapping the Logix <c>EngineeringUnit</c> element and providing access to its attributes and values.
/// </summary>
/// <remarks>
/// A comment contains the <see cref="Operand"/> representing the nested tag member the unit is associated with,
/// as well as the actual unit <see cref="Value"/>.
/// </remarks>\
[L5XType(L5XName.EngineeringUnit)]
public class Unit : LogixObject
{
    /// <summary>
    /// Creates a new <see cref="Unit"/> element with the provided operand tag and comment text.
    /// </summary>
    public Unit(TagName operand, string? value = default) : base(L5XName.EngineeringUnit)
    {
        Operand = operand;
        Value = value ?? string.Empty;
    }

    /// <summary>
    /// Creates a new <see cref="Unit"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Unit(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The operand tag name that identifies which member tag this unit is attached to. 
    /// </summary>
    /// <value>
    /// A <see cref="TagName"/> that starts with the '.' and contains the full dot down path to the nested tag member
    /// for which this unit is associated with.
    /// </value>
    public TagName Operand
    {
        get => GetRequiredValue<TagName>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The text containting the unit value.
    /// </summary>
    /// <value>A <see cref="string"/> containsing the unit text.</value>
    public string Value
    {
        get => Element.Value;
        set => Element.ReplaceNodes(new XCData(value));
    }
}