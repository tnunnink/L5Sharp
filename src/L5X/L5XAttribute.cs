// ReSharper disable InconsistentNaming
namespace L5Sharp.L5X
{
    /// <summary>
    /// An enumeration of L5X attributes for ease of referencing these value without using magic strings.
    /// </summary>
    internal enum L5XAttribute
    {
        ///<summary>
        /// Represents the <see cref="AckRequired"/> L5X attribute.
        ///</summary>
        AckRequired,

        ///<summary>
        /// Represents the <see cref="Address"/> L5X attribute.
        ///</summary>
        Address,

        ///<summary>
        /// Represents the <see cref="AlarmCountReset"/> L5X attribute.
        ///</summary>
        AlarmCountReset,
        
        ///<summary>
        /// Represents the <see cref="AliasFor"/> L5X attribute.
        ///</summary>
        AliasFor,

        ///<summary>
        /// Represents the <see cref="AttributeNumber"/> L5X attribute.
        ///</summary>
        AttributeNumber,

        ///<summary>
        /// Represents the <see cref="BitNumber"/> L5X attribute.
        ///</summary>
        BitNumber,

        ///<summary>
        /// Represents the <see cref="CacheConnections"/> L5X attribute.
        ///</summary>
        CacheConnections,

        ///<summary>
        /// Represents the <see cref="CanUseRPIFromProducer"/> L5X attribute.
        ///</summary>
        CanUseRPIFromProducer,

        ///<summary>
        /// Represents the <see cref="CatalogNumber"/> L5X attribute.
        ///</summary>
        CatalogNumber,

        ///<summary>
        /// Represents the <see cref="ChangesToDetect"/> L5X attribute.
        ///</summary>
        ChangesToDetect,

        ///<summary>
        /// Represents the <see cref="Class"/> L5X attribute.
        ///</summary>
        Class,

        ///<summary>
        /// Represents the <see cref="Code"/> L5X attribute.
        ///</summary>
        Code,

        ///<summary>
        /// Represents the <see cref="CommMethod"/> L5X attribute.
        ///</summary>
        CommMethod,

        ///<summary>
        /// Represents the <see cref="CommTypeCode"/> L5X attribute.
        ///</summary>
        CommTypeCode,

        ///<summary>
        /// Represents the <see cref="Condition"/> L5X attribute.
        ///</summary>
        Condition,

        ///<summary>
        /// Represents the <see cref="ConfigSize"/> L5X attribute.
        ///</summary>
        ConfigSize,

        ///<summary>
        /// Represents the <see cref="ConnectedFlag"/> L5X attribute.
        ///</summary>
        ConnectedFlag,

        ///<summary>
        /// Represents the <see cref="ConnectionPath"/> L5X attribute.
        ///</summary>
        ConnectionPath,

        ///<summary>
        /// Represents the <see cref="Constant"/> L5X attribute.
        ///</summary>
        Constant,

        ///<summary>
        /// Represents the <see cref="ContainsContext"/> L5X attribute.
        ///</summary>
        ContainsContext,

        ///<summary>
        /// Represents the <see cref="CreatedBy"/> L5X attribute.
        ///</summary>
        CreatedBy,

        ///<summary>
        /// Represents the <see cref="CreatedDate"/> L5X attribute.
        ///</summary>
        CreatedDate,

        ///<summary>
        /// Represents the <see cref="DataType"/> L5X attribute.
        ///</summary>
        DataType,

        ///<summary>
        /// Represents the <see cref="Deadband"/> L5X attribute.
        ///</summary>
        Deadband,

        ///<summary>
        /// Represents the <see cref="DestinationTag"/> L5X attribute.
        ///</summary>
        DestinationTag,

        ///<summary>
        /// Represents the <see cref="Dimension"/> L5X attribute.
        ///</summary>
        Dimension,

        ///<summary>
        /// Represents the <see cref="Dimensions"/> L5X attribute.
        ///</summary>
        Dimensions,

