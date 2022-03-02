using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    /// <summary>
    /// A fluent api for building <see cref="ITag{TDataType}"/> components.
    /// </summary>
    /// <typeparam name="TDataType">The data type of the tag.</typeparam>
    public interface ITagBuilder<TDataType> : IComponentBuilder<ITag<TDataType>> where TDataType : IDataType
    {
        /// <summary>
        /// Sets the alias property of the tag to the provided tag name value. 
        /// </summary>
        /// <param name="alias">The <see cref="TagName"/> value of the alias.</param>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> IsAliasFor(TagName alias);
        
        /// <summary>
        /// Sets the constant property of the tag to true, indicating the value is a constant. 
        /// </summary>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> IsConstant();
        
        /// <summary>
        /// Sets the external access property of the tag to the provided enumeration option.
        /// </summary>
        /// <param name="access">The <see cref="ExternalAccess"/> option indicating the ability of the tag to read from
        /// or written to.</param>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> WithAccess(ExternalAccess access);
        
        /// <summary>
        /// Sets the description property of the tag to the provided string text.
        /// </summary>
        /// <param name="description">The string description of the tag.</param>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> WithDescription(string description);
        
        /// <summary>
        /// Sets the dimensions property of the tag to the provided value.
        /// </summary>
        /// <param name="dimensions">The <see cref="Dimensions"/> value that sets the dimensions or the new array.</param>
        /// <returns>A new <see cref="ITagArrayBuilder{TDataType}"/> instance.
        /// Note that this returns a different builder - one that will return an <see cref="IArrayType{TDataType}"/>.
        /// </returns>
        ITagArrayBuilder<TDataType> WithDimensions(Dimensions dimensions);
        
        /// <summary>
        /// Set the radix property of the tag to the provided enumeration option.
        /// </summary>
        /// <param name="radix">The <see cref="Radix"/> option indicating the format of the tag value.</param>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> WithRadix(Radix radix);
        
        /// <summary>
        /// Set the usage property of the tag to the provided enumeration option.
        /// </summary>
        /// <param name="usage">The <see cref="TagUsage"/> option indicating the access of the tag.</param>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> WithUsage(TagUsage usage);
        
        /// <summary>
        /// Sets the value of the tag to the provided data type value.
        /// </summary>
        /// <param name="data">The data type that contains the data to set for the new tag.</param>
        /// <returns>The current <see cref="ITagBuilder{TDataType}"/> instance.</returns>
        ITagBuilder<TDataType> WithData(TDataType data);
    }
}