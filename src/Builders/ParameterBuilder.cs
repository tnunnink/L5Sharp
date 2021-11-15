using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public class ParameterBuilder<TDataType> : IParameterBuilder<TDataType> where TDataType : IDataType
    {
        private readonly string _name;
        private readonly TDataType _dataType;
        private TagUsage _usage;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _access;
        private string _aliasName;
        private string _description;
        private TDataType _value;
        private bool _required;
        private bool _visible;
        private bool _constant;

        public ParameterBuilder(string name, TDataType dataType)
        {
            _name = name;
            _dataType = dataType;
        }

        public IParameterBuilder<TDataType> WithUsage(TagUsage usage)
        {
            _usage = usage;
            return this;
        }

        public IParameterBuilder<TDataType> WithDimensions(Dimensions dimensions)
        {
            _dimensions = dimensions;
            return this;
        }

        public IParameterBuilder<TDataType> WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        public IParameterBuilder<TDataType> WithAccess(ExternalAccess access)
        {
            _access = access;
            return this;
        }

        public IParameterBuilder<TDataType> IsAliasFor(string aliasName)
        {
            _aliasName = aliasName;
            return this;
        }

        public IParameterBuilder<TDataType> WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public IParameterBuilder<TDataType> WithDefault(TDataType value)
        {
            _value = value;
            return this;
        }

        public IParameterBuilder<TDataType> IsRequired()
        {
            _required = true;
            return this;
        }

        public IParameterBuilder<TDataType> IsVisible()
        {
            _visible = true;
            return this;
        }

        public IParameterBuilder<TDataType> IsConstant()
        {
            _constant = true;
            return this;
        }

        public IParameter<TDataType> Create()
        {
            return new Parameter<TDataType>(_name, _dataType, _usage, _required, _visible, _dimensions, _radix, _access,
                _description, _constant);
        }
    }
}