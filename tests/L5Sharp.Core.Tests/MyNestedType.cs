using L5Sharp.Abstractions;
using L5Sharp.Creators;
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

        public IMember<BOOL> Indy = Member.Create<BOOL>(nameof(Indy), description:"Test BOOL Member");
        public IMember<STRING> Str = Member.Create<STRING>(nameof(Str), description:"Test String Member");
        public IMember<TIMER> Tmr = Member.Create<TIMER>(nameof(Tmr), description:"Test Timer Member");
        public IMember<MySimpleType> Simple = Member.Create<MySimpleType>(nameof(Simple));

        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MyNestedType();
    }

    public class MySimpleType : ComplexType
    {
        public MySimpleType() : base(nameof(MySimpleType))
        {
        }

        public override string Description => "Simple Type";

        public IMember<BOOL> M1 = Member.Create<BOOL>(nameof(M1));
        public IMember<SINT> M2 = Member.Create<SINT>(nameof(M2));
        public IMember<INT> M3 = Member.Create<INT>(nameof(M3));
        public IMember<DINT> M4 = Member.Create<DINT>(nameof(M4));
        public IMember<LINT> M5 = Member.Create<LINT>(nameof(M5));
        public IMember<REAL> M6 = Member.Create<REAL>(nameof(M6));

        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MySimpleType();
    }
}