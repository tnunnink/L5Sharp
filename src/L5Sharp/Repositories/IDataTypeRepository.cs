using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Repositories
{
    public interface IDataTypeRepository : IRepository<DataType>
    {
        IEnumerable<DataType> WithMemberType(IDataType dataType);
    }
}