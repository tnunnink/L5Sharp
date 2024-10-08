using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public abstract class LogixScoped : LogixObject
{
    /// <inheritdoc />
    protected LogixScoped(string name) : base(name)
    {
    }

    /// <inheritdoc />
    protected LogixScoped(XElement element) : base(element)
    {
    }
    
    /// <summary>
    /// The scope idetifying where in an L5X file this element exists. This can be a globally scoped controller element,
    /// a locally scoped program or instruction element, or neither (not attached to L5X tree).
    /// </summary>
    /// <value>A <see cref="Scope"/> object with information regarding the scope of the element.</value>
    /// <remarks>
    /// <para>
    /// The scope of an element is determined from the ancestors of the underlying <see cref="XElement"/>.
    /// This property is not inherent in the underlying XML (not serialized), but one that adds a lot of
    /// value as it helps uniquely identify elements within the L5X file, especially elements such as <c>Tag</c>,
    /// <c>Routine</c>, or <c>Rung</c>.
    /// </para>
    /// </remarks>
    public virtual Scope Scope => Scope.Of(Element);
}