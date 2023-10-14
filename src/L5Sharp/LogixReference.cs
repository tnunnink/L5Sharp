using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Elements;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// Represents a reference to a component within a Logix project. This could be a code reference or a reference
/// from another component. This class is meant to provide a uniform set of information for all types of references.
/// </summary>
public class LogixReference
{
    private readonly XElement _element;
    private readonly string _name;
    private readonly string _type;

    /// <summary>
    /// Creates a new <see cref="LogixReference"/> with a referencing element, component name and type.
    /// </summary>
    /// <param name="element">The referencing <see cref="XElement"/> object.</param>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <exception cref="ArgumentNullException">Any provided parameter is <c>null</c>.</exception>
    public LogixReference(XElement element, string name, string type)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(type)) throw new ArgumentException("Value cannot be null or empty.", nameof(type));
        _element = element ?? throw new ArgumentNullException(nameof(element));
        _name = name;
        _type = type;
    }

    /// <summary>
    /// 
    /// </summary>
    public ComponentKey Key => new(_type, _name);

    /// <summary>
    /// The referencing <see cref="XElement"/> object
    /// </summary>
    public LogixElement Reference => GetReference();

    /// <summary>
    /// The location of the reference if this is a <see cref="LogixCode"/> reference type.
    /// </summary>
    /// <value>A <see cref="string"/> containing the <c>Rung</c>, <c>Line</c>, or <c>DiagramElement</c> location for
    /// the reference. If this reference has no relevant location, an empty string is returned.
    /// </value>
    public string ReferenceId => GetIdentifier() ?? string.Empty;
    
    /// <summary>
    /// The type of the element referencing the component for this reference.
    /// </summary>
    public string ReferenceType => GetReference().GetType().L5XType();

    /// <summary>
    /// The <see cref="Scope"/> type that the reference is contained within.
    /// </summary>
    /// <value>A <see cref="Scope"/> indicating scope of the reference.</value>
    public Scope ScopeType => Scope.ScopeType(_element);

    /// <summary>
    /// The name of the scoped program, instruction, or controller that the reference is contained within.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of program, controller, or instruction the reference
    /// is contained within.
    /// </value>
    public string ScopeName => Scope.ScopeName(_element);

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
    /// Determines if this <see cref="LogixReference"/> is the same as another <see cref="LogixReference"/>.
    /// </summary>
    /// <param name="other">The other logix reference to compare to.</param>
    /// <returns><c>true</c> if the provided reference has the same scope, routine, location, and reference type,
    /// indicating that they are referring to the same code or component element in the L5X structure;
    /// Otherwise, <c>false</c>.
    /// </returns>
    public bool IsSame(LogixReference other)
    {
        return ScopeType == other.ScopeType &&
               ScopeName.IsSame(other.ScopeName) &&
               RoutineName.IsSame(other.RoutineName) &&
               ReferenceId.IsSame(other.ReferenceId) &&
               ReferenceType.IsSame(other.ReferenceType);
    }

    /// <summary>
    /// Determines the string that identifies this reference element within the context or scope of the L5X.
    /// </summary>
    private string? GetIdentifier()
    {
        return GetReference() switch
        {
            LogixComponent component => component.Name,
            LogixCode code => code.Location,
            DiagramElement diagram => $"{diagram.Location} ",
            _ => null
        };
    }

    /// <summary>
    /// Deserialized the element into the appropriate <see cref="LogixElement"/> type.
    /// </summary>
    private LogixElement GetReference() => LogixSerializer.Deserialize(_element);

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
                .Any(p => p.Attribute(L5XName.Name)?.Value == ScopeName))
            ?.LogixName();
    }
}