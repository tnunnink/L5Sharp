using System;

namespace L5Sharp.Configurations
{
    public interface IDataTypeConfiguration : IComponentConfiguration<IUserDefined>
    {
        IDataTypeConfiguration HasDescription(string description);
        IDataTypeConfiguration HasMember(IDataTypeMember member);
        IDataTypeConfiguration HasMember(string name, Action<IDataTypeMemberConfiguration> config = null);
    }
}