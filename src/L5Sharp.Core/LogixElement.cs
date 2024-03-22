using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace L5Sharp.Core;

/// <summary>
/// A base class for all types that can be serialized and deserialized from a L5X file. This abstract class enforces
/// the <see cref="ILogixSerializable"/> interface and a constructor taking a <see cref="XElement"/> for initialization
/// of and underlying element object. Deriving classes will have access to the underlying element and
/// methods for easily getting and setting data. Implementing classes need to also provide at least a constructor taking
/// a single <see cref="XElement"/> and pass it to the base constructor to be deserializable by the library.
/// </summary>
[PublicAPI]
public abstract class LogixElement : ILogixSerializable, ILogixParsable<LogixElement>
{
    /// <summary>
    /// Creates a new default <see cref="LogixElement"/> initialized with an <see cref="XElement"/> having the
    /// L5XType name of the element. 
    /// </summary>
    protected LogixElement()
    {
        Element = new XElement(GetType().L5XType());
    }

    /// <summary>
    /// Initializes a new <see cref="LogixElement"/> with the provided <see cref="XElement"/>
    /// </summary>
    /// <param name="element">The L5X <see cref="XElement"/> to initialize the entity with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    /// <exception cref="ArgumentException"><c>element</c> name does not match any configured L5XType for this class
    /// type. These are defined via the <see cref="L5XTypeAttribute"/> on the derived classes.</exception>
    protected LogixElement(XElement element)
    {
        Element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// The underlying <see cref="XElement"/> representing the backing data for the entity. Use this object to store
    /// and retrieve data for the component. This property is the basis for serialization and deserialization of
    /// L5X data.
    /// </summary>
    protected readonly XElement Element;

    /// <summary>
    /// Indicates whether this element is attached to a L5X document.
    /// </summary>
    /// <value><c>true</c> if this is an attached element; Otherwise, <c>false</c>.</value>
    /// <remarks>
    /// This simply looks to see if the element has a ancestor with the root RSLogix5000Content element or not.
    /// If so we will assume this element is attached to an overall L5X document.
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
    /// Returns the name of the L5XType for this <see cref="LogixElement"/> object.
    /// </summary>
    /// <returns>A <see cref="string"/> representing the name of the L5X element type.</returns>
    /// <remarks>
    /// The "L5XType" is nothing more than the name of the underlying <see cref="XElement"/> for the object.
    /// Most L5X element names correspond to a class type of the library with the same or similar name.
    /// However, each class type in this library can also support multiple element types (e.g. Tag/LocalTag/ConfigTag).
    /// This property will indicate the actual type of the underlying element as opposed to the actual class type of
    /// the object.
    /// </remarks>
    public string L5XType => Element.Name.LocalName;

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
    /// <c>Tag</c> and <c>Routine</c> components.
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
    /// Adds a new element of the same type directly after this element in the L5X document.
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
    public void AddAfter(LogixElement element)
    {
        if (element is null) throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException(
                "Can only perform operation for L5X attached elements. Add this element to the logix content before invoking.");

        if (element.L5XType != L5XType)
        {
            element = element.Convert(L5XType);
        }

        Element.AddAfterSelf(element.Serialize());
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
    public void AddBefore(LogixElement element)
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
    /// Returns a new deep cloned instance of the current type.
    /// </summary>
    /// <returns>A new <see cref="LogixElement"/> type with the same property values.</returns>
    /// <exception cref="InvalidOperationException">The object being cloned does not have a constructor accepting a
    /// single <see cref="XElement"/> argument.</exception>
    /// <remarks>This method will simply deserialize a new instance using the current underlying element data.</remarks>
    public LogixElement Clone() => new XElement(Element).Deserialize();

    /// <summary>
    /// Returns a new deep cloned instance as the specified <see cref="LogixElement"/> type.
    /// </summary>
    /// <typeparam name="TElement">The <see cref="LogixElement"/> type to cast to.</typeparam>
    /// <returns>A new instance of the specified element type with the same property values.</returns>
    /// <exception cref="InvalidOperationException">The object being cloned does not have a constructor accepting a
    /// single <see cref="XElement"/> argument.</exception>
    /// <exception cref="InvalidCastException">The deserialized type can not be cast to the specified generic type parameter.</exception>
    /// <remarks>This method will simply deserialize a new instance using the current underlying element data.</remarks>
    public TElement Clone<TElement>() where TElement : LogixElement => new XElement(Element).Deserialize<TElement>();

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
    public LogixElement Convert(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentException("Type name can not be null or empty to perform conversion", nameof(typeName));

        if (!GetType().L5XTypes().Contains(typeName))
            throw new InvalidOperationException($"Can not convert element type '{L5XType}' to type '{typeName}'.");

        if (L5XType == typeName) return this;

        Element.Name = typeName;
        return Element.Deserialize();
    }

    /// <summary>
    /// Converts this element to the specified element type name.
    /// </summary>
    /// <param name="typeName">The name of the element type to convert this logix element to. This will default to the
    /// first configured L5XType of the specified logix type parameter, but can optionally be provided if the type
    /// to convert to is not the default L5XType name.</param>
    /// <typeparam name="TElement">The <see cref="LogixElement"/> type to convert this element type to.</typeparam>
    /// <returns>A new <see cref="LogixElement"/> of the specified generic type with the converted element name.</returns>
    /// <exception cref="InvalidOperationException">The specified type name is not supported by this class type.</exception>
    /// <remarks>
    /// This simply updates the underlying element name to the provided name if it is a supported type name for this
    /// logix element class type, which is configured via the <see cref="L5XTypeAttribute"/> for the derived type. This
    /// is primarily needed so we can ensure adding elements of types that support multiple element names can be updated
    /// to the correct element name and ensure a proper L5X sequence within the document. For example, adding a <c>Tag</c>
    /// to a collection of AOI <c>LocalTags</c> needs the underlying element name updated to <c>LocalTag</c> in that case.
    /// </remarks>
    public TElement Convert<TElement>(string? typeName = null) where TElement : LogixElement
    {
        typeName ??= typeof(TElement).L5XType();

        if (!GetType().L5XTypes().Contains(typeName))
            throw new InvalidOperationException($"Can not convert element type '{L5XType}' to type '{typeName}'.");

        if (L5XType == typeName) return (TElement)this;

        Element.Name = typeName;
        return Element.Deserialize<TElement>();
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
    public void Replace(LogixElement element)
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
    /// Returns the underlying <see cref="XElement"/> for the <see cref="LogixElement"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> representing the serialized logix element.</returns>
    /// <remarks>
    /// <para>
    /// All logix elements are backed by an underlying <see cref="XElement"/> through which derived classes
    /// get and set properties. This means all classes in this library can be viewed as wrapper around an
    /// <see cref="XElement"/> or segment of XMl, and use deferred execution for retrieving and setting data.
    /// </para>
    /// <para>
    /// This method exposes the underlying element for extension and serialization purposes.
    /// Take care not to mutate the underlying element in a way that makes the schema invalid and non-importable.
    /// </para>
    /// </remarks>
    public virtual XElement Serialize() => Element;
    
    /// <inheritdoc />
    public static LogixElement Parse(string value)
    {
        var element = XElement.Parse(value);
        return element.Deserialize();
    }

    /// <inheritdoc />
    public static LogixElement? TryParse(string? value)
    {
        if (string.IsNullOrEmpty(value)) return default;
        var element = XElement.Parse(value);
        return element.Deserialize();
    }

    /// <summary>
    /// Gets the value of the specified attribute name from the element parsed as the specified generic type parameter if it exists.
    /// If the attribute does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>
    /// If found, the value of attribute parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetValue<T>([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets the value of the selected attribute parsed as the specified generic type parameter if it exists.
    /// If the attribute does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="selector">A selection delegate that allows custom selection of a element relative to <see cref="Element"/>.
    /// Use this to reach down the element hierarchy for nested values.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>
    /// If found, the value of attribute parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetValue<T>(Func<XElement, XElement?> selector, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = selector.Invoke(Element)?.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets the value of a child element attribute parsed as the specified generic type parameter if it exists.
    /// If the attribute does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="child">The name of the child element containing the attribute value to retrieve.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>
    /// If found, the value of attribute parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetValue<T>(XName child, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Element(child)?.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets the value of the specified attribute name from the element parsed as the specified generic type parameter.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>The value of attribute parsed as the generic type parameter.</returns>
    /// <exception cref="InvalidOperationException">No attribute with the provided name was found on <see cref="Element"/>.</exception>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T GetRequiredValue<T>([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : throw Element.L5XError(name);
    }

    /// <summary>
    /// Gets the value of the specified child element parsed as the specified generic type parameter if it exists.
    /// If the element does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="name">The name of the child element.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>
    /// If found, the value of child element parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetProperty<T>([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Element(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets a immediate child element of the specified member name if it exists and deserializes it as the
    /// specific generic type parameter. If the element does not exist, returns <c>default</c>.
    /// </summary>
    /// <param name="name">The name of the child element.</param>
    /// <typeparam name="T">The return type of the element.</typeparam>
    /// <returns>
    /// If found, the value of child element deserialized as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible for derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetComplex<T>([CallerMemberName] string? name = null) where T : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        return Element.Element(name)?.Deserialize<T>();
    }

    /// <summary>
    /// Gets a child <see cref="LogixContainer{TEntity}"/> with the specified element name, representing the root of a
    /// collection of contained elements.
    /// </summary>
    /// <param name="name">The name of the child container collection (e.g. Members).</param>
    /// <typeparam name="TChild">The child element type.</typeparam>
    /// <returns>A <see cref="LogixContainer{TEntity}"/> containing all the child elements of the specified type.</returns>
    /// <exception cref="InvalidOperationException">A child element with <c>name</c> does not exist.</exception>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected LogixContainer<TChild> GetContainer<TChild>([CallerMemberName] string? name = null)
        where TChild : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var container = Element.Element(name);
        if (container is null) throw Element.L5XError(name);
        return new LogixContainer<TChild>(container);
    }

