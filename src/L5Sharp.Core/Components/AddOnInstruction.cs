using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// A logix <c>AddOnInstruction</c> component. Contains the properties that comprise the L5X
/// AddOnInstructionDefinition element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.AddOnInstructionDefinition)]
public class AddOnInstruction : LogixComponent
{
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

    /// <summary>
    /// The order of child elements in an AddOnInstructionDefinition. This is required because the use could add
    /// elements in any order and Logix requires a specific order to be imported successfully. 
    /// </summary>
    private static readonly List<string> ElementOrder =
    [
        L5XName.Description,
        L5XName.RevisionNote,
        L5XName.SignatureHistory,
        L5XName.AdditionalHelpText,
        L5XName.Parameters,
        L5XName.LocalTags,
        L5XName.Routines,
        L5XName.Dependencies,
    ];

    /// <summary>
    /// Creates a new <see cref="AddOnInstruction"/> with default values.
    /// </summary>
    public AddOnInstruction() : base(L5XName.AddOnInstructionDefinition)
    {
        Revision = new Revision();
        ExecutePreScan = false;
        ExecutePostScan = false;
        ExecuteEnableInFalse = false;
        CreatedDate = DateTime.Now;
        CreatedBy = Environment.UserName;
        EditedDate = DateTime.Now;
        EditedBy = Environment.UserName;
        Parameters = [EnableIn(), EnableOut()];
        LocalTags = [];
        Routines = [new Routine("Logic")];
    }

