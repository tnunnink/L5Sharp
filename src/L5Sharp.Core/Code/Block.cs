using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace L5Sharp.Core;

/// <summary>
/// A <c>DiagramElement</c> type that defines the properties for nested function block elements in a
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
[L5XType(L5XName.IRef)]
[L5XType(L5XName.ORef)]
[L5XType(L5XName.ICon)]
[L5XType(L5XName.OCon)]
[L5XType(L5XName.Block)]
[L5XType(L5XName.Function)]
[L5XType(L5XName.AddOnInstruction)]
[L5XType(L5XName.JSR)]
[L5XType(L5XName.SBR)]
[L5XType(L5XName.RET)]
public class Block : LogixObject<Block>
{
    private static readonly HashSet<string> PinNames = [L5XName.VisiblePins, L5XName.In, L5XName.Ret];

    /// <summary>
    /// Creates a new <see cref="Block"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Block(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The unique identifier of the block for the current sheet content.
    /// </summary>
    /// <value>A zero based <see cref="uint"/> representing the block id.</value>
    public uint ID => GetRequiredValue<uint>();

    /// <summary>
    /// The X coordinate of the block element within the current sheet.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint X => GetRequiredValue<uint>();

    /// <summary>
    /// The Y coordinate of the block element within the current sheet.
    /// </summary>
    /// <remarks>
    /// The <c>X</c> and <c>Y</c> grid locations are a relative position from the upper-left corner of the sheet.
    /// X is the horizontal position; Y is the vertical position.
    /// </remarks>
    public uint Y => GetRequiredValue<uint>();

    /// <summary>
    /// The alphanumeric cell of the sheet where the block is located. 
    /// </summary>
    /// <value>A <see cref="string"/> containing the cell coordinates (e.g., A1, B2, etc.)</value>
    /// <remarks>
    /// This is determined from the <c>X</c> and <c>Y</c> coordinates of the element. Each cell is 200x200
    /// pixels, which can be used to calculate the cell location.
    /// </remarks>
    public string Cell => $"{(char)(X / 200 + 'A')}{Y / 200 + 1}";

    /// <summary>
    /// Represents the instuction, function, or element type name of the <see cref="Block"/> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the block type name.</value>
    /// <remarks></remarks>
    public string Type => GetBlockType(Element);

    /// <summary>
    /// Represents the Operand property of a generic function block element. This can be the backing tag name,
    /// reference name, connector name, or routine name depending on the block type.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when attempting to set the operand for an unsupported block type.</exception>
    /// <value>An <see cref="Argument"/> object containing the value, which could be a tag name, immediate value, or simple string name.</value>
    /// <remarks>
    /// Since this class models all the different function block types, this proprty has to determine which attribute
    /// to read/write to internally depending on the type. For IRef, ORef, Block, and AOI blocks, this represents the
    /// Operand tag name. For an ICon and OCOn, this represents the connector Name. For routine blocks (JSR, SBR, RET),
    /// this represents the Routine name. Note that this is nullable since a Function
    /// block does not have an operand or backing tag property, and therefore attempting to set a value for that block type
    /// will result in an exception.
    /// </remarks>
    public Argument Operand
    {
        get => Element.GetBlockOperand();
        set => SetBlockOperand(Element, value);
    }

    /// <summary>
    /// Indicates whether the block's description is hidden in the sheet content.
    /// </summary>
    /// <value>
    /// A <see cref="bool"/> where <c>true</c> indicates that the block description is hidden,
    /// and <c>false</c> means it is visible.
    /// </value>
    public bool HideDesc
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets a collection of <see cref="TagName"/> values representing pins associated with this <see cref="Block"/>.
    /// </summary>
    /// <value>A sequence of <see cref="TagName"/> instances corresponding to the visible pins, inputs, or return values of the block.</value>
    /// <remarks>
    /// 
    /// </remarks>
    public IEnumerable<TagName> Pins => GetBlockPins(Element);

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<TagName> Arguments => GetBlockTags(Element);

    /// <summary>
    /// Represents the input tags connected or referenced by the block in the sheet content.
    /// </summary>
    /// <value>An enumeration of <see cref="TagName"/> instances representing the input connections of the block.</value>
    /// <remarks>
    /// This will traverse the current sheet element to find all tags that are wired into the current block.
    /// If this block is not attached to a sheet, this will throw an exception. This is useful for determining references
    /// and converting blocks to instruction text syntax for analysis. 
    /// </remarks>
    public IEnumerable<TagName> Inputs => GetBlockInputs(Element);

    /// <summary>
    /// Represents the ouptput tags connected or referenced by the block in the sheet content.
    /// </summary>
    /// <value>An enumeration of <see cref="TagName"/> instances representing the output connections of the block.</value>
    /// <remarks>
    /// This will traverse the current sheet element to find all tags that are wired onto other blocks.
    /// If this block is not attached to a sheet, this will throw an exception. This is useful for determining references
    /// and converting blocks to instruction text syntax for analysis. 
    /// </remarks>
    public IEnumerable<TagName> Outputs => GetBlockOutputs(Element);

    /// <summary>
    /// The <see cref="Core.Sheet"/> that this block is contained within.
    /// </summary>
    /// <value>A <see cref="Core.Sheet"/></value>
    public Sheet? Sheet => GetAncestor<Sheet>();


