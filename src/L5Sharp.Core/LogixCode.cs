using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An abstract representation of Logix code found within the content portion of a logix <c>Routine</c>.
/// </summary>
/// <remarks>
/// <para>
/// This class is meant to specify a common set of properties and functions that all code elements should provide.
/// Rockwell supported programming languages  include RLL, ST, FBD, SFC.
/// </para>
/// <para>
/// This class overrides the default equality implementation to determine code equality by its location within the L5X tree.
/// In other words, two instances of code are equal if they are in the same program/instruction, routine, and have the
/// same number.
/// </para>
/// </remarks>
public abstract class LogixCode : LogixScoped
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
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// The parent <see cref="Core.Routine"/> component for the current <c>LogixCode</c> element.
    /// </summary>
    /// <value>A <see cref="Routine"/> element if found; Otherwise, and <c>null</c>.</value>
    public Routine? Routine
    {
        get
        {
            var routine = Element.Ancestors(L5XName.Routine).FirstOrDefault();
            return routine is not null ? new Routine(routine) : null;
        }
    }

    /// <summary>
    /// Returns a collection of <see cref="CrossReference"/> objects found within this code element.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="CrossReference"/> values contained by this code.</returns>
    public IEnumerable<CrossReference> References() => CrossReference.In(Element);

    /// <inheritdoc />
    public override string ToString() => $"{L5XType} {Number}".Trim();
}