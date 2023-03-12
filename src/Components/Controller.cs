using System;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// A logix <c>Controller</c> component. Contains the properties that comprise the L5X Controller element.
    /// </summary>
    [XmlType(L5XName.Controller)]
    [LogixSerializer(typeof(ControllerSerializer))]
    public class Controller : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The catalog number representing the processor of the controller component.
        /// </summary>
        /// <value>A <see cref="string"/> alpha numeric code.</value>
        public string ProcessorType { get; set; } = string.Empty;

        /// <summary>
        /// The revision or hardware version of the controller.
        /// </summary>
        /// <value>A <see cref="Core.Revision"/> value representing the major/minor version of the controller</value>
        public Revision? Revision { get; set; } = new();

        /// <summary>
        /// The date/time the current project was created. 
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing the date and time of creation.</value>
        public DateTime ProjectCreationDate { get; set; }

        /// <summary>
        /// The date/time the current project was last modified. 
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing the date and time of modification.</value>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Percentage of available CPU time (10...90) that is assigned to communication.
        /// </summary>
        public string TimeSlice { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify whether to share an unused <see cref="TimeSlice"/> or not.
        /// </summary>
        /// <value>Type a 0 to not share; type a 1 to share.</value>
        public string ShareUnusedTimeSlice { get; set; } = string.Empty;
        
        /// <summary>
        /// Name of the program to be executed upon restart after a power loss.
        /// </summary>
        public string PowerLossProgram { get; set; } = string.Empty;
        
        /// <summary>
        /// Name of the program to be executed when a major fault occurs.
        /// </summary>
        public string MajorFaultProgram { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify whether the SFC executes the current active steps before returning control 
        /// (CurrentActive) or whether the SFC executes all threads until reaching a false transition 
        /// (UntilFalse).
        /// </summary>
        public string SFCExecutionControl { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify whether the SFC restarts at the most recently executed step (MostRecent) or at the 
        /// initial step (InitialStep).
        /// </summary>
        public string SFCRestartPosition { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify how the SFC manages its state on a last scan. Select AutomaticReset, 
        /// ProgrammaticReset, or DontScan.
        /// </summary>
        public string SFCLastScan { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify the devices in the communication path. The communication path ends with the 
        /// controller (\Backplane\1). This is exported only if you select manual configuration of the 
        /// communication path in RSLinx® software.
        /// </summary>
        public string CommPath { get; set; } = string.Empty;

        /// <summary>
        /// Specify the type of communication driver. This is the name of the selected driver in 
        /// RSLinx® software. This is exported only if you select manual configuration of the 
        /// communication driver in RSLinx® software.
        /// </summary>
        public string CommDriver { get; set; } = string.Empty;

        /// <summary>
        /// Specify the serial number of the controller. If a serial number is specified, it is 
        /// imported into the project regardless of the MatchProjectToController setting. Type a 32-bit, 
        /// hexadecimal number with the 16# prefix, such as 16#0012_E2BC.
        /// </summary>
        public string ProjectSN { get; set; } = string.Empty;
        
        public string OnlineSN { get; set; } = string.Empty;

        /// <summary>
        /// Specify whether to be sure that the project matches the controller or not. Type Yes or No.
        /// </summary>
        public string MatchProjectToController { get; set; } = "No";

        /// <summary>
        /// Specify whether the consumed tags in the controller can connect to the producer with an
        /// RPI provided by the producer (true or false)
        /// </summary>
        public bool CanUseRPIFromProducer { get; set; } = false;
        
        /// <summary>
        /// Specify whether to inhibit the automatic update of controller firmware. Type a 0 to not inhibit; type a 1 to inhibit
        /// </summary>
        public string InhibitAutomaticFirmwareUpdate { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify the current project language for a project documentation project.
        /// </summary>
        public string CurrentProjectLanguage { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify the default project language for a project document at on project.
        /// </summary>
        public string DefaultProjectLanguage { get; set; } = string.Empty;
        
        /// <summary>
        /// Specify the controller project language for a project document at on project.
        /// </summary>
        public string ControllerLanguage { get; set; } = string.Empty;
        
        /// <summary>
        ///  Indicates the pass through state of documentation for the project.Type Disabled, Enabled, or EnabledWithAppend
        /// </summary>
        public string PassThroughConfiguration { get; set; } = string.Empty;
        
        /// <summary>
        /// Indicates the download project documentation configuration setting of the project.
        /// </summary>
        public bool DownloadProjectDocumentationAndExtendedProperties { get; set; } = true;
        
        /// <summary>
        /// Indicates the download custom properties configuration setting of the project.
        /// Only applies if the project is already configured to DownloadProjectDocumentation
        /// </summary>
        /// <remarks>
        /// Rockwell Automation® recommends setting this attribute to false only during startup
        /// testing to improve download speeds during commissioning testing. It should be set to true
        /// for the normal operating state of a system.
        /// </remarks>
        public bool DownloadProjectCustomProperties { get; set; } = true;
        
        public bool ReportMinorOverflow { get; set; }
        public bool IOForcesEnabled { get; set; }
        public bool SFCForcesEnabled { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string Verified { get; set; } = string.Empty;
        public bool ControllerEditsExist { get; set; }
        public bool PendingEditsExist { get; set; }
        public bool ForcesExist { get; set; }
        public string EditResource { get; set; } = string.Empty;
        public string Faulted { get; set; } = string.Empty;
        public string GeneralStatus { get; set; } = string.Empty;
        public string IOMapLEDStatus { get; set; } = string.Empty;
        public string KeySwitchPosition { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string ConnectedState { get; set; } = string.Empty;
        public bool IsProjectDirty { get; set; }
        public bool IsProjectDirtyFromAnotherWorkstation { get; set; }
        
        /// <summary>
        /// The EtherNet/IP™ Mode describes the relationship between the CIP™ EtherNet/IP™ ports
        /// and the physical Ethernet ports. The CIP™ EtherNet/IP™ port can be configured as one of two modes:
        /// Dual-IP, Linear/DLR
        /// </summary>
        public string EtherNetIPMode { get; set; } = string.Empty;
        
        
        public string InstructionUsageConfiguredAOIs { get; set; } = string.Empty;
        public string InstructionUsageConfiguredInstructions { get; set; } = string.Empty;
        public string IsPlantPAxTaskingModelEnabled { get; set; } = string.Empty;
        public string AutoDiagsEnabled { get; set; } = string.Empty;
        public string WebServerEnabled { get; set; } = string.Empty;
        public string Use { get; set; } = string.Empty;
    }
}