    /// <summary>
    /// Establishes a connection from this block to the specified target block, optionally using a specified pin.
    /// </summary>
    /// <param name="target">The target <see cref="Block"/> to connect to.</param>
    /// <param name="param">An optional <see cref="TagName"/> representing the pin to connect to on the target block.</param>
    /// <returns>The updated <see cref="Block"/> after establishing the connection.</returns>
    /// <exception cref="ArgumentNullException"><c>target</c> is null.</exception>
    /// <remarks>
    /// This will replace any existing wire connection on the target block if one exists in the current sheet.
    /// This overload also assumes that this block is a IREF or ICON that doesn't require a from param to identify which
    /// output paramater is forming the connection to the target. 
    /// </remarks>
    public Block WireTo(Block target, TagName? param = null)
    {
        var sheet = GetAncestor<Sheet>() ?? throw new InvalidOperationException("Can not connect blocks ...");
        sheet.Connect(this, target, to: param);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public Block WireTo(Block target, TagName? from, TagName? to)
    {
        var sheet = GetAncestor<Sheet>() ?? throw new InvalidOperationException("Can not connect blocks ...");
        sheet.Connect(this, target, from, to);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public Block WireTo(TagName target)
    {
        return this;
    }

    /// <summary>
    /// Moves this diagram element to the specified X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate to move this block to.</param>
    /// <param name="y">The Y coordinate to move this block to.</param>
    /// <returns>This</returns>
    public Block MoveTo(uint x, uint y)
    {
        Element.SetAttributeValue(L5XName.X, x);
        Element.SetAttributeValue(L5XName.Y, y);
        return this;
    }

    /// <summary>
    /// Moves this diagram element to the specified alphanumeric cell location.
    /// </summary>
    /// <param name="cell">The alphanumeric cell location to move this block to.</param>
    /// <exception cref="ArgumentException"><paramref name="cell"/> is null, empty, not two characters,
    /// does not start with a letter, or does not end with a digit.</exception>
    public Block MoveTo(string cell)
    {
        if (string.IsNullOrEmpty(cell))
            throw new ArgumentException("Can not perform function with null or empty cell location.");

        if (cell.Length != 2)
            throw new ArgumentException(
                $"Cell {cell} is not a valid length argument. Must be 2 character cell location.");

        if (!char.IsLetter(cell[0]))
            throw new ArgumentException($"Cell {cell} must start with a valid letter character");

        if (!char.IsDigit(cell[1]))
            throw new ArgumentException($"Cell {cell} must end with a valid number character");

        var x = (uint)(cell.ToUpper()[0] - 'A') * 200;
        var y = (uint)cell[1] * 200;

        Element.SetAttributeValue(L5XName.X, x);
        Element.SetAttributeValue(L5XName.Y, y);

        return this;
    }

    /// <summary>
    /// Moves the block by the specified offset values along the X and Y axes.
    /// </summary>
    /// <param name="dX">The offset by which to move the block along the X-axis.</param>
    /// <param name="dY">The offset by which to move the block along the Y-axis.</param>
    /// <returns>The current <see cref="Block"/> instance after applying the movement.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the resulting X or Y coordinate is negative.</exception>
    public Block MoveBy(int dX, int dY)
    {
        var x = X + dX;
        var y = Y + dY;

        if (x < 0 || y < 0)
            throw new ArgumentException($"Invalid coordinates ({x}, {y}). Coordinates must be non-negative.");

        if (x > uint.MaxValue || y > uint.MaxValue)
            throw new ArgumentException($"Movement by ({dX}, {dY}) would exceed maximum coordinate values.");

        Element.SetAttributeValue(L5XName.X, x);
        Element.SetAttributeValue(L5XName.Y, y);

        return this;
    }

    /// <summary>
    /// Converts the current <see cref="Block"/> into an <see cref="Instruction"/> based on its properties.
    /// </summary>
    /// <returns>An <see cref="Instruction"/> representation of the current <see cref="Block"/>.</returns>
    public Instruction ToInstruction()
    {
        var builder = new StringBuilder();

        builder.Append(Type);
        builder.Append($"({Operand}");

        //todo this is not technically right we need the tags that are getting mapped into the pins
        foreach (var argument in GetBlockArguents(Element))
        {
            builder.Append($",{argument}");
        }

        builder.Append(")");

        return Instruction.Parse(builder.ToString());
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            ValueType value => Equals(ID, value),
            Block block => Equals(ID, block.ID),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => ID.GetHashCode();

    #region Internal

    /// <summary>
    /// Gets the block type/name based on the FBD element type. This could be either the element name itself or
    /// the attribute value type/name depending on the FBD element. 
    /// </summary>
    private static string GetBlockType(XElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        var attribute = element.Name.LocalName switch
        {
            L5XName.Block => L5XName.Type,
            L5XName.Function => L5XName.Type,
            L5XName.AddOnInstruction => L5XName.Name,
            _ => null
        };

        if (attribute is null)
            return element.Name.LocalName;

        return element.Attribute(attribute)?.Value ?? throw element.L5XError(attribute);
    }

    /// <summary>
    /// Sets the block operand based on the specified <see cref="XElement"/> and <see cref="Argument"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> representing the block element to update.</param>
    /// <param name="value">The <see cref="Argument"/> to set as the operand.</param>
    /// <exception cref="NotSupportedException">Thrown when the block type does not support operands.</exception>
    private void SetBlockOperand(XElement element, Argument value)
    {
        switch (element.Name.LocalName)
        {
            case L5XName.IRef:
            case L5XName.ORef:
            case L5XName.Block:
            case L5XName.AddOnInstruction:
                Element.SetAttributeValue(L5XName.Operand, value);
                break;
            case L5XName.ICon:
            case L5XName.OCon:
                Element.SetAttributeValue(L5XName.Name, value);
                break;
            case L5XName.JSR:
            case L5XName.SBR:
            case L5XName.RET:
                Element.SetAttributeValue(L5XName.Routine, value);
                break;
            default:
                throw new NotSupportedException($"Block type '{GetElementType()}' does not support Operand.");
        }
    }

    /// <summary>
    /// Gets all pins configured for the current block in a flat list and returns them as a <see cref="TagName"/> value.
    /// </summary>
    private static IEnumerable<TagName> GetBlockPins(XElement element)
    {
        return element.Attributes()
            .Where(a => PinNames.Contains(a.Name.LocalName))
            .SelectMany(a => a.Value.Split(' '))
            .Select(t => t.ToTagName());
    }

    /// <summary>
    /// Retrieves the set of tag names associated with the block's element attributes.
    /// </summary>
    private static IEnumerable<TagName> GetBlockTags(XElement element)
    {
        var operand = element.GetBlockOperand();

        if (operand.Type.IsInvalid) return [];

        return element.Attributes()
            .Where(a => PinNames.Contains(a.Name.LocalName))
            .SelectMany(a => a.Value.Split(' '))
            .Select(t => TagName.Concat(operand, t));
    }

    /// <summary>
    /// Retrieves the set of tag names associated with the block's element attributes.
    /// </summary>
    private static IEnumerable<TagName> GetBlockArguents(XElement element)
    {
        return GetBlockInputs(element).Concat(GetBlockOutputs(element));
    }

    /// <summary>
    /// Retrieves a collection of input <see cref="TagName"/> objects associated with the specified block in the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> representing the block to extract input tags from.</param>
    /// <returns>A collection of <see cref="TagName"/> objects representing the block inputs.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the parent element is null or not of type Sheet.</exception>
    private static IEnumerable<TagName> GetBlockInputs(XElement element)
    {
        var sheet = element.Parent;

        if (sheet is null || sheet.Name.LocalName != L5XName.Sheet)
            throw new InvalidOperationException(
                $"No parent sheet element was found for element type '{element.Name.LocalName}'.");

        var inputWires = sheet.Elements(L5XName.Wire)
            .Where(w => w.Attribute(L5XName.ToID)?.Value == element.Attribute(L5XName.ID)?.Value)
            .Select(w => new
            {
                Id = w.Attribute(L5XName.FromID)?.Value,
                Param = w.Attribute(L5XName.FromParam)?.Value
            }).ToArray();

        var tagNames = new List<TagName>();

        foreach (var wire in inputWires)
        {
            var operand = sheet.Elements().Single(e => e.Attribute(L5XName.ID)?.Value == wire.Id).GetBlockOperand();
            tagNames.Add(TagName.Concat(operand, wire.Param ?? TagName.Empty));
        }

        return tagNames;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static IEnumerable<TagName> GetBlockOutputs(XElement element)
    {
        var sheet = element.Parent;

        if (sheet is null || sheet.Name.LocalName != L5XName.Sheet)
            throw new InvalidOperationException(
                $"No parent sheet element was found for element type '{element.Name.LocalName}'.");

        var outputWires = sheet.Elements(L5XName.Wire)
            .Where(w => w.Attribute(L5XName.FromID)?.Value == element.Attribute(L5XName.ID)?.Value)
            .Select(w => new
            {
                Id = w.Attribute(L5XName.ToID)?.Value,
                Param = w.Attribute(L5XName.ToParam)?.Value
            }).ToArray();

        var tagNames = new List<TagName>();

        foreach (var wire in outputWires)
        {
            var operand = sheet.Elements().Single(e => e.Attribute(L5XName.ID)?.Value == wire.Id).GetBlockOperand();
            tagNames.Add(TagName.Concat(operand, wire.Param ?? TagName.Empty));
        }

        return tagNames;
    }

    #endregion

    #region Elements

    /// <summary>
    /// Creates a new <c>IREF</c> function block element initialized with appropriate values.
    /// </summary>
    /// <param name="operand">The optional <see cref="Argument"/> to pass as the operand for the reference. This should
    /// be a tag name or immediate atomic value.</param>
    /// <returns>A <see cref="Block"/> instance representing the <c>IREF</c> block type.</returns>
    public static Block IREF(Argument? operand = null) => NewReference(L5XName.IRef, operand);

    /// <summary>
    /// Creates a new <c>OREF</c> function block element initialized with appropriate values.
    /// </summary>
    /// <param name="operand">The optional <see cref="Argument"/> to pass as the operand for the reference. This should
    /// be a tag name or immediate atomic value.</param>
    /// <returns>A <see cref="Block"/> instance representing the <c>OREF</c> block type.</returns>
    public static Block OREF(Argument? operand = null) => NewReference(L5XName.ORef, operand);

    /// <summary>
    /// Creates a new <c>ICON</c> function block element initialized with appropriate values.
    /// </summary>
    /// <param name="name">The required <see cref="string"/> name of the connector element.</param>
    /// <returns>A <see cref="Block"/> instance representing the <c>ICON</c> block type.</returns>
    public static Block ICON(string name) => NewConnector(L5XName.ICon, name);

    /// <summary>
    /// Creates a new <c>OCON</c> function block element initialized with appropriate values.
    /// </summary>
    /// <param name="name">The required <see cref="string"/> name of the connector element.</param>
    /// <returns>A <see cref="Block"/> instance representing the <c>OCON</c> block type.</returns>
    public static Block OCON(string name) => NewConnector(L5XName.OCon, name);

    /// <summary>
    /// Creates a new AOI <see cref="Block"/> with the provided name, operand, and pins
    /// </summary>
    /// <param name="name">The name of the AOI block.</param>
    /// <param name="operand">The <see cref="TagName"/> operand of the block.</param>
    /// <param name="pins">The set of pins for the block.</param>
    /// <returns>A new <see cref="Block"/> instance representing the <c>AOI</c> block type.</returns>
    public static Block AOI(string name, TagName? operand = null, params string[] pins) => NewAoi(name, operand, pins);

    /// <summary>
    /// Creates a new AOI <see cref="Block"/> with the provided definition and operand tag name.
    /// </summary>
    /// <param name="definition">The <see cref="AddOnInstruction"/> definition to initialize the block.</param>
    /// <param name="operand">The backing tag operand for the block.</param>
    /// <returns>A new <see cref="Block"/> instance representing the <c>AOI</c> block type.</returns>
    public static Block AOI(AddOnInstruction definition, TagName? operand = null) => NewAoi(definition, operand);

    /// <summary>
    /// Creates a new JSR routine <see cref="Block"/> with the provided routine name and optional parmaeters.
    /// </summary>
    /// <param name="routine">The name of the routine the block calls.</param>
    /// <param name="inputs">The set of input parmaeters to the routine.</param>
    /// <param name="outputs">The set of output parmaeters from the routine.</param>
    /// <returns>A new <see cref="Block"/> instance representing the JSR block type.</returns>
    public static Block JSR(string routine, string[]? inputs = null, string[]? outputs = null) =>
        NewRoutine(routine, inputs, outputs);

    /// <summary>
    /// Creates a new SBR routine <see cref="Block"/> with the provided routine name and input parmaeters.
    /// </summary>
    /// <param name="routine">The name of the routine the block calls.</param>
    /// <param name="parameters">The set of input parameters to the routine.</param>
    /// <returns>A new <see cref="Block"/> instance representing the SBR block type.</returns>
    public static Block SBR(string routine, params string[] parameters) => NewRoutine(L5XName.SBR, routine, parameters);

    /// <summary>
    /// Creates a new SBR routine <see cref="Block"/> with the provided routine name and input parmaeters.
    /// </summary>
    /// <param name="routine">The name of the routine the block calls.</param>
    /// <param name="parameters">The set of output parameters from the routine.</param>
    /// <returns>A new <see cref="Block"/> instance representing the RET block type.</returns>
    public static Block RET(string routine, params string[] parameters) => NewRoutine(L5XName.RET, routine, parameters);

    #endregion

    #region Blocks

    /// <summary>
    /// Creates a new <c>ABS</c> function block element with the optional operand tag name.
    /// </summary>
    /// <param name="operand">The optional tag name to initialize the block with. This will default to the function
    /// name with '_01' appended if not provided.</param>
    /// <returns>A <see cref="Block"/> object representing the <c>ABS</c> function.</returns>
    public static Block ABS(TagName? operand = null) =>
        NewBlock(nameof(ABS), operand, "Source Destination");

    ///<summary>
    /// Returns a new <c>ACS</c> Block element with the type initialized.
    ///</summary>
    public static Block ACS(TagName? operand = null) =>
        NewBlock(nameof(ACS), operand, "Source Destination");

    ///<summary>
    /// Returns a new <c>ADD</c> Block element with the type initialized.
    ///</summary>
    public static Block ADD(TagName? operand = null) =>
        NewBlock(nameof(ADD), operand, "SourceA SourceB Destination");

    ///<summary>
    /// Returns a new <c>ALM</c> Block element with the type initialized.
    ///</summary>
    public static Block ALM(TagName? operand = null) => NewBlock(nameof(ALM), operand,
        "In HHAlarm HAlarm LAlarm LLAlarm ROCPosAlarm ROCNegAlarm");

    ///<summary>
    /// Returns a new <c>ALMA</c> Block element with the type initialized.
    ///</summary>
    public static Block ALMA(TagName? operand = null) => NewBlock(nameof(ALMA), operand,
        "In HHInAlarm HInAlarm LInAlarm LLInAlarm ROCPosInAlarm ROCNegInAlarm HHAcked HAcked LAcked LLAcked ROCPosAcked ROCNegAcked Suppressed Disabled");

    ///<summary>
    /// Returns a new <c>ALMD</c> Block element with the type initialized.
    ///</summary>
    public static Block ALMD(TagName? operand = null) =>
        NewBlock(nameof(ALMD), operand, "In InAlarm Acked Suppressed Disabled");

    ///<summary>
    /// Returns a new <c>AND</c> Block element with the type initialized.
    ///</summary>
    public static Block AND(TagName? operand = null) =>
        NewBlock(nameof(AND), operand, "SourceA SourceB Destination");

    ///<summary>
    /// Returns a new <c>ASN</c> Block element with the type initialized.
    ///</summary>
    public static Block ASN(TagName? operand = null) =>
        NewBlock(nameof(ASN), operand, "Source Destination");

    ///<summary>
    /// Returns a new <c>ATN</c> Block element with the type initialized.
    ///</summary>
    public static Block ATN(TagName? operand = null) =>
        NewBlock(nameof(ATN), operand, "Source Destination");

    ///<summary>
    /// Returns a new <c>BAND</c> Block element with the type initialized.
    ///</summary>
    public static Block BAND(TagName? operand = null) =>
        NewBlock(nameof(BAND), operand, "In1 In2 In3 In4 Out");

    ///<summary>
    /// Returns a new <c>BNOT</c> Block element with the type initialized.
    ///</summary>
    public static Block BNOT(TagName? operand = null) => NewBlock(nameof(BNOT), operand, "In Out");

    ///<summary>
    /// Returns a new <c>BOR</c> Block element with the type initialized.
    ///</summary>
    public static Block BOR(TagName? operand = null) =>
        NewBlock(nameof(BOR), operand, "In1 In2 In3 In4 Out");

    ///<summary>
    /// Returns a new <c>BTDT</c> Block element with the type initialized.
    ///</summary>
    public static Block BTDT(TagName? operand = null) => NewBlock(nameof(BTDT), operand,
        "Source SourceBit Length DestBit Target Dest");

    ///<summary>
    /// Returns a new <c>BXOR</c> Block element with the type initialized.
    ///</summary>
    public static Block BXOR(TagName? operand = null) => NewBlock(nameof(BXOR), operand, "In1 In2 Out");

    ///<summary>
    /// Returns a new <c>COS</c> Block element with the type initialized.
    ///</summary>
    public static Block COS(TagName? operand = null) => NewBlock(nameof(COS), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>CTUD</c> Block element with the type initialized.
    ///</summary>
    public static Block CTUD(TagName? operand = null) =>
        NewBlock(nameof(CTUD), operand, "CUEnable CDEnable PRE Reset ACC DN");

    ///<summary>
    /// Returns a new <c>D2SD</c> Block element with the type initialized.
    ///</summary>
    public static Block D2SD(TagName? operand = null) => NewBlock(nameof(D2SD), operand,
        "ProgCommand State0Perm State1Perm FB0 FB1 HandFB ProgProgReq ProgOperReq ProgOverrideReq ProgHandReq Out Device0State Device1State CommandStatus FaultAlarm ModeAlarm ProgOper Override Hand");

    ///<summary>
    /// Returns a new <c>D3SD</c> Block element with the type initialized.
    ///</summary>
    public static Block D3SD(TagName? operand = null) => NewBlock(nameof(D3SD), operand,
        "Prog0Command Prog1Command Prog2Command State0Perm State1Perm State2Perm FB0 FB1 FB2 FB3 HandFB0 HandFB1 HandFB2 ProgProgReq ProgOperReq ProgOverrideReq ProgHandReq Out0 Out1 Out2 Device0State Device1State Device2State Command0Status Command1Status Command2Status FaultAlarm ModeAlarm ProgOper Override Hand");

    ///<summary>
    /// Returns a new <c>DEDT</c> Block element with the type initialized.
    ///</summary>
    public static Block DEDT(TagName? operand = null) => NewBlock(nameof(DEDT), operand, "In Out");

    ///<summary>
    /// Returns a new <c>DEG</c> Block element with the type initialized.
    ///</summary>
    public static Block DEG(TagName? operand = null) => NewBlock(nameof(DEG), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>DERV</c> Block element with the type initialized.
    ///</summary>
    public static Block DERV(TagName? operand = null) =>
        NewBlock(nameof(DERV), operand, "In ByPass Out");

    ///<summary>
    /// Returns a new <c>DFF</c> Block element with the type initialized.
    ///</summary>
    public static Block DFF(TagName? operand = null) =>
        NewBlock(nameof(DFF), operand, "D Clear Clock Q QNot");

    ///<summary>
    /// Returns a new <c>DIV</c> Block element with the type initialized.
    ///</summary>
    public static Block DIV(TagName? operand = null) =>
        NewBlock(nameof(DIV), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>ESEL</c> Block element with the type initialized.
    ///</summary>
    public static Block ESEL(TagName? operand = null) => NewBlock(nameof(ESEL), operand,
        "In1 In2 In3 In4 In5 In6 ProgSelector ProgProgReq ProgOperReq ProgOverrideReq Out SelectedIn ProgOper Override");

    ///<summary>
    /// Returns a new <c>EQU</c> Block element with the type initialized.
    ///</summary>
    public static Block EQU(TagName? operand = null) => NewBlock(nameof(EQU), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>FGEN</c> Block element with the type initialized.
    ///</summary>
    public static Block FGEN(TagName? operand = null) => NewBlock(nameof(FGEN), operand, "In Out");

    ///<summary>
    /// Returns a new <c>FRD</c> Block element with the type initialized.
    ///</summary>
    public static Block FRD(TagName? operand = null) => NewBlock(nameof(FRD), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>GEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block GEQ(TagName? operand = null) => NewBlock(nameof(GEQ), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>GRT</c> Block element with the type initialized.
    ///</summary>
    public static Block GRT(TagName? operand = null) => NewBlock(nameof(GRT), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>HLL</c> Block element with the type initialized.
    ///</summary>
    public static Block HLL(TagName? operand = null) =>
        NewBlock(nameof(HLL), operand, "In Out HighAlarm LowAlarm");

    ///<summary>
    /// Returns a new <c>HPF</c> Block element with the type initialized.
    ///</summary>
    public static Block HPF(TagName? operand = null) => NewBlock(nameof(HPF), operand, "In Out");

    ///<summary>
    /// Returns a new <c>INTG</c> Block element with the type initialized.
    ///</summary>
    public static Block INTG(TagName? operand = null) => NewBlock(nameof(INTG), operand, "In Out");

    ///<summary>
    /// Returns a new <c>JKFF</c> Block element with the type initialized.
    ///</summary>
    public static Block JKFF(TagName? operand = null) =>
        NewBlock(nameof(JKFF), operand, "Clear Clock Q QNot");

    ///<summary>
    /// Returns a new <c>LEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block LEQ(TagName? operand = null) => NewBlock(nameof(LEQ), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>LES</c> Block element with the type initialized.
    ///</summary>
    public static Block LES(TagName? operand = null) => NewBlock(nameof(LES), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>LIM</c> Block element with the type initialized.
    ///</summary>
    public static Block LIM(TagName? operand = null) =>
        NewBlock(nameof(LIM), operand, "LowLimit Test HighLimit");

    ///<summary>
    /// Returns a new <c>LN</c> Block element with the type initialized.
    ///</summary>
    public static Block LN(TagName? operand = null) => NewBlock(nameof(LN), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>LOG</c> Block element with the type initialized.
    ///</summary>
    public static Block LOG(TagName? operand = null) => NewBlock(nameof(LOG), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>LPF</c> Block element with the type initialized.
    ///</summary>
    public static Block LPF(TagName? operand = null) => NewBlock(nameof(LPF), operand, "In Out");

    ///<summary>
    /// Returns a new <c>MAVE</c> Block element with the type initialized.
    ///</summary>
    public static Block MAVE(TagName? operand = null) => NewBlock(nameof(MAVE), operand, "In Out");

    ///<summary>
    /// Returns a new <c>MAXC</c> Block element with the type initialized.
    ///</summary>
    public static Block MAXC(TagName? operand = null) =>
        NewBlock(nameof(MAXC), operand, "In Reset ResetValue Out");

    ///<summary>
    /// Returns a new <c>MEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block MEQ(TagName? operand = null) =>
        NewBlock(nameof(MEQ), operand, "Source Mask Compare");

    ///<summary>
    /// Returns a new <c>MINC</c> Block element with the type initialized.
    ///</summary>
    public static Block MINC(TagName? operand = null) =>
        NewBlock(nameof(MINC), operand, "In Reset ResetValue Out");

    ///<summary>
    /// Returns a new <c>MOD</c> Block element with the type initialized.
    ///</summary>
    public static Block MOD(TagName? operand = null) =>
        NewBlock(nameof(MOD), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>MSTD</c> Block element with the type initialized.
    ///</summary>
    public static Block MSTD(TagName? operand = null) =>
        NewBlock(nameof(MSTD), operand, "In SampleEnable Out");

    ///<summary>
    /// Returns a new <c>MUL</c> Block element with the type initialized.
    ///</summary>
    public static Block MUL(TagName? operand = null) =>
        NewBlock(nameof(MUL), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>MUX</c> Block element with the type initialized.
    ///</summary>
    public static Block MUX(TagName? operand = null) => NewBlock(nameof(MUX), operand,
        "In1 In2 In3 In4 In5 In6 In7 In8 Selector Out");

    ///<summary>
    /// Returns a new <c>MVMT</c> Block element with the type initialized.
    ///</summary>
    public static Block MVMT(TagName? operand = null) =>
        NewBlock(nameof(MVMT), operand, "Source Mask Target Dest");

    ///<summary>
    /// Returns a new <c>NEG</c> Block element with the type initialized.
    ///</summary>
    public static Block NEG(TagName? operand = null) => NewBlock(nameof(NEG), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>NEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block NEQ(TagName? operand = null) => NewBlock(nameof(NEQ), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>NOT</c> Block element with the type initialized.
    ///</summary>
    public static Block NOT(TagName? operand = null) => NewBlock(nameof(NOT), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>NTCH</c> Block element with the type initialized.
    ///</summary>
    public static Block NTCH(TagName? operand = null) => NewBlock(nameof(NTCH), operand, "In Out");

    ///<summary>
    /// Returns a new <c>OR</c> Block element with the type initialized.
    ///</summary>
    public static Block OR(TagName? operand = null) =>
        NewBlock(nameof(OR), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>OSFI</c> Block element with the type initialized.
    ///</summary>
    public static Block OSFI(TagName? operand = null) =>
        NewBlock(nameof(OSFI), operand, "InputBit OutputBit");

    ///<summary>
    /// Returns a new <c>OSRI</c> Block element with the type initialized.
    ///</summary>
    public static Block OSRI(TagName? operand = null) =>
        NewBlock(nameof(OSRI), operand, "InputBit OutputBit");

    ///<summary>
    /// Returns a new <c>PI</c> Block element with the type initialized.
    ///</summary>
    public static Block PI(TagName? operand = null) => NewBlock(nameof(PI), operand, "In Out");

    ///<summary>
    /// Returns a new <c>PIDE</c> Block element with the type initialized.
    ///</summary>
    public static Block PIDE(TagName? operand = null) => NewBlock(nameof(PIDE), operand,
        "PV SPProg SPCascade RatioProg CVProg FF HandFB ProgProgReq ProgOperReq ProgCasRatReq ProgAutoReq ProgManualReq ProgOverrideReq ProgHandReq CVEU SP PVHHAlarm PVHAlarm PVLAlarm PVLLAlarm PVROCPosAlarm PVROCNegAlarm DevHHAlarm DevHAlarm DevLAlarm DevLLAlarm ProgOper CasRat Auto Manual Override Hand");

    ///<summary>
    /// Returns a new <c>PMUL</c> Block element with the type initialized.
    ///</summary>
    public static Block PMUL(TagName? operand = null) => NewBlock(nameof(PMUL), operand, "In Multiplier Out");

    ///<summary>
    /// Returns a new <c>POSP</c> Block element with the type initialized.
    ///</summary>
    public static Block POSP(TagName? operand = null) => NewBlock(nameof(POSP), operand,
        "SP Position OpenedFB ClosedFB OpenOut CloseOut");

    ///<summary>
    /// Returns a new <c>RAD</c> Block element with the type initialized.
    ///</summary>
    public static Block RAD(TagName? operand = null) => NewBlock(nameof(RAD), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>RESD</c> Block element with the type initialized.
    ///</summary>
    public static Block RESD(TagName? operand = null) =>
        NewBlock(nameof(RESD), operand, "Set Reset Out OutNot");

    ///<summary>
    /// Returns a new <c>RLIM</c> Block element with the type initialized.
    ///</summary>
    public static Block RLIM(TagName? operand = null) =>
        NewBlock(nameof(RLIM), operand, "In ByPass Out");

    ///<summary>
    /// Returns a new <c>RMPS</c> Block element with the type initialized.
    ///</summary>
    public static Block RMPS(TagName? operand = null) => NewBlock(nameof(RMPS), operand,
        "PV CurrentSegProg OutProg SoakTimeProg ProgProgReq ProgOperReq ProgAutoReq ProgManualReq ProgHoldReq Out CurrentSeg SoakTimeLeft GuarRampOn GuarSoakOn ProgOper Auto Manual Hold");

    ///<summary>
    /// Returns a new <c>RTOR</c> Block element with the type initialized.
    ///</summary>
    public static Block RTOR(TagName? operand = null) =>
        NewBlock(nameof(RTOR), operand, "TimerEnable PRE Reset ACC DN");

    ///<summary>
    /// Returns a new <c>SCL</c> Block element with the type initialized.
    ///</summary>
    public static Block SCL(TagName? operand = null) => NewBlock(nameof(SCL), operand, "In Out");

    ///<summary>
    /// Returns a new <c>SCRV</c> Block element with the type initialized.
    ///</summary>
    public static Block SCRV(TagName? operand = null) => NewBlock(nameof(SCRV), operand, "In Out");

    ///<summary>
    /// Returns a new <c>SEL</c> Block element with the type initialized.
    ///</summary>
    public static Block SEL(TagName? operand = null) =>
        NewBlock(nameof(SEL), operand, "In1 In2 SelectorIn Out");

    ///<summary>
    /// Returns a new <c>SETD</c> Block element with the type initialized.
    ///</summary>
    public static Block SETD(TagName? operand = null) =>
        NewBlock(nameof(SETD), operand, "Set Reset Out OutNot");

    ///<summary>
    /// Returns a new <c>SIN</c> Block element with the type initialized.
    ///</summary>
    public static Block SIN(TagName? operand = null) =>
        NewBlock(nameof(SIN), operand, "Source Destination");

    ///<summary>
    /// Returns a new <c>SNEG</c> Block element with the type initialized.
    ///</summary>
    public static Block SNEG(TagName? operand = null) =>
        NewBlock(nameof(SNEG), operand, "In NegateEnable Out");

    ///<summary>
    /// Returns a new <c>SOC</c> Block element with the type initialized.
    ///</summary>
    public static Block SOC(TagName? operand = null) => NewBlock(nameof(SOC), operand, "In Out");

    ///<summary>
    /// Returns a new <c>SQR</c> Block element with the type initialized.
    ///</summary>
    public static Block SQR(TagName? operand = null) => NewBlock(nameof(SQR), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>SRTP</c> Block element with the type initialized.
    ///</summary>
    public static Block SRTP(TagName? operand = null) => NewBlock(nameof(SRTP), operand,
        "In HeatOut CoolOut HeatTimePercent CoolTimePercent");

    ///<summary>
    /// Returns a new <c>SSUM</c> Block element with the type initialized.
    ///</summary>
    public static Block SSUM(TagName? operand = null) => NewBlock(nameof(SSUM), operand,
        "In1 Select1 In2 Select2 In3 Select3 In4 Select4 Out");

    ///<summary>
    /// Returns a new <c>SUB</c> Block element with the type initialized.
    ///</summary>
    public static Block SUB(TagName? operand = null) =>
        NewBlock(nameof(SUB), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>TAN</c> Block element with the type initialized.
    ///</summary>
    public static Block TAN(TagName? operand = null) => NewBlock(nameof(TAN), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>TOD</c> Block element with the type initialized.
    ///</summary>
    public static Block TOD(TagName? operand = null) => NewBlock(nameof(TOD), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>TOFR</c> Block element with the type initialized.
    ///</summary>
    public static Block TOFR(TagName? operand = null) =>
        NewBlock(nameof(TOFR), operand, "TimerEnable PRE Reset ACC DN");

    ///<summary>
    /// Returns a new <c>TONR</c> Block element with the type initialized.
    ///</summary>
    public static Block TONR(TagName? operand = null) =>
        NewBlock(nameof(TONR), operand, "TimerEnable PRE Reset ACC DN");

    ///<summary>
    /// Returns a new <c>TOT</c> Block element with the type initialized.
    ///</summary>
    public static Block TOT(TagName? operand = null) => NewBlock(nameof(TOT), operand,
        "In ProgProgReq ProgOperReq ProgStartReq ProgStopReq ProgResetReq Total OldTotal ProgOper RunStop ProgResetDone TargetFlag TargetDev1Flag TargetDev2Flag");

    ///<summary>
    /// Returns a new <c>TRN</c> Block element with the type initialized.
    ///</summary>
    public static Block TRN(TagName? operand = null) => NewBlock(nameof(TRN), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>UPDN</c> Block element with the type initialized.
    ///</summary>
    public static Block UPDN(TagName? operand = null) =>
        NewBlock(nameof(UPDN), operand, "InPlus InMinus Out");

    ///<summary>
    /// Returns a new <c>XOR</c> Block element with the type initialized.
    ///</summary>
    public static Block XOR(TagName? operand = null) =>
        NewBlock(nameof(XOR), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>XPY</c> Block element with the type initialized.
    ///</summary>
    public static Block XPY(TagName? operand = null) =>
        NewBlock(nameof(XPY), operand, "SourceA SourceB Dest");

    #endregion

    #region Functions

    /// <summary>
    /// Creates a new <c>ABS__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>ABS__F</c> function block.</returns>
    public static Block ABS__F() => NewFunction(nameof(ABS__F));

    /// <summary>
    /// Creates a new <c>ADD__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>ADD__F</c> function block.</returns>
    public static Block ADD__F() => NewFunction(nameof(ADD__F));

    /// <summary>
    /// Creates a new <c>BAND__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>BAND__F</c> function block.</returns>
    public static Block BAND__F() => NewFunction(nameof(BAND__F));

    /// <summary>
    /// Creates a new <c>BNOT__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>BNOT__F</c> function block.</returns>
    public static Block BNOT__F() => NewFunction(nameof(BNOT__F));

    /// <summary>
    /// Creates a new <c>BXOR__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>BXOR__F</c> function block.</returns>
    public static Block BXOR__F() => NewFunction(nameof(BXOR__F));

    /// <summary>
    /// Creates a new <c>DIV__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>DIV__F</c> function block.</returns>
    public static Block DIV__F() => NewFunction(nameof(DIV__F));

    /// <summary>
    /// Creates a new <c>EQU__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>EQU__F</c> function block.</returns>
    public static Block EQU__F() => NewFunction(nameof(EQU__F));

    /// <summary>
    /// Creates a new <c>GEQ__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>GEQ__F</c> function block.</returns>
    public static Block GEQ__F() => NewFunction(nameof(GEQ__F));

    /// <summary>
    /// Creates a new <c>GRT__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>GRT__F</c> function block.</returns>
    public static Block GRT__F() => NewFunction(nameof(GRT__F));

    /// <summary>
    /// Creates a new <c>LEQ__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>LEQ__F</c> function block.</returns>
    public static Block LEQ__F() => NewFunction(nameof(LEQ__F));

    /// <summary>
    /// Creates a new <c>LES__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>LES__F</c> function block.</returns>
    public static Block LES__F() => NewFunction(nameof(LES__F));

    /// <summary>
    /// Creates a new <c>LIM__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>LIM__F</c> function block.</returns>
    public static Block LIM__F() => NewFunction(nameof(LIM__F));

    /// <summary>
    /// Creates a new <c>MEQ__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>MEQ__F</c> function block.</returns>
    public static Block MEQ__F() => NewFunction(nameof(MEQ__F));

    /// <summary>
    /// Creates a new <c>MOD__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>MOD__F</c> function block.</returns>
    public static Block MOD__F() => NewFunction(nameof(MOD__F));

    /// <summary>
    /// Creates a new <c>MUL__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>MUL__F</c> function block.</returns>
    public static Block MUL__F() => NewFunction(nameof(MUL__F));

    /// <summary>
    /// Creates a new <c>NEG__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>NEG__F</c> function block.</returns>
    public static Block NEG__F() => NewFunction(nameof(NEG__F));

    /// <summary>
    /// Creates a new <c>NEQ__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>NEQ__F</c> function block.</returns>
    public static Block NEQ__F() => NewFunction(nameof(NEQ__F));

    /// <summary>
    /// Creates a new <c>SQR__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>SQR__F</c> function block.</returns>
    public static Block SQR__F() => NewFunction(nameof(SQR__F));

    /// <summary>
    /// Creates a new <c>SUB__F</c> function block element initialized with appropriate values.
    /// </summary>
    /// <returns>A <see cref="Block"/> instance representing the <c>SUB__F</c> function block.</returns>
    public static Block SUB__F() => NewFunction(nameof(SUB__F));

    #endregion

    #region Factories

    private static Block NewReference(string type, Argument? operand = null)
    {
        var element = new XElement(type);
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Operand, operand ?? Argument.Empty));
        return new Block(element);
    }

    private static Block NewConnector(string type, string name)
    {
        var element = new XElement(type);
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Name, name));
        return new Block(element);
    }

    private static Block NewBlock(string type, TagName? operand = null, string? pins = null)
    {
        var element = new XElement(L5XName.Block);
        element.Add(new XAttribute(L5XName.Type, type));
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Operand, operand ?? $"{type}_01"));
        element.Add(new XAttribute(L5XName.VisiblePins, pins ?? string.Empty));
        return new Block(element);
    }

    private static Block NewFunction(string type)
    {
        var element = new XElement(L5XName.Function);
        element.Add(new XAttribute(L5XName.Type, type));
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        return new Block(element);
    }

    private static Block NewAoi(string type, TagName? operand = null, params string[] pins)
    {
        var element = new XElement(L5XName.AddOnInstruction);
        element.Add(new XAttribute(L5XName.Name, type));
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Operand, operand ?? Argument.Empty));
        element.Add(new XAttribute(L5XName.VisiblePins, pins.Combine(' ')));
        return new Block(element);
    }

    private static Block NewAoi(AddOnInstruction definition, TagName? operand = null)
    {
        var element = new XElement(L5XName.AddOnInstruction);
        element.Add(new XAttribute(L5XName.Name, definition.Name));
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Operand, operand ?? Argument.Empty));

        var pins = definition.Parameters.Where(p => p.Visible == true).Select(p => p.Name).Combine(' ');
        element.Add(new XAttribute(L5XName.VisiblePins, pins));

        return new Block(element);
    }

    private static Block NewRoutine(string routine, string[]? inputs = null, string[]? outputs = null)
    {
        var element = new XElement(L5XName.JSR);
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Routine, routine));
        element.Add(new XAttribute(L5XName.In, inputs.Combine(' ')));
        element.Add(new XAttribute(L5XName.Ret, outputs.Combine(' ')));
        return new Block(element);
    }

    private static Block NewRoutine(string type, string routine, params string[] parameters)
    {
        var element = new XElement(type);
        element.Add(new XAttribute(L5XName.ID, 0));
        element.Add(new XAttribute(L5XName.X, 0));
        element.Add(new XAttribute(L5XName.Y, 0));
        element.Add(new XAttribute(L5XName.Routine, routine));
        element.Add(new XAttribute(L5XName.VisiblePins, parameters.Combine(' ')));
        return new Block(element);
    }

    #endregion
}