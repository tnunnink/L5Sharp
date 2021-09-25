using System.Collections.Generic;
using LogixHelper.Enumerations;
using LogixHelper.Primitives;

namespace LogixHelper.Abstractions
{
    public interface IDataType
    {
        public string Name { get; }
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic { get; }
        public IEnumerable<DataTypeMember> Members { get; }
    }
}