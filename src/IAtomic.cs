using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IAtomic : IDataType
    {
        object Value { get; }
        string FormattedValue { get; }
        void SetValue(object value);
        void SetRadix(Radix radix);
        bool SupportsRadix(Radix radix);
    }

    public interface IAtomic<T> : IAtomic where T : struct
    {
        new T Value { get; }
        void SetValue(T value);
    }
}