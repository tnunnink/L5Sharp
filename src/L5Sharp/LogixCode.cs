using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An abstract representation of a segment of Logix code found within the content portion of a logix <c>Routine</c>.
/// </summary>
/// <remarks>
/// <para>
/// This class is meant to specify a common set of properties and functions that all code elements, regardless
/// of programming language type (RLL, ST, FBD, SFC) should provide so that we can retrieve information about the contents
/// and location of the code within the L5X file. It is also here to help constrain the type of element that the caller
/// can query from the content of a given routine type.
/// </para>
/// <para>
/// This class overrides the default equality implementation to determine code equality by it's location within the L5X tree.
/// In other words, two instances of code are equal if they are in the same program/instruction, routine, and have the
/// same number.
/// </para>
/// </remarks>
public abstract class LogixCode : LogixElement, ILogixReferencable
{
    /// <summary>
    /// Creates a new <see cref="LogixCode"/> instance with default values.
    /// </summary>
    protected LogixCode()
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
    /// <remarks>Logix ignores the these number identifiers upon importing, and only considers the order within the
    /// containing <c>Routine</c>. This makes the property somewhat useless, but is here all the same as it is
    /// inherent to the underlying XMl, and can help identify code elements from deserialized L5X documents.</remarks>
    public virtual int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public string Location => $"{L5XType} {Number}".Trim();

    /// <summary>
    /// The the parent <see cref="Core.Routine"/> component for the current <c>LogixCode</c> element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the <c>Routine</c> if found; Otherwise, and empty string.</value>
    public Routine? Routine
    {
        get
        {
            var routine = Element.Ancestors(L5XName.Routine).FirstOrDefault();
            return routine is not null ? new Routine(routine) : default;
        }
    }

    /// <summary>
    /// Returns a collection of <see cref="CrossReference"/> objects found within this code element.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="CrossReference"/> values contained by this code.</returns>
    public abstract IEnumerable<CrossReference> References();
}