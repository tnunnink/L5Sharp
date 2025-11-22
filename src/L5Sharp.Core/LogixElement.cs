using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a contract for all elements within the L5X serialization library.
/// Implementing classes serve as representations of specific elements in the L5X schema,
/// providing access to the underlying <see cref="XElement"/> for reading, writing, and serialization of data.
/// </summary>
public interface ILogixElement
{
    /// <summary>
    /// Returns the underlying <see cref="XElement"/> for the <see cref="LogixElement"/>.
    /// </summary>
    /// <returns>A <see cref="XElement"/> representing the serialized logix element.</returns>
    /// <remarks>
    /// <para>
    /// All logix elements are backed by an underlying <see cref="XElement"/> through which derived classes
    /// get and set properties. This means all classes in this library can be viewed as wrapper around an
    /// <see cref="XElement"/> or segment of XML, and use deferred execution for retrieving and setting data.
    /// </para>
    /// <para>
    /// This method exposes the underlying element for extension and serialization purposes.
    /// Take care not to mutate the underlying element in a way that makes the schema invalid and non-importable.
    /// </para>
    /// </remarks>
    XElement Serialize();

    /// <summary>
    /// Tries to retrieve the associated <see cref="L5X"/> document for the current element if available.
    /// </summary>
    /// <param name="document">
    /// When this method returns, contains the <see cref="L5X"/> document associated with the element,
    /// if the operation succeeded. Otherwise, it will be null.</param>
    /// <returns>True if the <see cref="L5X"/> document was successfully retrieved; otherwise, false.</returns>
    bool TryGetDocument(out L5X document);

    /// <summary>
    /// Casts this element instance to the specified type TElement.
    /// </summary>
    /// <typeparam name="TElement">The type to which you want to cast.</typeparam>
    /// <returns>The current instance, cast to the specified type.</returns>
    /// <exception cref="InvalidCastException">Thrown if the cast isn't valid.</exception>    
    TElement As<TElement>() where TElement : ILogixElement;

    /// <summary>
    /// Converts the current <see cref="LogixElement"/> instance to the specified type parameter.
    /// </summary>
    /// <typeparam name="TElement">The type of <see cref="ILogixElement"/> to which the instance should be converted.</typeparam>
    /// <returns>An instance of the specified type parameter <typeparamref name="TElement"/> representing the converted element.</returns>
    TElement To<TElement>() where TElement : ILogixElement;

    /// <summary>
    /// Creates a new instance of the current <see cref="ILogixElement"/> that is a deep clone of this instance.
    /// </summary>
    /// <returns>A new <see cref="ILogixElement"/> instance that is a deep copy of the current instance.</returns>
    /// <remarks>
    /// This method performs a deep clone of the underlying element, ensuring that all data structures and properties
    /// are retained in the cloned object. The returned object is of the same type as the original but does not share
    /// any references with it, meaning modifications to the clone will not affect the original instance.
    /// </remarks>
    ILogixElement Clone();

    /// <summary>
    /// Determines if the provided element is structurally or deeply equal to another by performing a compare
    /// of the underlying XML data for the objects. 
    /// </summary>
    /// <param name="other">The other <see cref="ILogixElement"/> to compare.</param>
    /// <returns><c>ture</c> if the objects are equivalent; Otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This internally compares the underlying XML nodes of the two elements to determine if all their
    /// elements, attributes, and values are equal. This includes all nested or descendant elements (i.e., it
    /// compares the entire XML structure).
    /// </remarks>
    bool EquivalentTo(ILogixElement other);
}

/// <summary>
/// A base class for all types that can be serialized and deserialized from a L5X file.
/// Deriving classes will have access to the underlying element and  methods for easily getting and setting data.
/// Implementing classes should provide a constructor taking a single <see cref="XElement"/> and pass it to the
/// base constructor to be deserializable by the library.
/// </summary>
public abstract class LogixElement : ILogixElement
{
    /// <summary>
    /// Creates a new <see cref="LogixElement"/> initialized with an <see cref="XElement"/> having the
    /// provided element name.
    /// </summary>
    protected LogixElement(string name)
    {
        Element = new XElement(name);
    }

    /// <summary>
    /// Initializes a new <see cref="LogixElement"/> with the provided <see cref="XElement"/>
    /// </summary>
    /// <param name="element">The L5X <see cref="XElement"/> to initialize the entity with.</param>
    /// <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
    protected LogixElement(XElement element)
    {
        Element = element ?? throw new ArgumentNullException(nameof(element));
    }

