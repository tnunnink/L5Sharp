using System;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Common;

/// <summary>
/// Represents a reference to a component within a Logix project. This could be a code reference or a reference
/// from another component. This class is meant to provide a uniform set of information for all types of references.
/// </summary>
[PublicAPI]
public class CrossReference
{
    private readonly XElement _element;

    /// <summary>
    /// Creates a new <see cref="CrossReference"/> with a referencing element, component name and type.
    /// </summary>
    /// <param name="element">The referencing <see cref="XElement"/> object.</param>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <exception cref="ArgumentNullException">Any provided parameter is <c>null</c>.</exception>
    public CrossReference(XElement element, string type, string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        ComponentType = type;
        ComponentName = name;
        _element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// Creates a new <see cref="CrossReference"/> with a referencing element, component name and type.
    /// </summary>
    /// <param name="element">The referencing <see cref="XElement"/> object.</param>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="instruction"></param>
    /// <exception cref="ArgumentNullException">Any provided parameter is <c>null</c>.</exception>
    public CrossReference(XElement element, string type, string name, Instruction instruction)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(type)) throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        ComponentType = type;
        ComponentName = name;
        Instruction = instruction;
        _element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// The corresponding <see cref="Common.ComponentKey"/> of the reference, indicating both the component type and name
    /// this element is in reference to.
    /// </summary>
    public ComponentKey ComponentKey => new(ComponentType, ComponentName);

    /// <summary>
    /// The type of the component the element is in reference to.
    /// </summary>
    /// <value>A <see cref="string"/> indicating the type of the component.</value>
    public string ComponentType { get; }

    /// <summary>
    /// The name of the component the element is in reference to.
    /// </summary>
    /// <value>A <see cref="string"/> indicating the name of the component.</value>
    public string ComponentName { get; }

    /// <summary>
    /// The specific instruction value the element is in reference to. 
    /// </summary>
    /// <value>An <see cref="Common.Instruction"/> object if found; Otherwise, <c>null</c>.</value>
    private Instruction? Instruction { get; }

    /// <summary>
    /// The referencing <see cref="XElement"/> object
    /// </summary>
    public LogixElement Reference => GetReference();

    /// <summary>
    /// The location of the reference if this is a <see cref="LogixCode"/> reference type.
    /// </summary>
    /// <value>A <see cref="string"/> containing the <c>Rung</c>, <c>Line</c>, or <c>DiagramBlock</c> location for
    /// the reference. If this reference has no relevant location, an empty string is returned.
    /// </value>
    public string ReferenceId => GetIdentifier() ?? string.Empty;

    /// <summary>
    /// The type of the element referencing the component for this reference.
    /// </summary>
    public string ReferenceType => GetReference().L5XType;

    /// <summary>
    /// The <see cref="Enums.Scope"/> type that the reference is contained within.
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> indicating scope of the reference.</value>
    public Scope Scope => Scope.ScopeType(_element);

    /// <summary>
    /// The name of the scoped program, instruction, or controller that the reference is contained within.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of program, controller, or instruction the reference
    /// is contained within.
    /// </value>
    public string Container => Scope.ScopeName(_element);

    /// <summary>
    /// The name of the <c>Routine</c> that the reference is contained within, it is a <see cref="LogixCode"/>
    /// type element.
    /// </summary>
    /// <value>A <see cref="string"/> representing the containing routine if found; Otherwise, an empty string.</value>
    public string RoutineName => _element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <summary>
    /// The name of the <c>Task</c> that the reference is contained within if applicable.
    /// </summary>
    /// <value>A <see cref="string"/> representing the containing task if found; Otherwise, an empty string.</value>
    public string TaskName => GetTaskName() ?? string.Empty;

    /// <summary>
    /// Determines if this <see cref="CrossReference"/> is the same as another <see cref="CrossReference"/>.
    /// </summary>
    /// <param name="other">The other logix reference to compare to.</param>
    /// <returns><c>true</c> if the provided reference has the same scope, routine, location, and reference type,
    /// indicating that they are referring to the same code or component element in the L5X structure;
    /// Otherwise, <c>false</c>.
    /// </returns>
    public bool IsSame(CrossReference other)
    {
        return ComponentKey == other.ComponentKey &&
               Scope == other.Scope &&
               Container.IsEquivalent(other.Container) &&
               RoutineName.IsEquivalent(other.RoutineName) &&
               ReferenceId.IsEquivalent(other.ReferenceId) &&
               ReferenceType.IsEquivalent(other.ReferenceType);
    }

    /// <summary>
    /// Determines the string that identifies this reference element within the context or scope of the L5X.
    /// </summary>
    private string? GetIdentifier()
    {
        return GetReference() switch
        {
            LogixComponent component => component.Name,
            LogixCode code => code.Identifier,
            DiagramBlock diagram => $"{diagram.Location} ",
            _ => null
        };
    }

    /// <summary>
    /// Deserialized the element into the appropriate <see cref="LogixElement"/> type.
    /// </summary>
    private LogixElement GetReference() => _element.Deserialize();

    /// <summary>
    /// Uses the underlying element hierarchy to determine the name of the <c>Task</c> that the reference is contained.
    /// This would only apply to code or scoped tag reference types. Controller scoped items are not contained
    /// within a task
    /// </summary>
    private string? GetTaskName()
    {
        return _element
            .Ancestors(L5XName.RSLogix5000Content)
            .Descendants(L5XName.Task)
            .FirstOrDefault(e => e.Descendants(L5XName.ScheduledProgram)
                .Any(p => p.Attribute(L5XName.Name)?.Value == Container))
            ?.LogixName();
    }
}