        ///<summary>
        /// Represents the <see cref="Disabled"/> L5X attribute.
        ///</summary>
        Disabled,

        ///<summary>
        /// Represents the <see cref="DisableUpdateOutputs"/> L5X attribute.
        ///</summary>
        DisableUpdateOutputs,

        ///<summary>
        /// Represents the <see cref="DownloadProjectCustomProperties"/> L5X attribute.
        ///</summary>
        DownloadProjectCustomProperties,

        ///<summary>
        /// Represents the <see cref="DownloadProjectDocumentationAndExtendedProperties"/> L5X attribute.
        ///</summary>
        DownloadProjectDocumentationAndExtendedProperties,

        ///<summary>
        /// Represents the <see cref="DrivesADCEnabled"/> L5X attribute.
        ///</summary>
        DrivesADCEnabled,

        ///<summary>
        /// Represents the <see cref="DrivesADCMode"/> L5X attribute.
        ///</summary>
        DrivesADCMode,

        ///<summary>
        /// Represents the <see cref="EditedBy"/> L5X attribute.
        ///</summary>
        EditedBy,

        ///<summary>
        /// Represents the <see cref="EditedDate"/> L5X attribute.
        ///</summary>
        EditedDate,

        ///<summary>
        /// Represents the <see cref="Enabled"/> L5X attribute.
        ///</summary>
        Enabled,

        ///<summary>
        /// Represents the <see cref="EnableIn"/> L5X attribute.
        ///</summary>
        EnableIn,
        
        ///<summary>
        /// Represents the <see cref="EnableTimeout"/> L5X attribute.
        ///</summary>
        EnableTimeout,

        ///<summary>
        /// Represents the <see cref="EncodedType"/> L5X attribute.
        ///</summary>
        EncodedType,

        ///<summary>
        /// Represents the <see cref="EncryptionConfig"/> L5X attribute.
        ///</summary>
        EncryptionConfig,

        ///<summary>
        /// Represents the <see cref="EventID"/> L5X attribute.
        ///</summary>
        EventID,
        
        ///<summary>
        /// Represents the <see cref="EventTag"/> L5X attribute.
        ///</summary>
        EventTag,
        
        ///<summary>
        /// Represents the <see cref="EventTrigger"/> L5X attribute.
        ///</summary>
        EventTrigger,

        ///<summary>
        /// Represents the <see cref="ExecuteEnableInFalse"/> L5X attribute.
        ///</summary>
        ExecuteEnableInFalse,

        ///<summary>
        /// Represents the <see cref="ExecutePostscan"/> L5X attribute.
        ///</summary>
        ExecutePostscan,

        ///<summary>
        /// Represents the <see cref="ExecutePrescan"/> L5X attribute.
        ///</summary>
        ExecutePrescan,

        ///<summary>
        /// Represents the <see cref="ExportDate"/> L5X attribute.
        ///</summary>
        ExportDate,

        ///<summary>
        /// Represents the <see cref="ExportOptions"/> L5X attribute.
        ///</summary>
        ExportOptions,

        ///<summary>
        /// Represents the <see cref="ExternalAccess"/> L5X attribute.
        ///</summary>
        ExternalAccess,

        ///<summary>
        /// Represents the <see cref="Family"/> L5X attribute.
        ///</summary>
        Family,
        
        ///<summary>
        /// Represents the <see cref="FaultRoutineName"/> L5X attribute.
        ///</summary>
        FaultRoutineName,

        ///<summary>
        /// Represents the <see cref="Format"/> L5X attribute.
        ///</summary>
        Format,

        ///<summary>
        /// Represents the <see cref="HEnabled"/> L5X attribute.
        ///</summary>
        HEnabled,

        ///<summary>
        /// Represents the <see cref="HHEnabled"/> L5X attribute.
        ///</summary>
        HHEnabled,

