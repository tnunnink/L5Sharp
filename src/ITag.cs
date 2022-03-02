using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Tag</b> component. 
    /// </summary>
    /// <typeparam name="TDataType">The <see cref="IDataType"/> of the tag.</typeparam>
    public interface ITag<out TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        /// <summary>
        /// Gets the <see cref="TagType"/> of the current <see cref="ITag{TDataType}"/> component.
        /// </summary>
        TagType TagType { get; }

        /// <summary>
        /// Gets the <see cref="TagUsage"/> of the current <see cref="ITag{TDataType}"/> component.
        /// </summary>
        TagUsage Usage { get; }
        
        /// <summary>
        /// Gets the <see cref="TagName"/> value that represents the alias of the current <see cref="ITag{TDataType}"/>
        /// component.
        /// </summary>
        TagName Alias { get; }
        
        /// <summary>
        /// Gets the value indicating whether the current <see cref="ITag{TDataType}"/> is a constant.
        /// </summary>
        bool Constant { get; }
        
        /// <summary>
        /// Gets the collection of member comments for the current <see cref="ITag{TDataType}"/>.
        /// </summary>
        Comments Comments {get;}
        
        /// <summary>
        /// Gets a <see cref="ITagMember{TDataType}"/> at the specified two-dimensional index from the
        /// underlying <see cref="IArrayType{TDataType}"/>.
        /// </summary>
        /// <param name="x">The X index of the two dimensional array.</param>
        /// <param name="y">The Y index of the two dimensional array.</param>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> element from the specified index.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="ITagMember{TDataType}"/> data type is not an <see cref="IArrayType{TDataType}"/>
        /// -or- the dimensions of the current tag type are not a two-dimensional array.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// X and Y do not form a valid index of the current array dimensions.
        /// </exception>
        /// <remarks>
        /// Like <see cref="IArrayType{TDataType}"/>, this overload is for two-dimensional array types only.
        /// If the current tag is not a two-dimensional array of some <see cref="IDataType"/>, this call will fail.  
        /// </remarks>
        ITagMember<IDataType> this[int x, int y] { get; }

        /// <summary>
        /// Gets a <see cref="ITagMember{TDataType}"/> at the specified three-dimensional index from the
        /// underlying <see cref="IArrayType{TDataType}"/>.
        /// </summary>
        /// <param name="x">The X index of the three dimensional array.</param>
        /// <param name="y">The Y index of the three dimensional array.</param>
        /// <param name="z">The X index of the three dimensional array.</param>
        /// <returns>
        /// A new <see cref="ITagMember{TDataType}"/> element from the specified index.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="ITagMember{TDataType}"/> data type is not an <see cref="IArrayType{TDataType}"/>
        /// -or- the dimensions of the current tag type are not a three-dimensional array.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// X, Y, and Z do not form a valid index of the current array dimensions.
        /// </exception>
        /// <remarks>
        /// Like <see cref="IArrayType{TDataType}"/>, this overload is for three-dimensional array types only.
        /// If the current tag is not a three-dimensional array of some <see cref="IDataType"/>, this call will fail.  
        /// </remarks>
        ITagMember<IDataType> this[int x, int y, int z] { get; }
    }
}