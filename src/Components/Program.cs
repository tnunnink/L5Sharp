using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Program</c> component. Contains the properties that comprise the L5X Program element. This type is a
/// container type. It does not include tags and routines. To access these sub-components, use the corresponding
/// component collection API on the <see cref="LogixContent"/> class.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Program : LogixComponent<Program>
{
    /// <inheritdoc />
    public Program()
    {
        Type = ProgramType.Normal;
    }

    /// <inheritdoc />
    public Program(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the type of the program (Normal, Equipment Phase).
    /// </summary>
    /// <value>A <see cref="Enums.ProgramType"/> enum representing the type of the program.</value>
    public ProgramType Type
    {
        get => GetValue<ProgramType>();
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether the program has current test edits pending.
    /// </summary>
    /// <value>>A <see cref="bool"/>; <c>true</c>if the program has test edits; otherwise <c>false</c>.</value>
    public bool TestEdits
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The value indicating whether the program is disabled (or inhibited).
    /// </summary>
    /// <value>A <see cref="bool"/>; <c>true</c> if the program is disabled; otherwise <c>false</c>.</value>
    public bool Disabled
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The name of the routine that serves as the entry point for the program (i.e. main routine).
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the main routine for the program.</value>
    public string MainRoutineName
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The name of the routine that serves as the fault routine for the program.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the fault routine for the program.</value>
    public string FaultRoutineName
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// A flag indicating whether the program is used as a folder or container for other programs,
    /// as opposed to a container of tags and logix.
    /// </summary>
    /// <value>A <see cref="bool"/>; <c>true</c> if the program is a folder; otherwise, <c>false</c>.</value>
    public bool UseAsFolder
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The collection of <see cref="Tag"/> objects for the program component.
    /// </summary>
    public ILogixCollection<Tag> Tags
    {
        get => GetCollection<Tag>();
        set => SetCollection(value);
    }

    /// <summary>
    /// The collection of <see cref="Routine"/> objects for the program component.
    /// </summary>
    public ILogixCollection<Routine> Routines
    {
        get => GetCollection<Routine>();
        set => SetCollection(value);
    }

    /// <inheritdoc />
    protected override XElement DefaultElement()
    {
        var element = base.DefaultElement();
        element.Add(new XElement(L5XName.Tags));
        element.Add(new XElement(L5XName.Routines));
        return element;
    }
}