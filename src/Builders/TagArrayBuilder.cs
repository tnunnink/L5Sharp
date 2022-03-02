using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Builders
{
    internal class TagArrayBuilder<TDataType> : ITagArrayBuilder<TDataType> where TDataType : IDataType
    {
        private readonly string _name;
        private readonly ArrayType<TDataType> _dataType;
        private Radix? _radix;
        private ExternalAccess? _access;
        private TagUsage? _usage;
        private TagName? _alias;
        private bool _constant;
        private string? _description;

        public TagArrayBuilder(string name, TDataType dataType, Dimensions dimensions, Radix? radix,
            ExternalAccess? access, TagUsage? usage, TagName? alias, bool constant, string? description)
        {
            _name = name;
            _dataType = new ArrayType<TDataType>(dimensions, dataType);
            _radix = radix;
            _access = access;
            _usage = usage;
            _alias = alias;
            _constant = constant;
            _description = description;
        }

        /// <inheritdoc />
        public ITag<IArrayType<TDataType>> Create() =>
            new Tag<IArrayType<TDataType>>(_name, _dataType, _radix, _access, _description, _usage, _alias, _constant);

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> IsAliasFor(TagName alias)
        {
            _alias = alias;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> IsConstant()
        {
            _constant = true;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> WithAccess(ExternalAccess access)
        {
            _access = access;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> WithDescription(string description)
        {
            _description = description;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> WithUsage(TagUsage usage)
        {
            _usage = usage;
            return this;
        }

        /// <inheritdoc />
        public ITagArrayBuilder<TDataType> WithData(IArrayType<TDataType> data)
        {
            _dataType.SetData(data);
            return this;
        }
    }
}