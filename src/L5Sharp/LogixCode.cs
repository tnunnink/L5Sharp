using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// An abstract representation of a segment of Logix code found within the content portion of a logix <c>Routine</c>.
/// </summary>
/// <remarks>
/// <para>
/// This class is meant to specify a common set of properties and functions that all code elements, regardless
/// of programming language type (RLL, ST, FBD, SFC) should provide so that we can retrieve information about the contents
/// and location of the code within the L5X file.
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
    /// inherent to the underlying XMl.</remarks>
    public virtual int Number
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// The location of the code within the L5X tree. This could be different for each code type, but generally
    /// will be the <c>Rung</c>, <c>Line</c>, or <c>Sheet</c> number within the containing routine.
    /// </summary>
    public virtual string Location => $"{Element.Name.LocalName} {Number}";
    
    /// <summary>
    /// The scope name of the logix code, indicating the program or instruction for which it is contained.
    /// </summary>
    /// <value> A <see cref="string"/> representing the name of the program or instruction in which this code
    /// is contained. If the code has <see cref="Scope.Null"/> scope, then an <see cref="string.Empty"/> string.
    /// </value>
    /// <remarks>
    /// <para>
    /// This value is retrieved from the ancestors of the underlying element. If no ancestors exists, meaning this
    /// code is not attached to a L5X tree, then this returns an empty string.
    /// </para>
    /// <para>
    /// This property is not inherent in the underlying XML of a code element (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify code within the L5X file.
    /// </para>
    /// </remarks>
    public string ScopeName => Scope.ScopeName(Element);
    
    /// <summary>
    /// The scope of the <c>LogixCode</c>, indicating whether it is a program scoped or instruction scoped
    /// code element, or neither (not attached to L5X tree).
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> option indicating the scope type for the element.</value>
    /// <remarks>
    /// <para>
    /// The scope of this element is determined from the ancestors of the underlying element. If the ancestor is
    /// program element first, it is <c>Program</c> scoped. If the ancestor is a instruction element first, it is
    /// <c>Instruction</c> scoped. If no ancestor is found, we assume the element has <c>Null</c> scope,
    /// meaning it is not attached to a L5X tree.
    /// </para>
    /// <para>
    /// This property is not inherent in the underlying XML of a component (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify components within the L5X file.
    /// </para>
    /// </remarks>
    public Scope ScopeType => Scope.ScopeType(Element);

    /// <summary>
    /// The name of the <c>Routine</c> that contains this code element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the <c>Routine</c> if found; Otherwise, and empty string.</value>
    public string Routine => Element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <summary>
    /// Returns a collection of <see cref="LogixReference"/> objects found within this code element.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="LogixReference"/> values contained by this code.</returns>
    public abstract IEnumerable<LogixReference> References();
}