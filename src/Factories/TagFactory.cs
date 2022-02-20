using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Factories
{
    /// <summary>
    /// Static factory class that creates instances of components that implement <see cref="ITag{TDataType}"/>. 
    /// </summary>
    public static class Tag
    {
        /// <summary>
        /// Creates a new <see cref="ITag{TDataType}"/> instance with the provided name and data type instance.
        /// </summary>
        /// <param name="name">The name of the tag, which must satisfy the <see cref="ComponentName"/> constraints.</param>
        /// <param name="dataType">The data type instance of the tag.</param>
        /// <param name="radix">The radix of the tag.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the tag.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The description of the tag.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the tag.
        /// If not provided, will default ot <see cref="TagUsage.Null"/></param>
        /// <param name="constant">The value indicating whether the tag is a constant.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> to create.</typeparam>
        /// <returns>A new <see cref="ITag{TDataType}"/> instance with the provided parameters.</returns>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>dataType</c> are null.</exception>
        /// <exception cref="ComponentNameInvalidException">
        /// <c>name</c> does not satisfy the <see cref="ComponentName"/> constraints.
        /// </exception>
        public static ITag<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null,
            TagUsage? usage = null, bool constant = false)
            where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType));

            return new Tag<TDataType>(name, dataType, radix, externalAccess, description, usage, constant);
        }

        /// <summary>
        /// Creates a new <see cref="ITag{TDataType}"/> instance with the provided name and data type instance.
        /// </summary>
        /// <param name="name">The name of the tag, which must satisfy the <see cref="ComponentName"/> constraints.</param>
        /// <param name="radix">The radix of the tag.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the tag.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The description of the tag.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the tag.
        /// If not provided, will default ot <see cref="TagUsage.Null"/></param>
        /// <param name="constant">The value indicating whether the tag is a constant.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> to create.</typeparam>
        /// <returns>A new <see cref="ITag{TDataType}"/> instance with the provided parameters.</returns>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>dataType</c> are null.</exception>
        /// <exception cref="ComponentNameInvalidException">
        /// <c>name</c> does not satisfy the <see cref="ComponentName"/> constraints.
        /// </exception>
        public static ITag<TDataType> Create<TDataType>(ComponentName name, Radix? radix = null,
            ExternalAccess? externalAccess = null, string? description = null,
            TagUsage? usage = null, bool constant = false)
            where TDataType : IDataType, new() =>
            Create(name, new TDataType(), radix, externalAccess, description, usage, constant);


        /// <summary>
        /// Creates a new array <see cref="ITag{TDataType}"/> instance with the provided name and data type instance.
        /// </summary>
        /// <param name="name">The name of the tag, which must satisfy the <see cref="ComponentName"/> constraints.</param>
        /// <param name="dataType">The data type instance of the tag.</param>
        /// <param name="dimensions">The dimensions of the tag array.</param>
        /// <param name="radix">The radix of the tag.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the tag.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The description of the tag.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the tag.
        /// If not provided, will default ot <see cref="TagUsage.Null"/></param>
        /// <param name="constant">The value indicating whether the tag is a constant.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> to create.</typeparam>
        /// <returns>A new <see cref="ITag{TDataType}"/> instance with the provided parameters.</returns>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>dataType</c> are null.</exception>
        /// <exception cref="ComponentNameInvalidException">
        /// <c>name</c> does not satisfy the <see cref="ComponentName"/> constraints.
        /// </exception>
        public static ITag<IArrayType<TDataType>> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, TagUsage? usage = null, bool constant = false)
            where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType));

            var arrayType = new ArrayType<TDataType>(dimensions, dataType, radix, externalAccess, description);

            return new Tag<IArrayType<TDataType>>(name, arrayType, radix, externalAccess, description, usage, constant);
        }

        /// <summary>
        /// Creates a new array <see cref="ITag{TDataType}"/> instance with the provided name and data type instance.
        /// </summary>
        /// <param name="name">The name of the tag, which must satisfy the <see cref="ComponentName"/> constraints.</param>
        /// <param name="dimensions">The dimensions of the tag array.</param>
        /// <param name="radix">The radix of the tag.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the tag.
        /// If not provided, will default to <see cref="ExternalAccess.ReadWrite"/>.</param>
        /// <param name="description">The description of the tag.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the tag.
        /// If not provided, will default ot <see cref="TagUsage.Null"/></param>
        /// <param name="constant">The value indicating whether the tag is a constant.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> to create.</typeparam>
        /// <returns>A new <see cref="ITag{TDataType}"/> instance with the provided parameters.</returns>
        /// <exception cref="ArgumentNullException"><c>name</c> or <c>dataType</c> are null.</exception>
        /// <exception cref="ComponentNameInvalidException">
        /// <c>name</c> does not satisfy the <see cref="ComponentName"/> constraints.
        /// </exception>
        public static ITag<IArrayType<TDataType>> Create<TDataType>(ComponentName name, Dimensions dimensions,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null,
            TagUsage? usage = null, bool constant = false)
            where TDataType : IDataType, new() =>
            Create(name, new TDataType(), dimensions, radix, externalAccess, description, usage, constant);
    }
}