    /// <summary>
    /// Creates a new <see cref="AddOnInstruction"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public AddOnInstruction(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Creates a new <see cref="AddOnInstruction"/> initialized with the provided name and option type and revision.
    /// </summary>
    /// <param name="name">The name of the instruction.</param>
    /// <param name="type">The <see cref="RoutineType"/> for the logic of the instruction.</param>
    /// <param name="revision">The optional revision of the instruction.</param>
    public AddOnInstruction(string name, RoutineType? type = default, Revision? revision = default) :
        base(L5XName.AddOnInstructionDefinition)
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Revision = revision ?? new Revision();
        ExecutePreScan = false;
        ExecutePostScan = false;
        ExecuteEnableInFalse = false;
        CreatedDate = DateTime.Now;
        CreatedBy = Environment.UserName;
        EditedDate = DateTime.Now;
        EditedBy = Environment.UserName;
        Parameters = [EnableIn(), EnableOut()];
        LocalTags = [];
        Routines = [new Routine("Logic", type)];
    }

    /// <summary>
    /// The <see cref="ComponentClass"/> value indicating whether this component is a standard or safety type component.
    /// </summary>
    /// <value>A <see cref="Core.ComponentClass"/> option representing class of the component.</value>
    /// <remarks>
    /// Specify the class of the Add-On Instruction. This attribute applies only to safety controller projects.
    /// </remarks>
    public ComponentClass? Class
    {
        get => GetValue<ComponentClass>();
        set => SetValue(value);
    }

    /// <summary>
    /// The revision of the instruction.
    /// </summary>
    /// <value>A <see cref="Core.Revision"/> representing the version of the instruction.</value>
    /// <remarks>
    /// Specify the revision of the Add-On Instruction, in the form of MajorRevision.MinorRevision.
    /// Each revision number can be 1...65,535.
    /// If there is no period, the number is treated as a major revision only
    /// </remarks>
    public Revision? Revision
    {
        get => GetValue<Revision>();
        set => SetValue(value);
    }

    /// <summary>
    /// Additional text indicating or identifying the revision of the instruction.
    /// </summary>
    /// <value>A <see cref="string"/> containing text of the revision extension.</value>
    public string? RevisionExtension
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Additional text describing release information or changes with the current revision(s).
    /// </summary>
    /// <value>A <see cref="string"/> containing the text of the revision note.</value>
    public string? RevisionNote
    {
        get => GetProperty<string>();
        set => SetPropertyAndOrder(value, ElementOrder);
    }

    /// <summary>
    /// The vendor or creator of the instruction.
    /// </summary>
    /// <value>A <see cref="string"/> value representing the name of the vendor.</value>
    public string? Vendor
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates that the instruction has and executes a pre scan routine.
    /// </summary>
    /// <value><c>true</c> if the instruction executes a pre scan routine; otherwise, <c>false</c>.</value>
    public bool ExecutePreScan
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates that the instruction has and executes a post scan routine.
    /// </summary>
    /// <value><c>true</c> if the instruction executes a post scan routine; otherwise, <c>false</c>.</value>
    public bool ExecutePostScan
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Indicates that the instruction has and executes a enable in false routine.
    /// </summary>
    /// <value>A <see cref="bool"/> - <c>true</c> if the instruction executes a enable in false routine; otherwise, <c>false</c>.</value>
    public bool ExecuteEnableInFalse
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The date and time that the instruction was created.
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing the creation date and time.</value>
    public DateTime? CreatedDate
    {
        get => GetDateTime(DateFormat);
        set => SetDateTime(value, DateFormat);
    }

    /// <summary>
    /// The name of the user that created the instruction.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the user.</value>
    public string? CreatedBy
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The date and time that the instruction was last edited.
    /// </summary>
    /// <value>A <see cref="DateTime"/> representing the edit date and time.</value>
    public DateTime? EditedDate
    {
        get => GetDateTime(DateFormat);
        set => SetDateTime(value, DateFormat);
    }

    /// <summary>
    /// The name of the user that last edited the instruction.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the user.</value>
    public string? EditedBy
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Specify the revision of the application last used to edit the Add-On Instruction.
    /// The default is the currently open version of the application.
    /// </summary>
    /// <value>A <see cref="Core.Revision"/> representing the version of the instruction.</value>
    public Revision? SoftwareRevision
    {
        get => GetValue<Revision>();
        set => SetValue(value);
    }

    /// <summary>
    /// The help text specific to the Add-On Instruction.
    /// </summary>
    /// <value>A <see cref="string"/> containing the help text.</value>
    public string? AdditionalHelpText
    {
        get => GetProperty<string>();
        set => SetPropertyAndOrder(value, ElementOrder);
    }

    /// <summary>
    /// Indicates whether the Add-On Instruction is protected with license-based Source Protection and locked
    /// </summary>
    /// <value><c>true</c> if the instruction is encrypted; otherwise, <c>false</c>.</value>
    public bool? IsEncrypted
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of <see cref="Parameter"/> that make up the structure of the instruction component.
    /// </summary>
    public LogixContainer<Parameter> Parameters
    {
        get => GetContainer<Parameter>();
        set => SetContainer(value);
    }

    /// <summary>
    /// The collection of local <see cref="Tag"/> objects used within the AoiBlock logic.
    /// </summary>
    public LogixContainer<LocalTag> LocalTags
    {
        get => GetContainer<LocalTag>();
        set => SetContainer(value);
    }

    /// <summary>
    /// The collection of local <see cref="Routine"/> objects that contain the logic for the instruction.
    /// </summary>
    public LogixContainer<Routine> Routines
    {
        get => GetContainer<Routine>();
        set => SetContainer(value);
    }

    /// <summary>
    /// Gets the required Logic <see cref="Routine"/> containing the code for the instruction. 
    /// </summary>
    /// <remarks>
    /// This is an extension to make accessing the code for the instruction easier. All instructions have at
    /// least a single routine called Logic which contains the code for the instruction.
    /// </remarks>
    public Routine Logic => Routines.SingleOrDefault(r => r.Name == nameof(Logic)) ??
                            throw new InvalidOperationException("No Logic routine is defined for AOI.");

    /// <summary>
    /// Returns the AoiBlock instruction logic with the parameters tag names replaced with the argument tag names of the
    /// provided instruction instance.
    /// </summary>
    /// <param name="instruction">The instruction instance for which to generate the underlying logic.</param>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> representing all the instruction's
    /// logic, with each instruction parameter tag name replaced with the arguments from the provided text.
    /// </returns>
    /// <remarks>
    /// This is helpful when trying to perform deep analysis on logic. By "flattening" the logic we can
    /// reason or evaluate it as if it was written in line. Currently only supports <see cref="Rung"/>
    /// content or code type.
    /// </remarks>
    public IEnumerable<NeutralText> LogicFor(Instruction instruction)
    {
        if (instruction is null)
            throw new ArgumentNullException(nameof(instruction));

        // All instructions primary logic is contained in the routine named 'Logic'
        var logic = Routines.FirstOrDefault(r => r.Name == "Logic");

        var rungs = logic?.Content<Rung>();
        if (rungs is null) return Enumerable.Empty<NeutralText>();

        //Skip first operand as it is always the AoiBlock tag, which does not have corresponding parameter within the logic.
        var arguments = instruction.Arguments.Select(a => a.ToString()).Skip(1).ToList();

        //Only required parameters are part of the instruction signature
        var parameters = Parameters.Where(p => p.Required is true).Select(p => p.Name).ToList();

        //Generate a mapping of the provided instructions arguments to instruction parameters.
        var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

        //Replace all parameter names with argument names in the instruction logic text, and return the results.
        return rungs.Select(r => r.Text)
            .Select(t => mapping.Aggregate(t, (current, pair) =>
            {
                if (!TagName.IsTag(pair.Argument)) return current;
                var replace = $@"(?<=[^.]){pair.Parameter}\b";
                return Regex.Replace(current, replace, pair.Argument.ToString());
            }))
            .ToList();
    }

    /// <summary>
    /// Returns the default built in EnableIn parameter.
    /// </summary>
    private static Parameter EnableIn()
    {
        return new Parameter
        {
            Name = "EnableIn",
            Description = "Enable Input - System Defined Parameter",
            DataType = "BOOL",
            TagType = TagType.Base,
            Usage = TagUsage.Input,
            Radix = Radix.Decimal,
            Required = false,
            Visible = false,
            ExternalAccess = ExternalAccess.ReadOnly
        };
    }

    /// <summary>
    /// Returns the default built in EnableOut parameter.
    /// </summary>
    private static Parameter EnableOut()
    {
        return new Parameter
        {
            Name = "EnableOut",
            Description = "Enable Output - System Defined Parameter",
            DataType = "BOOL",
            TagType = TagType.Base,
            Usage = TagUsage.Output,
            Radix = Radix.Decimal,
            Required = false,
            Visible = false,
            ExternalAccess = ExternalAccess.ReadOnly
        };
    }
}