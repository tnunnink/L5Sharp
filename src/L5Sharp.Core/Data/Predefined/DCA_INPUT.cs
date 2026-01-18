using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>DCA_INPUT</c> data type structure.
/// </summary>
[LogixData("DCA_INPUT")]
public sealed partial class DCA_INPUT : StructureData
{
    /// <summary>
    /// Creates a new <see cref="DCA_INPUT"/> instance initialized with default values.
    /// </summary>
    public DCA_INPUT() : base("DCA_INPUT")
    {
        EnableIn = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        RestartType = new BOOL();
        ColdStartType = new BOOL();
        ChannelA = new DINT();
        ChannelB = new DINT();
        Tolerance = new DINT();
        HighLimit = new DINT();
        LowLimit = new DINT();
        DiscrepancyTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        HTP = new BOOL();
        LTP = new BOOL();
        O1OnTime = new DINT();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
        Revision = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="DCA_INPUT"/> instance initialized with the provided element.
    /// </summary>
    public DCA_INPUT(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelA</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT ChannelA
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ChannelB</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT ChannelB
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Tolerance</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT Tolerance
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HighLimit</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT HighLimit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LowLimit</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT LowLimit
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiscrepancyTime</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT DiscrepancyTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HTP</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL HTP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LTP</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public BOOL LTP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1OnTime</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT O1OnTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Revision</c> member of the <see cref="DCA_INPUT"/> data type.
    /// </summary>
    public DINT Revision
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}