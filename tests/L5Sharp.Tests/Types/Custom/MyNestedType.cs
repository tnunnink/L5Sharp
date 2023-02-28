using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Custom
{
    /// <summary>
    /// A test type used to test nested complex data structure code
    /// </summary>
    public class MyNestedType : StructureType
    {
        public MyNestedType() : base(nameof(MyNestedType))
        {
            Messages = new[] { new MESSAGE(), new MESSAGE(), new MESSAGE(), new MESSAGE(), new MESSAGE() };
        }

        public BOOL Indy { get; set; } = new();
        public STRING Str { get; set; } = new();
        public TIMER Tmr { get; } = new();
        public MySimpleType Simple { get; } = new();

        public BOOL[] Flags { get; } =
        {
            new(), new(), new(), new(), new(), new(), new(), new(), new(), new()
        };

        public MESSAGE[] Messages { get; }

        public override DataTypeClass Class => DataTypeClass.User;
    }
}