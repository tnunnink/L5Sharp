using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp;

/// <summary>
/// A base class for all Logix types that can be serialized and deserialized from a L5X file. This abstract class enforces
/// the <see cref="ILogixSerializable"/> interface and a constructor taking a <see cref="XElement"/> for initialization
/// from the L5X.
/// </summary>
public abstract class LogixEntity<TEntity> : ILogixSerializable where TEntity : LogixEntity<TEntity>
{
    /// <summary>
    /// The underlying <see cref="XElement"/> representing the backing data for the entity. Use this object to store
    /// and retrieve data for the component. This property is the basis for serialization and deserialization of
    /// L5X data.
    /// </summary>
    protected readonly XElement Element;

    /// <summary>
    /// A default <see cref="XElement"/> to initialize <see cref="Element"/> when using the default parameterless
    /// constructor. This ensures we generate some stand in element to update as properties are set or updated. This
    /// property simply points to the <see cref="DefaultElement"/> method which can be overriden so that deriving classes
    /// can further define what the default element should look like.
    /// </summary>
    private XElement Default => DefaultElement();

    /// <summary>
    /// Creates a new default <see cref="LogixEntity{TEntity}"/> initialized with the <see cref="XElement"/> returned
    /// by <see cref="DefaultElement"/>.
    /// </summary>
    protected LogixEntity()
    {
        Element = Default;
    }

    /// <summary>
    /// Initialized a new <see cref="LogixEntity{TEntity}"/> with the provided <see cref="XElement"/>
    /// </summary>
    /// <param name="element">The L5X <see cref="XElement"/> to initialize the entity with.</param>
    protected LogixEntity(XElement element)
    {
        Element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// Returns the underlying <see cref="XElement"/> for the <see cref="LogixEntity{TEntity}"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> representing the serialized entity.</returns>
    public virtual XElement Serialize() => Element;

    /// <summary>
    /// Returns a new deep cloned instance of the current type.
    /// </summary>
    /// <returns>A new instance of the specified entity type with the same property values.</returns>
    /// <exception cref="InvalidOperationException">The object being cloned does not have a constructor accepting a single <see cref="XElement"/> argument.</exception>
    /// <remarks>This method will simply deserialize a new instance using the current underlying element data.</remarks>
    public virtual TEntity Clone() => LogixSerializer.Deserialize<TEntity>(Element);

    /// <summary>
    /// Generates a default <see cref="XElement"/> to initialize a default instance of an entity.
    /// </summary>
    /// <returns>A <see cref="XElement"/> with default attribute values and elements.</returns>
    protected virtual XElement DefaultElement() => new(typeof(TEntity).LogixTypeName());

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
    protected T? GetProperty<T>([CallerMemberName] string? name = null)
    {
        var value = Element.Element(name)?.Value;
        return value is not null ? value.Parse<T>() : default;
    }

    /// <summary>
    /// Gets the <see cref="ILogixCollection{TComponent}"/> representing the child elements of the specified element
    /// container name.
    /// </summary>
    /// <param name="name">The name of the collection container (e.g. Tags).</param>
    /// <typeparam name="T">The generic component type to return.</typeparam>
    /// <returns>A <see cref="ILogixCollection{TComponent}"/> containing all the child components of the type.</returns>
    /// <exception cref="InvalidOperationException">A child element with <c>name</c> does not exist.</exception>
    protected ILogixCollection<T> GetCollection<T>([CallerMemberName] string? name = null) where T : ILogixComponent
    {
        var container = Element.Element(name);

        if (container is null)
            throw new InvalidOperationException($"No container collection with name {name} exists.");

        return new LogixCollection<T>(container);
    }

    /// <summary>
    /// Gets the date/time value of the specified attribute name from the current element if it exists.
    /// If the attribute does not exist, returns default value
    /// </summary>
    /// <param name="name">The name of the date time attribute.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy' which is a typical L5X date time format.</param>
    /// <returns>The parsed <see cref="DateTime"/> value of the attribute.</returns>
    protected DateTime? GetDateTime(string? format = null, [CallerMemberName] string? name = null)
    {
        format ??= "ddd MMM d HH:mm:ss yyyy";

        var attribute = Element.Attribute(name);

        return attribute is not null
            ? DateTime.ParseExact(attribute.Value, format, CultureInfo.CurrentCulture)
            : default;
    }
    
    /// <summary>
    /// Adds or updates the specified attribute value with the provided value.
    /// </summary>
    /// <param name="name">The attribute name of the value to set.</param>
    /// <param name="value">The value to set.</param>
    /// <typeparam name="T">The value type.</typeparam>
    protected void SetValue<T>(T value, [CallerMemberName] string? name = null)
    {
        Element.SetAttributeValue(name, value);
    }

    /// <summary>
    /// Adds or updates the specified attribute value with the provided value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="selector"></param>
    /// <param name="name"></param>
    /// <typeparam name="T">The value type.</typeparam>
    protected void SetValue<T>(T value, Func<XElement, XAttribute?> selector, [CallerMemberName] string? name = null)
    {
        var attribute = selector.Invoke(Element);
        
        if (attribute is null)
        {
            Element.SetAttributeValue(name, value);
            return;
        }
        
        if (value is null)
        {
            attribute.Remove();
            return;
        }

        attribute.Value = value.ToString();
    }

    /// <summary>
    /// Adds or updates the specified attribute value with the provided value.
    /// </summary>
    /// <param name="name">The attribute name of the value to set.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="setCondition">The optional value predicate on which the value will be set if evaluated to <c>true</c>.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <remarks>
    /// This is just a helper extension to preform the repetitive logic of adding or setting a property of
    /// a component element. This method will remove the attribute if the provided value is null, add or update
    /// otherwise, and only perform any of this if a set condition is specified and satisfied be the value.
    /// Note that this method also calls ToString() on the provided value.
    /// </remarks>
    protected void SetProperty<T>(T value, [CallerMemberName] string? name = null, Predicate<T>? setCondition = null)
    {
        
        if (setCondition?.Invoke(value) == false)
            return;

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

        element.Value = value.ToString();
    }

    /// <summary>
    /// Adds or updates the specified child element container with the provided collection.
    /// </summary>
    /// <param name="value">The <see cref="LogixCollection{TEntity}"/> value to set.</param>
    /// <param name="name">The child element container name.</param>
    /// <typeparam name="T">The collection type parameter.</typeparam>
    /// <remarks>
    /// </remarks>
    protected void SetCollection<T>(IEnumerable<T>? value, [CallerMemberName] string? name = null)
        where T : ILogixComponent, ILogixSerializable
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
    /// <param name="value"></param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy' which is a typical L5X date time format.</param>
    /// <param name="name">The name of the date time attribute.</param>
    /// <returns>The parsed <see cref="DateTime"/> value of the attribute.</returns>
    protected void SetDateTime(DateTime value, string? format = null, [CallerMemberName] string? name = null)
    {
        format ??= "ddd MMM d HH:mm:ss yyyy";

        var attribute = Element.Attribute(name);

        if (attribute is null)
        {
            Element.Add(new XAttribute(name, value.ToString(format)));
            return;
        }

        attribute.Value = value.ToString(format);
    }
}