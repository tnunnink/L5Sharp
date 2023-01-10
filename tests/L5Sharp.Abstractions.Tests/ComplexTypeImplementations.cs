using System.Collections.Generic;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Abstractions.Tests
{
    public class TestComplex : ComplexType
    {
        public TestComplex() : base(nameof(TestComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;
        protected override IDataType New() => new TestComplex();

        public IMember<BOOL> TestMember = Member.Create<BOOL>(nameof(TestMember));

        public IMember<NestedComplex> Nested = Member.Create<NestedComplex>(nameof(Nested));
    }

    public class NestedComplex : ComplexType
    {
        public NestedComplex() : base(nameof(NestedComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;
        protected override IDataType New() => new NestedComplex();

        public IMember<DINT> M1 = Member.Create<DINT>(nameof(M1));
        public IMember<DINT> M2 = Member.Create<DINT>(nameof(M2));
        public IMember<DINT> M3 { get; } = Member.Create<DINT>(nameof(M3));
    }

    public class MemberConstructorComplex : ComplexType
    {
        public MemberConstructorComplex() : base(nameof(MemberLessComplex), new List<IMember<IDataType>>
        {
            Member.Create<BOOL>("Indy"),
            Member.Create<SINT>("Value"),
            Member.Create<TIMER>("TON"),
        })
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;
        protected override IDataType New() => new MemberConstructorComplex();
    }

    public class MemberLessComplex : ComplexType
    {
        public MemberLessComplex() : base(nameof(MemberLessComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;
        
        protected override IDataType New() => new MemberLessComplex();
    }

    public class NullNameComplex : ComplexType
    {
        public NullNameComplex() : base(null!)
        {
        }

        public override DataTypeClass Class => DataTypeClass.Unknown;
        
        protected override IDataType New() => new NullNameComplex();
    }

    public class DuplicateMemberNameComplex : ComplexType
    {
        public DuplicateMemberNameComplex() :
            base(nameof(DuplicateMemberNameComplex))
        {
        }

        public IMember<BOOL> Member01 = Member.Create<BOOL>("Member01");
        public IMember<BOOL> Member02 = Member.Create<BOOL>("Member01");

        public override DataTypeClass Class => DataTypeClass.Predefined;
        
        protected override IDataType New() => new DuplicateMemberNameComplex();
    }

    public class NullMemberComplex : ComplexType
    {
        public NullMemberComplex() : base(nameof(NullMemberComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;
        protected override IDataType New() => new NullMemberComplex();

        public IMember<DINT> Member { get; set; }
    }
}