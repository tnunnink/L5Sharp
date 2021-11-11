using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class DataTypes : ComponentCollection<IUserDefined>, IDataTypes
    {
        private readonly IController _controller;

        public DataTypes(IController controller)
        {
            _controller = controller;
        }
        
        public void Add(Action<IDataTypeConfiguration> config)
        {
            var configuration = new DataTypeConfiguration();
            config?.Invoke(configuration);
            Add(configuration);
        }

        public void Update(string name, Action<IDataTypeConfiguration> config)
        {
            throw new NotImplementedException();
        }
    }
}