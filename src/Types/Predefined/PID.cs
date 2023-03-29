using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace L5Sharp.Types.Predefined;

/// <summary>
/// A predefined data type that is built into Logix and used with PID instructions.
/// </summary>
public sealed class PID : StructureType
{
    /// <summary>
    /// Creates a new <see cref="PID"/> data type instance.
    /// </summary>
    public PID() : base(nameof(PID))
    {
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <summary>
    /// Gets the <c>CTL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT CTL { get; set; } = new();

    /// <summary>
    /// Gets the <c>EN</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL EN { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>CT</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CT { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>CL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CL { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PVT</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PVT { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DOE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DOE { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SWM</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SWM { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>CA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CA { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL MO { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PE { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>NDF</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NDF { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>NOBC</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NOBC { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>NOZC</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NOZC { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>INI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL INI { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SPOR</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SPOR { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>OLL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL OLL { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>OLH</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL OLH { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>EWD</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL EWD { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DVNA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DVNA { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DVPA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DVPA { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PVLA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PVLA { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PVHA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PVHA { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SP</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL SP { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>KP</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL KP { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>KI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL KI { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>KD</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL KD { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>BIAS</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL BIAS { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MAXS</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXS { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MINS</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINS { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DB</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DB { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL SO { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MAXO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXO { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MINO</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINO { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UPD</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL UPD { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PV</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PV { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>ERR</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL ERR { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>OUT</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL OUT { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PVH</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PVH { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PVL</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PVL { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DVP</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DVP { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DVN</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DVN { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PVDB</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL PVDB { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DVDB</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL DVDB { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MAXI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXI { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MINI</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINI { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>TIE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL TIE { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MAXCV</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXCV { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MINCV</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINCV { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MINTIE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MINTIE { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>MAXTIE</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="REAL"/> atomic value.</value>
    public REAL MAXTIE { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DATA</c> member of the <see cref="PID"/> type.
    /// </summary>
    /// <value>A <see cref="ArrayType{TLogixType}"/> atomic value.</value>
    public ArrayType<REAL> DATA { get; set; } = Logix.Array<REAL>(17);
}