using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public sealed class Undefined : IDataType
    {
        public string Name => nameof(Undefined);
        public string Description => "Undefined DataType";
        public Radix Radix => Radix.Null;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Predefined;
        public TagDataFormat DataFormat => null;
        public IDataType Instantiate()
        {
            return new Undefined();
        }
    }
}