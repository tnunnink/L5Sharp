using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Represents a common interface for all Logix components within the L5X architecture.
/// </summary>
/// <remarks>
/// This interface defines the core properties and operations that are shared among all Logix components.
/// Each component is identified by a unique name and may additionally include a description and specific use within the system.
/// Components supporting the `ILogixComponent` interface can be managed dynamically at runtime, allowing
/// for operations such as deletion, export, and modifications within an L5X file context.
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer> 
public interface ILogixComponent : ILogixEntity
{
    /// <summary>
    /// The <see cref="Use"/> of the component within the L5X file.
    /// </summary>
    /// <remarks>
    /// Typically used when exporting individual components (DataType, AoiBlock, Module) to indicate whether the component
    /// is the target of the L5X content or exists solely as a context or dependency of the target component. When
    /// saving a project as an L5X, the top-level controller component is the target, and all other components will
    /// not have this property. 
    /// </remarks>
    Use? Use { get; set; }

    /// <summary>
    /// The unique name of the component.
    /// </summary>
    /// <value>A <see cref="string"/> representing the component name.</value>
    /// <remarks>
    /// The name servers as a unique identifier for various types of components.
    /// In most cases, the component name should satisfy Logix naming constraints of alphanumeric and
    /// underscore ('_') characters, start with a letter, and be between 1 and 40 characters.
    /// Validation is not performed by this library, so importing components with invalid names may fail.
    /// </remarks>
    string Name { get; set; }

    /// <summary>
    /// The description of the component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    string? Description { get; set; }

    /// <summary>
    /// Retrieves a collection of <see cref="Reference"/> that indicate where this component is used within the current project.
    /// </summary>
    /// <returns>A collection of <see cref="Reference"/> representing the usages of this entity.</returns>
    /// <remarks>
    /// Usages represent references to other elements (typically code or tags) that use/reference this component by name.
    /// This is similar to the cross-referencing mechanism in Studio 5k and is meant to resemble it at some level.
    /// Each deriving type must implement logic as needed to find all usages of this entity withing an L5X document.
    /// </remarks>
    IEnumerable<Reference> References();

    /// <summary>
    /// Deletes this component and it's references from the current attached L5X file.
    /// </summary>
    /// <remarks>
    /// This method can be helpful for completely scrubbing a component and all its references from an L5X file.
    /// This is in contrast to simply removing the element from the containing collection,
    /// If this component is not attached to an L5X, then it will simply return and not throw any exceptions.
    /// </remarks>
    void Delete();

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided logix component as the target type.
    /// </summary>
    /// <param name="softwareRevision">The optional software revision, or version of Studio to export the component as.</param>
    /// <returns>A <see cref="L5X"/> containing the component as the target of the L5X.</returns>
    L5X Export(Revision? softwareRevision = null);
}

/// <summary>
/// Represents a fundamental building block for Logix components in the L5X architecture.
/// </summary>
/// <remarks>
/// The `LogixComponent` class serves as a base implementation for defining reusable and hierarchical structures
/// within an L5X framework. This class facilitates the management of component-level properties and behaviors,
/// enabling customization and extensibility where needed.
/// Derived classes can implement additional functionality tailored to specific control system requirements,
/// leveraging the core methods and attributes provided by `LogixComponent`.
/// </remarks>
/// <footer>
/// For further reference, consult Rockwell Automation's documentation on L5X structures and component definitions.
/// </footer>
public abstract class LogixComponent<TComponent> : LogixEntity<TComponent>, ILogixComponent
    where TComponent : LogixComponent<TComponent>
{
    /// <inheritdoc />
    protected LogixComponent(string name) : base(name)
    {
        Element.SetAttributeValue(L5XName.Name, string.Empty);
    }

    /// <inheritdoc />
    protected LogixComponent(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    public Use? Use
    {
        get => GetValue(Use.Parse);
        set => SetValue(value);
    }

    /// <inheritdoc />
    public virtual string Name
    {
        get => GetRequiredValue();
        set => SetRequiredValue(value);
    }

    /// <inheritdoc />
    public virtual string? Description
    {
        get => GetProperty();
        set => SetProperty(value);
    }

    /// <inheritdoc />
    /// <remarks>This override returns the component name of the type.</remarks>
    public override string ToString() => Name;

    /// <inheritdoc />
    public virtual IEnumerable<Reference> References()
    {
        if (!TryGetDocument(out var document))
            return [];

        return document.References(Name).Where(r => r.Scope.IsVisibleTo(Scope));
    }

    /// <inheritdoc />
    public virtual void Delete()
    {
        if (!TryGetDocument(out var doc)) return;

        var references = References();

        foreach (var reference in references)
        {
            if (doc.TryGet(reference, out var element))
            {
                element.Serialize().Remove();
            }
        }

        Element.Remove();
    }

    /// <inheritdoc />
    public virtual L5X Export(Revision? softwareRevision = null)
    {
        Use = Use.Target;
        softwareRevision ??= TryGetDocument(out var doc) ? doc.Content.SoftwareRevision : new Revision();

        var info = LogixContent.Create(this, softwareRevision);
        var content = new L5X(info);
        content.Add(this);

        foreach (var dependency in Dependencies())
        {
            if (dependency is not ILogixComponent component) continue;
            component.Use = Use.Context;
            content.Add(component);
        }

        return content;
    }

    /// <summary>
    /// Replaces all occurrences of a specified substring with another substring within the specified property of the component.
    /// </summary>
    /// <param name="find">The substring to be replaced.</param>
    /// <param name="replace">The substring that replaces the original substring.</param>
    /// <param name="selector">
    /// An expression that specifies the property of the component where the replacement should be performed.
    /// </param>
    /// <typeparam name="TProperty">The type of the property being selected for the replacement operation.</typeparam>
    /// <exception cref="ArgumentNullException">
    /// Thrown when either the <paramref name="find"/> or <paramref name="replace"/> parameter is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="selector"/> does not specify a valid property of type <see cref="MemberExpression"/>.
    /// </exception>
    public void Replace<TProperty>(string find, string replace, Expression<Func<TComponent, TProperty>> selector)
    {
        if (find is null)
            throw new ArgumentNullException(nameof(find));

        if (replace is null)
            throw new ArgumentNullException(nameof(replace));

        if (selector.Body is not MemberExpression memberExpression)
            throw new ArgumentException($"Property selector must be of type {typeof(MemberExpression)}.");

        Element.FindAndReplace(find, replace, [memberExpression.Member.Name]);
    }
}