using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataType"></typeparam>
    public interface ITag<out TDataType> : ILogixComponent, ITagMember<TDataType> where TDataType : IDataType
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
        
        /// <summary>
        /// Sets the <c>Name</c> of the <c>Tag</c> component to the provided value.
        /// </summary>
        /// <param name="name">The value of the <c>ComponentName</c>.</param>
        void SetName(ComponentName name);
        
        /// <summary>
        /// Sets the <c>Dimensions</c> of the <c>Tag</c> component to the provided value.
        /// </summary>
        /// <param name="dimensions">The value of the <c>Dimensions</c>.</param>
        void SetDimensions(Dimensions dimensions);
        
        /// <summary>
        /// Sets the <c>Usage</c> of the <c>Tag</c> component to the provided value.
        /// </summary>
        /// <param name="usage">The value of the <c>TagUsage</c>.</param>
        void SetUsage(TagUsage usage);
        
        /// <summary>
        /// Sets the <c>ExternalAccess</c> of the <c>Tag</c> component to the provided value.
        /// </summary>
        /// <param name="externalAccess">The value of the <c>ExternalAccess</c>.</param>
        void SetExternalAccess(ExternalAccess externalAccess);
        
        /// <summary>
        /// Set the <c>DataType</c> of the <c>Tag</c> component to the provided value.
        /// </summary>
        /// <param name="dataType">The value of the <c>DataType</c>.</param>
        /// <typeparam name="TType">The type of the <c>DataType</c> that is being set.</typeparam>
        /// <returns>A new instance of <c>Tag</c> with the updated <c>DataType</c>.</returns>
        /// <remarks>
        /// <see cref="ChangeDataType{TType}"/> returns a new instance of the tag so the
        /// </remarks>
        ITag<TType> ChangeDataType<TType>(TType dataType) where TType : IDataType;
    }
}