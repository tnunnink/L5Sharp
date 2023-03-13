using System.Linq;
using L5Sharp.Core;
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
        }

        public override DataTypeClass Class => DataTypeClass.User;

        /// <summary>
        /// A simple boolean member
        /// </summary>
        public BOOL Indy { get; set; } = new();

        /// <summary>
        /// A string member
        /// </summary>
        public STRING Str { get; set; } = new();

        /// <summary>
        /// A nested timer member
        /// </summary>
        public TIMER Tmr { get; set; } = new();

        /// <summary>
        /// A nested user defined type
        /// </summary>
        public MySimpleType Simple { get; set; } = new();

        /// <summary>
        /// A nested array of atomic values.
        /// </summary>
        public ArrayType<BOOL> Flags { get; set; } = Logix.Array<BOOL>(10);

        /// <summary>
        /// A nested array of structure types.
        /// </summary>
        public ArrayType<MESSAGE> Messages { get; set; } = Logix.Array<MESSAGE>(10);
    }
}