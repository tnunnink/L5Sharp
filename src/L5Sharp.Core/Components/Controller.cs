using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// A logix <c>Controller</c> component. Contains the properties that comprise the L5X Controller element.
/// </summary>
/// <remarks>
/// <para>A controller component may or may not contain various properties depending on if the exported L5X file
/// was a full project file or just a component export file. This is indicated in the <see cref="L5X"/> by the property
/// <c>ContainsContext</c>, which is true, means the controller element exists simply to contain other components that
/// are needed by the <c>TargetName</c> for successful re-imports of the content, and therefore will typically only have
/// a name, revision, and processor type.</para>
/// <para>
/// Observe these guidelines when defining a controller:<br/>
///     • All declarations must be ordered in the prescribed syntax.<br/>
///     • The maximum number of tasks varies by the controller type.<br/>
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
public sealed class Controller : LogixElement
{
    private const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.RedundancyInfo,
        L5XName.Security,
        L5XName.SafetyInfo,
        L5XName.DataTypes,
        L5XName.Modules,
        L5XName.AddOnInstructionDefinitions,
        L5XName.Tags,
        L5XName.Programs,
        L5XName.Tasks,
        L5XName.ParameterConnections,
        L5XName.CST,
        L5XName.WallClockTime,
        L5XName.Trends,
        L5XName.DataLogs,
        L5XName.QuickWatchLists,
        L5XName.TimeSynchronize,
        L5XName.EthernetPorts
    ];

    /// <summary>
    /// Creates a new <see cref="Controller"/> with default values.
    /// </summary>
    public Controller() : base(L5XName.Controller)
    {
        Revision = new Revision();
        ProjectCreationDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;

        DataTypes = [];
        Instructions = [];
        Modules = [];
        Tags = [];
        Programs = [];
        Tasks = [];
        ParameterConnections = [];
        Trends = [];
        WatchLists = [];
    }

    /// <summary>
    /// Creates a new <see cref="Controller"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Controller(XElement element) : base(element)
    {
        EnsureContainersExist();
    }

    /// <summary>
    /// Created a new <see cref="Controller"/> initialized with the provided name, processor type, and revision.
    /// </summary>
    /// <param name="name">The name of the controller.</param>
    /// <param name="processor">The catalog number specifying the processor type.</param>
    /// <param name="revision">AThe <see cref="Core.Revision"/> of the controller.</param>
    public Controller(string name, string processor, Revision revision) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        ProcessorType = processor;
        Revision = revision;
    }

    /// <summary>
    /// The <see cref="Use"/> of the component within the L5X file.
    /// </summary>
    /// <remarks>
    /// Typically used when exporting individual components (DataType, AoiBlock, Module) to indicate whether the component
    /// is the target of the L5X content or exists solely as a context or dependency of the target component. When
    /// saving a project as an L5X, the top-level controller component is the target, and all other components will
    /// not have this property. 
    /// </remarks>
    public Use? Use
    {
        get => GetValue<Use>();
        set => SetValue(value);
    }

    /// <summary>
    /// The unique name of the component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the component name.</value>
    /// <remarks>
    /// The name servers as a unique identifier for various types of components.
    /// In most cases, the component name should satisfy Logix naming constraints of alphanumeric and
    /// underscore ('_') characters, start with a letter, and be between 1 and 40 characters.
    /// Validation is not performed by this library, so importing components with invalid names may fail.
    /// </remarks>
    public string Name
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public string? Description
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    /// <summary>
    /// The catalog number representing the processor of the controller component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the alphanumeric catalog number.</value>
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
        get => GetValue<bool?>();
        set => SetBit(value);
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
            var major = Element.Attribute(L5XName.MajorRev)?.Value.Parse<ushort>();
            var minor = Element.Attribute(L5XName.MinorRev)?.Value.Parse<ushort>();
            return major.HasValue && minor.HasValue ? new Revision(major.Value, minor.Value) : null;
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
        get => GetDateTime(DateTimeFormat);
        set => SetDateTime(value, DateTimeFormat);
    }

    /// <summary>
    /// The date/time the current project was last modified. 
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing the date and time of modification.</value>
    public DateTime LastModifiedDate
    {
        get => GetDateTime(DateTimeFormat);
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
        get => GetValue<string>() is not null ? GetValue<string>() == "Yes" : null;
        set => SetValue(value is true ? "Yes" : "No");
    }

    /// <summary>
    /// Specify whether to inhibit the automatic update of controller firmware.
    /// </summary>
    public bool? InhibitAutomaticFirmwareUpdate
    {
        get => GetValue<int?>() is not null ? GetValue<int?>() == 1 : null;
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
    /// Specify the default project language for a project document at on a project.
    /// </summary>
    public string? DefaultProjectLanguage
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the controller project language for a project document at on a project.
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
        get => GetValue<bool?>();
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
        get => GetValue<bool?>();
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
    public bool? DownloadProjectCustomProperties
    {
        get => GetValue<bool?>();
        set => SetValue(value);
    }

    /// <summary>
    /// The EtherNet/IP™ Mode describes the relationship between the CIP™ EtherNet/IP™ ports
    /// and the physical Ethernet ports. The CIP™ EtherNet/IP™ port can be configured as one of two modes:
    /// Dual-IP, Linear/DLR
    /// </summary>
    public string? EtherNetIPMode
    {
        get => GetValue<string?>();
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

    /// <summary>
    /// The collection of <see cref="DataType"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<DataType> DataTypes
    {
        get => GetContainer<DataType>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="AddOnInstruction"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<AddOnInstruction> Instructions
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        get => GetContainer<AddOnInstruction>(L5XName.AddOnInstructionDefinitions);
        // ReSharper disable once ExplicitCallerInfoArgument
        private set => SetContainer(value, L5XName.AddOnInstructionDefinitions);
    }

    /// <summary>
    /// The collection of <see cref="Module"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<Module> Modules
    {
        get => GetContainer<Module>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="Tag"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<Tag> Tags
    {
        get => GetContainer<Tag>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="Program"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<Program> Programs
    {
        get => GetContainer<Program>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="Task"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<Task> Tasks
    {
        get => GetContainer<Task>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="ParameterConnection"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<ParameterConnection> ParameterConnections
    {
        get => GetContainer<ParameterConnection>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="Trend"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<Trend> Trends
    {
        get => GetContainer<Trend>();
        private set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="WatchList"/> objects defined for the controller component.
    /// </summary>
    public LogixContainer<WatchList> WatchLists
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        get => GetContainer<WatchList>(L5XName.QuickWatchLists);
        // ReSharper disable once ExplicitCallerInfoArgument
        private set => SetContainer(value, L5XName.QuickWatchLists);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="action"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Import(string fileName, Action<IImportPlanBuilder>? action = null)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("FileName can not be null or empty.", nameof(fileName));

        var source = L5X.Load(fileName);

        var builder = new ImportPlanBuilder();
        action?.Invoke(builder);
        var plan = builder.Build();
        plan.Execute(L5X, source);
    }

    #region Internal

    /// <summary>
    /// Will ensure that all known component container elements are created and ordered according to the defined L5X
    /// element order. This will ensure that no container element returns null and which would cause exceptions.
    /// If the containers cod unused (are empty), we will remove them upon save.
    /// </summary>
    private void EnsureContainersExist()
    {
        foreach (var container in ElementOrder.Where(e => e.IsContainerName()))
        {
            if (Element.Element(container) is not null) continue;
            Element.Add(new XElement(container));
        }

        EnsureOrder();
    }

    #endregion
}