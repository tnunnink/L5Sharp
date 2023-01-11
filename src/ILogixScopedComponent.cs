using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// A type of <see cref="ILogixComponent"/> that adds property to indicate the scope of the component.
    /// </summary>
    public interface ILogixScopedComponent
    {
        string Name { get; }

        string Description { get; }
        
        Scope Scope { get; }
    }
}