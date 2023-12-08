using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A common logix element that is able to be identified by name.
/// </summary>
/// <remarks>
/// <para>
/// This is the base class for all logix component classes. All components can be identified by a unique name that
/// is typically subject to the some naming constraints defined by a Rockwell. Logix internally may create
/// components that do not adhere to the naming constraints, which is why the property is a simple string.
/// Names should be unique any attempt to create duplicated names should fail.
/// All components also contain a simple string description and <see cref="Core.Use"/> to identify the
/// purpose of the component.
/// </para>
/// <para>
/// All components also contain some common functionality, such as the ability to find dependencies and references, and
/// to be exported individually as a new L5X component file. The default equality implementation is also overridden
/// to determine equality by the component type, name, and scope within the L5X tree.
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer> 
public abstract class LogixComponent : LogixElement, ILogixReferencable
{
    /// <inheritdoc />
    protected LogixComponent()
    {
        Element.SetAttributeValue(L5XName.Name, string.Empty);
    }

    /// <inheritdoc />
    protected LogixComponent(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <see cref="Use"/> of the component within the L5X file.
    /// </summary>
    /// <remarks>
    /// Typically used when exporting individual components (DataType, AoiBlock, Module) to indicate whether the component
    /// is the target of the L5X content, or exists solely as a context or dependency of the target component. When
    /// saving a project as an L5X, the top level controller component is the target, and all other components will
    /// not have this property. 
    /// </remarks>
    public Use? Use
    {
        get => GetValue<Use>();
        set => SetValue(value);
    }

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
    public virtual string Name
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public virtual string? Description
    {
        get => GetProperty<string>();
        set => SetDescription(value);
    }

    /// <summary>
    /// The global unique identifier <see cref="ComponentKey"/> of the component. 
    /// </summary>
    /// <value>
    /// A <see cref="ComponentKey"/> value representing composite set of properties that identify this component
    /// within an L5X tree.
    /// </value>
    public ComponentKey Key => new(GetType().L5XType(), Name);

    /// <summary>
    /// Returns a collection of <see cref="LogixComponent"/> that this component depends on to be valid within a given
    /// L5X file.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing all distinct <see cref="LogixComponent"/> objects this
    /// component depends on.</returns>
    /// <remarks>
    /// This is primarily useful for exporting individual components to a new L5X file. It allows them to also
    /// bring along all the other components they would need to be successfully importing into a logix project file.
    /// Each derived component implements this method since the dependencies are different for each type.
    /// </remarks>
    public virtual IEnumerable<LogixComponent> Dependencies() => Enumerable.Empty<LogixComponent>();

    /// <summary>
    /// Deletes this component and it's references from the current attached L5X file.
    /// </summary>
    /// <remarks>
    /// This method can be helpful for completely scrubbing a L5X file of the specific component, which means removing
    /// references to it as well as the component itself. This is on contrast to <see cref="LogixElement.Remove()"/>
    /// which will simply remove this element from the parent container. If this component is not attached to an L5X
    /// then it will simply return and not throw any exceptions. Obviously, use this with caution as you will not be able
    /// to undo the process except for the fact that you have reference to component being deleted. 
    /// </remarks>
    public virtual void Delete()
    {
        if (Element.Parent is null || !IsAttached) return;
        
        var references = References();

        foreach (var reference in references)
        {
            reference.Element.Remove();
        }
        
        Element.Remove();
    }

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided logix component as the target type.
    /// </summary>
    /// <param name="softwareRevision">The optional software revision, or version of Studio to export the component as.</param>
    /// <returns>A <see cref="L5X"/> containing the component as the target of the L5X.</returns>
    public virtual L5X Export(Revision? softwareRevision = null)
    {
        Use = Use.Target;
        softwareRevision ??= L5X?.Info.SoftwareRevision;

        var content = L5X.New(this, softwareRevision);

        var dependencies = Dependencies().ToList();
        foreach (var dependency in dependencies)
        {
            dependency.Use = Use.Context;
            content.Add(dependency);
        }

        return content;
    }

    /// <summary>
    /// Returns a collection of all <see cref="LogixElement"/> objects that reference this component by name.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="LogixElement"/> objects that have
    /// at least one property value referencing this component's name.
    /// </returns>
    public IEnumerable<CrossReference> References() =>
        L5X is not null ? L5X.ReferencesTo(this) : Enumerable.Empty<CrossReference>();

    /// <inheritdoc />
    /// <remarks>This override returns the component name of the type.</remarks>
    public override string ToString() => Name;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            LogixComponent other => Equals(Key, other.Key),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Key.GetHashCode();
}