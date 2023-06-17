using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Extensions;

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
    /// <c>LogixTypeName</c> of the entity. 
    /// </summary>
    protected LogixElement()
    {
        Element = new XElement(typeof(TElement).LogixTypeName());
    }

    /// <summary>
    /// Initialized a new <see cref="LogixElement{TEntity}"/> with the provided <see cref="XElement"/>
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
    /// <exception cref="InvalidOperationException">The object being cloned does not have a constructor accepting a single <see cref="XElement"/> argument.</exception>
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
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
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
    /// <param name="selector">A selection delegate that allows custom selection of a attribute relative to the entity element.
    /// Use this to reach up or down the element hierarchy for different properties</param>
    /// <typeparam name="T">The return type of the value.</typeparam>
    /// <returns>
    /// If found, the value of attribute parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetValue<T>(Func<XElement, XAttribute?> selector)
    {
        var value = selector.Invoke(Element)?.Value;
        return value is not null ? value.Parse<T>() : default;
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
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected T? GetProperty<T>([CallerMemberName] string? name = null)
    {
        var value = Element.Element(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets a child <see cref="LogixContainer{TEntity}"/> with the specified element name, representing the root of a
    /// collection of contained entities.
    /// </summary>
    /// <param name="name">The name of the child container collection (e.g. Members).</param>
    /// <typeparam name="TChild">The child entity type.</typeparam>
    /// <returns>A <see cref="LogixContainer{TEntity}"/> containing all the child entities of the type.</returns>
    /// <exception cref="InvalidOperationException">A child element with <c>name</c> does not exist.</exception>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected LogixContainer<TChild> GetContainer<TChild>([CallerMemberName] string? name = null)
        where TChild : LogixElement<TChild>
    {
        var container = Element.Element(name);

        if (container is null) throw new InvalidOperationException($"No container with name {name} exists.");

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
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetValue<T>(T value, [CallerMemberName] string? name = null)
    {
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
        
        Element.SetElementValue(name, new XCData(value.ToString()));
    }

    /// <summary>
    /// Sets the value of a child container, adds a child container, or removes a child container.
    /// </summary>
    /// <param name="value">The <see cref="LogixContainer{TComponent}"/> value to set.</param>
    /// <param name="name">The name of the child container collection (e.g. Members).</param>
    /// <typeparam name="TChild">The container type parameter.</typeparam>
    /// <remarks>
    /// This method it only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming its the name matches the underlying element property).
    /// </remarks>
    protected void SetContainer<TChild>(IEnumerable<TChild>? value, [CallerMemberName] string? name = null)
        where TChild : LogixElement<TChild>
    {
        if (value is null)
        {
            Element.Element(name)?.Remove();
            return;
        }

        var container = new XElement(name);
        container.Add(value.Select(e => e.Serialize()));

        var existing = Element.Element(name);

        if (existing is null)
        {
            Element.Add(container);
            return;
        }

        existing.ReplaceWith(container);
    }

    /// <summary>
    /// Gets the date/time value of the specified attribute name from the current element if it exists.
    /// If the attribute does not exist, returns default value
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy' which is a typical L5X date time format.</param>
    /// <param name="name">The name of the date time attribute.</param>
    /// /// <remarks>
    /// This is a specialized helper since the date time formats are different for different component
    /// properties, we need to allow that to be specified.
    /// </remarks>
    protected void SetDateTime(DateTime value, string? format = null, [CallerMemberName] string? name = null)
    {
        format ??= "ddd MMM d HH:mm:ss yyyy";
        var formatted = value.ToString(format);
        Element.SetAttributeValue(name, formatted);
    }
}