using System.Collections.Generic;
using L5Sharp.Base;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
{
    public interface IDataType : INamedComponent
    {
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic { get; }
        public IEnumerable<Member> Members { get; }
        public bool SupportsRadix(Radix radix); //todo still not sure about this
    }
}