using System;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Builders.Abstractions
{
    public interface IDataTypeBuilder : IBuilder<DataType>
    {
        IDataTypeBuilder HasDescription(string description);
        IDataTypeBuilder WithMember(string name, IDataType dataType, Action<IMemberBuilder> memberBuilder = null);
    }
}