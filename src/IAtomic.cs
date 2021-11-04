using L5Sharp.Enums;

namespace L5Sharp
{
    internal interface IAtomic : IDataType
    {
        public object DefaultValue { get; }
        public Radix DefaultRadix { get; }
        bool SupportsRadix(Radix radix);
        bool IsValidValue(object value);
        object ParseValue(string value);
    }
}