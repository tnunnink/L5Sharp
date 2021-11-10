using System;

namespace L5Sharp.Configurations
{
    public interface IDataTypeNameConfiguration
    {
        IDataTypeConfiguration HasName(string name);
    }

    public interface IDataTypeConfiguration
    {
        IDataTypeConfiguration HasDescription(string description);
        IDataTypeConfiguration HasMember(IMember<IDataType> member);
        IDataTypeConfiguration HasMember(Action<IMemberNameConfiguration> config);
    }
}