using L5Sharp.Types;
// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    public interface IString : IDataType
    {
        Dint LEN { get; }
        Sint[] DATA { get; }
        string GetValue();
        void SetValue(string value);
    }
}