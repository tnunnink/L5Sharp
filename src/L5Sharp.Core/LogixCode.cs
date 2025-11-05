using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An abstract representation of Logix code found within the content portion of a <c>Routine</c> component.
/// </summary>
/// <remarks>
/// This class is meant to specify a common set of properties and functions that all code elements should provide to
/// assist with code analysis and modification. Rockwell supported programming languages  include RLL, ST, FBD, SFC.
/// </remarks>
public abstract class LogixCode : LogixEntity
{
    /// <summary>
    /// Creates a new <see cref="LogixCode"/> instance with default values.
    /// </summary>
    protected LogixCode(string name) : base(name)
    {
    }

    /// <summary>
    /// Creates a new <see cref="LogixCode"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    protected LogixCode(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The zero based number indicating the position of the code within the containing <c>Routine</c>.
    /// </summary>
    /// <value>A <see cref="int"/> representing the zero-based order.</value>
    /// <remarks>
    /// Logix ignores the these number identifiers upon importing for routines contained in <c>Program</c> components,
    /// but is required for routines in an <c>AddOnInstruction</c>. 
    /// </remarks>
    public virtual int Number
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the parent <c>Program</c> component in which this code element is contained.
    /// </summary>
    /// <remarks>
    /// A <c>Program</c> is the top-level code container in a Logix system. This property provides a reference to the
    /// hierarchical parent for contextual operations such as navigation, analysis, or modification of the parent-child
    /// relationship.
    /// </remarks>
    public Program? Program => GetAncestor<Program>();

    /// <summary>
    /// Gets the parent <c>Routine</c> component in which this code element is contained.
    /// </summary>
    /// <remarks>
    /// A <c>Routine</c> defines a singular logical construct, such as ladder logic, structured text, or other
    /// programming languages supported by Logix, that can be executed within its containing <c>Program</c> or
    /// <c>AddOnInstruction</c>. Each <c>Routine</c> serves as a reusable and modular unit of execution.
    /// </remarks>
    public Routine? Routine => GetAncestor<Routine>();

    /// <summary>
    /// Gets a collection of <see cref="Instruction"/> instances defined in the code element.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="Instruction"/> objects representing the logical code blocks found in this code element.
    /// </returns>
    public abstract IEnumerable<Instruction> Instructions();

    /// <summary>
    /// Retrieves a collection of <see cref="TagName"/> values found within this code instance.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="TagName"/> objects representing the tags contained in the code element.
    /// </returns>
    public abstract IEnumerable<TagName> Tags();
    
    /// <inheritdoc />
    /// <remarks>
    /// For code, this method will return all parsed instruction references in the code element.
    /// This is because code elements are not "used" but use other components like tags or instructions, and we want
    /// to reverse this method to allow those components to find their usages susinctly.
    /// </remarks>
    public override IEnumerable<Reference> Usages()
    {
        return Instructions().Select(x => Reference.ToLogic(x));
    }
    
    /// <inheritdoc />
    public override IEnumerable<LogixComponent> Dependencies()
    {
        var dependencies = new List<LogixComponent>();

        foreach (var tagName in Tags())
        {
            if (!TryResolve<Tag>(tagName, out var tag)) continue;
            dependencies.Add(tag);
            dependencies.AddRange(tag.Dependencies());
        }

        foreach (var instruction in Instructions())
        {
            if (!TryResolve<AddOnInstruction>(instruction.Key, out var aoi)) continue;
            dependencies.Add(aoi);
            dependencies.AddRange(aoi.Dependencies());
        }

        return dependencies.Distinct(c => c.Reference);
    }

    /// <inheritdoc />
    public override string ToString() => $"{GetElementType()} {Number}".Trim();
}