using L5Sharp.Abstractions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Core.Tests
{
    /// <summary>
    /// A test type used to test nested complex data structure code
    /// </summary>
    public class MyNestedType : ComplexType
    {
        public MyNestedType() : base(nameof(MyNestedType))
        {
        }

        public override string Description => "This is the user defined description";

        public IMember<Bool> Indy = Member.Create<Bool>(nameof(Indy), description:"Test Bool Member");
        public IMember<String> Str = Member.Create<String>(nameof(Str), description:"Test String Member");
        public IMember<Timer> Tmr = Member.Create<Timer>(nameof(Tmr), description:"Test Timer Member");
        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MyNestedType();
    }
}