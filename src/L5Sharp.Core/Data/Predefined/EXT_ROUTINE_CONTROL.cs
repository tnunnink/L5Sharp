using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>EXT_ROUTINE_CONTROL</c> data type structure.
/// </summary>
[LogixData("EXT_ROUTINE_CONTROL")]
public sealed partial class EXT_ROUTINE_CONTROL : StructureData
{
    /// <summary>
    /// Creates a new <see cref="EXT_ROUTINE_CONTROL"/> instance initialized with default values.
    /// </summary>
    public EXT_ROUTINE_CONTROL() : base("EXT_ROUTINE_CONTROL")
    {
        ErrorCode = new SINT();
        NumParams = new SINT();
        ParameterDefs = new ArrayData<EXT_ROUTINE_PARAMETERS>(10);
        ReturnParamDef = new EXT_ROUTINE_PARAMETERS();
        EN = new BOOL();
        ReturnsValue = new BOOL();
        DN = new BOOL();
        ER = new BOOL();
        FirstScan = new BOOL();
        EnableOut = new BOOL();
        EnableIn = new BOOL();
        User1 = new BOOL();
        User0 = new BOOL();
        ScanType1 = new BOOL();
        ScanType0 = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="EXT_ROUTINE_CONTROL"/> instance initialized with the provided element.
    /// </summary>
    public EXT_ROUTINE_CONTROL(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 136;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        ErrorCode.UpdateData(data, offset + 0);
        NumParams.UpdateData(data, offset + 1);
        ParameterDefs.UpdateData(data, offset + 4);
        ReturnParamDef.UpdateData(data, offset + 124);
        EN.UpdateData((data[offset + 137] & (1 << 0)) != 0);
        ReturnsValue.UpdateData((data[offset + 137] & (1 << 1)) != 0);
        DN.UpdateData((data[offset + 137] & (1 << 2)) != 0);
        ER.UpdateData((data[offset + 137] & (1 << 3)) != 0);
        FirstScan.UpdateData((data[offset + 137] & (1 << 4)) != 0);
        EnableOut.UpdateData((data[offset + 137] & (1 << 5)) != 0);
        EnableIn.UpdateData((data[offset + 137] & (1 << 6)) != 0);
        User1.UpdateData((data[offset + 137] & (1 << 7)) != 0);
        User0.UpdateData((data[offset + 138] & (1 << 0)) != 0);
        ScanType1.UpdateData((data[offset + 138] & (1 << 1)) != 0);
        ScanType0.UpdateData((data[offset + 138] & (1 << 2)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>ErrorCode</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public SINT ErrorCode
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NumParams</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public SINT NumParams
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ParameterDefs</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public ArrayData<EXT_ROUTINE_PARAMETERS> ParameterDefs
    {
        get => GetArray<EXT_ROUTINE_PARAMETERS>();
        set => SetArray(value);
    }

    /// <summary>
    /// The <c>ReturnParamDef</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public EXT_ROUTINE_PARAMETERS ReturnParamDef
    {
        get => GetMember<EXT_ROUTINE_PARAMETERS>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ReturnsValue</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL ReturnsValue
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ER</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL ER
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FirstScan</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL FirstScan
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>User1</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL User1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>User0</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL User0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScanType1</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL ScanType1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ScanType0</c> member of the <see cref="EXT_ROUTINE_CONTROL"/> data type.
    /// </summary>
    public BOOL ScanType0
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}