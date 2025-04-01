using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace L5Sharp.Core;

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
    /// <inheritdoc />
    protected override List<string> ElementOrder =>
    [
        L5XName.Description,
        L5XName.RLLContent,
        L5XName.STContent,
        L5XName.FBDContent,
        L5XName.SFCContent
    ];

    /// <summary>
    /// Creates a new <see cref="Routine"/> with default values.
    /// </summary>
    /// <remarks>
    /// By default, this will be a RLL routine type.
    /// To specify a different type, use the <see cref="RoutineType"/> constructor.
    /// </remarks>
    public Routine() : base(L5XName.Routine)
    {
        UpdateContent(RoutineType.RLL);
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
    /// Creates a new <see cref="Routine"/> of the specified <see cref="RoutineType"/>.
    /// </summary>
    /// <param name="type">The <see cref="RoutineType"/> of the routine.</param>
    /// <exception cref="ArgumentNullException"><c>type</c> is null.</exception>
    public Routine(RoutineType type) : base(L5XName.Routine)
    {
        UpdateContent(type);
    }

    /// <summary>
    /// Creates a new <see cref="Routine"/> with the provided name and optional <see cref="RoutineType"/>
    /// </summary>
    /// <param name="name">The name of the routine.</param>
    /// <param name="type">The <see cref="RoutineType"/> of the routine. If null will default to <c>RLL</c>.</param>
    public Routine(string name, RoutineType? type = default) : base(L5XName.Routine)
    {
        Element.SetAttributeValue(L5XName.Name, name);
        UpdateContent(type ?? RoutineType.RLL);
    }

    /// <summary>
    /// The type of the <see cref="Routine"/> component.
    /// </summary>
    /// <value>A <see cref="RoutineType"/> enum specifying the type content the routine contains.</value>
    /// <remarks>
    /// Setting this property after construction will automatically remove the current content element to
    /// replace with a new element having the correct name. This means any configured content will be lost.
    /// </remarks>
    public RoutineType Type
    {
        get => GetRequiredValue<RoutineType>();
        set => UpdateContent(value);
    }

    /// <summary>
    /// The online edit type for the <c>ST</c>/<c>FBD</c>/<c>SFC</c> <see cref="Routine"/> type.
    /// </summary>
    /// <value>
    /// If the routine is a <c>ST</c>, <c>FBD</c>, or <c>SFC</c> type, then the <see cref="Core.OnlineEditType"/> value;
    /// Otherwise, <c>null</c> for <c>RLL</c> routines.
    /// </value>
    public OnlineEditType? OnlineEditType
    {
        get => GetValue<OnlineEditType>(e => e.Element(Type.ContentName));
        set => SetValue(value, e => e.Element(Type.ContentName));
    }

    /// <summary>
    /// The sheet size for the <c>FBD</c> or <c>SFC</c> <see cref="Routine"/> type.
    /// </summary>
    /// <value>
    /// If the routine is a <c>FBD</c> or <c>SFC</c> type, then the <see cref="Core.SheetSize"/> value;
    /// Otherwise, <c>null</c> for <c>RLL</c> or <c>ST</c> routines.
    /// </value>
    public SheetSize? SheetSize
    {
        get => GetValue<SheetSize>(e => e.Element(Type.ContentName));
        set => SetValue(value, e => e.Element(Type.ContentName));
    }

    /// <summary>
    /// The sheet orientation for the <c>FBD</c> or <c>SFC</c> <see cref="Routine"/> type.
    /// </summary>
    /// <value>
    /// If the routine is a <c>FBD</c> or <c>SFC</c> type, then the <see cref="Core.SheetOrientation"/> value;
    /// Otherwise, <c>null</c> for <c>RLL</c> or <c>ST</c> routines.
    /// </value>
    public SheetOrientation? SheetOrientation
    {
        get => GetValue<SheetOrientation>(e => e.Element(Type.ContentName));
        set => SetValue(value, e => e.Element(Type.ContentName));
    }

    /// <summary>
    /// Gets the parent <see cref="Core.Program"/> in which this routine is contained.
    /// If this routine has no container (meaning it is not scoped), then this property returns <c>null</c>. 
    /// </summary>
    /// <remarks>
    /// This is a navigation helper to allow easily retrieving the parent program for a given routine component.
    /// This requires a scoped/attached L5X as it traverses the L5X document tree to find the target component. 
    /// </remarks>
    public Program? Program => Element.Ancestors(L5XName.Program).FirstOrDefault()?.Deserialize<Program>();

    /// <summary>
    /// Returns a <see cref="LogixContainer{TObject}"/> of <see cref="Rung"/> found in this routine component.
    /// If this routine is not an RLL routine or does not contain rungs, then the resulting collection is empty.
    /// </summary>
    /// <remarks>
    /// This internally just calls <see cref="Content{TCode}"/> and is here mostly as a navigation helper.
    /// Obviously, if this routine is not an RLL routine, then you shouldn't use this property to add rung objects
    /// to the underlying XML, as it would result in import errors.
    /// </remarks>
    public LogixContainer<Rung> Rungs => Content<Rung>();

    /// <summary>
    /// Returns a <see cref="LogixContainer{TObject}"/> of <see cref="Line"/> found in this routine component.
    /// If this routine is not an ST routine or does not contain lines, then the resulting collection is empty.
    /// </summary>
    /// <remarks>
    /// This internally just calls <see cref="Content{TCode}"/> and is here mostly as a navigation helper.
    /// Obviously, if this routine is not an ST routine, then you shouldn't use this property to add line objects
    /// to the underlying XML, as it would result in import errors.
    /// </remarks>
    public LogixContainer<Line> Lines => Content<Line>();

    /// <summary>
    /// Returns a <see cref="LogixContainer{TObject}"/> of <see cref="Sheet"/> found in this routine component.
    /// If this routine is not an FBD routine or does not contain sheets, then the resulting collection is empty.
    /// </summary>
    /// <remarks>
    /// This internally just calls <see cref="Content{TCode}"/> and is here mostly as a navigation helper.
    /// Obviously, if this routine is not an FBD routine, then you shouldn't use this property to add sheet objects
    /// to the underlying XML, as it would result in import errors.
    /// </remarks>
    public LogixContainer<Line> Sheets => Content<Line>();

    /// <summary>
    /// Gets the routine content as a <see cref="LogixContainer{TElement}"/> containing elements of the specified type.
    /// </summary>
    /// <typeparam name="TCode">The content element type to return.</typeparam>
    /// <returns>A <see cref="LogixContainer{TElement}"/> with access to the root content and specified element types.</returns>
    /// <remarks>
    /// This method offers a dynamic interface for accessing content of any routine type.
    /// </remarks>
    public LogixContainer<TCode> Content<TCode>() where TCode : LogixCode
    {
        EnsureContentAdded();

        var content = Element.Element(Type.ContentName);

        return content is not null
            ? new LogixContainer<TCode>(content)
            : throw Element.L5XError(Type.ContentName);
    }

    /// <summary>
    /// Ensures that the expected content element is added to this routine element. This prevents emprty routines from
    /// throwing exceptions when the caller accesses <see cref="Content{TCode}"/>.
    /// </summary>
    private void EnsureContentAdded()
    {
        if (Element.Element(Type.ContentName) is not null) return;
        UpdateContent(Type);
    }

    /// <summary>
    /// Updates the current routine's content by setting the required Type attribute and adding or replacing the
    /// child content element with a new element having the name of the provided <see cref="RoutineType"/> content.
    /// </summary>
    private void UpdateContent(RoutineType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        Element.SetAttributeValue(L5XName.Type, type);

        var content = Element.Element(type.ContentName);

        if (content is null)
        {
            Element.Add(new XElement(type.ContentName));
            return;
        }

        content.ReplaceWith(new XElement(type.ContentName));
    }
}