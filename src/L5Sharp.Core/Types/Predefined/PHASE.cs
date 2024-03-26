using System.Xml.Linq;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Core;

/// <summary>
/// A predefined data type that is built into Logix and used with PID instructions.
/// </summary>
public sealed class PHASE : StructureType
{
    /// <summary>
    /// Creates a new <see cref="PID"/> data type instance.
    /// </summary>
    public PHASE() : base(nameof(PHASE))
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

    /// <inheritdoc />
    public PHASE(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the <c>State</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT State
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }


    /// <summary>
    /// Gets the <c>Running</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Running
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Holding</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Holding
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Restarting</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Restarting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Stopping</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Stopping
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Aborting</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Aborting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Resetting</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Resetting
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Idle</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Idle
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Held</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Held
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Complete</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Complete
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Stopped</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Stopped
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Aborted</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Aborted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PauseControl</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT PauseControl
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PauseEnabled</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Paused</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Paused
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>AutoPauseEnabled</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL AutoPauseEnabled
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>StepIndex</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT StepIndex
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Failure</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT Failure
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UnitID</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT UnitID
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Owner</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT Owner
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>PendingRequest</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT PendingRequest
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadInputParameters</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadInputParameters
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadInputParametersSubset</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadInputParametersSubset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UploadOutputParameters</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadOutputParameters
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UploadOutputParametersSubset</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadOutputParametersSubset
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadOutputParameterLimits</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadOutputParameterLimits
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>AcquireResources</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL AcquireResources
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>ReleaseResources</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL ReleaseResources
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SendMessageToLinkedPhase</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SendMessageToLinkedPhase
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SendMessageToLinkedPhaseAndWait</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SendMessageToLinkedPhaseAndWait
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>ReceiveMessageFromLinkedPhase</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL ReceiveMessageFromLinkedPhase
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>CancelMessageToLinkedPhase</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CancelMessageToLinkedPhase
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>SendMessageToOperator</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SendMessageToOperator
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>ClearMessageToOperator</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL ClearMessageToOperator
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>GenerateESignature</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL GenerateESignature
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadBatchData</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadBatchData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadMaterialTrackDataContainerInUse</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadMaterialTrackDataContainerInUse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadContainerBindingPriority</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadContainerBindingPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadSufficientMaterial</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadSufficientMaterial
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>DownloadMaterialTrackDatabaseData</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadMaterialTrackDatabaseData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UploadMaterialTrackDataContainerInUse</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadMaterialTrackDataContainerInUse
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UploadContainerBindingPriority</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadContainerBindingPriority
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>UploadMaterialTrackDatabaseData</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadMaterialTrackDatabaseData
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>AbortingRequest</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL AbortingRequest
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>NewInputParameters</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NewInputParameters
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Producing</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Producing
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <c>Standby</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Standby
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}