    /// <summary>
    /// Gets the first parent element of the current underlying element object with the specified name, and returns the
    /// a new deserialized instance of the parent type if found. If not found, returns <c>null</c>.
    /// </summary>
    /// <param name="ancestor">The name of the parent element to return. If not provided will use the default configured
    /// L5XType for the specified element type.</param>
    /// <typeparam name="TElement">The element type of the parent to return.</typeparam>
    /// <returns>A <see cref="LogixElement"/> representing the specified parent element if found;
    /// Otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// This makes getting parent types more concise for derived element types. If the element is not attached
    /// to a <c>L5X</c> document then this will return null. Note that we only get parent but don't set it. A parent is
    /// defined by adding a given logix element to the corresponding parent logix container.
    /// </remarks>
    protected TElement? GetAncestor<TElement>(string? ancestor = null) where TElement : LogixElement
    {
        ancestor ??= typeof(TElement).L5XType();
        return Element.Ancestors(ancestor).FirstOrDefault()?.Deserialize<TElement>();
    }

    /// <summary>
    /// Gets the date/time value of the specified attribute name from the current element if it exists.
    /// If the attribute does not exist, returns default value.
    /// </summary>
    /// <param name="name">The name of the date time attribute.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy' which is a typical L5X date time format.</param>
    /// <returns>The parsed <see cref="DateTime"/> value of the attribute.</returns>
    /// <remarks>
    /// This is a specialized helper since the date time formats are different for different component
    /// properties, we need to allow that to be specified.
    /// </remarks>
    protected DateTime? GetDateTime(string? format = null, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        format ??= "ddd MMM d HH:mm:ss yyyy";

        var attribute = Element.Attribute(name);

        return attribute is not null
            ? DateTime.ParseExact(attribute.Value, format, CultureInfo.CurrentCulture)
            : null;
    }

