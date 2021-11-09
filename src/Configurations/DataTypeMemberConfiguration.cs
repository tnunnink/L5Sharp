using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class DataTypeMemberConfiguration :
        IComponentConfiguration<IDataTypeMember<IDataType>>,
        IDataTypeMemberNameConfiguration,
        IDataTypeMemberTypeConfiguration,
        IDataTypeMemberConfiguration
    {
        private string _name;
        private IDataType _dataType;
        private string _description;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;

        public DataTypeMemberConfiguration()
        {
        }
        
        public IDataTypeMember<IDataType> Compile()
        {
            return new DataTypeMember<IDataType>(_name, _dataType, _dimensions, _radix, _externalAccess, _description);
        }

        public IDataTypeMemberTypeConfiguration WithName(string name)
        {
            _name = name;
            return this;
        }

        public IDataTypeMemberConfiguration OfType(IDataType dataType)
        {
            _dataType = dataType;
            return this;
        }

        public IDataTypeMemberConfiguration WithDescription(string description)
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