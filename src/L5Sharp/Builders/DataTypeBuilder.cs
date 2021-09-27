using System;
using System.Runtime.CompilerServices;
using L5Sharp.Primitives;
using L5Sharp.Abstractions;

[assembly: InternalsVisibleTo("L5Sharp.Tests.Internal")]

namespace L5Sharp.Builders
{
    internal class DataTypeBuilder : IDataTypeBuilder
    {
        private readonly DataType _dataType;

        public DataTypeBuilder(string name)
        {
            _dataType = new DataType(name);
        }

        public IDataTypeBuilder HasDescription(string description)
        {
            _dataType.Description = description;
            return this;
        }

        public IDataTypeBuilder WithMember(string name, IDataType dataType, Action<IMemberBuilder> memberBuilder)
        {
            _dataType.AddMember(name, dataType, memberBuilder);
            return this;
        }

        public DataType Build()
        {
            return _dataType;
        }
    }
}