using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp.Abstractions
{
    public interface IDataTypeRepository : IRepository<IDataType>
    {
        IEnumerable<IDataType> WithMemberType(IDataType dataType);
    }
}