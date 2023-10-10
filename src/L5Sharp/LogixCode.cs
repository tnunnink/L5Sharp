using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
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
public abstract class LogixCode : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="LogixCode"/> instance with default values.
    /// </summary>
    protected LogixCode()
    {
        Element.SetAttributeValue(L5XName.Number, 0);
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
    /// The name of the <c>Program</c> or <c>AddOnInstruction</c> that contains this code element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the <c>Program</c> if found; Otherwise, and empty string.</value>
    public string Program => Element.Ancestors().FirstOrDefault(IsContainerType)?.LogixName() ?? string.Empty;

    /// <summary>
    /// The name of the <c>Routine</c> that contains this code element.
    /// </summary>
    /// <value>A <see cref="string"/> containing the name of the <c>Routine</c> if found; Otherwise, and empty string.</value>
    public string Routine => Element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ?? string.Empty;

    /// <summary>
    /// Returns a collection of <see cref="TagName"/> objects found within this code element.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="TagName"/> values referenced by this code.</returns>
    public abstract IEnumerable<TagName> TagNames();

    /// <summary>
    /// Returns a collection of <see cref="Instruction"/> objects found within this code element.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Instruction"/> values referenced by this code.</returns>
    public abstract IEnumerable<Instruction> Instructions();

    private static bool IsContainerType(XElement element) =>
        element.L5XType() is L5XName.Program or L5XName.AddOnInstructionDefinition;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            LogixCode other => StringComparer.OrdinalIgnoreCase.Equals(Program, other.Program) &&
                               StringComparer.OrdinalIgnoreCase.Equals(Routine, other.Routine) &&
                               Number == other.Number,
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Program) ^
               StringComparer.OrdinalIgnoreCase.GetHashCode(Routine) ^
               Number;
    }
}