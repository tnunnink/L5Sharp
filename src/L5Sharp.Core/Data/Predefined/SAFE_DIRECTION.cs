using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SAFE_DIRECTION</c> data type structure.
/// </summary>
[LogixData("SAFE_DIRECTION")]
public sealed partial class SAFE_DIRECTION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SAFE_DIRECTION"/> instance initialized with default values.
    /// </summary>
    public SAFE_DIRECTION() : base("SAFE_DIRECTION")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        RestartType = new BOOL();
        ColdStartType = new BOOL();
        PositiveRequest = new BOOL();
        NegativeRequest = new BOOL();
        Reset = new BOOL();
        O1 = new BOOL();
        RR = new BOOL();
        FP = new BOOL();
        PositionWindow = new REAL();
        FaultType = new SINT();
        DiagnosticCode = new SINT();
    }

    /// <summary>
    /// Creates a new <see cref="SAFE_DIRECTION"/> instance initialized with the provided element.
    /// </summary>
    public SAFE_DIRECTION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartType</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL RestartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ColdStartType</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL ColdStartType
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositiveRequest</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL PositiveRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NegativeRequest</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL NegativeRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>O1</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL O1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RR</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL RR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FP</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public BOOL FP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PositionWindow</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public REAL PositionWindow
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FaultType</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public SINT FaultType
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DiagnosticCode</c> member of the <see cref="SAFE_DIRECTION"/> data type.
    /// </summary>
    public SINT DiagnosticCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

}
