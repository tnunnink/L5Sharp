using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Types.Predefined;

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
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <summary>
    /// Gets the <c>State</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT State { get; set; } = new();
    
    
    /// <summary>
    /// Gets the <c>Running</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Running { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Holding</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Holding { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Restarting</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Restarting { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Stopping</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Stopping { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Aborting</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Aborting { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Resetting</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Resetting { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Idle</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Idle { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Held</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Held { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Complete</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Complete { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Stopped</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Stopped { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Aborted</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Aborted { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PauseControl</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT PauseControl { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PauseEnabled</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL PauseEnabled { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Paused</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Paused { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>AutoPauseEnabled</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL AutoPauseEnabled { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>StepIndex</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT StepIndex { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Failure</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT Failure { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UnitID</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT UnitID { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Owner</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT Owner { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>PendingRequest</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="DINT"/> atomic value.</value>
    public DINT PendingRequest { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadInputParameters</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadInputParameters { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadInputParametersSubset</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadInputParametersSubset { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UploadOutputParameters</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadOutputParameters { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UploadOutputParametersSubset</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadOutputParametersSubset { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadOutputParameterLimits</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadOutputParameterLimits { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>AcquireResources</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL AcquireResources { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>ReleaseResources</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL ReleaseResources { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SendMessageToLinkedPhase</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SendMessageToLinkedPhase { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SendMessageToLinkedPhaseAndWait</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SendMessageToLinkedPhaseAndWait { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>ReceiveMessageFromLinkedPhase</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL ReceiveMessageFromLinkedPhase { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>CancelMessageToLinkedPhase</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL CancelMessageToLinkedPhase { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>SendMessageToOperator</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL SendMessageToOperator { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>ClearMessageToOperator</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL ClearMessageToOperator { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>GenerateESignature</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL GenerateESignature { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadBatchData</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadBatchData { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadMaterialTrackDataContainerInUse</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadMaterialTrackDataContainerInUse { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadContainerBindingPriority</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadContainerBindingPriority { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadSufficientMaterial</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadSufficientMaterial { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>DownloadMaterialTrackDatabaseData</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL DownloadMaterialTrackDatabaseData { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UploadMaterialTrackDataContainerInUse</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadMaterialTrackDataContainerInUse { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UploadContainerBindingPriority</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadContainerBindingPriority { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>UploadMaterialTrackDatabaseData</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL UploadMaterialTrackDatabaseData { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>AbortingRequest</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL AbortingRequest { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>NewInputParameters</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL NewInputParameters { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Producing</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Producing { get; set; } = new();
    
    /// <summary>
    /// Gets the <c>Standby</c> member of the <see cref="PHASE"/> type.
    /// </summary>
    /// <value>A <see cref="BOOL"/> atomic value.</value>
    public BOOL Standby { get; set; } = new();
}