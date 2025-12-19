using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>PHASE</c> data type structure.
/// </summary>
[LogixData("PHASE")]
public sealed partial class PHASE : StructureData
{
    /// <summary>
    /// Creates a new <see cref="PHASE"/> instance initialized with default values.
    /// </summary>
    public PHASE() : base("PHASE")
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
        PauseControl = new DINT();
        PauseEnabled = new BOOL();
        Paused = new BOOL();
        AutoPauseEnabled = new BOOL();
        StepIndex = new DINT();
        Failure = new DINT();
        UnitID = new DINT();
        Owner = new DINT();
        PendingRequest = new DINT();
        DownloadInputParameters = new BOOL();
        DownloadInputParametersSubset = new BOOL();
        UploadOutputParameters = new BOOL();
        UploadOutputParametersSubset = new BOOL();
        DownloadOutputParameterLimits = new BOOL();
        AcquireResources = new BOOL();
        ReleaseResources = new BOOL();
        SendMessageToLinkedPhase = new BOOL();
        SendMessageToLinkedPhaseAndWait = new BOOL();
        ReceiveMessageFromLinkedPhase = new BOOL();
        CancelMessageToLinkedPhase = new BOOL();
        SendMessageToOperator = new BOOL();
        ClearMessageToOperator = new BOOL();
        GenerateESignature = new BOOL();
        DownloadBatchData = new BOOL();
        DownloadMaterialTrackDataContainerInUse = new BOOL();
        DownloadContainerBindingPriority = new BOOL();
        DownloadSufficientMaterial = new BOOL();
        DownloadMaterialTrackDatabaseData = new BOOL();
        UploadMaterialTrackDataContainerInUse = new BOOL();
        UploadContainerBindingPriority = new BOOL();
        UploadMaterialTrackDatabaseData = new BOOL();
        AbortingRequest = new BOOL();
        NewInputParameters = new BOOL();
        Producing = new BOOL();
        Standby = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="PHASE"/> instance initialized with the provided element.
    /// </summary>
    public PHASE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>State</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT State
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Running</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Holding</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Holding
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Restarting</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Restarting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopping</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborting</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Aborting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Resetting</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Resetting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Idle</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Idle
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Held</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Held
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Complete</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Complete
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Stopped</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Aborted</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseControl</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT PauseControl
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PauseEnabled</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL PauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Paused</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Paused
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AutoPauseEnabled</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL AutoPauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>StepIndex</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT StepIndex
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Failure</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT Failure
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UnitID</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT UnitID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Owner</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PendingRequest</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public DINT PendingRequest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadInputParameters</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadInputParameters
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadInputParametersSubset</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadInputParametersSubset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UploadOutputParameters</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL UploadOutputParameters
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UploadOutputParametersSubset</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL UploadOutputParametersSubset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadOutputParameterLimits</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadOutputParameterLimits
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AcquireResources</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL AcquireResources
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ReleaseResources</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL ReleaseResources
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SendMessageToLinkedPhase</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL SendMessageToLinkedPhase
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SendMessageToLinkedPhaseAndWait</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL SendMessageToLinkedPhaseAndWait
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ReceiveMessageFromLinkedPhase</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL ReceiveMessageFromLinkedPhase
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>CancelMessageToLinkedPhase</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL CancelMessageToLinkedPhase
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>SendMessageToOperator</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL SendMessageToOperator
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ClearMessageToOperator</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL ClearMessageToOperator
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>GenerateESignature</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL GenerateESignature
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadBatchData</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadBatchData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadMaterialTrackDataContainerInUse</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadMaterialTrackDataContainerInUse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadContainerBindingPriority</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadContainerBindingPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadSufficientMaterial</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadSufficientMaterial
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>DownloadMaterialTrackDatabaseData</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL DownloadMaterialTrackDatabaseData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UploadMaterialTrackDataContainerInUse</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL UploadMaterialTrackDataContainerInUse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UploadContainerBindingPriority</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL UploadContainerBindingPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>UploadMaterialTrackDatabaseData</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL UploadMaterialTrackDatabaseData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AbortingRequest</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL AbortingRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NewInputParameters</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL NewInputParameters
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Producing</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Producing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Standby</c> member of the <see cref="PHASE"/> data type.
    /// </summary>
    public BOOL Standby
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
