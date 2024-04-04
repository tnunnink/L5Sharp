using System;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class implementing <see cref="LogixElement"/> that adds common properties and methods shared by most elements or
/// components. These features include reference to the containing <see cref="L5X"/> document, the <see cref="Scope"/>
/// and <see cref="Container"/> to identify where in the document they exists, and methods <see cref="AddBefore"/>,
/// <see cref="AddBefore"/>, <see cref="Remove"/>, and <see cref="Replace"/> which allow easy way to mutate the object
/// or collection of objects.
/// </summary>
public abstract class LogixObject : LogixElement
{
    /// <inheritdoc />
    protected LogixObject(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixObject(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Indicates whether this element is attached to an L5X document.
    /// </summary>
    /// <value><c>true</c> if this is an attached element; Otherwise, <c>false</c>.</value>
    /// <remarks>
    /// This simply looks to see if the element has a ancestor with the root RSLogix5000Content element or not.
    /// If so we will assume this element is attached to an L5X document.
    /// </remarks>
    public bool IsAttached => Element.Ancestors(L5XName.RSLogix5000Content).Any();

    /// <summary>
    /// Returns the <see cref="L5X"/> instance this <see cref="LogixElement"/> is attached to if it is attached. 
    /// </summary>
    /// <returns>
    /// If the current element is attached to a L5X document (i.e. has the root content element),
    /// then the <see cref="L5X"/> instance; Otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This allows attached logix elements to reach up to the L5X file in order to traverse or retrieve
    /// other elements in the L5X. This is helpful/used for other extensions and cross referencing functions.
    /// </remarks>
    public L5X? L5X => Element.Ancestors(L5XName.RSLogix5000Content).FirstOrDefault()?.Annotation<L5X>();

    /// <summary>
    /// The scope of the element, indicating whether it is a globally scoped controller element,
    /// a locally scoped program or instruction element, or neither (not attached to L5X tree).
    /// </summary>
    /// <value>A <see cref="Core.Scope"/> option indicating the scope type for the element.</value>
    /// <remarks>
    /// <para>
    /// The scope of an element is determined from the ancestors of the underlying <see cref="XElement"/>.
    /// If the ancestor is a program element first, it is <c>Program</c> scoped.
    /// If the ancestor is a AddOnInstructionDefinition element first, it is <c>Instruction</c> scoped.
    /// If the ancestor is a controller element first, it is <c>Controller</c> scoped.
    /// If no ancestor is found, we assume the component has <c>Null</c> scope, meaning it is not attached to a L5X tree.
    /// </para>
    /// <para>
    /// This property is not inherent in the underlying XML (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify elements within the L5X file, especially
    /// <c>Tag</c>, <c>Routine</c>, and code elements.
    /// </para>
    /// </remarks>
    public Scope Scope => Scope.Type(Element);

    /// <summary>
    /// The logix name of an ancestral element, indicating the program, instruction, or controller
    /// for which this element is contained. This is essentially the scope name for a given logix element.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of the program, instruction, or controller in which this component
    /// is contained. If the component has <see cref="Scope.Null"/> scope, then an <see cref="string.Empty"/> string.
    /// </value>
    /// <remarks>
    /// <para>
    /// This value is retrieved from the ancestors of the underlying element. If no ancestors exists, meaning this
    /// element is not attached to a L5X tree, then this returns an empty string.
    /// </para>
    /// <para>
    /// This property is not inherent in the underlying XML (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify elements within the L5X file, especially scoped components with same name.
    /// </para>
    /// </remarks>
    public string Container => Scope.Container(Element);

    /// <summary>
    /// Adds a new object of the same type directly after this object in the L5X document.
    /// </summary>
    /// <param name="item">The logix object to add.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element -or-
    /// the provided logix element is not the same type or convertable to the type of this logix element.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the <see cref="L5X"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// It will also automatically perform the "type conversion" of the provided element if possible.
    /// This just means it will attempt to change the element name to match this element name so that the
    /// underlying element type will have the correct sequence name. This is used primarily for types that support
    /// multiple elements (i.e. Tags).
    /// </remarks>
    public void AddAfter(LogixObject item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));

        if (Element.Parent is null)
            throw new InvalidOperationException(
                "Can only perform operation for L5X attached elements. Add this element to the logix content before invoking.");

        if (item.L5XType != L5XType)
        {
            item = item.Convert(L5XType);
        }

        Element.AddAfterSelf(item.Serialize());
    }

