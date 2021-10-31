using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class DataTypes : ComponentCollection<IUserDefined>, IDataTypes
    {
        public void Add(string name, Action<IDataTypeConfiguration> config = null)
        {
            var configuration = new DataTypeConfiguration(name);
            config?.Invoke(configuration);
            Add(configuration);
        }
    }
}