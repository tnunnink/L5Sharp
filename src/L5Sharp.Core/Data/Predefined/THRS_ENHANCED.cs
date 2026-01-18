using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>THRS_ENHANCED</c> data type structure.
/// </summary>
[LogixData("THRS_ENHANCED")]
public sealed partial class THRS_ENHANCED : StructureData
{
    /// <summary>
    /// Creates a new <see cref="THRS_ENHANCED"/> instance initialized with default values.
    /// </summary>
    public THRS_ENHANCED() : base("THRS_ENHANCED")
    {
        EnableIn = new BOOL();
        Enable = new BOOL();
        Disconnected = new BOOL();
        RightButtonNormallyOpen = new BOOL();
        RightButtonNormallyClosed = new BOOL();
        LeftButtonNormallyOpen = new BOOL();
        LeftButtonNormallyClosed = new BOOL();
        InputStatus = new BOOL();
        Reset = new BOOL();
        DiscrepancyTime = new DINT();
        EnableOut = new BOOL();
        O1 = new BOOL();
        FP = new BOOL();
        BR = new BOOL();
        SB = new BOOL();
        FaultCode = new DINT();
        DiagnosticCode = new DINT();
    }
    
    /// <summary>
    /// Creates a new <see cref="THRS_ENHANCED"/> instance initialized with the provided element.
    /// </summary>
    public THRS_ENHANCED(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Enable</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL Enable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Disconnected</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL Disconnected
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RightButtonNormallyOpen</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL RightButtonNormallyOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RightButtonNormallyClosed</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL RightButtonNormallyClosed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LeftButtonNormallyOpen</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL LeftButtonNormallyOpen
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LeftButtonNormallyClosed</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL LeftButtonNormallyClosed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InputStatus</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL InputStatus
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiscrepancyTime</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public DINT DiscrepancyTime
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BR</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL BR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SB</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public BOOL SB
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultCode</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public DINT FaultCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="THRS_ENHANCED"/> data type.
    /// </summary>
    public DINT DiagnosticCode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }
}