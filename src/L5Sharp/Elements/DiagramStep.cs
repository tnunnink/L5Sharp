using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties for a step instruction within a
/// Sequential Function Chart (SFC).
/// </summary>
/// <remarks>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Step)]
public class DiagramStep : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="DiagramStep"/> with default values.
    /// </summary>
    public DiagramStep()
    {
    }

    /// <summary>
    /// Creates a new <see cref="DiagramStep"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DiagramStep(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The backing tag name for the <c>DiagramStep</c> instance.
    /// </summary>
    /// <value>A <see cref="string"/> containing the tag name if it exists; Otherwise, <c>null</c>.</value>
    public string? Operand
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramStep</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool HideDesc
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The X coordinate of the <see cref="DiagramElement"/> within the containing <c>Sheet</c>.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint DescX
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// The Y coordinate of the <see cref="DiagramElement"/> within the containing <c>Sheet</c>.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint DescY
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// The Y coordinate of the <see cref="DiagramElement"/> within the containing <c>Sheet</c>.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint DescWidth
    {
        get => GetValue<uint>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramStep</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool InitialStep
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramStep</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool PresetUsesExpression
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramStep</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool LimitHighUsesExpression
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramStep</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool LimitLowUsesExpression
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Whether or not to hide the description for the <c>DiagramStep</c>.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    public bool ShowActions
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public IEnumerable<Line> Preset
    {
        get => Element.Element(L5XName.Preset)?.Descendants(L5XName.Line).Select(d => new Line(d)) ??
               Enumerable.Empty<Line>();
        set
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (!value.Any())
            {
                Element.Element(L5XName.Preset)?.Remove();
                return;
            }
            
            var preset = Element.Element(L5XName.Preset) is null
                ? new XElement(L5XName.Preset)
                : Element.Element(L5XName.Preset)!;
            
            preset.ReplaceNodes(new XElement(L5XName.STContent, value.Select(l => l.Serialize())));
        }
    }
}