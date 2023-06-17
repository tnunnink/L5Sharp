using System;
using System.Xml.Linq;
using L5Sharp.Enums;

namespace L5Sharp;

/// <summary>
/// A common logix element that is able to be identified by name.
/// </summary>
/// <remarks>
/// This is the base class for all logix component classes. All components can be identified by a unique name that
/// is typically subject to the some naming constraints defined by a Rockwell. Logix internally may create
/// components that do not adhere to the naming constraints, which is why the property is a simple string.
/// Names should be unique any attempt to create duplicated names should fail.
/// All components also contain a simple string description and <see cref="Enums.Use"/> to identify the purpose of the component. 
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer> 
public abstract class LogixComponent<TComponent> : LogixElement<TComponent>
    where TComponent : LogixComponent<TComponent>
{
    /// <inheritdoc />
    protected LogixComponent()
    {
        Name = string.Empty;
    }

    /// <inheritdoc />
    protected LogixComponent(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <see cref="Use"/> of the component within the L5X file.
    /// </summary>
    /// <remarks>
    /// Typically used when exporting individual components (DataType, AOI, Module) to indicate whether the component
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
    /// <value>A <see cref="string"/> representing the component name if it exists; otherwise, and empty string.</value>
    /// <remarks>
    /// The name servers as a unique identifier for various types of components.
    /// In most cases, the component name should satisfy Logix naming constraints of alphanumeric and
    /// underscore ('_') characters, start with a letter, and be between 1 and 40 characters.
    /// Validation is not performed by this library, so importing components with invalid names may fail.
    /// </remarks>
    public string Name
    {
        get => GetValue<string>() ?? string.Empty;
        set => SetValue(value);
    }

    /// <summary>
    /// The description of the component.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description.</value>
    public virtual string? Description
    {
        get => GetProperty<string>();
        set
        {
            if (value is null)
            {
                Element.Element(L5XName.Description)?.Remove();
                return;
            }

            //Description must be the first child element.
            Element.AddFirst(new XElement(L5XName.Description, new XCData(value)));
        }
    }

    /// <summary>
    /// Adds a new component of the same type directly after this component in the L5X document.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element.
    /// This could happen if the component was created in memory and not yet added to the L5X.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the L5X or <see cref="LogixContent"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    public virtual void AddAfter(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        Element.AddAfterSelf(component.Serialize());
    }

    /// <summary>
    /// Adds a new component of the same type directly before this component in the L5X document.
    /// </summary>
    /// <param name="component">The component to add.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element.
    /// This could happen if the component was created in memory and not yet added to the L5X.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the L5X or <see cref="LogixContent"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    public virtual void AddBefore(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        Element.AddAfterSelf(component.Serialize());
    }

    /// <summary>
    /// Removes the component from it's parent container.
    /// </summary>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element.
    /// This could happen if the component was created in memory and not yet added to the L5X.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the L5X or <see cref="LogixContent"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    public virtual void Remove()
    {
        Element.Remove();
    }

    /// <summary>
    /// Replaces the component instance with a new instance of the same type.
    /// </summary>
    /// <param name="component">The new component to replace this component with.</param>
    /// <exception cref="ArgumentNullException"><c>component</c> is null.</exception>
    /// <exception cref="InvalidOperationException">No parent exists for the underlying element.
    /// This could happen if the component was created in memory and not yet added to the L5X.
    /// </exception>
    /// <remarks>
    /// This method requires the component be attached to the L5X or <see cref="LogixContent"/>, as it will
    /// access the parent of the underlying <see cref="XElement"/> to perform the function.
    /// </remarks>
    public virtual void Replace(TComponent component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        Element.ReplaceWith(component.Serialize());
    }
}