using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace L5Sharp;

/// <summary>
/// A base class for all types that can be serialized and deserialized from a L5X file. This abstract class enforces
/// the <see cref="ILogixSerializable"/> interface and a constructor taking a <see cref="XElement"/> for initialization
/// of and underlying element object. Deriving classes will have access to the underlying element and
/// methods for easily getting and setting data.
/// </summary>
public abstract class LogixElement<TElement> : ILogixSerializable where TElement : LogixElement<TElement>
{
    /// <summary>
    /// Creates a new default <see cref="LogixElement{TEntity}"/> initialized with an <see cref="XElement"/> having the
    /// L5XType name of the element. 
    /// </summary>
    protected LogixElement()
    {
        Element = new XElement(typeof(TElement).L5XType());
    }

    /// <summary>
    /// Initializes a new <see cref="LogixElement{TEntity}"/> with the provided <see cref="XElement"/>
    /// </summary>
    /// <param name="element">The L5X <see cref="XElement"/> to initialize the entity with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
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
    /// Returns the underlying <see cref="XElement"/> for the <see cref="LogixElement{TEntity}"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> representing the serialized entity.</returns>
    public virtual XElement Serialize() => Element;

    /// <summary>
    /// Returns a new deep cloned instance of the current type.
    /// </summary>
    /// <returns>A new instance of the specified entity type with the same property values.</returns>
    /// <exception cref="InvalidOperationException">The object being cloned does not have a constructor accepting a
    /// single <see cref="XElement"/> argument.</exception>
    /// <remarks>This method will simply deserialize a new instance using the current underlying element data.</remarks>
    public TElement Clone() => (TElement)LogixSerializer.Deserialize(GetType(), new XElement(Element));

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
        var value = Element.Element(child)?.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets the value of the specified attribute name from the element parsed as the specified generic type parameter.
    /// If the attribute does not exist, throw a <see cref="L5XException"/>.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>The value of attribute parsed as the generic type parameter.</returns>
    /// <exception cref="L5XException">No attribute with the provided name was found on <see cref="Element"/>.</exception>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T GetRequiredValue<T>([CallerMemberName] string? name = null)
    {
        var value = Element.Attribute(name)?.Value;
        return value is not null ? value.Parse<T>() : throw new L5XException(name!, Element);
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
        var value = Element.Element(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }
    
    /// <summary>
    /// Gets a child <see cref="LogixElement{TElement}"/> deserialized as the specified type if it exists.
    /// If the element does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="name">The name of the child element.</param>
    /// <typeparam name="T">The return type of the element.</typeparam>
    /// <returns>
    /// If found, the value of child element deserialized as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetComplex<T>([CallerMemberName] string? name = null) where T : LogixElement<T>
    {
        var value = Element.Element(name);
        return value is not null ? LogixSerializer.Deserialize<T>(value) : default;
    }

    /// <summary>
    /// Gets a child <see cref="LogixContainer{TEntity}"/> with the specified element name, representing the root of a
    /// collection of contained elements.
    /// </summary>
    /// <param name="name">The name of the child container collection (e.g. Members).</param>
    /// <typeparam name="TChild">The child element type.</typeparam>
    /// <returns>A <see cref="LogixContainer{TEntity}"/> containing all the child elements of the specified type.</returns>
    /// <exception cref="L5XException">A child element with <c>name</c> does not exist.</exception>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected LogixContainer<TChild> GetContainer<TChild>([CallerMemberName] string? name = null)
        where TChild : LogixElement<TChild>
    {
        var container = Element.Element(name);
        if (container is null) throw new L5XException(name!, Element);
        return new LogixContainer<TChild>(container);
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
        format ??= "ddd MMM d HH:mm:ss yyyy";

        var attribute = Element.Attribute(name);

        return attribute is not null
            ? DateTime.ParseExact(attribute.Value, format, CultureInfo.CurrentCulture)
            : default;
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
        var element = selector.Invoke(Element);
        if (element is null) throw new L5XException(name!, Element);
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
        if (value is null)
        {
            Element.Element(name)?.Remove();
            return;
        }

        var element = Element.Element(name);
        if (element is null)
        {
            Element.Add(new XElement(name, new XCData(value.ToString())));
            return;
        }

        element.ReplaceWith(new XElement(name, new XCData(value.ToString())));
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
        where TChild : LogixElement<TChild>
    {
        if (value is null)
        {
            Element.Element(name)?.Remove();
            return;
        }

        var existing = Element.Element(name);
        if (existing is null)
        {
            Element.Add(value.Serialize());
            return;
        }

        existing.ReplaceWith(value.Serialize());
    }

    /// <summary>
    /// Sets the date/time value of a attribute, adds a attribute, or removes a attribute if null.
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy' which is a typical L5X date time format.</param>
    /// <param name="name">The name of the date time attribute.</param>
    /// /// <remarks>
    /// This is a specialized helper since the date time formats are different for different component
    /// properties, we should allow that to be specified.
    /// </remarks>
    protected void SetDateTime(DateTime? value, string? format = null, [CallerMemberName] string? name = null)
    {
        if (value is null)
        {
            Element.Attribute(name)?.Remove();
            return;
        }

        format ??= "ddd MMM d HH:mm:ss yyyy";
        var formatted = value.Value.ToString(format);
        Element.SetAttributeValue(name, formatted);
    }
}