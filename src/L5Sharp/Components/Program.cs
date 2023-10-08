using System;
using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Program</c> component. Contains the properties that comprise the L5X Program element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Program : LogixComponent
{
    /// <summary>
    /// Creates a new <see cref="Program"/> with default values.
    /// </summary>
    public Program()
    {
        Type = ProgramType.Normal;
        TestEdits = default;
        Disabled = default;
        MainRoutineName = default;
        FaultRoutineName = default;
        UseAsFolder = default;
        Tags = new LogixContainer<Tag>();
        Routines = new LogixContainer<Routine>();
    }

    /// <summary>
    /// Creates a new <see cref="Program"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Program(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Gets the type of the program (Normal, Equipment Phase).
    /// </summary>
    /// <value>A <see cref="Enums.ProgramType"/> enum representing the type of the program.</value>
    public ProgramType Type
    {
        get => GetValue<ProgramType>() ?? ProgramType.Normal;
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
    public string? MainRoutineName
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The name of the routine that serves as the fault routine for the program.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the fault routine for the program.</value>
    public string? FaultRoutineName
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
    public LogixContainer<Tag> Tags
    {
        get => GetContainer<Tag>();
        set => SetContainer(value);
    }

    /// <summary>
    /// The collection of <see cref="Routine"/> objects for the program component.
    /// </summary>
    public LogixContainer<Routine> Routines
    {
        get => GetContainer<Routine>();
        set => SetContainer(value);
    }
}