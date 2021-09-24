using LogixHelper.Enumerations;

namespace LogixHelper.Abstractions
{
    public interface IDataType
    {
        public string Name { get; }
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic { get; }
    }
}