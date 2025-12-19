using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>MOTION_INSTRUCTION</c> data type structure.
/// </summary>
[LogixData("MOTION_INSTRUCTION")]
public sealed partial class MOTION_INSTRUCTION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="MOTION_INSTRUCTION"/> instance initialized with default values.
    /// </summary>
    public MOTION_INSTRUCTION() : base("MOTION_INSTRUCTION")
    {
        FLAGS = new DINT();
        EN = new BOOL();
        DN = new BOOL();
        ER = new BOOL();
        PC = new BOOL();
        IP = new BOOL();
        AC = new BOOL();
        ACCEL = new BOOL();
        DECEL = new BOOL();
        TrackingMaster = new BOOL();
        CalculatedDataAvailable = new BOOL();
        ERR = new INT();
        STATUS = new SINT();
        STATE = new SINT();
        SEGMENT = new DINT();
        EXERR = new SINT();
    }

    /// <summary>
    /// Creates a new <see cref="MOTION_INSTRUCTION"/> instance initialized with the provided element.
    /// </summary>
    public MOTION_INSTRUCTION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>FLAGS</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public DINT FLAGS
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EN</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL EN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ER</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL ER
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PC</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL PC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>IP</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL IP
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AC</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL AC
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ACCEL</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL ACCEL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DECEL</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL DECEL
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TrackingMaster</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL TrackingMaster
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CalculatedDataAvailable</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public BOOL CalculatedDataAvailable
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ERR</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public INT ERR
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>STATUS</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public SINT STATUS
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>STATE</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public SINT STATE
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SEGMENT</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public DINT SEGMENT
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>EXERR</c> member of the <see cref="MOTION_INSTRUCTION"/> data type.
    /// </summary>
    public SINT EXERR
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

}