    /// <summary>
    /// The underlying <see cref="XElement"/> representing the backing data for the object. Use this to store
    /// and retrieve data for the object. This property is the basis for serialization and deserialization of
    /// L5X data.
    /// </summary>
    protected readonly XElement Element;

    /// <summary>
    /// A list containing the order of any child elements for the current logix element.
    /// By default, this is an empty collection, but derived classes can override this. When this collection contains names,
    /// any adding of properties, containers, or complex types will then use this list to sort the order of the elements
    /// in the underlying parent element. 
    /// </summary>
    protected virtual List<string> ElementOrder { get; } = [];

    /// <inheritdoc />
    public virtual XElement Serialize() => Element;

    /// <inheritdoc />
    public bool TryGetDocument(out L5X document)
    {
        document = null!;

        var l5X = Element.Ancestors(L5XName.RSLogix5000Content).FirstOrDefault()?.Annotation<L5X>();
        if (l5X is null) return false;

        document = l5X;
        return true;
    }

    /// <inheritdoc />
    public TElement As<TElement>() where TElement : ILogixElement => (TElement)(object)this;

    /// <inheritdoc />
    public TElement To<TElement>() where TElement : ILogixElement => Element.Deserialize<TElement>();

    /// <inheritdoc />
    public ILogixElement Clone() => new XElement(Element).Deserialize();

    /// <inheritdoc />
    public bool EquivalentTo(ILogixElement other) => XNode.DeepEquals(Serialize(), other.Serialize());

    /// <summary>
    /// Gets the value of the specified attribute name from the element parsed as the specified generic type parameter if it exists.
    /// If the attribute does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>
    /// If found, the value of attribute parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming it's the name matches the underlying element property).
    /// </remarks>
    protected string? GetValue([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        return Element.Attribute(name)?.Value;
    }

    /// <summary>
    /// Retrieves the value of the specified attribute or element from the current <see cref="XElement"/> and parses it into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the value should be parsed.</typeparam>
    /// <param name="parser">A function that converts a string to the desired type <typeparamref name="T"/>.</param>
    /// <param name="name">The name of the attribute or element to retrieve. Defaults to the name of the calling member if not specified.</param>
    /// <returns>The parsed value of the specified attribute or element if found, otherwise the default value for <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="parser"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="name"/> is null or an empty string.</exception>
    protected T? GetValue<T>(Func<string, T> parser, [CallerMemberName] string? name = null)
    {
        if (parser is null)
            throw new ArgumentNullException(nameof(parser));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value;

        return value is not null ? parser(value) : default;
    }

    /// <summary>
    /// Gets the value of the specified attribute name from the element parsed as the specified generic type parameter.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>The value of attribute parsed as the generic type parameter.</returns>
    /// <exception cref="InvalidOperationException">No attribute with the provided name was found on <see cref="Element"/>.</exception>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected string GetRequiredValue([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        return Element.Attribute(name)?.Value ?? throw Element.L5XError(name);
    }

    /// <summary>
    /// Retrieves a required attribute value from the current element and parses it using the provided parser function.
    /// </summary>
    /// <typeparam name="T">The type of the value to be parsed and returned.</typeparam>
    /// <param name="parser">The function used to parse the string value into the specified type <typeparamref name="T"/>.</param>
    /// <param name="name">The name of the attribute to retrieve. Defaults to the calling member's name if not provided.</param>
    /// <returns>The parsed value of the specified type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the attribute name is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the required attribute is not found in the element.</exception>
    protected T GetRequiredValue<T>(Func<string, T> parser, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value;

        return value is not null ? parser(value) : throw Element.L5XError(name);
    }

    /// <summary>
    /// Gets a collection of values for the specified attribute name parsed as the specified generic type parameter if it exists.
    /// If the element does not exist, it returns an empty collection of the generic type parameters.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="separator">The value separator character. Default is a space character.</param>
    /// <returns>
    /// If found, all values of the attribute split on the specified separator and parsed as the generic type parameter.
    /// If not found, returns an empty collection.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting attributes with a collection of values as a single string with a certain separator
    /// character as concise as possible for derived classes. This method is added here since only types like <see cref="Block"/>
    /// are using this method overload.
    /// </remarks>
    protected IEnumerable<string> GetValues(char separator = ' ', [CallerMemberName] string? name = null)
    {
        if (name is null || name.IsEmpty())
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value;

        return value is not null ? value.Split(separator, StringSplitOptions.RemoveEmptyEntries) : [];
    }

