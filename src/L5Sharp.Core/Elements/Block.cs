using System;
using System.Collections.Generic;
using System.Linq;
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
public class Block : DiagramElement
{
    /// <summary>
    /// Creates a new <see cref="Block"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Block(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The descriptive indication of the location of this <see cref="DiagramElement"/> within the containing <c>Diagram</c>. 
    /// </summary>
    /// <value>A <see cref="string"/> indicating the cell and optional sheet or chart number where the block is located.</value>
    /// <remarks>
    /// This is an internally defined value so that we can identify instructions when referencing components.
    /// </remarks>
    public override string Location =>
        Sheet is not null ? $"Sheet {Sheet.Number} {Cell} ({X}, {Y})" : $"{Cell} ({X}, {Y})";

    /// <summary>
    /// Represents the instuction, function, or element type name of the <see cref="Block"/> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the block type name.</value>
    /// <remarks></remarks>
    public string Type
    {
        get
        {
            return L5XType switch
            {
                L5XName.Block => Element.Attribute(L5XName.Type)?.Value ?? throw Element.L5XError(L5XName.Type),
                L5XName.Function => Element.Attribute(L5XName.Type)?.Value ?? throw Element.L5XError(L5XName.Type),
                L5XName.AddOnInstruction => Element.Attribute(L5XName.Name)?.Value ??
                                            throw Element.L5XError(L5XName.Name),
                _ => Element.L5XType(),
            };
        }
    }

