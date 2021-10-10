using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface IDataType : IComponent
    {
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic { get; }
        public object DefaultValue { get; }
        public Radix DefaultRadix { get; }
        public TagDataFormat DataFormat { get; }
        public IEnumerable<IMember> Members { get; }
        public bool SupportsRadix(Radix radix);
        public bool IsValidValue(object value);
    }
}