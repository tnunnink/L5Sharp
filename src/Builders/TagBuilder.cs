using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Builders
{
    internal class TagBuilder<TDataType> : ITagBuilder<TDataType> where TDataType : IDataType
    {
        private readonly string _name;
        private readonly TDataType _dataType;
        private Radix? _radix;
        private ExternalAccess? _access;
        private TagUsage? _usage;
        private TagName? _alias;
        private bool _constant;
        private string? _description;

        public TagBuilder(ComponentName name, TDataType dataType)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
        }

        /// <inheritdoc />
        public ITag<TDataType> Create() =>
            new Tag<TDataType>(_name, _dataType, _radix, _access, _description, _usage, _alias, _constant);

        /// <inheritdoc />
        public ITagBuilder<TDataType> IsAliasFor(TagName alias)
        {
            _alias = alias;
            return this;
        }

        /// <inheritdoc />
        public ITagBuilder<TDataType> IsConstant()
        {
            _constant = true;
            return this;
        }

        /// <inheritdoc />
        public ITagBuilder<TDataType> WithAccess(ExternalAccess access)
        {
            _access = access;
            return this;
        }

        /// <inheritdoc />
        public ITagBuilder<TDataType> WithDescription(string description)
        {
            _description = description;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> WithDimensions(Dimensions dimensions)
        {
            return new TagArrayBuilder<TDataType>(_name, _dataType, dimensions, _radix, _access, _usage, _alias,
                _constant, _description);
        }

        /// <inheritdoc />
        public ITagBuilder<TDataType> WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        /// <inheritdoc />
        public ITagBuilder<TDataType> WithUsage(TagUsage usage)
        {
            _usage = usage;
            return this;
        }

        /// <inheritdoc />
        public ITagBuilder<TDataType> WithData(TDataType data)
        {
            _dataType.SetData(data);
            return this;
        }
    }
}