        ///<summary>
        /// Represents the <see cref="HHLimit"/> L5X attribute.
        ///</summary>
        HHLimit,

        ///<summary>
        /// Represents the <see cref="HHMinDurationEnable"/> L5X attribute.
        ///</summary>
        HHMinDurationEnable,

        ///<summary>
        /// Represents the <see cref="HHOperAck"/> L5X attribute.
        ///</summary>
        HHOperAck,

        ///<summary>
        /// Represents the <see cref="HHOperShelve"/> L5X attribute.
        ///</summary>
        HHOperShelve,

        ///<summary>
        /// Represents the <see cref="HHOperUnshelve"/> L5X attribute.
        ///</summary>
        HHOperUnshelve,

        ///<summary>
        /// Represents the <see cref="HHProgAck"/> L5X attribute.
        ///</summary>
        HHProgAck,

        ///<summary>
        /// Represents the <see cref="HHSeverity"/> L5X attribute.
        ///</summary>
        HHSeverity,

        ///<summary>
        /// Represents the <see cref="Hidden"/> L5X attribute.
        ///</summary>
        Hidden,

        ///<summary>
        /// Represents the <see cref="HLimit"/> L5X attribute.
        ///</summary>
        HLimit,

        ///<summary>
        /// Represents the <see cref="HMinDurationEnable"/> L5X attribute.
        ///</summary>
        HMinDurationEnable,

        ///<summary>
        /// Represents the <see cref="HOperAck"/> L5X attribute.
        ///</summary>
        HOperAck,

        ///<summary>
        /// Represents the <see cref="HOperShelve"/> L5X attribute.
        ///</summary>
        HOperShelve,

        ///<summary>
        /// Represents the <see cref="HOperUnshelve"/> L5X attribute.
        ///</summary>
        HOperUnshelve,

        ///<summary>
        /// Represents the <see cref="HProgAck"/> L5X attribute.
        ///</summary>
        HProgAck,

        ///<summary>
        /// Represents the <see cref="HSeverity"/> L5X attribute.
        ///</summary>
        HSeverity,

        ///<summary>
        /// Represents the <see cref="Id"/> L5X attribute.
        ///</summary>
        Id,

        ///<summary>
        /// Represents the <see cref="In"/> L5X attribute.
        ///</summary>
        In,

        ///<summary>
        /// Represents the <see cref="Index"/> L5X attribute.
        ///</summary>
        Index,

        ///<summary>
        /// Represents the <see cref="InFault"/> L5X attribute.
        ///</summary>
        InFault,

        ///<summary>
        /// Represents the <see cref="InhibitAutomaticFirmwareUpdate"/> L5X attribute.
        ///</summary>
        InhibitAutomaticFirmwareUpdate,

        ///<summary>
        /// Represents the <see cref="Inhibited"/> L5X attribute.
        ///</summary>
        Inhibited,

        ///<summary>
        /// Represents the <see cref="InhibitTask"/> L5X attribute.
        ///</summary>
        InhibitTask,

        ///<summary>
        /// Represents the <see cref="InputConnectionType"/> L5X attribute.
        ///</summary>
        InputConnectionType,

        ///<summary>
        /// Represents the <see cref="InputCxnPoint"/> L5X attribute.
        ///</summary>
        InputCxnPoint,

        ///<summary>
        /// Represents the <see cref="InputProductionTrigger"/> L5X attribute.
        ///</summary>
        InputProductionTrigger,

        ///<summary>
        /// Represents the <see cref="InputSize"/> L5X attribute.
        ///</summary>
        InputSize,

        ///<summary>
        /// Represents the <see cref="InputTagSuffix"/> L5X attribute.
        ///</summary>
        InputTagSuffix,
        
        ///<summary>
        /// Represents the <see cref="IsEncrypted"/> L5X attribute.
        ///</summary>
        IsEncrypted,

