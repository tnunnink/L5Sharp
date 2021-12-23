using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix Tag component.
    /// </summary>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the <c>ITag</c>.</typeparam>
    public interface ITag<out TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <c>TagType</c> of the <c>ITag</c> component.
        /// </summary>
        TagType TagType { get; }
        
        /// <summary>
        /// Gets the <c>Scope</c> of the <c>ITag</c> component.
        /// </summary>
        Scope Scope { get; }
        
        /// <summary>
        /// Gets the <c>TagUsage</c> of the <c>ITag</c> component.
        /// </summary>
        TagUsage Usage { get; }
        
        /// <summary>
        /// Gets the <c>Constant</c> of the <c>ITag</c> component.
        /// </summary>
        bool Constant { get; }
        
        /// <summary>
        /// Gets the collection of member comments for the current <c>Tag</c>.
        /// </summary>
        Comments Comments {get;}
    }
}