    /// <summary>
    /// Sets the value of an attribute, adds an attribute, or removes an attribute.
    /// </summary>
    /// <param name="name">The name of the attribute to set.</param>
    /// <param name="value">The value to assign to the attribute. The attribute is removed if the value is null.
    /// Otherwise, the value is converted to its string representation and assigned to the Value property of the attribute.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetValue<T>(T? value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        Element.SetAttributeValue(name, value);
    }

    /// <summary>
    /// Sets the value of an attribute, adds an attribute, or removes an attribute for a element obtained using the
    /// provided selector delegate.
    /// </summary>
    /// <param name="value">
    /// The value to assign to the attribute. The attribute is removed if the value is null.
    /// Otherwise, the value is converted to its string representation and assigned to the Value property of the attribute.
    /// </param>
    /// <param name="selector">A selection delegate that allows custom selection of a element relative to <see cref="Element"/>.
    /// Use this to reach down the element hierarchy for nested values.</param>
    /// <param name="name">The name of the attribute to set.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// This method helps make getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetValue<T>(T? value, Func<XElement, XElement?> selector, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = selector.Invoke(Element);
        if (element is null) throw Element.L5XError(name);
        element.SetAttributeValue(name, value);
    }

    /// <summary>
    /// Sets the value of an attribute, adds an attribute, or removes an attribute for a nested element
    /// specified by provided element name.
    /// </summary>
    /// <param name="name">The name of the attribute to set.</param>
    /// <param name="child">The name of the child <see cref="XElement"/> for which to set the attribute.
    /// If the element does not exist and attribute is not null, will create the element and add to the parent <see cref="Element"/>.</param>
    /// <param name="value">The value to assign to the attribute. The attribute is removed if the value is null.
    /// Otherwise, the value is converted to its string representation and assigned to the Value property of the attribute.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetValue<T>(T? value, XName child, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        if (value is null)
        {
            Element.Element(child)?.Attribute(name)?.Remove();
            return;
        }

        var element = Element.Element(child);
        if (element is null)
        {
            element = new XElement(child);
            Element.Add(element);
        }

        element.SetAttributeValue(name, value);
    }

    /// <summary>
    /// Sets or adds the value of an attribute on the underlying element.
    /// </summary>
    /// <param name="name">The name of the attribute to set.</param>
    /// <param name="value">The value to assign to the attribute. If null, an exception will be thrown.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// This method will throw an exception if the <c>value</c> is null.
    /// </remarks>
    protected void SetRequiredValue<T>(T value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        if (value is null)
            throw new ArgumentNullException(nameof(value), $"Property {name} can not be null.");

        Element.SetAttributeValue(name, value);
    }

    /// <summary>
    /// Sets the value of a child element, adds a child element, or removes a child element.
    /// </summary>
    /// <param name="name">The name of the element to set.</param>
    /// <param name="value">The value to assign to the child element. The child element is removed if the value is null.
    /// Otherwise, the value is converted to its string representation, wrapped in a <see cref="XCData"/> object,
    /// and assigned to the Value property of the child element.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetProperty<T>(T value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = Element.Element(name);

        if (value is null)
        {
            element?.Remove();
            return;
        }

        var property = value.ToString() ?? throw new ArgumentException("Property value can not be null", nameof(value));

        if (element is null)
        {
            Element.Add(new XElement(name, new XCData(property)));
            return;
        }

        element.ReplaceWith(new XElement(name, new XCData(property)));
    }
    
    /// <summary>
    /// Adds, updates, or removes the first child element with the provided value type object.
    /// </summary>
    /// <param name="name">The name of the property element to add, update, or remove.</param>
    /// <param name="value">The value to assign to the child element. The child element is removed if the value is null.
    /// Otherwise, the value is converted to its string representation, wrapped in a <see cref="XCData"/> object,
    /// and assigned to the Value property of the child element.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// <para>
    /// This method will always set the first child element directly under the element for which it is called. This is
    /// important for various element types as they need to ensure the the order of child elements or properties to
    /// be correctly imported.
    /// </para>
    /// <para>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </para>
    /// </remarks>
    protected void SetFirstProperty<T>(T value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var property = value?.ToString();
        var element = Element.Element(name);

        if (property is null)
        {
            element?.Remove();
            return;
        }

        if (element is null)
        {
            Element.AddFirst(new XElement(name, new XCData(property)));
            return;
        }

        element.ReplaceWith(new XElement(name, new XCData(property)));
    }
    

    /// <summary>
    /// Sets the complex type object of a child element, adds a child element, or removes a child element.
    /// </summary>
    /// <param name="name">The name of the element to set.</param>
    /// <param name="value">The complex type to assign to the child element.
    /// The child element is removed if the value is null.
    /// Otherwise, the value is serialized and added as a child element to the current type's element.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetComplex<T>(T? value, [CallerMemberName] string? name = null) where T : ILogixSerializable
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = Element.Element(name);

        if (value is null)
        {
            element?.Remove();
            return;
        }

        if (element is null)
        {
            Element.Add(value.Serialize());
            return;
        }

        element.ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Sets the value of a child container, adds a child container, or removes a child container.
    /// </summary>
    /// <param name="value">The <see cref="LogixContainer{TComponent}"/> value to set. The child container is removed
    /// if the value is null. Otherwise, the value is serialized and added (or replaces the existing) to underlying parent element.</param>
    /// <param name="name">The name of the child container collection (e.g. Members).</param>
    /// <typeparam name="TChild">The container type parameter.</typeparam>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetContainer<TChild>(LogixContainer<TChild>? value, [CallerMemberName] string? name = null)
        where TChild : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = Element.Element(name);

        if (value is null)
        {
            element?.Remove();
            return;
        }

        if (element is null)
        {
            Element.Add(value.Serialize());
            return;
        }

        element.ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Sets the date/time value of a attribute, adds a attribute, or removes a attribute if null.
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy' which is a typical L5X date time format.</param>
    /// <param name="name">The name of the date time attribute.</param>
    /// <remarks>
    /// This is a specialized helper since the date time formats are different for different component
    /// properties, we should allow that to be specified.
    /// </remarks>
    protected void SetDateTime(DateTime? value, string? format = null, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        if (value is null)
        {
            Element.Attribute(name)?.Remove();
            return;
        }

        format ??= "ddd MMM d HH:mm:ss yyyy";
        var formatted = value.Value.ToString(format);
        Element.SetAttributeValue(name, formatted);
    }

    /// <summary>
    /// Adds, removes, or updates the common logix description child element on the current underlying element object.
    /// If null, will remove the child element. If not null, will either add as the first child element or replace the
    /// existing child element.
    /// </summary>
    /// <param name="value">The description value to set.</param>
    /// <remarks>
    /// This is a specialized helper to make setting the description value as concise as possible for derived
    /// classes. Many logix elements will have a description element.
    /// </remarks>
    protected void SetDescription(string? value = null)
    {
        if (value is null)
        {
            Element.Element(L5XName.Description)?.Remove();
            return;
        }

        var description = Element.Element(L5XName.Description);

        if (description is null)
        {
            Element.AddFirst(new XElement(L5XName.Description, new XCData(value)));
            return;
        }

        description.ReplaceWith(new XElement(L5XName.Description, new XCData(value)));
    }
}