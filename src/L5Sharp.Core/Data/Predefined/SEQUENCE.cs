using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQUENCE</c> data type structure.
/// </summary>
[LogixData("SEQUENCE")]
public sealed partial class SEQUENCE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQUENCE"/> instance initialized with default values.
    /// </summary>
    public SEQUENCE() : base("SEQUENCE")
    {
        State = new DINT();
        Running = new BOOL();
        Holding = new BOOL();
        Restarting = new BOOL();
        Stopping = new BOOL();
        Aborting = new BOOL();
        Resetting = new BOOL();
        Idle = new BOOL();
        Held = new BOOL();
        Complete = new BOOL();
        Stopped = new BOOL();
        Aborted = new BOOL();
        Unknown = new BOOL();
        Mode = new DINT();
        Control = new DINT();
        PauseControl = new DINT();
        PauseEnabled = new BOOL();
        Paused = new BOOL();
        AutoPauseEnabled = new BOOL();
        Failure = new DINT();
        UnitID = new DINT();
        Owner = new DINT();
        InternalFailure = new DINT();
        ExecutionIdHI = new LINT();
        ExecutionIdLO = new LINT();
        ValidCommands = new DINT();
        StartValid = new BOOL();
        HoldValid = new BOOL();
        StopValid = new BOOL();
        RestartValid = new BOOL();
        AbortValid = new BOOL();
        ResetValid = new BOOL();
        AutoPauseValid = new BOOL();
        PauseValid = new BOOL();
        ResumeValid = new BOOL();
        ClearFailureValid = new BOOL();
        AutoModeValid = new BOOL();
        ManualModeValid = new BOOL();
        InitializeParamTagValid = new BOOL();
        DefineUSSIValid = new BOOL();
        USSI = new STRING();
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQUENCE"/> instance initialized with the provided element.
    /// </summary>
    public SEQUENCE(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 144;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        State.UpdateData(data, offset + 0);
        Running.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        Holding.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        Restarting.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        Stopping.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        Aborting.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        Resetting.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        Idle.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        Held.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        Complete.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Stopped.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        Aborted.UpdateData((data[offset + 6] & (1 << 2)) != 0);
        Unknown.UpdateData((data[offset + 6] & (1 << 3)) != 0);
        Mode.UpdateData(data, offset + 6);
        Control.UpdateData(data, offset + 10);
        PauseControl.UpdateData(data, offset + 14);
        PauseEnabled.UpdateData((data[offset + 18] & (1 << 4)) != 0);
        Paused.UpdateData((data[offset + 18] & (1 << 5)) != 0);
        AutoPauseEnabled.UpdateData((data[offset + 18] & (1 << 6)) != 0);
        Failure.UpdateData(data, offset + 18);
        UnitID.UpdateData(data, offset + 22);
        Owner.UpdateData(data, offset + 26);
        InternalFailure.UpdateData(data, offset + 30);
        ExecutionIdHI.UpdateData(data, offset + 34);
        ExecutionIdLO.UpdateData(data, offset + 42);
        ValidCommands.UpdateData(data, offset + 50);
        StartValid.UpdateData((data[offset + 54] & (1 << 7)) != 0);
        HoldValid.UpdateData((data[offset + 55] & (1 << 0)) != 0);
        StopValid.UpdateData((data[offset + 55] & (1 << 1)) != 0);
        RestartValid.UpdateData((data[offset + 55] & (1 << 2)) != 0);
        AbortValid.UpdateData((data[offset + 55] & (1 << 3)) != 0);
        ResetValid.UpdateData((data[offset + 55] & (1 << 4)) != 0);
        AutoPauseValid.UpdateData((data[offset + 55] & (1 << 5)) != 0);
        PauseValid.UpdateData((data[offset + 55] & (1 << 6)) != 0);
        ResumeValid.UpdateData((data[offset + 55] & (1 << 7)) != 0);
        ClearFailureValid.UpdateData((data[offset + 56] & (1 << 0)) != 0);
        AutoModeValid.UpdateData((data[offset + 56] & (1 << 1)) != 0);
        ManualModeValid.UpdateData((data[offset + 56] & (1 << 2)) != 0);
        InitializeParamTagValid.UpdateData((data[offset + 56] & (1 << 3)) != 0);
        DefineUSSIValid.UpdateData((data[offset + 56] & (1 << 4)) != 0);
        USSI.UpdateData(data, offset + 56);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>State</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT State
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Running</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Holding</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Holding
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Restarting</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Restarting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopping</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborting</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Aborting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Resetting</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Resetting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Idle</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Idle
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Held</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Held
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Complete</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Complete
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopped</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborted</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Unknown</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Unknown
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT Mode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Control</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT Control
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseControl</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT PauseControl
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseEnabled</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL PauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Paused</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL Paused
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AutoPauseEnabled</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL AutoPauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Failure</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT Failure
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnitID</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT UnitID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Owner</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InternalFailure</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT InternalFailure
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ExecutionIdHI</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public LINT ExecutionIdHI
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ExecutionIdLO</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public LINT ExecutionIdLO
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ValidCommands</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public DINT ValidCommands
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StartValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL StartValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL HoldValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL StopValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL RestartValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AbortValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL AbortValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL ResetValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AutoPauseValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL AutoPauseValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL PauseValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResumeValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL ResumeValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ClearFailureValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL ClearFailureValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AutoModeValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL AutoModeValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ManualModeValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL ManualModeValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InitializeParamTagValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL InitializeParamTagValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DefineUSSIValid</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public BOOL DefineUSSIValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>USSI</c> member of the <see cref="SEQUENCE"/> data type.
    /// </summary>
    public STRING USSI
    {
        get => GetMember<STRING>();
        set => SetMember(value);
    }
}