using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    public sealed class Undefined : IDataType
    {
        public ComponentName Name => nameof(Undefined);
        public string Description => "Undefined DataType";
        public Radix Radix => Radix.Null;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => null;
        public TagDataFormat DataFormat => null;
        public IDataType Instantiate()
        {
            return new Undefined();
        }
    }
}