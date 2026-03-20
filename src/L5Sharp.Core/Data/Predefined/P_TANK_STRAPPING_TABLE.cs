using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>P_TANK_STRAPPING_TABLE</c> data type structure.
/// </summary>
[LogixData("P_TANK_STRAPPING_TABLE")]
public sealed partial class P_TANK_STRAPPING_TABLE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="P_TANK_STRAPPING_TABLE"/> instance initialized with default values.
    /// </summary>
    public P_TANK_STRAPPING_TABLE() : base("P_TANK_STRAPPING_TABLE")
    {
        EnableIn = new BOOL();
        EnableOut = new BOOL();
        Inp_InitializeReq = new BOOL();
        Inp_Level = new REAL();
        Inp_FreeWaterLevel = new REAL();
        Inp_ObsAPI = new REAL();
        Inp_AvgProdTemp = new REAL();
        Inp_AmbTemp = new REAL();
        Cfg_MinorPerMajor = new REAL();
        Cfg_HasCorrTempShell = new BOOL();
        Cfg_HasFloatRoofAdj = new BOOL();
        Cfg_HasMoreObj = new BOOL();
        Cfg_CalTemp = new REAL();
        Cfg_ShellCoefOfExp = new REAL();
        Cfg_K = new REAL();
        Cfg_FloatRoofLevel = new REAL();
        Cfg_FloatRoofCalAPI = new REAL();
        Cfg_FloatRoofVolPerAPI = new REAL();
        Val_TotObsVol = new REAL();
        Val_FreeWater = new REAL();
        Val_TempShell = new REAL();
        Val_CorrTempShell = new REAL();
        Val_FloatRoofAdj = new REAL();
        Val_GrossObsVol = new REAL();
        Sts_Initialized = new BOOL();
        Sts_UnderMin = new BOOL();
        Sts_OverMax = new BOOL();
    }
    
    /// <summary>
    /// Creates a new <see cref="P_TANK_STRAPPING_TABLE"/> instance initialized with the provided element.
    /// </summary>
    public P_TANK_STRAPPING_TABLE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 108;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        EnableIn.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        EnableOut.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Inp_InitializeReq.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Inp_Level.UpdateData(data, offset + 5);
        Inp_FreeWaterLevel.UpdateData(data, offset + 9);
        Inp_ObsAPI.UpdateData(data, offset + 13);
        Inp_AvgProdTemp.UpdateData(data, offset + 17);
        Inp_AmbTemp.UpdateData(data, offset + 21);
        Cfg_MinorPerMajor.UpdateData(data, offset + 25);
        Cfg_HasCorrTempShell.UpdateData((data[offset + 29] & (1 << 3)) != 0);
        Cfg_HasFloatRoofAdj.UpdateData((data[offset + 29] & (1 << 4)) != 0);
        Cfg_HasMoreObj.UpdateData((data[offset + 29] & (1 << 5)) != 0);
        Cfg_CalTemp.UpdateData(data, offset + 29);
        Cfg_ShellCoefOfExp.UpdateData(data, offset + 33);
        Cfg_K.UpdateData(data, offset + 37);
        Cfg_FloatRoofLevel.UpdateData(data, offset + 41);
        Cfg_FloatRoofCalAPI.UpdateData(data, offset + 45);
        Cfg_FloatRoofVolPerAPI.UpdateData(data, offset + 49);
        Val_TotObsVol.UpdateData(data, offset + 53);
        Val_FreeWater.UpdateData(data, offset + 57);
        Val_TempShell.UpdateData(data, offset + 61);
        Val_CorrTempShell.UpdateData(data, offset + 65);
        Val_FloatRoofAdj.UpdateData(data, offset + 69);
        Val_GrossObsVol.UpdateData(data, offset + 73);
        Sts_Initialized.UpdateData((data[offset + 77] & (1 << 6)) != 0);
        Sts_UnderMin.UpdateData((data[offset + 77] & (1 << 7)) != 0);
        Sts_OverMax.UpdateData((data[offset + 78] & (1 << 0)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>EnableIn</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL EnableIn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EnableOut</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL EnableOut
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_InitializeReq</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Inp_InitializeReq
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_Level</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Inp_Level
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_FreeWaterLevel</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Inp_FreeWaterLevel
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_ObsAPI</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Inp_ObsAPI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AvgProdTemp</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Inp_AvgProdTemp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inp_AmbTemp</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Inp_AmbTemp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_MinorPerMajor</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_MinorPerMajor
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasCorrTempShell</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Cfg_HasCorrTempShell
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasFloatRoofAdj</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Cfg_HasFloatRoofAdj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_HasMoreObj</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Cfg_HasMoreObj
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_CalTemp</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_CalTemp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_ShellCoefOfExp</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_ShellCoefOfExp
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_K</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_K
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FloatRoofLevel</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_FloatRoofLevel
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FloatRoofCalAPI</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_FloatRoofCalAPI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Cfg_FloatRoofVolPerAPI</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Cfg_FloatRoofVolPerAPI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TotObsVol</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Val_TotObsVol
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_FreeWater</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Val_FreeWater
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_TempShell</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Val_TempShell
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_CorrTempShell</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Val_CorrTempShell
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_FloatRoofAdj</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Val_FloatRoofAdj
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Val_GrossObsVol</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public REAL Val_GrossObsVol
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_Initialized</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Sts_Initialized
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_UnderMin</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Sts_UnderMin
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Sts_OverMax</c> member of the <see cref="P_TANK_STRAPPING_TABLE"/> data type.
    /// </summary>
    public BOOL Sts_OverMax
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}