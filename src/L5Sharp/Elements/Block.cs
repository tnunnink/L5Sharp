using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramBlock</c> type that defines the properties for nested function block elements in a
/// Function Block Diagram (FBD).
/// </summary>
/// <remarks>
/// A <c>Block</c> represents a function block within the FBD. These are blocks that represent
/// specific logix built-in instructions as opposed to AOIs. A <c>Block</c> differs from a <c>Function</c>
/// in that it requires a backing tag to operate over, whereas a <c>Function</c> represents a simple logic gate or
/// operation that takes inputs and produces an output without the need for a backing tag.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Block, L5XName.Sheet)]
public class Block : FunctionBlock
{
    /// <summary>
    /// Creates a new <see cref="FunctionBlock"/> with default values.
    /// </summary>
    public Block()
    {
    }

    /// <summary>
    /// Creates a new <see cref="FunctionBlock"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Block(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The mnemonic name specifying the type of function for the <c>DiagramBlock</c> instance.
    /// </summary>
    /// <value>A <see cref="string"/> containing the type of the function if it exists; Otherwise, <c>null</c>.</value>
    public string? Type
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The backing tag name for the <c>DiagramBlock</c> instance.
    /// </summary>
    /// <value>A <see cref="string"/> containing the tag name if it exists; Otherwise, <c>null</c>.</value>
    public TagName? Operand
    {
        get => GetValue<TagName>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramBlock</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool? HideDesc
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// A collection of pin names that are visible for the <c>Block</c> element.
    /// </summary>
    /// <value>
    /// A array of strings containing the names of the pins if found. If not found then an
    /// empty collection.
    /// </value>
    /// <remarks>
    /// To update the property, you must assign a new array of pin names. Note that this property only applies to
    /// diagram types <c>Block</c> and <c>AddOnInstruction</c>. Invalid configuration of the element may result in a
    /// failure to import.
    /// </remarks>
    public IEnumerable<TagName> VisiblePins => throw new NotImplementedException();

    public void AddPin(TagName tagName)
    {
        
    }
    
    public void AddPins(IEnumerable<TagName> tagName)
    {
        
    }
    
    public void RemovePin(TagName tagName)
    {
        
    }
    
    public void ReplacePin(TagName current, TagName replacement)
    {
        
    }
    
    public void ReplacePins(IEnumerable<TagName> pins)
    {
        
    }
}