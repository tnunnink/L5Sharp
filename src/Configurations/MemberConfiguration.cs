using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class MemberConfiguration :
        IComponentConfiguration<IMember<IDataType>>,
        IMemberNameConfiguration,
        IMemberTypeConfiguration,
        IMemberConfiguration
    {
        private string _name;
        private IDataType _dataType;
        private string _description;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;

        public MemberConfiguration()
        {
        }
        
        public IMember<IDataType> Compile()
        {
            return Member.Create(_name, _dataType, _dimensions, _radix, _externalAccess, _description);
        }

        public IMemberTypeConfiguration WithName(string name)
        {
            _name = name;
            return this;
        }

        public IMemberConfiguration OfType(IDataType dataType)
        {
            _dataType = dataType;
            return this;
        }

        public IMemberConfiguration WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public IMemberConfiguration WithDimension(Dimensions dimensions)
        {
            _dimensions = dimensions;
            return this;
        }

        public IMemberConfiguration WithRadix(Radix radix)
        {
            _radix = radix;
            return this;
        }

        public IMemberConfiguration WithAccess(ExternalAccess externalAccess)
        {
            _externalAccess = externalAccess;
            return this;
        }
    }
}