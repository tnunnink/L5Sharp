using System.Xml.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace L5Sharp.Core;

/// <summary>
/// A predefined data type that is built into Logix and used with PID instructions.
/// </summary>
public sealed class PID : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PID"/> data type instance.
    /// </summary>
    public PID() : base(nameof(PID))
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
        DATA = ArrayData.New<REAL>(17);
    }

    /// <inheritdoc />
    public PID(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the <c>CTL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT CTL
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>EN</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>CT</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>CL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PVT</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PVT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DOE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DOE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SWM</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SWM
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>CA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL MO
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PE
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>NDF</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NDF
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>NOBC</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NOBC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>NOZC</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NOZC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>INI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL INI
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SPOR</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SPOR
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>OLL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL OLL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>OLH</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL OLH
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>EWD</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL EWD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DVNA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DVNA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DVPA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DVPA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PVLA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PVLA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PVHA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PVHA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SP</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL SP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>KP</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL KP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>KI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL KI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>KD</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL KD
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>BIAS</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL BIAS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MAXS</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MINS</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINS
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DB</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL SO
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MAXO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXO
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MINO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINO
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UPD</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL UPD
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PV</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>ERR</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL ERR
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>OUT</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL OUT
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PVH</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PVH
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PVL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PVL
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DVP</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DVP
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DVN</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DVN
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PVDB</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PVDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DVDB</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DVDB
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MAXI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MINI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINI
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>TIE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL TIE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MAXCV</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MINCV</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINCV
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MINTIE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINTIE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>MAXTIE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXTIE
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DATA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="ArrayData{TLogixType}"/> atomic value.</value>
    public ArrayData<REAL> DATA
    {
        get => GetMember<ArrayData<REAL>>();
        set => SetMember(value);
    }
}