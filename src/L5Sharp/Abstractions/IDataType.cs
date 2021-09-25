using System.Collections.Generic;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
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