using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A component of the <see cref="Trend"/> that defines the tags used for the trend
/// </summary>
/// <remarks>
///Observe these guidelines when defining a trend:<br/>
///    • A trend can support as many as eight pen declarations.<br/>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Pen : LogixObject
{
    /// <summary>
    /// Creates a new <see cref="Pen"/> with default values.
    /// </summary>
    public Pen()
    {
        Name = string.Empty;
        Color = "16#00ff_0000";
        Visible = true;
        Width = 1;
        Type = PenType.Analog;
        Style = 0;
        Marker = 0;
        Min = 0;
        Max = 100;
    }

    /// <summary>
    /// Creates a new <see cref="Pen"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Pen(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// The unique name of the <c>Pen</c>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the component name. This property is required for valid elements.</value>
    public string Name
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the <c>Pen</c>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public string? Description
    {
        get => GetProperty<string>();
        set => SetDescription(value);
    }

    /// <summary>
    /// Specify the color of the line in RGB format. Type the hex number for the color (16#0000_0000 – 16#00FF_FFFF).
    /// </summary>
    public string? Color
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify whether or not the line should be visible
    /// </summary>
    /// <value><c>true</c> if the pen is visible; otherwise, false.</value>
    public bool? Visible
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the width of the line in pixels (1...10).
    /// </summary>
    public int? Width
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the line type (Analog, Digital, or Full-Width).
    /// </summary>
    public PenType? Type
    {
        get => GetValue<PenType>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the style of line.
    /// </summary>
    /// <value>
    /// Type: For: <br/>
    ///  0 ……………. <br/>
    ///  1 … … … … <br/>
    ///  2 . . . . . . . . . . . <br/>
    ///  3 … . … . … . … <br/>
    ///  4 … .. … .. … .. <br/>
    /// </value>
    public int Style
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify the line marker (0...83)
    /// </summary>
    public int? Marker
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// Specify the minimum value for the pen. The minimum cannot be greater than or equal to the maximum.
    /// </summary>
    public double? Min
    {
        get => GetValue<int>();
        set => SetValue(value);
    } 
    
    /// <summary>
    /// Specify the maximum value for the pen. The maximum cannot be less than or equal to the minimum.
    /// </summary>
    public double? Max
    {
        get => GetValue<int>();
        set => SetValue(value);
    } 
    
    /// <summary>
    /// Specify engineering units. For example, rpm, gallon, fps, and degrees.
    /// </summary>
    public string? EngUnits
    {
        get => GetValue<string>();
        set => SetValue(value);
    }
}