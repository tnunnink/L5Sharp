using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public class TagBuilder<TDataType> : ITagBuilder<TDataType> where TDataType : IDataType
    {
        private readonly string _name;
        private readonly TDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _access;
        private string _aliasName;
        private string _description;
        private TagUsage _usage;
        private TDataType _value;
        private bool _constant;

        public TagBuilder(string name, TDataType dataType)
        {
            _name = name;
            _dataType = dataType;
        }

        public ITagBuilder<TDataType> WithDimensions(Dimensions dimensions)
        {
            _dimensions = dimensions;
            return this;
        }

        public ITagBuilder<TDataType> WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        public ITagBuilder<TDataType> WithAccess(ExternalAccess access)
        {
            _access = access;
            return this;
        }

        public ITagBuilder<TDataType> IsAliasFor(string aliasName)
        {
            _aliasName = aliasName;
            return this;
        }

        public ITagBuilder<TDataType> WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ITagBuilder<TDataType> WithUsage(TagUsage usage)
        {
            _usage = usage;
            return this;
        }

        public ITagBuilder<TDataType> IsConstant()
        {
            _constant = true;
            return this;
        }

        public ITagBuilder<TDataType> WithValue(TDataType value)
        {
            _value = value;
            return this;
        }

        public ITag<TDataType> Create()
        {
            var tag = new Tag<TDataType>(_name, _dataType, _dimensions, _radix, _access, _description, _usage,
                _constant);
            
            //tag.SetData(_value);

            return tag;
        }
    }
}