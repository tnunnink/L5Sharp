using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp.Configurations
{
    public class DataTypeConfiguration : IComponentConfiguration<IUserDefined>, IDataTypeNameConfiguration,
        IDataTypeConfiguration
    {
        private string _name;
        private string _description;
        private readonly List<IMember<IDataType>> _members = new List<IMember<IDataType>>();

        public IUserDefined Compile()
        {
            return new DataType(_name, _members, _description);
        }

        public IDataTypeConfiguration HasName(string name)
        {
            _name = name;
            return this;
        }

        public IDataTypeConfiguration HasDescription(string description)
        {
            _description = description;
            return this;
        }

        public IDataTypeConfiguration HasMember(IMember<IDataType> member)
        {
            _members.Add(member);
            return this;
        }

        public IDataTypeConfiguration HasMember(Action<IMemberNameConfiguration> config)
        {
            var configuration = new MemberConfiguration();
            config?.Invoke(configuration);
            var member = configuration.Compile();
            _members.Add(member);
            return this;
        }
    }
}