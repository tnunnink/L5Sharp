using System.Collections.Generic;
using L5Sharp.Abstractions;

namespace L5Sharp.Repositories
{
    public interface IDataTypeRepository : IRepository<IUserDefined>
    {
        IEnumerable<IDataType> WithMemberType(IDataType dataType);
    }
}