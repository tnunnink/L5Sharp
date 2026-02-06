using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

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
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 12;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Status.UpdateData((data[offset + 1] & (1 << 0)) != 0);
        State.UpdateData(data, offset + 1);
        Idle.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Arming.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Armed.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Firing.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Stopped.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Aborted.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Held.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Holding.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Unknown.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        FiringAttr.UpdateData(data, offset + 6);
        NotFiring.UpdateData((data[offset + 10] & (1 << 2)) != 0);
        Acquiring.UpdateData((data[offset + 10] & (1 << 3)) != 0);
        Committed.UpdateData((data[offset + 10] & (1 << 4)) != 0);
        Stopping.UpdateData((data[offset + 10] & (1 << 5)) != 0);
        Resetting.UpdateData((data[offset + 10] & (1 << 6)) != 0);
        Paused.UpdateData((data[offset + 10] & (1 << 7)) != 0);
        
        return offset + GetSize();
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