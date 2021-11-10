using System;
using L5Sharp.Configurations;

namespace L5Sharp
{
    public interface IDataTypes : IComponentCollection<IUserDefined>
    {
        void Add(Action<IDataTypeConfiguration> config);
    }
}