        ///<summary>
        /// Represents the <see cref="KeepTestEditsOnSwitchOver"/> L5X attribute.
        ///</summary>
        KeepTestEditsOnSwitchOver,

        ///<summary>
        /// Represents the <see cref="Label"/> L5X attribute.
        ///</summary>
        Label,

        ///<summary>
        /// Represents the <see cref="LargePacketUsage"/> L5X attribute.
        ///</summary>
        LargePacketUsage,

        ///<summary>
        /// Represents the <see cref="LastModifiedDate"/> L5X attribute.
        ///</summary>
        LastModifiedDate,

        ///<summary>
        /// Represents the <see cref="Latched"/> L5X attribute.
        ///</summary>
        Latched,

        ///<summary>
        /// Represents the <see cref="LEnabled"/> L5X attribute.
        ///</summary>
        LEnabled,

        ///<summary>
        /// Represents the <see cref="Length"/> L5X attribute.
        ///</summary>
        Length,

        ///<summary>
        /// Represents the <see cref="LLEnabled"/> L5X attribute.
        ///</summary>
        LLEnabled,

        ///<summary>
        /// Represents the <see cref="LLimit"/> L5X attribute.
        ///</summary>
        LLimit,

        ///<summary>
        /// Represents the <see cref="LLLimit"/> L5X attribute.
        ///</summary>
        LLLimit,

        ///<summary>
        /// Represents the <see cref="LLMinDurationEnable"/> L5X attribute.
        ///</summary>
        LLMinDurationEnable,

        ///<summary>
        /// Represents the <see cref="LLOperAck"/> L5X attribute.
        ///</summary>
        LLOperAck,

        ///<summary>
        /// Represents the <see cref="LLOperShelve"/> L5X attribute.
        ///</summary>
        LLOperShelve,

        ///<summary>
        /// Represents the <see cref="LLOperUnshelve"/> L5X attribute.
        ///</summary>
        LLOperUnshelve,

        ///<summary>
        /// Represents the <see cref="LLProgAck"/> L5X attribute.
        ///</summary>
        LLProgAck,

        ///<summary>
        /// Represents the <see cref="LLSeverity"/> L5X attribute.
        ///</summary>
        LLSeverity,

        ///<summary>
        /// Represents the <see cref="LMinDurationEnable"/> L5X attribute.
        ///</summary>
        LMinDurationEnable,

        ///<summary>
        /// Represents the <see cref="LocalElement"/> L5X attribute.
        ///</summary>
        LocalElement,

        ///<summary>
        /// Represents the <see cref="LocalIndex"/> L5X attribute.
        ///</summary>
        LocalIndex,

        ///<summary>
        /// Represents the <see cref="LocalTimeAdjustment"/> L5X attribute.
        ///</summary>
        LocalTimeAdjustment,

        ///<summary>
        /// Represents the <see cref="LOperAck"/> L5X attribute.
        ///</summary>
        LOperAck,

        ///<summary>
        /// Represents the <see cref="LOperShelve"/> L5X attribute.
        ///</summary>
        LOperShelve,

        ///<summary>
        /// Represents the <see cref="LOperUnshelve"/> L5X attribute.
        ///</summary>
        LOperUnshelve,

        ///<summary>
        /// Represents the <see cref="LProgAck"/> L5X attribute.
        ///</summary>
        LProgAck,

        ///<summary>
        /// Represents the <see cref="LSeverity"/> L5X attribute.
        ///</summary>
        LSeverity,

        ///<summary>
        /// Represents the <see cref="MainRoutineName"/> L5X attribute.
        ///</summary>
        MainRoutineName,

        ///<summary>
        /// Represents the <see cref="Major"/> L5X attribute.
        ///</summary>
        Major,

        ///<summary>
        /// Represents the <see cref="MajorFault"/> L5X attribute.
        ///</summary>
        MajorFault,

