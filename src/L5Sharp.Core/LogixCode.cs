using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a general interface for Logix code components within a control system.
/// </summary>
/// <remarks>
/// This interface defines the common structure and behavior for all Logix code elements, such as Rungs, Sheets, Charts,
/// and Lines. It provides mechanisms for accessing associated metadata, instructions, and tags, enabling interaction and
/// analysis of programmatic content in Logix systems.
/// </remarks>
public interface ILogixCode : ILogixEntity
{
    /// <summary>
    /// The zero based number indicating the position of the code within the containing <c>Routine</c>.
    /// </summary>
    /// <value>A <see cref="int"/> representing the zero-based order.</value>
    /// <remarks>
    /// Logix ignores the these number identifiers upon importing for routines contained in <c>Program</c> components,
    /// but is required for routines in an <c>AddOnInstruction</c>. 
    /// </remarks>
    int Number { get; set; }

    /// <summary>
    /// Gets the parent <c>Program</c> component in which this code element is contained.
    /// </summary>
    /// <remarks>
    /// A <c>Program</c> is the top-level code container in a Logix system. This property provides a reference to the
    /// hierarchical parent for contextual operations such as navigation, analysis, or modification of the parent-child
    /// relationship.
    /// </remarks>
    Program? Program { get; }

    /// <summary>
    /// Gets the parent <c>Routine</c> component in which this code element is contained.
    /// </summary>
    /// <remarks>
    /// A <c>Routine</c> defines a singular logical construct, such as ladder logic, structured text, or other
    /// programming languages supported by Logix, that can be executed within its containing <c>Program</c> or
    /// <c>AddOnInstruction</c>. Each <c>Routine</c> serves as a reusable and modular unit of execution.
    /// </remarks>
    Routine? Routine { get; }

    /// <summary>
    /// Gets a collection of <see cref="Instruction"/> instances defined in the code element.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Instruction"/> objects representing the logical code blocks found in this code element.
    /// </returns>
    IEnumerable<Instruction> Instructions();

    /// <summary>
    /// Retrieves a collection of <see cref="TagName"/> values found within this code instance.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="TagName"/> objects representing the tags contained in the code element.
    /// </returns>
    IEnumerable<TagName> Tags();
}

/// <summary>
/// Represents a base definition for a Logix code element within the Rockwell Automation environment.
/// </summary>
/// <remarks>
/// This abstract class serves as a foundation for implementing Logix code components that are specific to the
/// automation programming environment. It provides shared functionality and behaviors that can be extended
/// by derived classes to represent distinct types of Logix code, such as <c>Rung</c> or <c>Line</c>.
/// </remarks>
public abstract class LogixCode<TCode> : LogixEntity<TCode>, ILogixCode where TCode : LogixCode<TCode>
{
    /// <inheritdoc />
    protected LogixCode(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixCode(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public virtual int Number
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <inheritdoc />
    public Program? Program => GetAncestor<Program>();

    /// <inheritdoc />
    public Routine? Routine => GetAncestor<Routine>();

    /// <inheritdoc />
    public abstract IEnumerable<Instruction> Instructions();

    /// <inheritdoc />
    public abstract IEnumerable<TagName> Tags();

    /// <inheritdoc />
    public override string ToString() => $"{Element.Name.LocalName} {Number}".Trim();
}