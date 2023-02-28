using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Custom
{
    public class MySimpleType : StructureType
    {
        public MySimpleType() : base(nameof(MySimpleType))
        {
        }

        public BOOL M1 { get; set; } = new();
        public SINT M2 { get; set; } = new();
        public INT M3 { get; set; } = new();
        public DINT M4 { get; set; } = new();
        public LINT M5 { get; set; } = new();
        public REAL M6 { get; set; } = new();

        public override DataTypeClass Class => DataTypeClass.User;
    }
}