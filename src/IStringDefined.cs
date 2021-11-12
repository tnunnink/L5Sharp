using L5Sharp.Types;
// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp
{
    public interface IStringDefined : IDataType
    {
        string Value { get; }
        IMember<Dint> LEN { get; }
        IMember<Sint> DATA { get; }
        void SetValue(string value);
    }
}