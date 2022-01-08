using System.Collections.Generic;
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

        public IMember<Bool> M1 = Member.Create<Bool>(nameof(M1));
        public IMember<Sint> M2 = Member.Create<Sint>(nameof(M2));
        public IMember<Int> M3 = Member.Create<Int>(nameof(M3));
        public IMember<Dint> M4 = Member.Create<Dint>(nameof(M4));
        public IMember<Lint> M5 = Member.Create<Lint>(nameof(M5));
        public IMember<Real> M6 = Member.Create<Real>(nameof(M6));

        public override DataTypeClass Class => DataTypeClass.User;
        protected override IDataType New() => new MySimpleType();
    }
}