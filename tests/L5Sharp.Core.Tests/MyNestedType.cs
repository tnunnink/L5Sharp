using L5Sharp.Components;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Core.Tests
{
    /// <summary>
    /// A test type used to test nested complex data structure code
    /// </summary>
    public class MyNestedType : UserDefined
    {
        public MyNestedType() :
            base(nameof(MyNestedType), "This is a test data type")
        {
            Members.Add(Indy);
            Members.Add(Str);
            Members.Add(Tmr);
        }

        public IMember<Bool> Indy = Member.Create<Bool>(nameof(Indy));
        public IMember<String> Str = Member.Create<String>(nameof(Str));
        public IMember<Timer> Tmr = Member.Create<Timer>(nameof(Tmr));
    }
}