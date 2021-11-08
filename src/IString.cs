using System.Collections.Generic;
using L5Sharp.Types;
// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    public interface IString : IDataType
    {
        IEnumerable<IMember<IDataType>> Members { get; }
        IMember<Dint> LEN { get; }
        IMember<Sint> DATA { get; }
        string Get();
        void Set(string value);
    }
}