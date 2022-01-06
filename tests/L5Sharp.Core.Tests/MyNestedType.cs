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

        public IMember<Bool> Indy = Member.Create<Bool>(nameof(Indy));
        public IMember<String> Str = Member.Create<String>(nameof(Str));
        public IMember<Timer> Tmr = Member.Create<Timer>(nameof(Tmr));
        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MyNestedType();
    }
}