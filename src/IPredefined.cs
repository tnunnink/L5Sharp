using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IPredefined : IDataType
    {
        public bool IsAtomic { get; }
        public object DefaultValue { get; }
        public Radix DefaultRadix { get; }
        public IMember GetMember(string name);
    }
}