using System;
using L5Sharp.Core;

namespace L5Sharp.Configurations
{
    public class DataTypeConfiguration : IDataTypeConfiguration
    {
        private readonly DataType _dataType;

        public DataTypeConfiguration(string name)
        {
            _dataType = new DataType(name);
        }
        public IUserDefined Compile()
        {
            return _dataType;
        }
        public IDataTypeConfiguration HasDescription(string description)
        {
            _dataType.SetDescription(description);
            return this;
        }
        public IDataTypeConfiguration HasMember(IDataTypeMember member)
        {
            _dataType.Members.Add(member);
            return this;
        }
        public IDataTypeConfiguration HasMember(string name, Action<IDataTypeMemberConfiguration> config = null)
        {
            var configuration = new DataTypeMemberConfiguration(name);
            
            config?.Invoke(configuration);
            
            var member = configuration.Compile();
            _dataType.Members.Add(member);
            
            return this;
        }
    }
}