using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp.Abstractions
{
    public interface IDataTypeRepository : IRepository<DataType>
    {
        IEnumerable<DataType> WithMemberType(IDataType dataType);
    }
}