using System;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class implementing <see cref="LogixElement"/> that adds common properties and methods shared by most elements or
/// components. Features include the <see cref="Scope"/> to identify where in the document they exist,
/// and methods <see cref="AddBefore"/>, <see cref="AddBefore"/>, <see cref="Remove"/>, and
/// <see cref="Replace(L5Sharp.Core.LogixObject)"/> which allow easy interface to mutate the object or collection of objects.
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
    /// Adds a new object of the same type directly after this object in the L5X document.
    /// </summary>
    /// <param name="item">The logix object to add.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element -or-
    /// the provided logix element is a different type or convertable to the type of this logix element.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the <see cref="L5X"/> as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// It will also automatically perform the "type conversion" of the provided element if possible.
    /// This just means it will attempt to change the element name to match this element name so that the
    /// underlying element type will have the correct sequence name. This is used primarily for types that support
    /// multiple elements (i.e., Tags).
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
    /// the provided logix element is a different type or convertable to the type of this logix element.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to an <see cref="L5X"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// It will also automatically perform the "type conversion" of the provided element if possible.
    /// This just means it will attempt to change the element name to match this element name so that the
    /// underlying element type will have the correct sequence name. This is used primarily for types that support
    /// multiple elements (i.e., Tags).
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
    /// <exception cref="InvalidOperationException">This class type does not support the specified type name.</exception>
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
    /// <exception cref="InvalidOperationException">This class type does not support the specified type name.</exception>
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
    /// Removes the element from its parent container.
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
    /// the provided logix element is different type or convertable to the type of this logix element.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the <see cref="L5X"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// It will also automatically perform the "type conversion" of the provided element if possible.
    /// This just means it will attempt to change the element name to match this element name so that the
    /// underlying element type will have the correct sequence name. This is used primarily for types that support
    /// multiple elements (i.e., Tags).
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

    /// <summary>
    /// Replaces occurrences of a specified string within the properties of the underlying XML element.
    /// </summary>
    /// <param name="find">The string to find within the element properties.</param>
    /// <param name="replace">The string to replace the found occurrences with.</param>
    /// <param name="properties">An optional array of property names that specify the scope of the replacement operation.
    /// If no property is specified, then this method will operate on every immediate and nested property in the object.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="find"/> or <paramref name="replace"/> is <c>null</c>.</exception>
    public void Replace(string find, string replace, string[]? properties = null)
    {
        if (find is null)
            throw new ArgumentNullException(nameof(find));

        if (replace is null)
            throw new ArgumentNullException(nameof(replace));

        Element.FindAndReplace(find, replace, properties ?? []);
    }
}

/// <summary>
/// A generic abstract <see cref="LogixObject"/> that implements the <see cref="ILogixParsable{T}"/> interface.
/// This generic type class allows us to specify the strong return types for methods <see cref="Parse"/>,
/// <see cref="TryParse"/> and <see cref="Clone"/>. This means we don't have to implement these methods for every
/// derivative type, and allows these types to be used with the <see cref="LogixParser"/> in a dynamic fashion.
/// </summary>
/// <typeparam name="TObject">The type implementing <see cref="LogixObject"/></typeparam>
public abstract class LogixObject<TObject> : LogixObject, ILogixParsable<TObject>
    where TObject : LogixObject, ILogixParsable<TObject>
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
    /// Returns a new deep-cloned instance of this object.
    /// </summary>
    /// <returns>A new object instance of the same type with the same property values.</returns>
    /// <remarks>This method will simply deserialize a new instance using the current underlying element data.</remarks>
    public new TObject Clone() => new XElement(Serialize()).Deserialize<TObject>();

    /// <summary>
    /// Parses the provided string as the specified generic <see cref="LogixObject{TObject}"/>.
    /// </summary>
    /// <param name="value">The XML string value to parse.</param>
    /// <returns>A new <see cref="LogixObject"/> instance that represents the parsed value.</returns>
    /// <remarks>
    /// Internally this uses XElement.Parse along with our <see cref="LogixSerializer"/> to instantiate the concrete instance.
    /// This means the user can use the <see cref="LogixParser"/> extensions to also parse XML into strongly typed logix objects.
    /// Also note that since this uses internal XElement and casts the type, this method can throw exceptions for invalid
    /// XML or XML that is parsed to a different type than the one specified here.
    /// </remarks>
    public static TObject Parse(string value)
    {
        var element = XElement.Parse(value);
        return element.Deserialize<TObject>();
    }

    /// <summary>
    /// Attempts to parse the provided string and returned the strongly typed object. If unsuccessful, then this method
    /// returns <c>null</c>.
    /// </summary>
    /// <param name="value">The XML string value to parse.</param>
    /// <returns>A new <see cref="LogixObject"/> instance that represents the parsed value if successful, otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// Internally this uses XElement.Parse along with our <see cref="LogixSerializer"/> to instantiate the concrete instance.
    /// This means the user can use the <see cref="LogixParser"/> extensions to also parse XML into strongly typed logix objects.
    /// Note that this method will just return null if any exception is caught. This could be for invalid XML formats
    /// of invalid type casts.
    /// </remarks>
    public static TObject? TryParse(string? value)
    {
        if (value is null || value.IsEmpty()) //this satisfies the .NET 2.0 compiler warnings about null.
            return null;

        var trimmed = value.Trim();
        if (trimmed.Length == 0 || trimmed[0] != '<') return null;

        try
        {
            var element = XElement.Parse(trimmed);
            return element.Deserialize<TObject>();
        }
        catch (Exception)
        {
            return null;
        }
    }
}