using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IAtomic : IDataType
    {
        object Default { get; }
        object GetValue();
        void SetValue(object value);
        bool SupportsRadix(Radix radix);
    }

    public interface IAtomic<T> : IAtomic where T : struct
    {
        new T GetValue();
        void SetValue(T value);
    }
}