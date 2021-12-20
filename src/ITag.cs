using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    public interface ITag<out TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <c>TagType</c> of the <c>Tag</c> component.
        /// </summary>
        TagType TagType { get; }
        
        /// <summary>
        /// Gets the <c>Scope</c> of the <c>Tag</c> component.
        /// </summary>
        Scope Scope { get; }
        TagUsage Usage { get; }
        
        /// <summary>
        /// Gets the value indicating whether the tag value is a constant.
        /// </summary>
        bool Constant { get; set; }
        
        /// <summary>
        /// Gets the collection of member comments for the current <c>Tag</c>.
        /// </summary>
        Comments Comments {get;}
    }
}