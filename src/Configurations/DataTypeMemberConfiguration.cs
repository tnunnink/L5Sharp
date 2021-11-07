using System;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Configurations
{
    public class DataTypeMemberConfiguration : IDataTypeMemberConfiguration
    {
        private readonly string _name;
        private IDataType _dataType;

        public DataTypeMemberConfiguration(string name)
        {
            _name = name;
        }

        public IDataTypeMember<IDataType> Compile()
        {
            return new DataTypeMember<IDataType>(name, new Undefined());;
        }

        public IDataTypeMemberConfiguration OfType(IDataType dataType)
        {
            _dataType = dataType;
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