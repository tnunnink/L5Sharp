using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class DataTypes : ComponentCollection<IUserDefined>, IDataTypes
    {
        public void Add(Action<IDataTypeConfiguration> config)
        {
            var configuration = new DataTypeConfiguration();
            config?.Invoke(configuration);
            Add(configuration);
        }
    }
}