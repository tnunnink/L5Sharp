using System.Collections.Generic;
using L5Sharp.Types;
// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    public interface IString : IDataType
    {
        string Value { get; }
        IMember<Dint> LEN { get; }
        IMember<Sint> DATA { get; }
        IEnumerable<IMember<IDataType>> Members { get; }
        void SetValue(string value);
    }
}