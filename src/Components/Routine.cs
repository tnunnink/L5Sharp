using System;
using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A logix <c>Routine</c> component. Contains the properties for a generic Routine element. This type does not
/// include content property. More specific routine types are derived from this base class.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
public class Routine : LogixComponent<Routine>
{
    /// <summary>
    /// Creates a new <see cref="Routine"/> with default values.
    /// </summary>
    /// <remarks>
    /// By default this will be a RLL routine type.
    /// To specify a different type, use the <see cref="RoutineType"/> constructor.
    /// </remarks>
    public Routine()
    {
        Element.Add(new XAttribute(L5XName.Type, RoutineType.Rll));
        Element.Add(new XElement($"{Type.Value}Content"));
    }

    /// <summary>
    /// Creates a new <see cref="Routine"/> of the specified <see cref="RoutineType"/>.
    /// </summary>
    /// <param name="type">The <see cref="RoutineType"/> of the routine.</param>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    public Routine(RoutineType type)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));
        Element.Add(new XAttribute(L5XName.Type, type));
        Element.Add(new XElement($"{Type.Value}Content"));
    }

    /// <summary>
    /// Creates a new <see cref="Routine"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Routine(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The type of the <see cref="Routine"/> component.
    /// </summary>
    /// <value>A <see cref="Enums.RoutineType"/> enum specifying the type content the routine contains.</value>
    public RoutineType Type => GetRequiredValue<RoutineType>();

    /// <summary>
    /// Gets the routine content as a <see cref="LogixContainer{TElement}"/> containing elements of the specified type.
    /// </summary>
    /// <typeparam name="TElement">The content element type to return.</typeparam>
    /// <returns>A <see cref="LogixContainer{TElement}"/> with access to the root content and specified element types.</returns>
    /// <exception cref="L5XException">No content element corresponding to the specified <see cref="Type"/> exists for the
    /// underlying <see cref="XElement"/>. This can happen if the provided element is not valid.</exception>
    /// <remarks>
    /// This method offers a dynamic interface for accessing content of any routine type. If the underlying routine
    /// content does not match the <see cref="Type"/> specified, a L5XException will be thrown.
    /// </remarks>
    public LogixContainer<TElement> Content<TElement>() where TElement : LogixElement<TElement>
    {
        var content = Element.Element($"{Type.Value}Content");

        return content is not null
            ? new LogixContainer<TElement>(content)
            : throw new L5XException($"{Type.Value}Content", Element);
    }
}