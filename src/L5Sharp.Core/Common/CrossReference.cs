using System;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

/// <summary>
/// Represents a reference to a component within a Logix project. This could be a code reference or a reference
/// from another component. This class is meant to provide a uniform set of information for all types of references,
/// however, code references have some additional information that is useful for further identifying the target or
/// location of the reference.
/// </summary>
public class CrossReference
{
    private readonly XElement _element;

    /// <summary>
    /// Creates a new <see cref="CrossReference"/> with a referencing element, component name and type, and optional instruction data.
    /// </summary>
    /// <param name="element">The referencing <see cref="XElement"/> object.</param>
    /// <param name="type">The type of the component that is being referenced.</param>
    /// <param name="reference">The name of the component that is being referenced.</param>
    /// <param name="instruction">The optional instruction name/key for the reference.
    /// This is intended for code references as opposed to component references. Will be null if not applicable.</param>
    /// <param name="operand"></param>
    /// <exception cref="ArgumentNullException"><c>element</c>, <c>type</c>, or <c>name</c> is <c>null</c>.</exception>
    public CrossReference(XElement element, string type, string reference, string? instruction = null,
        string? operand = null)
    {
        _element = element ?? throw new ArgumentNullException(nameof(element));

        if (string.IsNullOrEmpty(type))
            throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        if (string.IsNullOrEmpty(reference))
            throw new ArgumentException("Name cannot be null or empty.", nameof(reference));

        Type = type;
        Reference = reference;
        Instruction = instruction;
        Operand = operand;
    }

    /// <summary>
    /// The corresponding <see cref="ComponentKey"/> of the reference, indicating both the component type and name
    /// this element is in reference to.
    /// </summary>
    public ComponentKey Key => new(Type, Reference);

    /// <summary>
    /// The type of the component the element references.
    /// </summary>
    /// <value>A <see cref="string"/> indicating the type of the component.</value>
    public string Type { get; }

    /// <summary>
    /// The name of the component or element that is referenced.
    /// </summary>
    /// <value>A <see cref="string"/> indicating the name of the component.</value>
    public string Reference { get; }

    /// <summary>
    /// The <see cref="LogixElement"/> that is contains the reference to the component.
    /// </summary>
    /// <value>The <see cref="LogixElement"/> object that contains the component reference. This may be another
    /// <c>Component</c>, a <c>Code</c> instance, or even a single <c>DiagramElement</c> object.</value>
    public LogixObject Element => _element.Deserialize<LogixObject>();

    /// <summary>
    /// The type of the <c>LogixElement</c> that contains the reference to the component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the element type.</value>
    /// <remarks>This helps further identify the reference element relative to other references.</remarks>
    public string ElementType => _element.Name.LocalName;

    /// <summary>
    /// A unique identifier of the <c>LogixElement</c> that contains the reference to the component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name or number identifying the reference element.</value>
    /// <remarks>
    /// This will ultimately be either the name of the referencing component (for Tag references),
    /// the number of the referencing rung or line of logic (for RLL and ST code), or the ID of the referencing
    /// diagram block (for FBD/SFC code). This helps further identify the reference element relative to other references.
    /// </remarks>
    public string ElementId => _element.Attribute(L5XName.ID) is not null ? _element.Attribute(L5XName.ID)!.Value
        : _element.Attribute(L5XName.Number) is not null ? _element.Attribute(L5XName.Number)!.Value
        : _element.Attribute(L5XName.Name) is not null ? _element.Attribute(L5XName.Name)!.Value
        : string.Empty;

    /// <summary>
    /// The name of the <c>Task</c> that the reference is contained within if applicable.
    /// </summary>
    /// <value>A <see cref="string"/> representing the containing task if found; Otherwise, an empty string.</value>
    /// <remarks>
    /// This could potentially be helpful for analyzing references to tags that are used across multiple
    /// Task components.
    /// </remarks>
    public string Task => ScopeLevel.Task(_element);

    /// <summary>
    /// The <see cref="ScopeLevel"/> type that the reference is contained within.
    /// </summary>
    /// <value>A <see cref="ScopeLevel"/> indicating scope of the reference.</value>
    public ScopeLevel Scope => ScopeLevel.Type(_element);

    /// <summary>
    /// The name of the scoped program, instruction, or controller that the reference is contained within.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of program, controller, or instruction the reference
    /// is contained within.
    /// </value>
    public string Container => ScopeLevel.Container(_element);

    /// <summary>
    /// The name of the <c>Routine</c> that the reference is contained within, it is a <see cref="LogixCode"/>
    /// type element.
    /// </summary>
    /// <value>A <see cref="string"/> representing the containing routine if found; Otherwise, an empty string.</value>
    public string Routine => _element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <summary>
    /// The instruction object containing the reference to the component if this reference is a logic or code reference. 
    /// </summary>
    /// <value>If the reference is a code reference, then the <see cref="Core.Instruction"/> object the reference
    /// was found; Otherwise, <c>null</c>.</value>
    /// <remarks>
    /// The helps further identify where in the logix element the reference is located. Having the associated
    /// instruction can help for searching or filtering references and finding other references sharing the common
    /// instruction.
    /// </remarks>
    public string? Instruction { get; }

    /// <summary>
    /// 
    /// </summary>
    public string? Operand { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not CrossReference other)
            return false;

        return Key == other.Key &&
               Scope == other.Scope &&
               Container.IsEquivalent(other.Container) &&
               Routine.IsEquivalent(other.Routine) &&
               ElementId.IsEquivalent(other.ElementId) &&
               ElementType.IsEquivalent(other.ElementType);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        /*return HashCode.Combine(Key, Scope, Container, Routine, ElementId, ElementType);*/
        unchecked // overflow is fine, the result will just wrap
        {
            var hash = 17;
            hash = hash * 23 + Key.GetHashCode();
            hash = hash * 23 + Scope.GetHashCode();
            hash = hash * 23 + Container.GetHashCode();
            hash = hash * 23 + Routine.GetHashCode();
            hash = hash * 23 + ElementId.GetHashCode();
            hash = hash * 23 + ElementType.GetHashCode();
            return hash;
        }
    }

    /// <inheritdoc />
    public override string ToString() => Key.ToString();
}