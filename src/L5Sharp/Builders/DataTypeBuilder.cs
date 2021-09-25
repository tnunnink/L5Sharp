using System;
using System.Linq;
using L5Sharp.Primitives;
using L5Sharp.Abstractions;

namespace L5Sharp.Builders
{
    internal class DataTypeBuilder : IDataTypeBuilder
    {
        private readonly Controller _controller;
        private readonly DataType _dataType;

        internal DataTypeBuilder(Controller controller, string name, string description)
        {
            _controller = controller;
            _dataType = new DataType(name, description);
        }

        public DataType Build()
        {
            return _dataType;
        }

        public IDataTypeBuilder WithMember(string name, string dataType, string description = null)
        {
            var type = _controller.DataTypes.SingleOrDefault(d => d.Name == dataType);
            if (type == null)
                throw new InvalidOperationException($"Can not find data type {dataType}");
            
            
            return this;
        }
    }
}