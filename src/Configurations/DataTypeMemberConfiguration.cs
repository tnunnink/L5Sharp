using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class DataTypeMemberConfiguration : IDataTypeMemberConfiguration
    {
        private readonly string _name;
        private IDataType _dataType;
        private string _description;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;

        public DataTypeMemberConfiguration(string name)
        {
            _name = name;
        }

        public IDataTypeMember<IDataType> Compile()
        {
            return new DataTypeMember<IDataType>(_name, _dataType, _dimensions, _radix, _externalAccess, _description);
        }

        public IDataTypeMemberConfiguration OfType(IDataType dataType)
        {
            _dataType = dataType;
            return this;
        }

        public IDataTypeMemberConfiguration HasDescription(string description)
        {
            _description = description;
            return this;
        }

        public IDataTypeMemberConfiguration WithDimension(Dimensions dimensions)
        {
            _dimensions = dimensions;
            return this;
        }

        public IDataTypeMemberConfiguration WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        public IDataTypeMemberConfiguration WithAccess(ExternalAccess externalAccess)
        {
            _externalAccess = externalAccess;
            return this;
        }
    }
}