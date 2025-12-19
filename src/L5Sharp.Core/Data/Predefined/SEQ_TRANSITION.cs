using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_TRANSITION</c> data type structure.
/// </summary>
[LogixData("SEQ_TRANSITION")]
public sealed partial class SEQ_TRANSITION : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_TRANSITION"/> instance initialized with default values.
    /// </summary>
    public SEQ_TRANSITION() : base("SEQ_TRANSITION")
    {
        Status = new BOOL();
        State = new DINT();
        Idle = new BOOL();
        Arming = new BOOL();
        Armed = new BOOL();
        Firing = new BOOL();
        Stopped = new BOOL();
        Aborted = new BOOL();
        Held = new BOOL();
        Holding = new BOOL();
        Unknown = new BOOL();
        FiringAttr = new DINT();
        NotFiring = new BOOL();
        Acquiring = new BOOL();
        Committed = new BOOL();
        Stopping = new BOOL();
        Resetting = new BOOL();
        Paused = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="SEQ_TRANSITION"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_TRANSITION(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Status
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public DINT State
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Idle</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Idle
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Arming</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Arming
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Armed</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Armed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Firing</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Firing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopped</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborted</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Held</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Held
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Holding</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Holding
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Unknown</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Unknown
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FiringAttr</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public DINT FiringAttr
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NotFiring</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL NotFiring
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Acquiring</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Acquiring
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Committed</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Committed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopping</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Resetting</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Resetting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Paused</c> member of the <see cref="SEQ_TRANSITION"/> data type.
    /// </summary>
    public BOOL Paused
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
