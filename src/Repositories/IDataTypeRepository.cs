using System.Collections.Generic;
using L5Sharp.Abstractions;

namespace L5Sharp.Repositories
{
    public interface IDataTypeRepository : IRepository<IDataType>
    {
        IEnumerable<IDataType> WithMemberType(IDataType dataType);
    }
}