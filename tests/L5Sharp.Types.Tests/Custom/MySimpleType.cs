using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types.Tests.Custom
{
    public class MySimpleType : StructureType
    {
        public MySimpleType() : base(nameof(MySimpleType), "Simple Type")
        {
        }

        public BOOL M1 = new();
        public SINT M2 = new();
        public INT M3 = new();
        public DINT M4 = new();
        public LINT M5 = new();
        public REAL M6 = new();

        public override DataTypeClass Class => DataTypeClass.User;
    }
}