        ///<summary>
        /// Represents the <see cref="MajorFaultProgram"/> L5X attribute.
        ///</summary>
        MajorFaultProgram,

        ///<summary>
        /// Represents the <see cref="MajorRev"/> L5X attribute.
        ///</summary>
        MajorRev,

        ///<summary>
        /// Represents the <see cref="MasterID"/> L5X attribute.
        ///</summary>
        MasterID,

        ///<summary>
        /// Represents the <see cref="MatchProjectToController"/> L5X attribute.
        ///</summary>
        MatchProjectToController,

        ///<summary>
        /// Represents the <see cref="MaxShelveDuration"/> L5X attribute.
        ///</summary>
        MaxShelveDuration,

        ///<summary>
        /// Represents the <see cref="MessageType"/> L5X attribute.
        ///</summary>
        MessageType,

        ///<summary>
        /// Represents the <see cref="MinDurationPRE"/> L5X attribute.
        ///</summary>
        MinDurationPRE,

        ///<summary>
        /// Represents the <see cref="Minor"/> L5X attribute.
        ///</summary>
        Minor,

        ///<summary>
        /// Represents the <see cref="MinorRev"/> L5X attribute.
        ///</summary>
        MinorRev,

        ///<summary>
        /// Represents the <see cref="Name"/> L5X attribute.
        ///</summary>
        Name,

        ///<summary>
        /// Represents the <see cref="Number"/> L5X attribute.
        ///</summary>
        Number,

        ///<summary>
        /// Represents the <see cref="ObjectType"/> L5X attribute.
        ///</summary>
        ObjectType,

        ///<summary>
        /// Represents the <see cref="OperAck"/> L5X attribute.
        ///</summary>
        OperAck,

        ///<summary>
        /// Represents the <see cref="OperAckAll"/> L5X attribute.
        ///</summary>
        OperAckAll,

        ///<summary>
        /// Represents the <see cref="Operand"/> L5X attribute.
        ///</summary>
        Operand,

        ///<summary>
        /// Represents the <see cref="OperDisable"/> L5X attribute.
        ///</summary>
        OperDisable,

        ///<summary>
        /// Represents the <see cref="OperEnable"/> L5X attribute.
        ///</summary>
        OperEnable,

        ///<summary>
        /// Represents the <see cref="OperReset"/> L5X attribute.
        ///</summary>
        OperReset,

        ///<summary>
        /// Represents the <see cref="OperShelve"/> L5X attribute.
        ///</summary>
        OperShelve,

        ///<summary>
        /// Represents the <see cref="OperSuppress"/> L5X attribute.
        ///</summary>
        OperSuppress,

        ///<summary>
        /// Represents the <see cref="OperUnshelve"/> L5X attribute.
        ///</summary>
        OperUnshelve,

        ///<summary>
        /// Represents the <see cref="OperUnsuppress"/> L5X attribute.
        ///</summary>
        OperUnsuppress,

        ///<summary>
        /// Represents the <see cref="OutputCxnPoint"/> L5X attribute.
        ///</summary>
        OutputCxnPoint,
        
        ///<summary>
        /// Represents the <see cref="OutputRedundantOwner"/> L5X attribute.
        ///</summary>
        OutputRedundantOwner,

        ///<summary>
        /// Represents the <see cref="OutputSize"/> L5X attribute.
        ///</summary>
        OutputSize,

        ///<summary>
        /// Represents the <see cref="OutputTagSuffix"/> L5X attribute.
        ///</summary>
        OutputTagSuffix,

        ///<summary>
        /// Represents the <see cref="Owner"/> L5X attribute.
        ///</summary>
        Owner,

        ///<summary>
        /// Represents the <see cref="ParentModPortId"/> L5X attribute.
        ///</summary>
        ParentModPortId,

        ///<summary>
        /// Represents the <see cref="ParentModule"/> L5X attribute.
        ///</summary>
        ParentModule,

