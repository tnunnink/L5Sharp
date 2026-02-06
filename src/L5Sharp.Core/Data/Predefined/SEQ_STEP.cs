using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>SEQ_STEP</c> data type structure.
/// </summary>
[LogixData("SEQ_STEP")]
public sealed partial class SEQ_STEP : StructureData
{
    /// <summary>
    /// Creates a new <see cref="SEQ_STEP"/> instance initialized with default values.
    /// </summary>
    public SEQ_STEP() : base("SEQ_STEP")
    {
        Status = new DINT();
        X = new BOOL();
        FS = new BOOL();
        SA = new BOOL();
        LS = new BOOL();
        DN = new BOOL();
        OV = new BOOL();
        AlarmEn = new BOOL();
        AlarmLow = new BOOL();
        AlarmHigh = new BOOL();
        Reset = new BOOL();
        PRE = new DINT();
        T = new DINT();
        TMax = new DINT();
        Count = new DINT();
        LimitLow = new DINT();
        LimitHigh = new DINT();
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
        Starting = new BOOL();
        Downloading = new BOOL();
        NotConnected = new BOOL();
        Inactive = new BOOL();
        Unknown = new BOOL();
        Mode = new DINT();
        Control = new DINT();
        PauseControl = new DINT();
        PauseEnabled = new BOOL();
        Paused = new BOOL();
        AutoPauseEnabled = new BOOL();
        Index = new DINT();
        Failure = new DINT();
        InternalFailure = new DINT();
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
    }
    
