using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class DataTypeMemberConfiguration : IDataTypeMemberConfiguration
    {
        private string _name;
        private IDataType _dataType;
        private string _description;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;

        public IDataTypeMember Compile()
        {
            return new DataTypeMember(_name, _dataType, _dimensions, _radix, _externalAccess, _description);
        }

        void IComponentConfiguration<IDataTypeMember>.HasName(string name)
        {
            _name = name;
        }

        public IDataTypeMemberConfiguration HasDataType(IDataType dataType)
        {
            _dataType = dataType;
            return this;
        }

        public IDataTypeMemberConfiguration HasDescription(string description)
        {
            _description = description;
            return this;
        }

        public IDataTypeMemberConfiguration HasDimension(Dimensions dimensions)
        {
            _dimensions = dimensions;
            return this;
        }

        public IDataTypeMemberConfiguration HasRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        public IDataTypeMemberConfiguration HasAccess(ExternalAccess externalAccess)
        {
            _externalAccess = externalAccess;
            return this;
        }
    }
}