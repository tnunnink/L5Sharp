using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PID</c> data type structure.
/// </summary>
[LogixData("PID")]
public sealed partial class PID : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PID"/> instance initialized with default values.
    /// </summary>
    public PID() : base("PID")
    {
        CTL = new DINT();
        EN = new BOOL();
        CT = new BOOL();
        CL = new BOOL();
        PVT = new BOOL();
        DOE = new BOOL();
        SWM = new BOOL();
        CA = new BOOL();
        MO = new BOOL();
        PE = new BOOL();
        NDF = new BOOL();
        NOBC = new BOOL();
        NOZC = new BOOL();
        INI = new BOOL();
        SPOR = new BOOL();
        OLL = new BOOL();
        OLH = new BOOL();
        EWD = new BOOL();
        DVNA = new BOOL();
        DVPA = new BOOL();
        PVLA = new BOOL();
        PVHA = new BOOL();
        SP = new REAL();
        KP = new REAL();
        KI = new REAL();
        KD = new REAL();
        BIAS = new REAL();
        MAXS = new REAL();
        MINS = new REAL();
        DB = new REAL();
        SO = new REAL();
        MAXO = new REAL();
        MINO = new REAL();
        UPD = new REAL();
        PV = new REAL();
        ERR = new REAL();
        OUT = new REAL();
        PVH = new REAL();
        PVL = new REAL();
        DVP = new REAL();
        DVN = new REAL();
        PVDB = new REAL();
        DVDB = new REAL();
        MAXI = new REAL();
        MINI = new REAL();
        TIE = new REAL();
        MAXCV = new REAL();
        MINCV = new REAL();
        MINTIE = new REAL();
        MAXTIE = new REAL();
        DATA = new REAL[17];
    }

    /// <summary>
    /// Creates a new <see cref="PID"/> instance initialized with the provided element.
    /// </summary>
    public PID(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>CTL</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public DINT CTL
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CT</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL CT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CL</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL CL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVT</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL PVT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DOE</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL DOE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SWM</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL SWM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CA</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL CA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MO</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL MO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PE</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL PE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NDF</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL NDF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NOBC</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL NOBC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NOZC</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL NOZC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>INI</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL INI
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SPOR</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL SPOR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OLL</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL OLL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OLH</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL OLH
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EWD</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL EWD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DVNA</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL DVNA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DVPA</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL DVPA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVLA</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL PVLA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVHA</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public BOOL PVHA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SP</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>KP</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL KP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>KI</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL KI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>KD</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL KD
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>BIAS</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL BIAS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MAXS</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MAXS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MINS</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MINS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DB</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL DB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SO</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL SO
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MAXO</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MAXO
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MINO</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MINO
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UPD</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL UPD
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PV</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ERR</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL ERR
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OUT</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL OUT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVH</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL PVH
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVL</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL PVL
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DVP</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL DVP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DVN</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL DVN
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVDB</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL PVDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DVDB</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL DVDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MAXI</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MAXI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MINI</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MINI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TIE</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL TIE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MAXCV</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MAXCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MINCV</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MINCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MINTIE</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MINTIE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>MAXTIE</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL MAXTIE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DATA</c> member of the <see cref="PID"/> data type.
    /// </summary>
    public REAL[] DATA
    {
        get => GetArray<REAL>();
        set => SetArray(value);
    }

}