        ///<summary>
        /// Represents the <see cref="PassThroughConfiguration"/> L5X attribute.
        ///</summary>
        PassThroughConfiguration,

        ///<summary>
        /// Represents the <see cref="Port"/> L5X attribute.
        ///</summary>
        Port,

        ///<summary>
        /// Represents the <see cref="PortEnabled"/> L5X attribute.
        ///</summary>
        PortEnabled,

        ///<summary>
        /// Represents the <see cref="PrimCxnInputSize"/> L5X attribute.
        ///</summary>
        PrimCxnInputSize,

        ///<summary>
        /// Represents the <see cref="PrimCxnOutputSize"/> L5X attribute.
        ///</summary>
        PrimCxnOutputSize,

        ///<summary>
        /// Represents the <see cref="Priority"/> L5X attribute.
        ///</summary>
        Priority,

        ///<summary>
        /// Represents the <see cref="Priority1"/> L5X attribute.
        ///</summary>
        Priority1,

        ///<summary>
        /// Represents the <see cref="Priority2"/> L5X attribute.
        ///</summary>
        Priority2,

        ///<summary>
        /// Represents the <see cref="ProcessorType"/> L5X attribute.
        ///</summary>
        ProcessorType,

        ///<summary>
        /// Represents the <see cref="ProductCode"/> L5X attribute.
        ///</summary>
        ProductCode,

        ///<summary>
        /// Represents the <see cref="ProductType"/> L5X attribute.
        ///</summary>
        ProductType,

        ///<summary>
        /// Represents the <see cref="ProgAck"/> L5X attribute.
        ///</summary>
        ProgAck,

        ///<summary>
        /// Represents the <see cref="ProgAckAll"/> L5X attribute.
        ///</summary>
        ProgAckAll,

        ///<summary>
        /// Represents the <see cref="ProgDisable"/> L5X attribute.
        ///</summary>
        ProgDisable,

        ///<summary>
        /// Represents the <see cref="ProgEnable"/> L5X attribute.
        ///</summary>
        ProgEnable,

        ///<summary>
        /// Represents the <see cref="ProgrammaticallySendEventTrigger"/> L5X attribute.
        ///</summary>
        ProgrammaticallySendEventTrigger,

        ///<summary>
        /// Represents the <see cref="ProgReset"/> L5X attribute.
        ///</summary>
        ProgReset,

        ///<summary>
        /// Represents the <see cref="ProgSuppress"/> L5X attribute.
        ///</summary>
        ProgSuppress,

        ///<summary>
        /// Represents the <see cref="ProgTime"/> L5X attribute.
        ///</summary>
        ProgTime,

        ///<summary>
        /// Represents the <see cref="ProgUnshelve"/> L5X attribute.
        ///</summary>
        ProgUnshelve,

        ///<summary>
        /// Represents the <see cref="ProgUnshelveAll"/> L5X attribute.
        ///</summary>
        ProgUnshelveAll,

        ///<summary>
        /// Represents the <see cref="ProgUnsuppress"/> L5X attribute.
        ///</summary>
        ProgUnsuppress,

        ///<summary>
        /// Represents the <see cref="ProjectCreationDate"/> L5X attribute.
        ///</summary>
        ProjectCreationDate,

        ///<summary>
        /// Represents the <see cref="ProjectSN"/> L5X attribute.
        ///</summary>
        ProjectSN,

        ///<summary>
        /// Represents the <see cref="PTPEnable"/> L5X attribute.
        ///</summary>
        PTPEnable,

        ///<summary>
        /// Represents the <see cref="Radix"/> L5X attribute.
        ///</summary>
        Radix,

        ///<summary>
        /// Represents the <see cref="Rate"/> L5X attribute.
        ///</summary>
        Rate,

        ///<summary>
        /// Represents the <see cref="ReportMinorOverflow"/> L5X attribute.
        ///</summary>
        ReportMinorOverflow,

