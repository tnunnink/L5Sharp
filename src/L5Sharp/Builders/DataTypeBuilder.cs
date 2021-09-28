using System;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Primitives;

[assembly: InternalsVisibleTo("L5Sharp.Builder.Tests")]

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
            if (memberBuilder == null)
            {
                _dataType.AddMember(name, dataType);
                return this;
            }

            var builder = new MemberBuilder(name, dataType);
            memberBuilder.Invoke(builder);

            var member = builder.Build();

            _dataType.AddMember(member.Name, member.DataType, member.Description, member.Dimension, member.Radix,
                member.ExternalAccess);

            return this;
        }

        public DataType Build()
        {
            return _dataType;
        }
    }
}