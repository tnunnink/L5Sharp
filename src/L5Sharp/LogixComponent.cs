using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp;

/// <summary>
/// A common logix element that is able to be identified by name.
/// </summary>
/// <remarks>
/// <para>
/// This is the base class for all logix component classes. All components can be identified by a unique name that
/// is typically subject to the some naming constraints defined by a Rockwell. Logix internally may create
/// components that do not adhere to the naming constraints, which is why the property is a simple string.
/// Names should be unique any attempt to create duplicated names should fail.
/// All components also contain a simple string description and &lt;see cref="Enums.Use"/&gt; to identify the
/// purpose of the component.
/// </para>
/// </remarks>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer> 
public abstract class LogixComponent : LogixElement
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
    public ComponentKey Key => new(Element.Name.LocalName, ScopeName, Name);
    
    /// <summary>
    /// The scope name of the logix component, indicating the program or controller for which it is contained.
    /// </summary>
    /// <value> A <see cref="string"/> representing the name of the program or controller in which this component
    /// is contained. If the component has <see cref="Scope.Null"/> scope, then an <see cref="string.Empty"/> string.
    /// </value>
    /// <remarks>
    /// <para>
    /// This value is retrieved from the ancestors of the underlying element. If no ancestors exists, meaning this
    /// component is not attached to a L5X tree, then this returns an empty string.
    /// </para>
    /// <para>
    /// This property is not inherent in the underlying XML of a component (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify components within the L5X file, especially scoped components with same name.
    /// </para>
    /// </remarks>
    public string ScopeName => Scope.Container(Element);
    
    /// <summary>
    /// The scope of the logix component, indicating whether it is a globally scoped controller component,
    /// a locally scoped program component, or neither (not attached to L5X tree).
    /// </summary>
    /// <value>A <see cref="Enums.Scope"/> option indicating the scope type for the component.</value>
    /// <remarks>
    /// <para>
    /// The scope of a component is determined from the ancestors of the underlying element. If the ancestor is
    /// program element first, it is <c>Program</c> scoped. If the ancestor is a controller element first, it is
    /// <c>Controller</c> scoped. If no ancestor is found, we assume the component has <c>Null</c> scope,
    /// meaning it is not attached to a L5X tree.
    /// </para>
    /// <para>
    /// This property is not inherent in the underlying XML of a component (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify components within the L5X file, especially scoped components with same name.
    /// </para>
    /// </remarks>
    public Scope ScopeType => Scope.FromElement(Element);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<LogixElement> Dependencies() => Enumerable.Empty<LogixElement>();

    /// <summary>
    /// Creates a new <see cref="L5X"/> with the provided logix component as the target type.
    /// </summary>
    /// <param name="softwareRevision">The optional software revision, or version of Studio to export the component as.</param>
    /// <returns>A <see cref="L5X"/> containing the component as the target of the L5X.</returns>
    public virtual L5X Export(Revision? softwareRevision = null)
    {
        Use = Use.Target;
        
        //todo what params like software revision should be passed in?
        var content = L5X.New(this);
        content.Add(this);
        
        var dependencies = Dependencies().ToList();
        foreach (var dependency in dependencies)
        {
            //shit this won't work...
            /*if (dependency is LogixComponent<>)
            {

            }
            content.Add(dependency);*/
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
    public virtual IEnumerable<LogixElement> References() => Enumerable.Empty<LogixElement>();

    /// <summary>
    /// Returns a collection of <see cref="LogixElement"/> of the specified element type that reference this component by name.
    /// </summary>
    /// <typeparam name="TElement">The element type for which to filter the returned references.</typeparam>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="LogixElement"/> objects of the specified type
    /// that have at least one property value referencing this component's name.
    /// </returns>
    public IEnumerable<TElement> References<TElement>() where TElement : LogixElement =>
        References().Where(r => r is TElement).Cast<TElement>();

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