    /// <summary>
    /// Adds a new element of the same type directly before this element in the L5X document.
    /// </summary>
    /// <param name="element">The logix element to add.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element -or-
    /// the provided logix element is not the same type or convertable to the type of this logix element.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the <see cref="L5X"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// It will also automatically perform the "type conversion" of the provided element if possible.
    /// This just means it will attempt to change the element name to match this element name so that the
    /// underlying element type will have the correct sequence name. This is used primarily for types that support
    /// multiple elements (i.e. Tags).
    /// </remarks>
    public void AddBefore(LogixObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException(
                "Can only perform operation for L5X attached elements. Add this element to the L5X before invoking.");

        if (element.L5XType != L5XType)
        {
            element = element.Convert(L5XType);
        }

        Element.AddBeforeSelf(element.Serialize());
    }

    /// <summary>
    /// Converts this element to the specified element type name. 
    /// </summary>
    /// <param name="typeName">The name of the element type to convert this logix element to.</param>
    /// <returns>A new <see cref="LogixElement"/> instance with the converted element name.</returns>
    /// <exception cref="ArgumentException"><c>typeName</c> is null or empty.</exception>
    /// <exception cref="InvalidOperationException">The specified type name is not supported by this class type.</exception>
    /// <remarks>
    /// This simply updates the underlying element name to the provided name if it is a supported type name for this
    /// logix element class type, which is configured via the <see cref="L5XTypeAttribute"/> for the derived type. This
    /// is primarily needed so we can ensure adding elements of types that support multiple element names can be updated
    /// to the correct element name and ensure a proper L5X sequence within the document. For example, adding a <c>Tag</c>
    /// to a collection of AOI <c>LocalTags</c> needs the underlying element name updated to <c>LocalTag</c> in that case.
    /// </remarks>
    public LogixObject Convert(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentException("Type name can not be null or empty to perform conversion", nameof(typeName));

        if (!GetType().L5XTypes().Contains(typeName))
            throw new InvalidOperationException($"Can not convert element type '{L5XType}' to type '{typeName}'.");

        if (L5XType == typeName) return this;

        Element.Name = typeName;
        return (LogixObject)Element.Deserialize();
    }

    /// <summary>
    /// Converts this element to the specified element type name.
    /// </summary>
    /// <param name="typeName">The name of the element type to convert this logix element to. This will default to the
    /// first configured L5XType of the specified logix type parameter, but can optionally be provided if the type
    /// to convert to is not the default L5XType name.</param>
    /// <typeparam name="TObject">The <see cref="LogixObject"/> to convert this object type to.</typeparam>
    /// <returns>A new <see cref="LogixElement"/> of the specified generic type with the converted element name.</returns>
    /// <exception cref="InvalidOperationException">The specified type name is not supported by this class type.</exception>
    /// <remarks>
    /// This simply updates the underlying element name to the provided name if it is a supported type name for this
    /// logix element class type, which is configured via the <see cref="L5XTypeAttribute"/> for the derived type. This
    /// is primarily needed so we can ensure adding elements of types that support multiple element names can be updated
    /// to the correct element name and ensure a proper L5X sequence within the document. For example, adding a <c>Tag</c>
    /// to a collection of AOI <c>LocalTags</c> needs the underlying element name updated to <c>LocalTag</c> in that case.
    /// </remarks>
    public TObject Convert<TObject>(string? typeName = null) where TObject : LogixObject
    {
        typeName ??= typeof(TObject).L5XType();

        if (!GetType().L5XTypes().Contains(typeName))
            throw new InvalidOperationException($"Can not convert element type '{L5XType}' to type '{typeName}'.");

        if (L5XType == typeName) return (TObject)this;

        Element.Name = typeName;
        return Element.Deserialize<TObject>();
    }

    /// <summary>
    /// Removes the element from it's parent container.
    /// </summary>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element.
    /// This could happen if the component was created in memory and not yet added to the L5X.
    /// </exception>
    /// <remarks>
    /// This method requires the element be attached to the <see cref="L5X"/>, or at least have a parent
    /// containing element as it will access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    public void Remove()
    {
        if (Element.Parent is null)
            throw new InvalidOperationException(
                "Can only perform operation for L5X attached elements. Add this element to the L5X before invoking.");

        Element.Remove();
    }

    /// <summary>
    /// Replaces the element instance with a new instance of the same type.
    /// </summary>
    /// <param name="element">The new logix element to replace this element with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element -or-
    /// the provided logix element is not the same type or convertable to the type of this logix element.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the <see cref="L5X"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// It will also automatically perform the "type conversion" of the provided element if possible.
    /// This just means it will attempt to change the element name to match this element name so that the
    /// underlying element type will have the correct sequence name. This is used primarily for types that support
    /// multiple elements (i.e. Tags).
    /// </remarks>
    public void Replace(LogixObject element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException(
                "Can only perform operation for L5X attached elements. Add this element to the L5X before invoking.");

        if (element.L5XType != L5XType)
        {
            element = element.Convert(L5XType);
        }

        Element.ReplaceWith(element.Serialize());
    }
}