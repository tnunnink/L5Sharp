using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IPredefined : IDataType
    {
        public object DefaultValue { get; }
        public Radix DefaultRadix { get; }
        public IMember GetMember(string name);
        bool SupportsRadix(Radix radix);
        bool IsValidValue(object value);
    }
}