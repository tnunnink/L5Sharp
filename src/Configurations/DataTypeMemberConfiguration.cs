using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public class DataTypeMemberConfiguration : IDataTypeMemberConfiguration
    {
        private readonly DataTypeMember _member;

        public DataTypeMemberConfiguration(string name)
        {
            _member = new DataTypeMember(name, Predefined.Undefined);
        }

        public IDataTypeMember Compile()
        {
            return _member;
        }

        public IDataTypeMemberConfiguration OfType(IDataType dataType)
        {
            _member.SetDataType(dataType);
            return this;
        }

        public IDataTypeMemberConfiguration HasDescription(string description)
        {
            _member.SetDescription(description);
            return this;
        }

        public IDataTypeMemberConfiguration WithDimension(Dimensions dimensions)
        {
            _member.SetDimensions(dimensions);
            return this;
        }

        public IDataTypeMemberConfiguration WithRadix(Radix radix)
        {
            _member.SetRadix(radix);
            return this;
        }

        public IDataTypeMemberConfiguration WithAccess(ExternalAccess externalAccess)
        {
            _member.SetExternalAccess(externalAccess);
            return this;
        }
    }
}