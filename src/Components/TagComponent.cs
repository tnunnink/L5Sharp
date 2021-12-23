using System;
using L5Sharp.Builders;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// Static factory class that creates instances of components that implement <see cref="ITag{TDataType}"/>. 
    /// </summary>
    public static class Tag
    {
        /// <summary>
        /// Creates a new <c>ITag</c> instance with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the <c>ITag</c>. The name must satisfy Logix naming constraints.</param>
        /// <param name="dataType">The data type of the <c>ITag</c>.</param>
        /// <param name="dimensions">The dimensions of the <c>ITag</c>.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the <c>ITag</c>.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the <c>ITag</c>.
        /// If not provided, will default to <see cref="ExternalAccess.None"/>.</param>
        /// <param name="description">The description of the <c>ITag</c>.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the <c>ITag</c>.</param>
        /// <param name="constant"></param>
        /// <returns>A new <c>ITag</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">When name or dataType are null.</exception>
        public static ITag<IDataType> Create(ComponentName name, IDataType dataType, Dimensions? dimensions = null,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null,
            TagUsage? usage = null, bool constant = false) => 
            Create<IDataType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant);

        /// <summary>
        /// Creates a new <c>ITag</c> instance with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the <c>ITag</c>. The name must satisfy Logix naming constraints.</param>
        /// <param name="dimensions">The dimensions of the <c>ITag</c>.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the <c>ITag</c>.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the <c>ITag</c>.
        /// If not provided, will default to <see cref="ExternalAccess.None"/>.</param>
        /// <param name="description">The description of the <c>ITag</c>.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the <c>ITag</c>.</param>
        /// <param name="constant"></param>
        /// <returns>A new <c>ITag</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">When name or dataType are null.</exception>
        public static ITag<TDataType> Create<TDataType>(ComponentName name, Dimensions? dimensions = null,
            Radix? radix = null, ExternalAccess? externalAccess = null, string? description = null,
            TagUsage? usage = null, bool constant = false) 
            where TDataType : IDataType, new() => 
            Create(name, new TDataType(), dimensions, radix, externalAccess, description, usage, constant);

        /// <summary>
        /// Creates a new <c>ITag</c> instance with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the <c>ITag</c>. The name must satisfy Logix naming constraints.</param>
        /// <param name="dataType">The data type of the <c>ITag</c>.</param>
        /// <param name="dimensions">The dimensions of the <c>ITag</c>.
        /// If not provided, will default to <see cref="Dimensions.Empty"/>.</param>
        /// <param name="radix">The radix of the <c>ITag</c>.
        /// If not provided, will default to <see cref="Radix.Default(IDataType)"/>.</param>
        /// <param name="externalAccess">The external access of the <c>ITag</c>.
        /// If not provided, will default to <see cref="ExternalAccess.None"/>.</param>
        /// <param name="description">The description of the <c>ITag</c>.
        /// If not provided, will default to <see cref="string.Empty"/>.</param>
        /// <param name="usage">The usage of the <c>ITag</c>.</param>
        /// <param name="constant"></param>
        /// <typeparam name="TDataType">The DataType of the <c>ITag</c>.</typeparam>
        /// <returns>A new <c>ITag</c> instance with the specified arguments.</returns>
        /// <exception cref="ArgumentNullException">When name or dataType are null.</exception>
        public static ITag<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions? dimensions = null, Radix? radix = null, ExternalAccess? externalAccess = null,
            string? description = null, TagUsage? usage = null, bool constant = false)
            where TDataType : IDataType
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType));

            return new Tag<TDataType>(name, dataType, dimensions, radix, externalAccess, description, usage, constant);
        }

        public static ITagBuilder<IDataType> Build(ComponentName name, IDataType dataType)
        {
            return new TagBuilder<IDataType>(name, dataType);
        }

        public static ITagBuilder<TDataType> Build<TDataType>(ComponentName name)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new TagBuilder<TDataType>(name, dataType);
        }

        public static ITagBuilder<TDataType> Build<TDataType>(ComponentName name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new TagBuilder<TDataType>(name, dataType);
        }
    }
}