    /// <summary>
    /// Gets the value of the specified child element parsed as the specified generic type parameter if it exists.
    /// If the element does not exist, returns <c>default</c> value of the generic type parameter.
    /// </summary>
    /// <param name="name">The name of the child element.</param>
    /// <returns>
    /// If found, the value of a child element parsed as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected string? GetProperty([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        return Element.Element(name)?.Value;
    }

    /// <summary>
    /// Gets an immediate child element of the specified member name if it exists and deserializes it as the
    /// specific generic type parameter. If the element does not exist, returns <c>default</c>.
    /// </summary>
    /// <param name="name">The name of the child element.</param>
    /// <typeparam name="TElement">The return type of the element.</typeparam>
    /// <returns>
    /// If found, the value of a child element deserialized as the generic type parameter.
    /// If not found, returns <c>default</c>.
    /// </returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible for derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected TElement? GetComplex<TElement>([CallerMemberName] string? name = null) where TElement : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        return Element.Element(name)?.Deserialize<TElement>();
    }

    /// <summary>
    /// Gets a child <see cref="LogixContainer{TEntity}"/> with the specified element name, representing the root of a
    /// collection of contained elements.
    /// </summary>
    /// <param name="name">The name of the child container collection (e.g., Members).</param>
    /// <typeparam name="TElement">The child element type.</typeparam>
    /// <returns>A <see cref="LogixContainer{TEntity}"/> containing all the child elements of the specified type.</returns>
    /// <exception cref="InvalidOperationException">A child element with <c>name</c> does not exist.</exception>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected LogixContainer<TElement> GetContainer<TElement>([CallerMemberName] string? name = null)
        where TElement : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var container = Element.Element(name);

        if (container is null)
            throw Element.L5XError(name);

        return new LogixContainer<TElement>(container);
    }

    /// <summary>
    /// Tries to get a child <see cref="LogixContainer{TEntity}"/> with the specified element name, representing
    /// the root of a collection of contained elements. Returns null if not found (instead of throwing an exception).
    /// </summary>
    /// <param name="name">The name of the child container collection (e.g., Members).</param>
    /// <typeparam name="TElement">The child element type.</typeparam>
    /// <returns>A <see cref="LogixContainer{TEntity}"/> containing all the child elements of the specified type.</returns>
    /// <remarks>
    /// This method makes getting/setting data on <see cref="Element"/> as concise as possible from derived classes.
    /// This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving classes don't have to specify
    /// the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected LogixContainer<TElement>? TryGetContainer<TElement>([CallerMemberName] string? name = null)
        where TElement : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var container = Element.Element(name);

        return container is not null ? new LogixContainer<TElement>(container) : null;
    }

    /// <summary>
    /// Gets the first parent element of the current underlying element object with the specified name and returns 
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
        ancestor ??= typeof(TElement).Name;
        return Element.Ancestors(ancestor).FirstOrDefault()?.Deserialize<TElement>();
    }


    /// <summary>
    /// Retrieves a boolean value from the element attribute specified by the property name.
    /// </summary>
    /// <param name="name">The name of the attribute to retrieve the boolean value from. Defaults to the caller's name if not provided.</param>
    /// <returns>
    /// A nullable boolean value parsed from the attribute. Returns true for "1" or "true",
    /// false for "0" or "false", or null if the attribute is not found.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if the provided name is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the attribute value cannot be parsed into a boolean type.
    /// </exception>
    protected bool GetBool([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value.ToLower();

        return value switch
        {
            "1" => true,
            "0" => false,
            "true" => true,
            "false" => false,
            _ => throw new ArgumentOutOfRangeException($"Value '{value}' could not be parsed to type '{typeof(bool)}'")
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    protected bool? TryGetBool([CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var value = Element.Attribute(name)?.Value.ToLower();

        return value switch
        {
            "1" => true,
            "0" => false,
            "true" => true,
            "false" => false,
            _ => null
        };
    }

    /// <summary>
    /// Gets the date/time value of the specified attribute name from the current element if it exists.
    /// If the attribute does not exist, it returns the default value.
    /// </summary>
    /// <param name="name">The name of the date time attribute.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy,' which is a typical L5X date time format.</param>
    /// <returns>The parsed <see cref="DateTime"/> value of the attribute.</returns>
    /// <remarks>
    /// This is a specialized helper since the date time formats are different for different component
    /// properties, we need to allow that to be specified.
    /// </remarks>
    protected DateTime GetDateTime(string? format = null, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        format ??= "ddd MMM d HH:mm:ss yyyy";

        var attribute = Element.Attribute(name);

        return attribute is not null
            ? DateTime.ParseExact(attribute.Value, format, CultureInfo.CurrentCulture)
            : default;
    }

    /// <summary>
    /// Retrieves a <see cref="Revision"/> from the attributes of the underlying <see cref="XElement"/> based on the provided
    /// major and minor attribute names.
    /// </summary>
    /// <param name="majorName">The name of the attribute representing the major revision number.</param>
    /// <param name="minorName">The name of the attribute representing the minor revision number.</param>
    /// <returns>A <see cref="Revision"/> object containing the major and minor revision numbers parsed from the attributes.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="majorName"/> or <paramref name="minorName"/> is null or empty.</exception>
    protected Revision GetRevision(string majorName, string minorName)
    {
        if (string.IsNullOrEmpty(majorName))
            throw new ArgumentException("majorName can not be null or empty", nameof(majorName));

        if (string.IsNullOrEmpty(minorName))
            throw new ArgumentException("minorName can not be null or empty", nameof(minorName));

        var majorValue = Element.Attribute(majorName)?.Value ?? "0";
        var minorValue = Element.Attribute(minorName)?.Value ?? "0";

        return new Revision(ushort.Parse(majorValue), ushort.Parse(minorValue));
    }

    /// <summary>
    /// Gets the element's <see cref="LogixData"/> representing the tag data structure. 
    /// </summary>
    /// <returns>
    /// A <see cref="LogixData"/> object representing the simple or complex data structure for the element.
    /// </returns>
    /// <remarks>
    /// This is a specialized helper used to get a tag or parameter <see cref="LogixData"/> data structure.
    /// This method will get the first data element with a supported data format and deserialize the object as a
    /// concrete <see cref="LogixData"/> using the logix serializer. This will work for either Data or DefaultData
    /// elements. This helper is meant for <c>Tag</c> and <c>Parameter</c>.
    /// </remarks>
    protected virtual LogixData GetData()
    {
        //This assumes the element is a tag or parameter object and has the child Data element with a supported format.
        var data = Element.Elements().FirstOrDefault(e =>
            DataFormat.Supported.Any(f => f == e.Attribute(L5XName.Format)?.Value));

        //Return that or Null of not found.
        return data is not null ? data.Deserialize<LogixData>() : LogixType.Null;
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
    /// the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected void SetValue<T>(T? value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        Element.SetAttributeValue(name, value);
    }

    /// <summary>
    /// Sets the value of an attribute, adds an attribute, or removes an attribute for a nested element
    /// specified by the provided element name.
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
    /// the property name (assuming the name matches the underlying element property).
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

        if (element is not null)
        {
            element.SetAttributeValue(name, value);
            return;
        }

        element = new XElement(child, new XAttribute(name, value));
        Element.Add(element);
        EnsureOrder();
    }

    /// <summary>
    /// Sets or adds the value of an attribute on the underlying element.
    /// </summary>
    /// <param name="name">The name of the attribute to set.</param>
    /// <param name="value">The value to assign to the attribute. If null, an exception will be thrown.</param>
    /// <typeparam name="T">The value type.</typeparam>
    /// <exception cref="ArgumentNullException"><c>value</c> is null.</exception>
    /// <remarks>
    /// This method is only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming the name matches the underlying element property).
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
    /// <remarks>
    /// This method is only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected void SetProperty(string? value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = Element.Element(name);

        if (value is null)
        {
            element?.Remove();
            return;
        }

        if (element is not null)
        {
            element.ReplaceWith(new XElement(name, new XCData(value)));
            return;
        }

        Element.Add(new XElement(name, new XCData(value)));
        EnsureOrder();
    }

    /// <summary>
    /// Sets the complex type object of a child element, adds a child element, or removes a child element.
    /// </summary>
    /// <param name="name">The name of the element to set.</param>
    /// <param name="value">The complex type to assign to the child element.
    /// The child element is removed if the value is null.
    /// Otherwise, the value is serialized and added as a child element to the current type's element.</param>
    /// <typeparam name="TElement">The value type.</typeparam>
    /// <remarks>
    /// This method is only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected void SetComplex<TElement>(TElement? value, [CallerMemberName] string? name = null)
        where TElement : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = Element.Element(name);

        if (value is null)
        {
            element?.Remove();
            return;
        }

        if (element is not null)
        {
            element.ReplaceWith(value.Serialize());
            return;
        }

        Element.Add(value.Serialize());
        EnsureOrder();
    }

    /// <summary>
    /// Sets the value of a child container, adds a child container, or removes a child container.
    /// </summary>
    /// <param name="value">The <see cref="LogixContainer{TComponent}"/> value to set. The child container is removed
    /// if the value is null. Otherwise, the value is serialized and added (or replaces the existing) to an underlying parent element.</param>
    /// <param name="name">The name of the child container collection (e.g., Members).</param>
    /// <typeparam name="TElement">The container type parameter.</typeparam>
    /// <remarks>
    /// This method is only available to make getting/setting data on <see cref="Element"/> as concise
    /// as possible from derived classes. This method uses the <see cref="CallerMemberNameAttribute"/> so the deriving
    /// classes don't have to specify the property name (assuming the name matches the underlying element property).
    /// </remarks>
    protected void SetContainer<TElement>(LogixContainer<TElement>? value, [CallerMemberName] string? name = null)
        where TElement : LogixElement
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        var element = Element.Element(name);

        if (value is null)
        {
            element?.Remove();
            return;
        }

        if (element is not null)
        {
            element.ReplaceWith(value.Serialize());
            return;
        }

        Element.Add(value.Serialize());
        EnsureOrder();
    }

    /// <summary>
    /// Adds, updates, or removes an attribute with the specified name and value converted to a bit (0/1).
    /// If <paramref name="value"/> is null, this method will remove the attribute. Otherwise, it will add or set the
    /// current value.
    /// </summary>
    /// <param name="value">The value to set true/false, which will be converted to 0/1.</param>
    /// <param name="name">The name of the bit attribute.</param>
    /// <remarks>
    /// This is a specialized helper since Logix uses 0/1 instead of true/false for some properties/attributes.
    /// Properties that need to write a 0/1 to correctly import L5X content should use this method.
    /// </remarks>
    protected void SetBit(bool? value, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        if (value is null)
        {
            Element.Attribute(name)?.Remove();
            return;
        }

        var bit = value is true ? "1" : "0";
        Element.SetAttributeValue(name, bit);
    }

    /// <summary>
    /// Sets the date/time value of an attribute, adds an attribute, or removes an attribute if null.
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="format">The format of the date time.
    /// If not provided will default to 'ddd MMM d HH:mm:ss yyyy,' which is a typical L5X date time format.</param>
    /// <param name="name">The name of the date time attribute.</param>
    /// <remarks>
    /// This is a specialized helper since the date time formats are different for different component
    /// properties, we should allow that to be specified.
    /// </remarks>
    protected void SetDateTime(DateTime value, string? format = null, [CallerMemberName] string? name = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name can not be null or empty", nameof(name));

        format ??= "ddd MMM d HH:mm:ss yyyy";
        var formatted = value.ToString(format);
        Element.SetAttributeValue(name, formatted);
    }

    /// <summary>
    /// Add or updates the child data element with the data of the provided <see cref="LogixData"/> object.
    /// </summary>
    /// <param name="data">The <see cref="LogixData"/> data to update.</param>
    /// <remarks>
    /// This is a specialized helper that will generate the new formatted data element to wrap the provided type element
    /// if needed. It will also determine which XName to use based on whether this is a tag, local tag, or parameter.
    /// This is only intended for use with Tag and Parameter and will completely add or replace the existing root data
    /// element.
    /// </remarks>
    protected virtual void SetData(LogixData? data)
    {
        //Parameter and LocalTag have the element DefaultData instead of Data.
        var name = Element.Name.LocalName is L5XName.Parameter or L5XName.LocalTag ? L5XName.DefaultData : L5XName.Data;

        //Always use our Null type instead of actual null.
        data ??= LogixType.Null;

        var formatted = GenerateDataElement(name, data);

        //Try to get the existing child data element.
        var existing = Element.Elements(name).FirstOrDefault(e =>
            DataFormat.Supported.Any(f => f == e.Attribute(L5XName.Format)?.Value));

        //If existing is null, then add the new data.
        if (existing is null)
        {
            Element.Add(formatted);
            return;
        }

        //If found, then replace the existing element.
        existing.ReplaceWith(formatted);
        return;

        //Local function to generate a new formatted data element for the given type.
        XElement GenerateDataElement(string n, LogixData t)
        {
            //First check for a string type since there is no child element (Data is the element in this case)
            if (t is StringData str) return str.Serialize();

            //All other format types are wrapped in a containing data element with a format attribute.
            var f = new XElement(n, new XAttribute(L5XName.Format, DataFormat.FromData(t)));
            f.Add(t.Serialize());
            return f;
        }
    }

    /// <summary>
    /// Orders the child elements using the configured <see cref="ElementOrder"/> list. If no elements are found, this
    /// will simply return. If derived classes have overriden this collection, then we will join the ordered names
    /// with the current elements and then replace all child nodes with the ordered set.
    /// </summary>
    protected void EnsureOrder()
    {
        if (ElementOrder.Count == 0) return;
        var ordered = ElementOrder.Join(Element.Elements(), n => n, e => e.Name.LocalName, (_, e) => e).ToList();
        Element.ReplaceNodes(ordered);
    }
}