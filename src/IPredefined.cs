using System.Collections.Generic;

namespace L5Sharp
{
    public interface IPredefined : IDataType
    {
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}