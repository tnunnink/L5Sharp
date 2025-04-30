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
/// is typically subject to some naming constraints defined by a Rockwell. Logix internally may create
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
public abstract class LogixComponent : LogixScoped
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
    public virtual IEnumerable<LogixComponent> Dependencies() => [];

    /// <summary>
    /// Returns a collection of all <see cref="LogixElement"/> objects that reference this component by name.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing <see cref="LogixElement"/> objects that have
    /// at least one property value referencing this component's name.
    /// </returns>
    public IEnumerable<CrossReference> References() => L5X?.References(this) ?? [];

    /// <summary>
    /// Deletes this component and it's references from the current attached L5X file.
    /// </summary>
    /// <remarks>
    /// This method can be helpful for completely scrubbing a L5X file of the specific component, which means removing
    /// references to it as well as the component itself. This is on contrast to <see cref="LogixObject.Remove()"/>
    /// which will simply remove this element from the parent container. If this component is not attached to an L5X
    /// then it will simply return and not throw any exceptions. Obviously, use this with caution as you will not be able
    /// to undo the process except for the fact that you have reference to component being deleted. 
    /// </remarks>
    public virtual void Delete()
    {
        if (Element.Parent is null) return;

        var references = References();

        foreach (var reference in references)
        {
            if (L5X?.TryGet(reference.Scope, out var element) is true)
            {
                element.Remove();
            }
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

    /// <inheritdoc />
    /// <remarks>This override returns the component name of the type.</remarks>
    public override string ToString() => Name;
}

/// <summary>
/// A generic abstract <see cref="LogixComponent"/> that implements the <see cref="ILogixParsable{T}"/> interface.
/// This generic type class allow us to specify the strong return types for methods <see cref="Parse"/>,
/// <see cref="TryParse"/> and <see cref="Clone"/>. This means we don't have to implement these methods for every
/// derivative type, and allows these types to be used with the <see cref="LogixParser"/> in a dynamic fashion.
/// </summary>
/// <typeparam name="TComponent">The type implementing <see cref="LogixComponent"/></typeparam>
public abstract class LogixComponent<TComponent> : LogixComponent, ILogixParsable<TComponent>
    where TComponent : LogixComponent, ILogixParsable<TComponent>
{
    /// <inheritdoc />
    protected LogixComponent(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixComponent(XElement element) : base(element)
    {
    }

    /// <summary>
    /// Returns a new deep cloned instance as the specified <see cref="LogixElement"/> type.
    /// </summary>
    /// <returns>A new instance of the specified element type with the same property values.</returns>
    public new TComponent Clone() => new XElement(Serialize()).Deserialize<TComponent>();

    /// <summary>
    /// Parses the provided string and returned the strongly typed component object.
    /// </summary>
    /// <param name="value">The XML string value to parse.</param>
    /// <returns>A new <see cref="LogixComponent"/> instance that represents the parsed value.</returns>
    /// <remarks>
    /// Internally this uses XElement.Parse along with our <see cref="LogixSerializer"/> to instantiate the concrete instance.
    /// This means the user can use the <see cref="LogixParser"/> extensions to also parse XML into strongly typed logix objects.
    /// Also note that since this uses internal XElement and casts the type, this method can throw exceptions for invalid
    /// XML or XML that is parsed to a different type than the one specified here.
    /// </remarks>
    public static TComponent Parse(string value)
    {
        var element = XElement.Parse(value);
        return element.Deserialize<TComponent>();
    }

    /// <summary>
    /// Attempts to parse the provided string and returned the strongly typed component object.
    /// If unsuccessful, then this method returns <c>null</c>.
    /// </summary>
    /// <param name="value">The XML string value to parse.</param>
    /// <returns>A new <see cref="LogixComponent"/> instance that represents the parsed value if successful, otherwise, <c>null</c>.</returns>
    /// <remarks>
    /// Internally this uses XElement.Parse along with our <see cref="LogixSerializer"/> to instantiate the concrete instance.
    /// This means the user can use the <see cref="LogixParser"/> extensions to also parse XML into strongly typed logix objects.
    /// Note that this method will just return null if any exception is caught. This could be for invalid XML formats
    /// of invalid type casts.
    /// </remarks>
    public static TComponent? TryParse(string? value)
    {
        if (value is null || value.IsEmpty())
            return null;

        var trimmed = value.Trim();
        if (trimmed.Length == 0 || trimmed[0] != '<') return null;

        try
        {
            var element = XElement.Parse(trimmed);
            return element.Deserialize<TComponent>();
        }
        catch (Exception)
        {
            return null;
        }
    }
}