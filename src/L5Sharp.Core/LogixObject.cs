using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a generic interface within the Logix framework, defining operations and behaviors
/// for all Logix object types of type <typeparamref name="TElement"/>.
/// <see cref="ILogixObject{TElement}"/> addes methods for collection mutation from instance objects.
/// This interface should only be implemented by deriving types that appear in a collection or containers.
/// </summary>
/// <typeparam name="TElement">The type of the element derived from <see cref="ILogixElement"/> that this interface operates on.</typeparam>
public interface ILogixObject<TElement> where TElement : ILogixElement
{
    /// <summary>
    /// Creates a deep copy of the current object, preserving all its properties and structure.
    /// </summary>
    /// <returns>
    /// A new instance of the same type with identical content as the original object.
    /// </returns>
    /// <remarks>
    /// This method ensures that changes to the cloned object do not affect the original object and vice versa.
    /// It is used to duplicate Logix objects while maintaining their integrity and associations.
    /// </remarks>
    TElement Clone();

    /// <summary>
    /// Inserts the specified element into the collection immediately after the current element.
    /// </summary>
    /// <param name="element">The element to be added after the current element.</param>
    /// <remarks>
    /// This method modifies the collection by positioning the provided element directly after the
    /// existing element it is called on.
    /// This method requires the element be attached to an <see cref="L5X"/>,
    /// as it will access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    void AddAfter(TElement element);

    /// <summary>
    /// Inserts the specified element directly before the current object in the collection or hierarchy.
    /// </summary>
    /// <param name="element">The element to insert before the current object.</param>
    /// <remarks>
    /// This method modifies the collection by positioning the provided element directly before the
    /// existing element it is called on.
    /// This method requires the element be attached to an <see cref="L5X"/>,
    /// as it will access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    void AddBefore(TElement element);

    /// <summary>
    /// Appends the specified element to the current object's collection of elements.
    /// </summary>
    /// <param name="element">The element to append.</param>
    /// <remarks>
    /// This operation modifies the structure by adding the provided element to the end of the collection.
    /// This method requires the element be attached to an <see cref="L5X"/>,
    /// as it will access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    void Append(TElement element);

    /// <summary>
    /// Creates a duplicate of the current object and allows for modification using a user-defined configuration.
    /// </summary>
    /// <param name="config">
    /// A delegate that specifies the modifications to the duplicate instance. The delegate takes the duplicated
    /// instance as a parameter for configuration.
    /// </param>
    /// <returns>
    /// A new instance of the same type as the current object, with modifications applied as defined
    /// in the provided configuration.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="config"/> parameter is null.</exception>
    /// <remarks>
    /// This method ensures a new instance is created from the original object. Modifications provided
    /// by the configuration delegate are applied to the duplicate, allowing flexible and tailored duplication.
    /// This method will also attempt to add the duplicated instance to the underlying L5X (object collection) if available.
    /// </remarks>
    public TElement Duplicate(Action<TElement> config);

    /// <summary>
    /// Replaces the current object or its content with the specified element.
    /// </summary>
    /// <param name="element">The new element that will replace the existing one.</param>
    /// <remarks>
    /// This method provides a mechanism for dynamic replacement of components or elements,
    /// allowing for updates or modifications to the Logix structure without creating a new instance.
    /// This method requires the element be attached to an <see cref="L5X"/>,
    /// as it will access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    void Replace(TElement element);

    /// <summary>
    /// Replaces occurrences of a specified string with another string within the properties of the current object.
    /// </summary>
    /// <param name="find">The string value to search for in the properties.</param>
    /// <param name="replace">The string value to replace the occurrences of the found string.</param>
    /// <param name="properties">An optional array of property names to limit the replacement scope. If null, all properties are considered.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="find"/> or <paramref name="replace"/> argument is null.</exception>
    /// <remarks>
    /// This method searches for the specified string within the designated properties of the object and replaces it
    /// with the provided replacement string. It provides a mechanism for tailored updates to specific content within
    /// an object's properties.
    /// </remarks>
    void Replace(string find, string replace, string[]? properties = null);

    /// <summary>
    /// Removes the current element from its parent in the underlying L5X structure.
    /// </summary>
    /// <remarks>
    /// This method detaches the element from the XML document it is part of. If the element is not attached
    /// to a parent (i.e., it is not part of a loaded L5X structure), an <see cref="InvalidOperationException"/>
    /// will be thrown.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the element is not attached to a parent element within the L5X structure.
    /// </exception>
    void Remove();
}

/// <summary>
/// A base implementation of <see cref="ILogixObject{TElement}"/>.
/// Encapsulates core operations to enable manipulation, configuration, and collection management
/// of elements derived from <see cref="LogixElement"/> within the system.
/// </summary>
/// <typeparam name="TObject">The type of Logix element derived from <see cref="LogixElement"/>
/// that this class implements and manipulates.</typeparam>
public abstract class LogixObject<TObject> : LogixElement, ILogixObject<TObject> where TObject : ILogixElement
{
    /// <inheritdoc />
    protected LogixObject(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixObject(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public new TObject Clone()
    {
        return new XElement(Serialize()).Deserialize<TObject>();
    }

    /// <inheritdoc />
    public void AddAfter(TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException("Operation requires attached parent reference.");

        Element.AddAfterSelf(element.Serialize());
    }

    /// <inheritdoc />
    public void AddBefore(TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException("Operation requires attached parent reference.");

        Element.AddBeforeSelf(element.Serialize());
    }

    /// <inheritdoc />
    public void Append(TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException("Operation requires attached parent reference.");

        Element.Parent.Add(element.Serialize());
    }

    /// <inheritdoc />
    public TObject Duplicate(Action<TObject> config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        var duplicate = new XElement(Element).Deserialize<TObject>();
        config.Invoke(duplicate);

        if (TryGetDocument(out _))
        {
            AddAfter(duplicate);
        }

        return duplicate;
    }

    /// <inheritdoc />
    public void Replace(TObject element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        if (Element.Parent is null)
            throw new InvalidOperationException("Cannot replace element that is not attached to a parent.");

        Element.ReplaceWith(element.Serialize());
    }

    /// <inheritdoc />
    public void Replace(string find, string replace, string[]? properties = null)
    {
        if (find is null)
            throw new ArgumentNullException(nameof(find));

        if (replace is null)
            throw new ArgumentNullException(nameof(replace));

        Element.FindAndReplace(find, replace, properties ?? []);
    }

    /// <inheritdoc />
    public void Remove()
    {
        if (Element.Parent is null) return;
        Element.Remove();
    }
}