using System.Collections.Generic;

namespace L5Sharp
{
    public interface IAddOnDefined : IDataType
    {
        IEnumerable<IMember<IDataType>> Members { get; }
    }
}