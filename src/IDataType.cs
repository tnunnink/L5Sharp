using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IDataType : IComponent
    {
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic { get; }
        public TagDataFormat DataFormat { get; }
        public IEnumerable<IMember> Members { get; }
        public IEnumerable<IDataType> GetDependentTypes();
    }
}