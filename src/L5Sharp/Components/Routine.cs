﻿using System;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Utilities;

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
        Element.Add(new XAttribute(L5XName.Type, RoutineType.RLL));
        Element.Add(new XElement(ContentName(RoutineType.RLL)));
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
        Element.Add(new XElement(ContentName(type)));
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
    public RoutineType Type
    {
        get => GetRequiredValue<RoutineType>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The online edit type for the <c>ST</c>/<c>FBD</c>/<c>SFC</c> <see cref="Routine"/> type.
    /// </summary>
    /// <value>
    /// If the routine is a <c>ST</c>, <c>FBD</c>, or <c>SFC</c> type, then the <see cref="Enums.OnlineEditType"/> value;
    /// Otherwise, <c>null</c> for <c>RLL</c> routines.
    /// </value>
    public OnlineEditType? OnlineEditType
    {
        get => GetValue<OnlineEditType>(e => e.Element(ContentName(Type)));
        set => SetValue(value, e => e.Element(ContentName(Type)));
    }

    /// <summary>
    /// The sheet size for the <c>FBD</c> or <c>SFC</c> <see cref="Routine"/> type.
    /// </summary>
    /// <value>
    /// If the routine is a <c>FBD</c> or <c>SFC</c> type, then the <see cref="Enums.SheetSize"/> value;
    /// Otherwise, <c>null</c> for <c>RLL</c> or <c>ST</c> routines.
    /// </value>
    public SheetSize? SheetSize
    {
        get => GetValue<SheetSize>(e => e.Element(ContentName(Type)));
        set => SetValue(value, e => e.Element(ContentName(Type)));
    }

    /// <summary>
    /// The sheet orientation for the <c>FBD</c> or <c>SFC</c> <see cref="Routine"/> type.
    /// </summary>
    /// <value>
    /// If the routine is a <c>FBD</c> or <c>SFC</c> type, then the <see cref="Enums.SheetOrientation"/> value;
    /// Otherwise, <c>null</c> for <c>RLL</c> or <c>ST</c> routines.
    /// </value>
    public SheetOrientation? SheetOrientation
    {
        get => GetValue<SheetOrientation>(e => e.Element(ContentName(Type)));
        set => SetValue(value, e => e.Element(ContentName(Type)));
    }

    /// <summary>
    /// Gets the routine content as a <see cref="LogixContainer{TElement}"/> containing elements of the specified type.
    /// </summary>
    /// <typeparam name="TElement">The content element type to return.</typeparam>
    /// <returns>A <see cref="LogixContainer{TElement}"/> with access to the root content and specified element types.</returns>
    /// <exception cref="InvalidOperationException">No content element corresponding to the specified <see cref="Type"/> exists for the
    /// underlying <see cref="XElement"/>. This can happen if the provided element is not valid.</exception>
    /// <remarks>
    /// This method offers a dynamic interface for accessing content of any routine type. If the underlying routine
    /// content does not match the <see cref="Type"/> specified, a L5XException will be thrown.
    /// </remarks>
    public LogixContainer<TElement> Content<TElement>() where TElement : LogixElement
    {
        var content = Element.Element(ContentName(Type));

        return content is not null
            ? new LogixContainer<TElement>(content)
            : throw Element.L5XError(ContentName(Type));
    }

    /// <summary>
    /// Generates the L5XName representing the child content element for the routine based on the specified
    /// <see cref="RoutineType"/>. 
    /// </summary>
    /// <param name="type">The routine type for which to generate the L5XName.</param>
    /// <returns>A <see cref="string"/> representing the L5XName of the routine content element.</returns>
    private static string ContentName(RoutineType type) => $"{type.Value}Content";
}