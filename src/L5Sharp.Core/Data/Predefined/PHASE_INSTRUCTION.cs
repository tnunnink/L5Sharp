using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PHASE_INSTRUCTION</c> data type structure.
/// </summary>
[LogixData("PHASE_INSTRUCTION")]
public sealed partial class PHASE_INSTRUCTION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PHASE_INSTRUCTION"/> instance initialized with default values.
    /// </summary>
    public PHASE_INSTRUCTION() : base("PHASE_INSTRUCTION")
    {
        Status = new DINT();
        EN = new BOOL();
        ER = new BOOL();
        PC = new BOOL();
        IP = new BOOL();
        WA = new BOOL();
        ABORT = new BOOL();
        ERR = new INT();
        EXERR = new INT();
    }

    /// <summary>
    /// Creates a new <see cref="PHASE_INSTRUCTION"/> instance initialized with the provided element.
    /// </summary>
    public PHASE_INSTRUCTION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ER</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL ER
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PC</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL PC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IP</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL IP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>WA</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL WA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ABORT</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL ABORT
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ERR</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public INT ERR
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EXERR</c> member of the <see cref="PHASE_INSTRUCTION"/> data type.
    /// </summary>
    public INT EXERR
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

}