    /// <summary>
    /// Creates a new <see cref="SEQ_STEP"/> instance initialized with the provided element.
    /// </summary>
    public SEQ_STEP(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    /// <remarks>
    /// This value was generated based on the type definition exported from Studio 5k.
    /// </remarks>
    public override int GetSize() => 60;
    
    /// <inheritdoc />
    /// <remarks>
    /// This mapping was generated based on the type definition exported from Studio 5K.
    /// </remarks>
    public override int UpdateData(byte[] data, int offset)
    {
        Status.UpdateData(data, offset + 0);
        X.UpdateData((data[offset + 5] & (1 << 0)) != 0);
        FS.UpdateData((data[offset + 5] & (1 << 1)) != 0);
        SA.UpdateData((data[offset + 5] & (1 << 2)) != 0);
        LS.UpdateData((data[offset + 5] & (1 << 3)) != 0);
        DN.UpdateData((data[offset + 5] & (1 << 4)) != 0);
        OV.UpdateData((data[offset + 5] & (1 << 5)) != 0);
        AlarmEn.UpdateData((data[offset + 5] & (1 << 6)) != 0);
        AlarmLow.UpdateData((data[offset + 5] & (1 << 7)) != 0);
        AlarmHigh.UpdateData((data[offset + 6] & (1 << 0)) != 0);
        Reset.UpdateData((data[offset + 6] & (1 << 1)) != 0);
        PRE.UpdateData(data, offset + 6);
        T.UpdateData(data, offset + 10);
        TMax.UpdateData(data, offset + 14);
        Count.UpdateData(data, offset + 18);
        LimitLow.UpdateData(data, offset + 22);
        LimitHigh.UpdateData(data, offset + 26);
        State.UpdateData(data, offset + 30);
        Running.UpdateData((data[offset + 34] & (1 << 2)) != 0);
        Holding.UpdateData((data[offset + 34] & (1 << 3)) != 0);
        Restarting.UpdateData((data[offset + 34] & (1 << 4)) != 0);
        Stopping.UpdateData((data[offset + 34] & (1 << 5)) != 0);
        Aborting.UpdateData((data[offset + 34] & (1 << 6)) != 0);
        Resetting.UpdateData((data[offset + 34] & (1 << 7)) != 0);
        Idle.UpdateData((data[offset + 35] & (1 << 0)) != 0);
        Held.UpdateData((data[offset + 35] & (1 << 1)) != 0);
        Complete.UpdateData((data[offset + 35] & (1 << 2)) != 0);
        Stopped.UpdateData((data[offset + 35] & (1 << 3)) != 0);
        Aborted.UpdateData((data[offset + 35] & (1 << 4)) != 0);
        Starting.UpdateData((data[offset + 35] & (1 << 5)) != 0);
        Downloading.UpdateData((data[offset + 35] & (1 << 6)) != 0);
        NotConnected.UpdateData((data[offset + 35] & (1 << 7)) != 0);
        Inactive.UpdateData((data[offset + 36] & (1 << 0)) != 0);
        Unknown.UpdateData((data[offset + 36] & (1 << 1)) != 0);
        Mode.UpdateData(data, offset + 36);
        Control.UpdateData(data, offset + 40);
        PauseControl.UpdateData(data, offset + 44);
        PauseEnabled.UpdateData((data[offset + 48] & (1 << 2)) != 0);
        Paused.UpdateData((data[offset + 48] & (1 << 3)) != 0);
        AutoPauseEnabled.UpdateData((data[offset + 48] & (1 << 4)) != 0);
        Index.UpdateData(data, offset + 48);
        Failure.UpdateData(data, offset + 52);
        InternalFailure.UpdateData(data, offset + 56);
        ValidCommands.UpdateData(data, offset + 60);
        StartValid.UpdateData((data[offset + 64] & (1 << 5)) != 0);
        HoldValid.UpdateData((data[offset + 64] & (1 << 6)) != 0);
        StopValid.UpdateData((data[offset + 64] & (1 << 7)) != 0);
        RestartValid.UpdateData((data[offset + 65] & (1 << 0)) != 0);
        AbortValid.UpdateData((data[offset + 65] & (1 << 1)) != 0);
        ResetValid.UpdateData((data[offset + 65] & (1 << 2)) != 0);
        AutoPauseValid.UpdateData((data[offset + 65] & (1 << 3)) != 0);
        PauseValid.UpdateData((data[offset + 65] & (1 << 4)) != 0);
        ResumeValid.UpdateData((data[offset + 65] & (1 << 5)) != 0);
        ClearFailureValid.UpdateData((data[offset + 65] & (1 << 6)) != 0);
        
        return offset + GetSize();
    }

    /// <summary>
    /// The <c>Status</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT Status
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>X</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL X
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FS</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL FS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SA</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL SA
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LS</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL LS
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DN</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>OV</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL OV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmEn</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL AlarmEn
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmLow</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL AlarmLow
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AlarmHigh</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL AlarmHigh
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Reset</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Reset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PRE</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>T</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT T
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>TMax</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT TMax
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Count</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public new DINT Count
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitLow</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT LimitLow
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>LimitHigh</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT LimitHigh
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>State</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT State
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Running</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Holding</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Holding
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Restarting</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Restarting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopping</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborting</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Aborting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Resetting</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Resetting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Idle</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Idle
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Held</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Held
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Complete</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Complete
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopped</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborted</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Starting</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Starting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Downloading</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Downloading
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NotConnected</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL NotConnected
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Inactive</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Inactive
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Unknown</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Unknown
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Mode</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT Mode
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Control</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT Control
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseControl</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT PauseControl
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseEnabled</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL PauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Paused</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL Paused
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AutoPauseEnabled</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL AutoPauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Index</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT Index
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Failure</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT Failure
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>InternalFailure</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT InternalFailure
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ValidCommands</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public DINT ValidCommands
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StartValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL StartValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HoldValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL HoldValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StopValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL StopValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>RestartValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL RestartValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AbortValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL AbortValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResetValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL ResetValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AutoPauseValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL AutoPauseValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL PauseValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ResumeValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL ResumeValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ClearFailureValid</c> member of the <see cref="SEQ_STEP"/> data type.
    /// </summary>
    public BOOL ClearFailureValid
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}