        ///<summary>
        /// Represents the <see cref="RequestedLength"/> L5X attribute.
        ///</summary>
        RequestedLength,

        ///<summary>
        /// Represents the <see cref="Required"/> L5X attribute.
        ///</summary>
        Required,

        ///<summary>
        /// Represents the <see cref="Revision"/> L5X attribute.
        ///</summary>
        Revision,
        
        ///<summary>
        /// Represents the <see cref="RevisionExtension"/> L5X attribute.
        ///</summary>
        RevisionExtension,

        ///<summary>
        /// Represents the <see cref="ROCNegLimit"/> L5X attribute.
        ///</summary>
        ROCNegLimit,

        ///<summary>
        /// Represents the <see cref="ROCNegOperAck"/> L5X attribute.
        ///</summary>
        ROCNegOperAck,

        ///<summary>
        /// Represents the <see cref="ROCNegOperShelve"/> L5X attribute.
        ///</summary>
        ROCNegOperShelve,

        ///<summary>
        /// Represents the <see cref="ROCNegOperUnshelve"/> L5X attribute.
        ///</summary>
        ROCNegOperUnshelve,

        ///<summary>
        /// Represents the <see cref="ROCNegProgAck"/> L5X attribute.
        ///</summary>
        ROCNegProgAck,

        ///<summary>
        /// Represents the <see cref="ROCNegSeverity"/> L5X attribute.
        ///</summary>
        ROCNegSeverity,

        ///<summary>
        /// Represents the <see cref="ROCPeriod"/> L5X attribute.
        ///</summary>
        ROCPeriod,

        ///<summary>
        /// Represents the <see cref="ROCPosLimit"/> L5X attribute.
        ///</summary>
        ROCPosLimit,

        ///<summary>
        /// Represents the <see cref="ROCPosOperAck"/> L5X attribute.
        ///</summary>
        ROCPosOperAck,

        ///<summary>
        /// Represents the <see cref="ROCPosOperShelve"/> L5X attribute.
        ///</summary>
        ROCPosOperShelve,

        ///<summary>
        /// Represents the <see cref="ROCPosOperUnshelve"/> L5X attribute.
        ///</summary>
        ROCPosOperUnshelve,

        ///<summary>
        /// Represents the <see cref="ROCPosProgAck"/> L5X attribute.
        ///</summary>
        ROCPosProgAck,

        ///<summary>
        /// Represents the <see cref="ROCPosSeverity"/> L5X attribute.
        ///</summary>
        ROCPosSeverity,

        ///<summary>
        /// Represents the <see cref="RPI"/> L5X attribute.
        ///</summary>
        RPI,

        ///<summary>
        /// Represents the <see cref="SafetyEnabled"/> L5X attribute.
        ///</summary>
        SafetyEnabled,

        ///<summary>
        /// Represents the <see cref="SchemaRevision"/> L5X attribute.
        ///</summary>
        SchemaRevision,

        ///<summary>
        /// Represents the <see cref="Scope"/> L5X attribute.
        ///</summary>
        Scope,

        ///<summary>
        /// Represents the <see cref="ServiceCode"/> L5X attribute.
        ///</summary>
        ServiceCode,

        ///<summary>
        /// Represents the <see cref="Severity"/> L5X attribute.
        ///</summary>
        Severity,

        ///<summary>
        /// Represents the <see cref="SFCExecutionControl"/> L5X attribute.
        ///</summary>
        SFCExecutionControl,

        ///<summary>
        /// Represents the <see cref="SFCLastScan"/> L5X attribute.
        ///</summary>
        SFCLastScan,

        ///<summary>
        /// Represents the <see cref="SFCRestartPosition"/> L5X attribute.
        ///</summary>
        SFCRestartPosition,

        ///<summary>
        /// Represents the <see cref="ShelveDuration"/> L5X attribute.
        ///</summary>
        ShelveDuration,

