using System.Collections.Generic;
using L5Sharp.Enumerations;

namespace L5Sharp.Abstractions
{
    public interface IDataType : INamedComponent
    {
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic { get; }
        public IEnumerable<IMember> Members { get; }
        public bool SupportsRadix(Radix radix); //todo still not sure about this
    }
}