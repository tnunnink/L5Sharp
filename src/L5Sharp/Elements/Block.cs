using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Utilities;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

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
    /// Creates a new <see cref="Block"/> with the required block type value.
    /// </summary>
    /// <param name="type">The name of the FBD block type.</param>
    /// <param name="parameters"></param>
    public Block(string type, Params? parameters = null)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Operand = $"{Type}_01";
        HideDesc = false;
        VisiblePins = parameters;
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
    public string Type
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
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
        get => GetValue<bool?>();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of input parameters to the routine being called by the <see cref="JSR"/> <c>FunctionBlock</c> element.
    /// </summary>
    /// <value>A <see cref="Params"/> object wrapping the underlying attribute containing the <c>In</c> parameters
    /// if exists; Otherwise, <c>null</c>.</value>
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
    public Params? VisiblePins
    {
        get => Element.Attribute(L5XName.VisiblePins) is not null
            ? new Params(Element.Attribute(L5XName.VisiblePins)!)
            : default;
        set => SetValue(value is not null ? string.Join(" ", value) : null);
    }

    /// <inheritdoc />
    public override IEnumerable<CrossReference> References()
    {
        var references = new List<CrossReference> { new(Element, L5XName.Instruction, Type) };

        var instruction = ToInstruction();
        var tags = instruction.Arguments.Where(a => a.IsTag);
        foreach (var tag in tags)
        {
            references.Add(new CrossReference(Element, L5XName.Tag, tag.ToString(), instruction));
        }

        return references;
    }

    /// <inheritdoc />
    public override Instruction ToInstruction()
    {
        var instruction = new Instruction(Type, Operand ?? Argument.Empty);

        if (VisiblePins is null) return instruction;
        foreach (var pin in VisiblePins)
        {
            var endpoints = Endpoints(pin).ToArray();
            if (endpoints.Any())
            {
                instruction = instruction.Append(endpoints);
            }
        }

        return instruction;
    }

    /// <inheritdoc />
    protected override IEnumerable<Argument> GetArguments(string? param = null)
    {
        yield return Operand is not null && param is not null && VisiblePins?.Contains(param) == true
            ? TagName.Concat(Operand, param)
            : Argument.Empty;
    }

    #region Blocks
    
    /*public static Block IREF(string operand) => new Block(nameof(IREF))*/
    
    ///<summary>
    /// Returns a ne <c>ABS</c> Block element with the type initialized.
    ///</summary>
    public static Block ABS => new (nameof(ABS), Params.Pins("Source, Destination"));
    
    ///<summary>
    /// Returns a ne <c>ACS</c> Block element with the type initialized.
    ///</summary>
    public static Block ACS => new (nameof(ACS), Params.Pins("Source, Destination"));
    
    ///<summary>
    /// Returns a ne <c>ADD</c> Block element with the type initialized.
    ///</summary>
    public static Block ADD => new (nameof(ADD), Params.Pins("SourceA, SourceB, Destination"));
    
    ///<summary>
    /// Returns a ne <c>ALM</c> Block element with the type initialized.
    ///</summary>
    public static Block ALM => new (nameof(ALM), Params.Pins("In, HHAlarm, HAlarm, LAlarm, LLAlarm, ROCPosAlarm, ROCNegAlarm"));
    
    ///<summary>
    /// Returns a new <c>ALMA</c> Block element with the type initialized.
    ///</summary>
    public static Block ALMA => new (nameof(ALMA), Params.Pins("In, HHInAlarm, HInAlarm, LInAlarm, LLInAlarm, ROCPosInAlarm, ROCNegInAlarm, HHAcked, HAcked, LAcked, LLAcked, ROCPosAcked, ROCNegAcked, Suppressed, Disabled"));
    
    ///<summary>
    /// Returns a new <c>ALMD</c> Block element with the type initialized.
    ///</summary>
    public static Block ALMD => new (nameof(ALMD), Params.Pins("In, InAlarm, Acked, Suppressed, Disabled"));
    
    ///<summary>
    /// Returns a ne <c>AND</c> Block element with the type initialized.
    ///</summary>
    public static Block AND => new (nameof(AND), Params.Pins("SourceA, SourceB, Destination"));
    
    ///<summary>
    /// Returns a ne <c>ASN</c> Block element with the type initialized.
    ///</summary>
    public static Block ASN => new (nameof(ASN), Params.Pins("Source, Destination"));
    
    ///<summary>
    /// Returns a ne <c>ATN</c> Block element with the type initialized.
    ///</summary>
    public static Block ATN => new (nameof(ATN), Params.Pins("Source, Destination"));
    
    ///<summary>
    /// Returns a new <c>BAND</c> Block element with the type initialized.
    ///</summary>
    public static Block BAND => new (nameof(BAND), Params.Pins("In1, In2, In3, In4, Out"));
    
    ///<summary>
    /// Returns a new <c>BNOT</c> Block element with the type initialized.
    ///</summary>
    public static Block BNOT => new (nameof(BNOT), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a ne <c>BOR</c> Block element with the type initialized.
    ///</summary>
    public static Block BOR => new (nameof(BOR), Params.Pins("In1, In2, In3, In4, Out"));
    
    ///<summary>
    /// Returns a new <c>BTDT</c> Block element with the type initialized.
    ///</summary>
    public static Block BTDT => new (nameof(BTDT), Params.Pins("Source, SourceBit, Length, DestBit, Target, Dest"));
    
    ///<summary>
    /// Returns a new <c>BXOR</c> Block element with the type initialized.
    ///</summary>
    public static Block BXOR => new (nameof(BXOR), Params.Pins("In1, In2, Out"));
    
    ///<summary>
    /// Returns a ne <c>COS</c> Block element with the type initialized.
    ///</summary>
    public static Block COS => new (nameof(COS), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>CTUD</c> Block element with the type initialized.
    ///</summary>
    public static Block CTUD => new (nameof(CTUD), Params.Pins("CUEnable, CDEnable, PRE, Reset, ACC, DN"));
    
    ///<summary>
    /// Returns a new <c>D2SD</c> Block element with the type initialized.
    ///</summary>
    public static Block D2SD => new (nameof(D2SD), Params.Pins("ProgCommand, State0Perm, State1Perm, FB0, FB1, HandFB, ProgProgReq, ProgOperReq, ProgOverrideReq, ProgHandReq, Out, Device0State, Device1State, CommandStatus, FaultAlarm, ModeAlarm, ProgOper, Override, Hand"));
    
    ///<summary>
    /// Returns a new <c>D3SD</c> Block element with the type initialized.
    ///</summary>
    public static Block D3SD => new (nameof(D3SD), Params.Pins("Prog0Command, Prog1Command, Prog2Command, State0Perm, State1Perm, State2Perm, FB0, FB1, FB2, FB3, HandFB0, HandFB1, HandFB2, ProgProgReq, ProgOperReq, ProgOverrideReq, ProgHandReq, Out0, Out1, Out2, Device0State, Device1State, Device2State, Command0Status, Command1Status, Command2Status, FaultAlarm, ModeAlarm, ProgOper, Override, Hand"));
    
    ///<summary>
    /// Returns a new <c>DEDT</c> Block element with the type initialized.
    ///</summary>
    public static Block DEDT => new (nameof(DEDT), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a ne <c>DEG</c> Block element with the type initialized.
    ///</summary>
    public static Block DEG => new (nameof(DEG), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>DERV</c> Block element with the type initialized.
    ///</summary>
    public static Block DERV => new (nameof(DERV), Params.Pins("In, ByPass, Out"));
    
    ///<summary>
    /// Returns a ne <c>DFF</c> Block element with the type initialized.
    ///</summary>
    public static Block DFF => new (nameof(DFF), Params.Pins("D, Clear, Clock, Q, QNot"));
    
    ///<summary>
    /// Returns a ne <c>DIV</c> Block element with the type initialized.
    ///</summary>
    public static Block DIV => new (nameof(DIV), Params.Pins("SourceA, SourceB, Dest"));
    
    ///<summary>
    /// Returns a new <c>ESEL</c> Block element with the type initialized.
    ///</summary>
    public static Block ESEL => new (nameof(ESEL), Params.Pins("In1, In2, In3, In4, In5, In6, ProgSelector, ProgProgReq, ProgOperReq, ProgOverrideReq, Out, SelectedIn, ProgOper, Override"));
    
    ///<summary>
    /// Returns a ne <c>EQU</c> Block element with the type initialized.
    ///</summary>
    public static Block EQU => new (nameof(EQU), Params.Pins("SourceA, SourceB"));
    
    ///<summary>
    /// Returns a new <c>FGEN</c> Block element with the type initialized.
    ///</summary>
    public static Block FGEN => new (nameof(FGEN), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a ne <c>FRD</c> Block element with the type initialized.
    ///</summary>
    public static Block FRD => new (nameof(FRD), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a ne <c>GEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block GEQ => new (nameof(GEQ), Params.Pins("SourceA, SourceB"));
    
    ///<summary>
    /// Returns a ne <c>GRT</c> Block element with the type initialized.
    ///</summary>
    public static Block GRT => new (nameof(GRT), Params.Pins("SourceA, SourceB"));
    
    ///<summary>
    /// Returns a ne <c>HLL</c> Block element with the type initialized.
    ///</summary>
    public static Block HLL => new (nameof(HLL), Params.Pins("In, Out, HighAlarm, LowAlarm"));
    
    ///<summary>
    /// Returns a ne <c>HPF</c> Block element with the type initialized.
    ///</summary>
    public static Block HPF => new (nameof(HPF), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a new <c>INTG</c> Block element with the type initialized.
    ///</summary>
    public static Block INTG => new (nameof(INTG), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a new <c>JKFF</c> Block element with the type initialized.
    ///</summary>
    public static Block JKFF => new (nameof(JKFF), Params.Pins("Clear, Clock, Q, QNot"));
    
    ///<summary>
    /// Returns a ne <c>LEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block LEQ => new (nameof(LEQ), Params.Pins("SourceA, SourceB"));
    
    ///<summary>
    /// Returns a ne <c>LES</c> Block element with the type initialized.
    ///</summary>
    public static Block LES => new (nameof(LES), Params.Pins("SourceA, SourceB"));
    
    ///<summary>
    /// Returns a ne <c>LIM</c> Block element with the type initialized.
    ///</summary>
    public static Block LIM => new (nameof(LIM), Params.Pins("LowLimit, Test, HighLimit"));
    
    ///<summary>
    /// Returns a n <c>LN</c> Block element with the type initialized.
    ///</summary>
    public static Block LN => new (nameof(LN), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a ne <c>LOG</c> Block element with the type initialized.
    ///</summary>
    public static Block LOG => new (nameof(LOG), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a ne <c>LPF</c> Block element with the type initialized.
    ///</summary>
    public static Block LPF => new (nameof(LPF), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a new <c>MAVE</c> Block element with the type initialized.
    ///</summary>
    public static Block MAVE => new (nameof(MAVE), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a new <c>MAXC</c> Block element with the type initialized.
    ///</summary>
    public static Block MAXC => new (nameof(MAXC), Params.Pins("In, Reset, ResetValue, Out"));
    
    ///<summary>
    /// Returns a ne <c>MEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block MEQ => new (nameof(MEQ), Params.Pins("Source, Mask, Compare"));
    
    ///<summary>
    /// Returns a new <c>MINC</c> Block element with the type initialized.
    ///</summary>
    public static Block MINC => new (nameof(MINC), Params.Pins("In, Reset, ResetValue, Out"));
    
    ///<summary>
    /// Returns a ne <c>MOD</c> Block element with the type initialized.
    ///</summary>
    public static Block MOD => new (nameof(MOD), Params.Pins("SourceA, SourceB, Dest"));
    
    ///<summary>
    /// Returns a new <c>MSTD</c> Block element with the type initialized.
    ///</summary>
    public static Block MSTD => new (nameof(MSTD), Params.Pins("In, SampleEnable, Out"));
    
    ///<summary>
    /// Returns a ne <c>MUL</c> Block element with the type initialized.
    ///</summary>
    public static Block MUL => new (nameof(MUL), Params.Pins("SourceA, SourceB, Dest"));
    
    ///<summary>
    /// Returns a ne <c>MUX</c> Block element with the type initialized.
    ///</summary>
    public static Block MUX => new (nameof(MUX), Params.Pins("In1, In2, In3, In4, In5, In6, In7, In8, Selector, Out"));
    
    ///<summary>
    /// Returns a new <c>MVMT</c> Block element with the type initialized.
    ///</summary>
    public static Block MVMT => new (nameof(MVMT), Params.Pins("Source, Mask, Target, Dest"));
    
    ///<summary>
    /// Returns a ne <c>NEG</c> Block element with the type initialized.
    ///</summary>
    public static Block NEG => new (nameof(NEG), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a ne <c>NEQ</c> Block element with the type initialized.
    ///</summary>
    public static Block NEQ => new (nameof(NEQ), Params.Pins("SourceA, SourceB"));
    
    ///<summary>
    /// Returns a ne <c>NOT</c> Block element with the type initialized.
    ///</summary>
    public static Block NOT => new (nameof(NOT), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>NTCH</c> Block element with the type initialized.
    ///</summary>
    public static Block NTCH => new (nameof(NTCH), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a n <c>OR</c> Block element with the type initialized.
    ///</summary>
    public static Block OR => new (nameof(OR), Params.Pins("SourceA, SourceB, Dest"));
    
    ///<summary>
    /// Returns a new <c>OSFI</c> Block element with the type initialized.
    ///</summary>
    public static Block OSFI => new (nameof(OSFI), Params.Pins("InputBit, OutputBit"));
    
    ///<summary>
    /// Returns a new <c>OSRI</c> Block element with the type initialized.
    ///</summary>
    public static Block OSRI => new (nameof(OSRI), Params.Pins("InputBit, OutputBit"));
    
    ///<summary>
    /// Returns a n <c>PI</c> Block element with the type initialized.
    ///</summary>
    public static Block PI => new (nameof(PI), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a new <c>PIDE</c> Block element with the type initialized.
    ///</summary>
    public static Block PIDE => new (nameof(PIDE), Params.Pins("PV, SPProg, SPCascade, RatioProg, CVProg, FF, HandFB, ProgProgReq, ProgOperReq, ProgCasRatReq, ProgAutoReq, ProgManualReq, ProgOverrideReq, ProgHandReq, CVEU, SP, PVHHAlarm, PVHAlarm, PVLAlarm, PVLLAlarm, PVROCPosAlarm, PVROCNegAlarm, DevHHAlarm, DevHAlarm, DevLAlarm, DevLLAlarm, ProgOper, CasRat, Auto, Manual, Override, Hand"));
    
    ///<summary>
    /// Returns a new <c>PMUL</c> Block element with the type initialized.
    ///</summary>
    public static Block PMUL => new (nameof(PMUL), Params.Pins("In, Multiplier, Out"));
    
    ///<summary>
    /// Returns a new <c>POSP</c> Block element with the type initialized.
    ///</summary>
    public static Block POSP => new (nameof(POSP), Params.Pins("SP, Position, OpenedFB, ClosedFB, OpenOut, CloseOut"));
    
    ///<summary>
    /// Returns a ne <c>RAD</c> Block element with the type initialized.
    ///</summary>
    public static Block RAD => new (nameof(RAD), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>RESD</c> Block element with the type initialized.
    ///</summary>
    public static Block RESD => new (nameof(RESD), Params.Pins("Set, Reset, Out, OutNot"));
    
    ///<summary>
    /// Returns a new <c>RLIM</c> Block element with the type initialized.
    ///</summary>
    public static Block RLIM => new (nameof(RLIM), Params.Pins("In, ByPass, Out"));
    
    ///<summary>
    /// Returns a new <c>RMPS</c> Block element with the type initialized.
    ///</summary>
    public static Block RMPS => new (nameof(RMPS), Params.Pins("PV, CurrentSegProg, OutProg, SoakTimeProg, ProgProgReq, ProgOperReq, ProgAutoReq, ProgManualReq, ProgHoldReq, Out, CurrentSeg, SoakTimeLeft, GuarRampOn, GuarSoakOn, ProgOper, Auto, Manual, Hold"));
    
    ///<summary>
    /// Returns a new <c>RTOR</c> Block element with the type initialized.
    ///</summary>
    public static Block RTOR => new (nameof(RTOR), Params.Pins("TimerEnable, PRE, Reset, ACC, DN"));
    
    ///<summary>
    /// Returns a ne <c>SCL</c> Block element with the type initialized.
    ///</summary>
    public static Block SCL => new (nameof(SCL), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a new <c>SCRV</c> Block element with the type initialized.
    ///</summary>
    public static Block SCRV => new (nameof(SCRV), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a ne <c>SEL</c> Block element with the type initialized.
    ///</summary>
    public static Block SEL => new (nameof(SEL), Params.Pins("In1, In2, SelectorIn, Out"));
    
    ///<summary>
    /// Returns a new <c>SETD</c> Block element with the type initialized.
    ///</summary>
    public static Block SETD => new (nameof(SETD), Params.Pins("Set, Reset, Out, OutNot"));
    
    ///<summary>
    /// Returns a ne <c>SIN</c> Block element with the type initialized.
    ///</summary>
    public static Block SIN => new (nameof(SIN), Params.Pins("Source, Destination"));
    
    ///<summary>
    /// Returns a new <c>SNEG</c> Block element with the type initialized.
    ///</summary>
    public static Block SNEG => new (nameof(SNEG), Params.Pins("In, NegateEnable, Out"));
    
    ///<summary>
    /// Returns a ne <c>SOC</c> Block element with the type initialized.
    ///</summary>
    public static Block SOC => new (nameof(SOC), Params.Pins("In, Out"));
    
    ///<summary>
    /// Returns a ne <c>SQR</c> Block element with the type initialized.
    ///</summary>
    public static Block SQR => new (nameof(SQR), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>SRTP</c> Block element with the type initialized.
    ///</summary>
    public static Block SRTP => new (nameof(SRTP), Params.Pins("In, HeatOut, CoolOut, HeatTimePercent, CoolTimePercent"));
    
    ///<summary>
    /// Returns a new <c>SSUM</c> Block element with the type initialized.
    ///</summary>
    public static Block SSUM => new (nameof(SSUM), Params.Pins("In1, Select1, In2, Select2, In3, Select3, In4, Select4, Out"));
    
    ///<summary>
    /// Returns a ne <c>SUB</c> Block element with the type initialized.
    ///</summary>
    public static Block SUB => new (nameof(SUB), Params.Pins("SourceA, SourceB, Dest"));
    
    ///<summary>
    /// Returns a ne <c>TAN</c> Block element with the type initialized.
    ///</summary>
    public static Block TAN => new (nameof(TAN), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a ne <c>TOD</c> Block element with the type initialized.
    ///</summary>
    public static Block TOD => new (nameof(TOD), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>TOFR</c> Block element with the type initialized.
    ///</summary>
    public static Block TOFR => new (nameof(TOFR), Params.Pins("TimerEnable, PRE, Reset, ACC, DN"));
    
    ///<summary>
    /// Returns a new <c>TONR</c> Block element with the type initialized.
    ///</summary>
    public static Block TONR => new (nameof(TONR), Params.Pins("TimerEnable, PRE, Reset, ACC, DN"));
    
    ///<summary>
    /// Returns a ne <c>TOT</c> Block element with the type initialized.
    ///</summary>
    public static Block TOT => new (nameof(TOT), Params.Pins("In, ProgProgReq, ProgOperReq, ProgStartReq, ProgStopReq, ProgResetReq, Total, OldTotal, ProgOper, RunStop, ProgResetDone, TargetFlag, TargetDev1Flag, TargetDev2Flag"));
    
    ///<summary>
    /// Returns a ne <c>TRN</c> Block element with the type initialized.
    ///</summary>
    public static Block TRN => new (nameof(TRN), Params.Pins("Source, Dest"));
    
    ///<summary>
    /// Returns a new <c>UPDN</c> Block element with the type initialized.
    ///</summary>
    public static Block UPDN => new (nameof(UPDN), Params.Pins("InPlus, InMinus, Out"));
    
    ///<summary>
    /// Returns a ne <c>XOR</c> Block element with the type initialized.
    ///</summary>
    public static Block XOR => new (nameof(XOR), Params.Pins("SourceA, SourceB, Dest"));
    
    ///<summary>
    /// Returns a ne <c>XPY</c> Block element with the type initialized.
    ///</summary>
    public static Block XPY => new Block(nameof(XPY), Params.Pins("SourceA, SourceB, Dest"));

    #endregion
}