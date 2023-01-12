using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Types.Tests.Custom
{
    /// <summary>
    /// A test type used to test nested complex data structure code
    /// </summary>
    public class MyNestedType : StructureType
    {
        public MyNestedType() : base(nameof(MyNestedType), "This is the user defined description")
        {
        }

        public BOOL Indy = new();
        public STRING Str = new();
        public TIMER Tmr = new();
        public MySimpleType Simple = new();

        public override DataTypeClass Class => DataTypeClass.User;
    }

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