    /// <summary>
    /// Represents the Operand property of a generic function block element. This can be the backing tag name,
    /// reference name, connector name, or routine name depending on the block type.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when attempting to set the operand for an unsupported block type.</exception>
    /// <value>An <see cref="Argument"/> object containing the value, which could be a tag name, immediate value, or simple string name.</value>
    /// <remarks>
    /// Since this class models all the different function block types, this proprty has to determine which attribute
    /// to read/write to internally depending on the type. For IRef, ORef, Block, and AOI blocks, this represents the
    /// Operand tag name. For an ICon and OCOn, this represents the connector Name. For a routine blocks (JSR, SBR, RET),
    /// this represents the Routine name. Note that this is nullable since a Function
    /// block does not have an operand or backing tag property, and therefore attempting to set a value for that block type
    /// will result in an exception.
    /// </remarks>
    public Argument? Operand
    {
        get
        {
            return L5XType switch
            {
                L5XName.ICon => Element.Attribute(L5XName.Name)?.Value.Parse<Argument>(),
                L5XName.OCon => Element.Attribute(L5XName.Name)?.Value.Parse<Argument>(),
                L5XName.JSR => Element.Attribute(L5XName.Routine)?.Value.Parse<Argument>(),
                L5XName.SBR => Element.Attribute(L5XName.Routine)?.Value.Parse<Argument>(),
                L5XName.RET => Element.Attribute(L5XName.Routine)?.Value.Parse<Argument>(),
                _ => Element.Attribute(L5XName.Operand)?.Value.Parse<Argument>()
            };
        }
        set
        {
            switch (L5XType)
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
                    throw new NotSupportedException($"Block type '{L5XType}' does not support Operand.");
            }
        }
    }

    /// <summary>
    /// Whether or not to hide the description for the block element.
    /// </summary>
    /// <value><c>true</c> if the description is hidden; Otherwise; <c>false</c>.</value>
    /// <remarks>This property is only found on IREF, OREF, and Block type elements.</remarks>
    public bool? HideDesc
    {
        get => GetValue<bool?>();
        set => SetValue(value);
    }

    /// <summary>
    /// The pins
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    public IEnumerable<TagName> Pins
    {
        get
        {
            return L5XType switch
            {
                L5XName.Block => GetValues<TagName>(L5XName.VisiblePins),
                L5XName.AddOnInstruction => GetValues<TagName>(L5XName.VisiblePins),
                L5XName.JSR => GetValues<TagName>(L5XName.In).Concat(GetValues<TagName>(L5XName.Ret)),
                L5XName.SBR => GetValues<TagName>(L5XName.In),
                L5XName.RET => GetValues<TagName>(L5XName.Ret),
                _ => Enumerable.Empty<TagName>()
            };
        }
    }

    /// <summary>
    /// The <see cref="Core.Sheet"/> that this block is contained within.
    /// </summary>
    /// <value>A <see cref="Core.Sheet"/></value>
    public Sheet? Sheet => Element.Parent is not null ? new Sheet(Element.Parent) : default;

    /// <summary>
    /// Retrieves a collection of tag names that the function block contains.
    /// </summary>
    /// <value>A collection of <see cref="TagName"/> values.</value>
    /// <remarks>
    /// This is not a underlying block property of the XML but rather a helper that combins the
    /// <see cref="Operand"/> and <see cref="Pins"/> to form a set of tag names that can be references by the block.
    /// </remarks>
    public IEnumerable<TagName> Tags
    {
        get
        {
            if (Operand is null || !Operand.IsTag) return Enumerable.Empty<TagName>();
            var tags = new List<TagName> { (TagName)Operand };
            tags.AddRange(Pins.Select(p => TagName.Concat(Operand.ToString(), p)));
            return tags;
        }
    }

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        var references = new List<CrossReference> { new(Element, L5XName.Instruction, Type) };

        if (Operand is null || !Operand.IsTag)
            return references;

        references.Add(new CrossReference(Element, L5XName.Tag, Operand.ToString(), Type));
        references.AddRange(Tags.Select(t => new CrossReference(Element, L5XName.Tag, t, Type)));
        return references;
    }

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
    /// Returns a ne <c>ACS</c> Block element with the type initialized.
    ///</summary>
    public static Block ACS(TagName? operand = null) =>
        NewBlock(nameof(ACS), operand, "Source Destination");

    ///<summary>
    /// Returns a ne <c>ADD</c> Block element with the type initialized.
    ///</summary>
    public static Block ADD(TagName? operand = null) =>
        NewBlock(nameof(ADD), operand, "SourceA SourceB Destination");

    ///<summary>
    /// Returns a ne <c>ALM</c> Block element with the type initialized.
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
    /// Returns a ne <c>AND</c> Block element with the type initialized.
    ///</summary>
    public static Block AND(TagName? operand = null) =>
        NewBlock(nameof(AND), operand, "SourceA SourceB Destination");

    ///<summary>
    /// Returns a ne <c>ASN</c> Block element with the type initialized.
    ///</summary>
    public static Block ASN(TagName? operand = null) =>
        NewBlock(nameof(ASN), operand, "Source Destination");

    ///<summary>
    /// Returns a ne <c>ATN</c> Block element with the type initialized.
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
    /// Returns a ne <c>BOR</c> Block element with the type initialized.
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
    /// Returns a ne <c>COS</c> Block element with the type initialized.
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
    /// Returns a ne <c>DEG</c> Block element with the type initialized.
    ///</summary>
    public static Block DEG(TagName? operand = null) => NewBlock(nameof(DEG), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>DERV</c> Block element with the type initialized.
    ///</summary>
    public static Block DERV(TagName? operand = null) =>
        NewBlock(nameof(DERV), operand, "In ByPass Out");

    ///<summary>
    /// Returns a ne <c>DFF</c> Block element with the type initialized.
    ///</summary>
    public static Block DFF(TagName? operand = null) =>
        NewBlock(nameof(DFF), operand, "D Clear Clock Q QNot");

    ///<summary>
    /// Returns a ne <c>DIV</c> Block element with the type initialized.
    ///</summary>
    public static Block DIV(TagName? operand = null) =>
        NewBlock(nameof(DIV), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>ESEL</c> Block element with the type initialized.
    ///</summary>
    public static Block ESEL(TagName? operand = null) => NewBlock(nameof(ESEL), operand,
        "In1 In2 In3 In4 In5 In6 ProgSelector ProgProgReq ProgOperReq ProgOverrideReq Out SelectedIn ProgOper Override");

    ///<summary>
    /// Returns a ne <c>EQU</c> Block element with the type initialized.
    ///</summary>
    public static Block EQU(TagName? operand = null) => NewBlock(nameof(EQU), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a new <c>FGEN</c> Block element with the type initialized.
    ///</summary>
    public static Block FGEN(TagName? operand = null) => NewBlock(nameof(FGEN), operand, "In Out");

    ///<summary>
    /// Returns a ne <c>FRD</c> Block element with the type initialized.
    ///</summary>
    public static Block FRD(TagName? operand = null) => NewBlock(nameof(FRD), operand, "Source Dest");

    ///<summary>
    /// Returns a ne <c>GEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block GEQ(TagName? operand = null) => NewBlock(nameof(GEQ), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a ne <c>GRT</c> Block element with the type initialized.
    ///</summary>
    public static Block GRT(TagName? operand = null) => NewBlock(nameof(GRT), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a ne <c>HLL</c> Block element with the type initialized.
    ///</summary>
    public static Block HLL(TagName? operand = null) =>
        NewBlock(nameof(HLL), operand, "In Out HighAlarm LowAlarm");

    ///<summary>
    /// Returns a ne <c>HPF</c> Block element with the type initialized.
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
    /// Returns a ne <c>LEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block LEQ(TagName? operand = null) => NewBlock(nameof(LEQ), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a ne <c>LES</c> Block element with the type initialized.
    ///</summary>
    public static Block LES(TagName? operand = null) => NewBlock(nameof(LES), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a ne <c>LIM</c> Block element with the type initialized.
    ///</summary>
    public static Block LIM(TagName? operand = null) =>
        NewBlock(nameof(LIM), operand, "LowLimit Test HighLimit");

    ///<summary>
    /// Returns a n <c>LN</c> Block element with the type initialized.
    ///</summary>
    public static Block LN(TagName? operand = null) => NewBlock(nameof(LN), operand, "Source Dest");

    ///<summary>
    /// Returns a ne <c>LOG</c> Block element with the type initialized.
    ///</summary>
    public static Block LOG(TagName? operand = null) => NewBlock(nameof(LOG), operand, "Source Dest");

    ///<summary>
    /// Returns a ne <c>LPF</c> Block element with the type initialized.
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
    /// Returns a ne <c>MEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block MEQ(TagName? operand = null) =>
        NewBlock(nameof(MEQ), operand, "Source Mask Compare");

    ///<summary>
    /// Returns a new <c>MINC</c> Block element with the type initialized.
    ///</summary>
    public static Block MINC(TagName? operand = null) =>
        NewBlock(nameof(MINC), operand, "In Reset ResetValue Out");

    ///<summary>
    /// Returns a ne <c>MOD</c> Block element with the type initialized.
    ///</summary>
    public static Block MOD(TagName? operand = null) =>
        NewBlock(nameof(MOD), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a new <c>MSTD</c> Block element with the type initialized.
    ///</summary>
    public static Block MSTD(TagName? operand = null) =>
        NewBlock(nameof(MSTD), operand, "In SampleEnable Out");

    ///<summary>
    /// Returns a ne <c>MUL</c> Block element with the type initialized.
    ///</summary>
    public static Block MUL(TagName? operand = null) =>
        NewBlock(nameof(MUL), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a ne <c>MUX</c> Block element with the type initialized.
    ///</summary>
    public static Block MUX(TagName? operand = null) => NewBlock(nameof(MUX), operand,
        "In1 In2 In3 In4 In5 In6 In7 In8 Selector Out");

    ///<summary>
    /// Returns a new <c>MVMT</c> Block element with the type initialized.
    ///</summary>
    public static Block MVMT(TagName? operand = null) =>
        NewBlock(nameof(MVMT), operand, "Source Mask Target Dest");

    ///<summary>
    /// Returns a ne <c>NEG</c> Block element with the type initialized.
    ///</summary>
    public static Block NEG(TagName? operand = null) => NewBlock(nameof(NEG), operand, "Source Dest");

    ///<summary>
    /// Returns a ne <c>NEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block NEQ(TagName? operand = null) => NewBlock(nameof(NEQ), operand, "SourceA SourceB");

    ///<summary>
    /// Returns a ne <c>NOT</c> Block element with the type initialized.
    ///</summary>
    public static Block NOT(TagName? operand = null) => NewBlock(nameof(NOT), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>NTCH</c> Block element with the type initialized.
    ///</summary>
    public static Block NTCH(TagName? operand = null) => NewBlock(nameof(NTCH), operand, "In Out");

    ///<summary>
    /// Returns a n <c>OR</c> Block element with the type initialized.
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
    /// Returns a n <c>PI</c> Block element with the type initialized.
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
    /// Returns a ne <c>RAD</c> Block element with the type initialized.
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
    /// Returns a ne <c>SCL</c> Block element with the type initialized.
    ///</summary>
    public static Block SCL(TagName? operand = null) => NewBlock(nameof(SCL), operand, "In Out");

    ///<summary>
    /// Returns a new <c>SCRV</c> Block element with the type initialized.
    ///</summary>
    public static Block SCRV(TagName? operand = null) => NewBlock(nameof(SCRV), operand, "In Out");

    ///<summary>
    /// Returns a ne <c>SEL</c> Block element with the type initialized.
    ///</summary>
    public static Block SEL(TagName? operand = null) =>
        NewBlock(nameof(SEL), operand, "In1 In2 SelectorIn Out");

    ///<summary>
    /// Returns a new <c>SETD</c> Block element with the type initialized.
    ///</summary>
    public static Block SETD(TagName? operand = null) =>
        NewBlock(nameof(SETD), operand, "Set Reset Out OutNot");

    ///<summary>
    /// Returns a ne <c>SIN</c> Block element with the type initialized.
    ///</summary>
    public static Block SIN(TagName? operand = null) =>
        NewBlock(nameof(SIN), operand, "Source Destination");

    ///<summary>
    /// Returns a new <c>SNEG</c> Block element with the type initialized.
    ///</summary>
    public static Block SNEG(TagName? operand = null) =>
        NewBlock(nameof(SNEG), operand, "In NegateEnable Out");

    ///<summary>
    /// Returns a ne <c>SOC</c> Block element with the type initialized.
    ///</summary>
    public static Block SOC(TagName? operand = null) => NewBlock(nameof(SOC), operand, "In Out");

    ///<summary>
    /// Returns a ne <c>SQR</c> Block element with the type initialized.
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
    /// Returns a ne <c>SUB</c> Block element with the type initialized.
    ///</summary>
    public static Block SUB(TagName? operand = null) =>
        NewBlock(nameof(SUB), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a ne <c>TAN</c> Block element with the type initialized.
    ///</summary>
    public static Block TAN(TagName? operand = null) => NewBlock(nameof(TAN), operand, "Source Dest");

    ///<summary>
    /// Returns a ne <c>TOD</c> Block element with the type initialized.
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
    /// Returns a ne <c>TOT</c> Block element with the type initialized.
    ///</summary>
    public static Block TOT(TagName? operand = null) => NewBlock(nameof(TOT), operand,
        "In ProgProgReq ProgOperReq ProgStartReq ProgStopReq ProgResetReq Total OldTotal ProgOper RunStop ProgResetDone TargetFlag TargetDev1Flag TargetDev2Flag");

    ///<summary>
    /// Returns a ne <c>TRN</c> Block element with the type initialized.
    ///</summary>
    public static Block TRN(TagName? operand = null) => NewBlock(nameof(TRN), operand, "Source Dest");

    ///<summary>
    /// Returns a new <c>UPDN</c> Block element with the type initialized.
    ///</summary>
    public static Block UPDN(TagName? operand = null) =>
        NewBlock(nameof(UPDN), operand, "InPlus InMinus Out");

    ///<summary>
    /// Returns a ne <c>XOR</c> Block element with the type initialized.
    ///</summary>
    public static Block XOR(TagName? operand = null) =>
        NewBlock(nameof(XOR), operand, "SourceA SourceB Dest");

    ///<summary>
    /// Returns a ne <c>XPY</c> Block element with the type initialized.
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

    #region Internal

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

    private IEnumerable<KeyValuePair<Argument, string>> Endpoints(string? param = null)
    {
        if (Sheet is null) return Enumerable.Empty<KeyValuePair<Argument, string>>();

        var arguments = new List<KeyValuePair<Argument, string>>();

        arguments.AddRange(GetInputs(Sheet, param));
        arguments.AddRange(GetOutputs(Sheet, param));

        return arguments;
    }

    private IEnumerable<KeyValuePair<Argument, string>> GetInputs(Sheet sheet, string? param)
    {
        var arguments = new List<KeyValuePair<Argument, string>>();

        var inputs = sheet.Wires().Where(w => w.IsTo(ID, param));

        foreach (var wire in inputs)
        {
            var block = sheet.Block(wire.FromID);
            if (block is null) continue;

            if (block.Type == L5XName.OCon)
            {
                arguments.AddRange(GetPair()?.Endpoints() ?? Enumerable.Empty<KeyValuePair<Argument, string>>());
                continue;
            }

            var arg = block.GetArguments(wire.FromParam);
            arguments.Add(new KeyValuePair<Argument, string>(arg, block.Type));
        }

        return arguments;
    }

    private IEnumerable<KeyValuePair<Argument, string>> GetOutputs(Sheet sheet, string? param)
    {
        var arguments = new List<KeyValuePair<Argument, string>>();

        var wires = sheet.Wires().Where(w => w.IsFrom(ID, param));

        foreach (var wire in wires)
        {
            var block = sheet.Block(wire.ToID);
            if (block is null) continue;

            if (block.Type == L5XName.ICon)
            {
                arguments.AddRange(GetPair()?.Endpoints() ?? Enumerable.Empty<KeyValuePair<Argument, string>>());
                continue;
            }

            var arg = block.GetArguments(wire.ToParam);
            arguments.Add(new KeyValuePair<Argument, string>(arg, block.Type));
        }

        return arguments;
    }

    private TagName GetArguments(string? param = null)
    {
        var operand = Operand is not null && Operand.IsTag ? new TagName(Operand.ToString()) : TagName.Empty;
        var parameter = param is not null ? new TagName(param) : TagName.Empty;
        return TagName.Concat(operand, parameter);

        /*return L5XType switch
        {
            L5XName.IRef => Operand is not null && Operand.IsTag ? (TagName)Operand : TagName.Empty,
            L5XName.ORef => Operand is not null && Operand.IsTag ? (TagName)Operand : TagName.Empty,
            L5XName.Block => Operand is not null && param is not null ? TagName.Concat(Operand.ToString(), param) : TagName.Empty,
            L5XName.Function => param is not null ? new TagName(param) : Argument.Empty,
            L5XName.AddOnInstruction => new[]
            {
                Operand is not null && param is not null ? TagName.Concat(Operand.ToString(), param) : Argument.Empty
            },
            L5XName.JSR => new[] { param is not null ? new TagName(param) : Argument.Empty },
            L5XName.SBR => new[] { param is not null ? new TagName(param) : Argument.Empty },
            L5XName.RET => new[] { param is not null ? new TagName(param) : Argument.Empty },
            _ => new[] { Argument.Empty }
        };*/
    }

    /// <summary>
    /// Gets the connector block (ICON/OCON) that is the pair/compliment to this connector block. 
    /// </summary>
    private Block? GetPair() => Sheet?.Blocks().FirstOrDefault(b => b.ID != ID && Equals(b.Operand, Operand));

    #endregion
}