        ///<summary>
        /// Represents the <see cref="ShutdownParentOnFault"/> L5X attribute.
        ///</summary>
        ShutdownParentOnFault,

        ///<summary>
        /// Represents the <see cref="Size"/> L5X attribute.
        ///</summary>
        Size,

        ///<summary>
        /// Represents the <see cref="SoftwareRevision"/> L5X attribute.
        ///</summary>
        SoftwareRevision,

        ///<summary>
        /// Represents the <see cref="Specifier"/> L5X attribute.
        ///</summary>
        Specifier,

        ///<summary>
        /// Represents the <see cref="State"/> L5X attribute.
        ///</summary>
        State,

        ///<summary>
        /// Represents the <see cref="TagType"/> L5X attribute.
        ///</summary>
        TagType,

        ///<summary>
        /// Represents the <see cref="Target"/> L5X attribute.
        ///</summary>
        Target,

        ///<summary>
        /// Represents the <see cref="TargetName"/> L5X attribute.
        ///</summary>
        TargetName,

        ///<summary>
        /// Represents the <see cref="TargetObject"/> L5X attribute.
        ///</summary>
        TargetObject,

        ///<summary>
        /// Represents the <see cref="TargetType"/> L5X attribute.
        ///</summary>
        TargetType,

        ///<summary>
        /// Represents the <see cref="TestEdits"/> L5X attribute.
        ///</summary>
        TestEdits,

        ///<summary>
        /// Represents the <see cref="TimeZone"/> L5X attribute.
        ///</summary>
        TimeZone,

        ///<summary>
        /// Represents the <see cref="Type"/> L5X attribute.
        ///</summary>
        Type,

        ///<summary>
        /// Represents the <see cref="Unicast"/> L5X attribute.
        ///</summary>
        Unicast,

        ///<summary>
        /// Represents the <see cref="Upstream"/> L5X attribute.
        ///</summary>
        Upstream,

        ///<summary>
        /// Represents the <see cref="Usage"/> L5X attribute.
        ///</summary>
        Usage,

        ///<summary>
        /// Represents the <see cref="Use"/> L5X attribute.
        ///</summary>
        Use,

        ///<summary>
        /// Represents the <see cref="UseAsFolder"/> L5X attribute.
        ///</summary>
        UseAsFolder,

        ///<summary>
        /// Represents the <see cref="UseProgTime"/> L5X attribute.
        ///</summary>
        UseProgTime,

        ///<summary>
        /// Represents the <see cref="UserDefinedCatalogNumber"/> L5X attribute.
        ///</summary>
        UserDefinedCatalogNumber,

        ///<summary>
        /// Represents the <see cref="UserDefinedMajor"/> L5X attribute.
        ///</summary>
        UserDefinedMajor,

        ///<summary>
        /// Represents the <see cref="UserDefinedMinor"/> L5X attribute.
        ///</summary>
        UserDefinedMinor,

        ///<summary>
        /// Represents the <see cref="UserDefinedProductCode"/> L5X attribute.
        ///</summary>
        UserDefinedProductCode,

        ///<summary>
        /// Represents the <see cref="UserDefinedProductType"/> L5X attribute.
        ///</summary>
        UserDefinedProductType,

        ///<summary>
        /// Represents the <see cref="UserDefinedVendor"/> L5X attribute.
        ///</summary>
        UserDefinedVendor,

        ///<summary>
        /// Represents the <see cref="Value"/> L5X attribute.
        ///</summary>
        Value,

        ///<summary>
        /// Represents the <see cref="Vendor"/> L5X attribute.
        ///</summary>
        Vendor,

        ///<summary>
        /// Represents the <see cref="Visible"/> L5X attribute.
        ///</summary>
        Visible,

        ///<summary>
        /// Represents the <see cref="Watchdog"/> L5X attribute.
        ///</summary>
        Watchdog,
    }
}