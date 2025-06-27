﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// A logix <c>Program</c> component. Contains the properties that comprise the L5X Program element.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Program : LogixComponent<Program>
{
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.Tags,
        L5XName.Routines,
        L5XName.ChildPrograms
    ];

    /// <summary>
    /// Creates a new <see cref="Program"/> with default values.
    /// </summary>
    public Program() : base(L5XName.Program)
    {
        Type = ProgramType.Normal;
        TestEdits = false;
        Disabled = false;
        UseAsFolder = false;
        Tags = [];
        Routines = [];
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
    /// Creates a new <see cref="Program"/> initialized with the provided name and optional type.
    /// </summary>
    /// <param name="name">The name of the Program.</param>
    /// <param name="type">The <see cref="ProgramType"/> of the Program.</param>
    public Program(string name, ProgramType? type = null) : this()
    {
        Element.SetAttributeValue(L5XName.Name, name);
        Type = type ?? ProgramType.Normal;
    }

    /// <summary>
    /// Gets the type of the program (Normal, Equipment Phase).
    /// </summary>
    /// <value>A <see cref="ProgramType"/> enum representing the type of the program.</value>
    public ProgramType Type
    {
        get => GetValue<ProgramType>() ?? ProgramType.Normal;
        set => SetValue(value);
    }

    /// <summary>
    /// The <see cref="ComponentClass"/> value indicating whether this component is a standard or safety type component.
    /// </summary>
    /// <value>A <see cref="Core.ComponentClass"/> option representing class of the component.</value>
    /// <remarks>
    /// Specify the class of the program. This attribute applies only to safety controller projects.
    /// Do not use this attribute if the program is an Equipment Phase program.
    /// </remarks>
    public ComponentClass? Class
    {
        get => GetValue<ComponentClass>();
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
    /// The name of the routine that serves as the entry point for the program (i.e., main routine).
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

    /// <summary>
    /// The collection of program names that are children of this <see cref="Program"/> component.
    /// This defines the structure of the program tree in the logical organizer.
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> containing the string program names.</value>
    /// <remarks>
    /// <para>
    /// This member just returns the read-only list of child program names. To modify the list, use the local
    /// <see cref="AddChild"/> and <see cref="RemoveChild"/> methods. This list is what is serialized and defines the collection
    /// of child programs for a given program in the logical organizer.
    /// </para>
    /// <para>
    /// To get access to the actual child
    /// programs, use <see cref="Programs"/>, which is a built-in helper that uses the parent <c>L5X</c>
    /// to retrieve the child program components.
    /// </para>
    /// </remarks>
    public IEnumerable<string> Children =>
        Element.Descendants(L5XName.ChildProgram).Select(e => e.LogixName()).ToList();

    /// <summary>
    /// Gets a collection of <c>Program</c> components that are children of this <see cref="Program"/> component. 
    /// </summary>
    /// <value>A <see cref="IEnumerable{T}"/> of <see cref="Program"/> component elements.</value>
    /// <remarks>
    /// This is a helper to retrieve the other program component objects as children of this <c>Program</c>.
    /// This allows the caller to traver down the logical hierarchy of programs. This requires an attached L5X as
    /// it reaches back up the document tree and back down to find the child programs. If this component is not
    /// attached to an L5X or as no <see cref="Children"/> configured, then this will return an empty collection.
    /// </remarks>
    public IEnumerable<Program> Programs =>
        L5X?.Programs.Where(p => Children.Any(c => c == p.Name)) ?? [];

    /// <summary>
    /// Gets the parent <see cref="Core.Program"/> in which this program is contained. If this program has no container,
    /// then this property returns <c>null</c>. 
    /// </summary>
    /// <remarks>
    /// This is a navigation helper to allow easily retrieving the parent program container for a given program component.
    /// This requires a scoped/attached L5X as it traverses the L5X document tree to find the target component. 
    /// </remarks>
    public Program? Parent => L5X?.Programs.FirstOrDefault(p => p.Children.Contains(Name));

    /// <summary>
    /// Finds the <see cref="Core.Task"/> in which this <see cref="Program"/> is scheduled.
    /// </summary>
    /// <value>If this component is attached and is scheduled to a defined <c>Task</c>, then the
    /// <see cref="Core.Task"/> component instance, Otherwise, null.</value>
    /// <remarks>
    /// This is a helper for retrieving the parent <c>Task</c> for this program.
    /// This requires a scoped/attached L5X as it traverses the L5X document tree to find the target component.
    /// </remarks>
    public Task? Task => L5X?.Tasks.FirstOrDefault(t => t.Scheduled.Any(p => p.IsEquivalent(Name)));

    /// <inheritdoc />
    public override IEnumerable<LogixComponent> Dependencies()
    {
        if (L5X is null) return [];

        return Tags.SelectMany(t => t.Dependencies())
            .Concat(Routines.SelectMany(r => r.Dependencies()))
            .Distinct(c => c.Scope.Path);
    }

    /// <summary>
    /// Adds the specified program name as a child of this <see cref="Program"/> component.
    /// </summary>
    /// <param name="programName">The name of the program to add as a child.</param>
    /// <exception cref="ArgumentException"><c>programName</c> is null or empty.</exception>
    public void AddChild(string programName)
    {
        if (string.IsNullOrEmpty(programName))
            throw new ArgumentException("Can not remove program with null or empty name.", nameof(programName));

        var element = new XElement(L5XName.ChildProgram, new XAttribute(L5XName.Name, programName));

        if (Element.Element(L5XName.ChildProgram) is null)
            Element.Add(new XElement(L5XName.ChildProgram));

        Element.Element(L5XName.ChildProgram)!.Add(element);
    }

    /// <summary>
    /// Removes the program with the specified name from the children collection of this <see cref="Program"/>
    /// component.
    /// </summary>
    /// <param name="programName">The name of the child program to remove.</param>
    /// <exception cref="ArgumentException"><c>programName</c> is null or empty.</exception>
    public void RemoveChild(string programName)
    {
        if (string.IsNullOrEmpty(programName))
            throw new ArgumentException("Can not remove program with null or empty name.", nameof(programName));

        Element.Element(L5XName.ChildPrograms)?.Elements().Where(e => e.LogixName().IsEquivalent(programName)).Remove();
    }
}