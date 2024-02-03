using System;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace L5Sharp.Core;

/// <summary>
/// A logix <c>Controller</c> component. Contains the properties that comprise the L5X Controller element.
/// </summary>
/// <remarks>
/// <para>A controller component may or may not contains various properties depending on if the exported L5X file
/// was a full project file or just a component export file. This is indicated in the <see cref="L5X"/> by the property
/// <c>ContainsContext</c>, which if true, means the controller element exists simply to contain other components that
/// are needed by the <c>TargetName</c> for successful re-imports of the content, and therefore will typically only have
/// a name, revision, and processor type.</para>
/// <para>
/// Observe these guidelines when defining a controller:<br/>
///     • All declarations must be ordered in the prescribed syntax.<br/>
///     • The maximum number of tasks vary by the controller type.<br/>
///     • There can be only one continuous task.<br/>
///     • Programs can be scheduled under only one task.<br/>
///     • There can be a maximum of 1000 programs under a task.<br/>
///     • Scheduled programs must be defined.<br/>
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[PublicAPI]
public class Controller : LogixComponent
{
    private const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    /// <summary>
    /// Creates a new <see cref="Controller"/> with default values.
    /// </summary>
    public Controller() : base(new XElement(L5XName.Controller))
    {
        Use = Use.Context;
    }

    /// <summary>
    /// Creates a new <see cref="Controller"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Controller(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The catalog number representing the processor of the controller component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the alpha numeric catalog number.</value>
    public string? ProcessorType
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Percentage of available CPU time (10...90) that is assigned to communication.
    /// </summary>
    public string? TimeSlice
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify whether to share an unused <see cref="TimeSlice"/> or not.
    /// </summary>
    public bool? ShareUnusedTimeSlice
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Name of the program to be executed upon restart after a power loss.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the program.</value>
    public string? PowerLossProgram
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Name of the program to be executed when a major fault occurs.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the program.</value>
    public string? MajorFaultProgram 
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the devices in the communication path. The communication path ends with the 
    /// controller (\Backplane\1). This is exported only if you select manual configuration of the 
    /// communication path in RSLinx® software.
    /// </summary>
    public string? CommPath 
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the type of communication driver. This is the name of the selected driver in 
    /// RSLinx® software. This is exported only if you select manual configuration of the 
    /// communication driver in RSLinx® software.
    /// </summary>
    public string? CommDriver
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The revision or hardware version of the controller.
    /// </summary>
    /// <value>A <see cref="Core.Revision"/> value representing the major/minor version of the controller</value>
    public Revision? Revision
    {
        get
        {
            var major = Element.Attribute(L5XName.MajorRev)?.Value;
            var minor = Element.Attribute(L5XName.MinorRev)?.Value;
            return major is not null && minor is not null ? new Revision($"{major}.{minor}") : default;
        }
        set
        {
            Element.SetAttributeValue(L5XName.MajorRev, value?.Major);
            Element.SetAttributeValue(L5XName.MinorRev, value?.Minor);
        }
    }

    /// <summary>
    /// The date/time the current project was created. 
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing the date and time of creation.</value>
    public DateTime ProjectCreationDate
    {
        get => GetDateTime(DateTimeFormat) ?? default;
        set => SetDateTime(value, DateTimeFormat);
    }

    /// <summary>
    /// The date/time the current project was last modified. 
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing the date and time of modification.</value>
    public DateTime LastModifiedDate
    {
        get => GetDateTime(DateTimeFormat) ?? default;
        set => SetDateTime(value, DateTimeFormat);
    }

    /// <summary>
    /// Specify whether the SFC executes the current active steps before returning control 
    /// (CurrentActive) or whether the SFC executes all threads until reaching a false transition 
    /// (UntilFalse).
    /// </summary>
    public SFCExecutionControl? SFCExecutionControl
    {
        get => GetValue<SFCExecutionControl>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify whether the SFC restarts at the most recently executed step (MostRecent) or at the 
    /// initial step (InitialStep).
    /// </summary>
    public SFCRestartPosition? SFCRestartPosition
    {
        get => GetValue<SFCRestartPosition>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify how the SFC manages its state on a last scan. Select AutomaticReset, 
    /// ProgrammaticReset, or DontScan.
    /// </summary>
    public SFCLastScan? SFCLastScan
    {
        get => GetValue<SFCLastScan>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the serial number of the controller. If a serial number is specified, it is 
    /// imported into the project regardless of the MatchProjectToController setting. Type a 32-bit, 
    /// hexadecimal number with the 16# prefix, such as 16#0012_E2BC.
    /// </summary>
    // ReSharper disable once InconsistentNaming to match logix name.
    public string? ProjectSN
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify whether to be sure that the project matches the controller or not.
    /// </summary>
    public bool? MatchProjectToController
    {
        get => GetValue<string?>() is not null ? GetValue<string?>() == "Yes" : default;
        set => SetValue(value is true ? "Yes" : "No");
    }

    /// <summary>
    /// Specify whether to inhibit the automatic update of controller firmware.
    /// </summary>
    public bool? InhibitAutomaticFirmwareUpdate
    {
        get => GetValue<int?>() is not null ? GetValue<int?>() == 1 : default;
        set => SetValue(value is true ? 1 : 0);
    }

    /// <summary>
    /// Specify the current project language for a project documentation project.
    /// </summary>
    public string? CurrentProjectLanguage
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the default project language for a project document at on project.
    /// </summary>
    public string? DefaultProjectLanguage
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the controller project language for a project document at on project.
    /// </summary>
    public string? ControllerLanguage
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify whether the consumed tags in the controller can connect to the producer with an
    /// RPI provided by the producer (true or false)
    /// </summary>
    public bool? CanUseRPIFromProducer
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates the pass through state of documentation for the project.
    /// </summary>
    /// <value>A <see cref="PassThroughOption"/> representing the configured value.</value>
    public PassThroughOption? PassThroughConfiguration
    {
        get => GetValue<PassThroughOption>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates the download project documentation configuration setting of the project.
    /// </summary>
    public bool? DownloadProjectDocumentationAndExtendedProperties
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates the download custom properties configuration setting of the project.
    /// Only applies if the project is already configured to DownloadProjectDocumentation
    /// </summary>
    /// <remarks>
    /// Rockwell Automation® recommends setting this attribute to false only during startup
    /// testing to improve download speeds during commissioning testing. It should be set to true
    /// for the normal operating state of a system.
    /// </remarks>
    public bool? DownloadCustomProperties
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The EtherNet/IP™ Mode describes the relationship between the CIP™ EtherNet/IP™ ports
    /// and the physical Ethernet ports. The CIP™ EtherNet/IP™ port can be configured as one of two modes:
    /// Dual-IP, Linear/DLR
    /// </summary>
    public string? EtherNetIPMode
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The <see cref="Core.RedundancyInfo"/> object that specifies the redundancy configuration of the controller.
    /// </summary>
    public RedundancyInfo? RedundancyInfo
    {
        get => GetComplex<RedundancyInfo>();
        set => SetComplex(value);
    }
    
    /// <summary>
    /// The <see cref="Core.Security"/> object that specifies the security configuration of the controller.
    /// </summary>
    public Security? Security
    {
        get => GetComplex<Security>();
        set => SetComplex(value);
    }
    
    /// <summary>
    /// The <see cref="Core.SafetyInfo"/> object that specifies the safety configuration of the controller.
    /// </summary>
    public SafetyInfo? SafetyInfo
    {
        get => GetComplex<SafetyInfo>();
        set => SetComplex(value);
    }
}