using System.Collections.Generic;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Abstractions.Tests
{
        public class TestComplex : ComplexType
    {
        public TestComplex() : base(nameof(TestComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New() => new TestComplex();

        public IMember<Bool> TestMember = Member.Create<Bool>(nameof(TestMember));
        
        public IMember<NestedType> Nested = Member.Create<NestedType>(nameof(Nested));
    }
    
    public class MemberLessComplex : ComplexType
    {
        public MemberLessComplex() : base(nameof(MemberLessComplex))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New() => new TestComplex();
    }
    
    public class MemberConstructorComplex : ComplexType
    {
        public MemberConstructorComplex() : base(nameof(MemberLessComplex), string.Empty, new List<IMember<IDataType>>
        {
            Member.Create<Bool>("Indy"),
            Member.Create<Sint>("Value"),
            Member.Create<Timer>("TON"),
        })
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New() => new TestComplex();
    }
    
    public class MemberComboComplex : ComplexType
    {
        public MemberComboComplex() : base(nameof(MemberComboComplex), string.Empty, new List<IMember<IDataType>>
        {
            Member.Create<Bool>("Indy"),
            Member.Create<Sint>("Value"),
            Member.Create<Timer>("TON"),
        })
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New() => new TestComplex();
        
        public IMember<Dint> M1 = Member.Create<Dint>(nameof(M1));
        public IMember<Dint> M2 = Member.Create<Dint>(nameof(M2));
        public IMember<Dint> M3 = Member.Create<Dint>(nameof(M3));
    }

    public class NestedType : ComplexType
    {
        public NestedType() : base(nameof(NestedType))
        {
        }

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New() => new NestedType();

        public IMember<Dint> M1 = Member.Create<Dint>(nameof(M1));
        public IMember<Dint> M2 = Member.Create<Dint>(nameof(M2));
        public IMember<Dint> M3 { get; } = Member.Create<Dint>(nameof(M3));
    }

    public class MyNullNamePredefined : ComplexType
    {
        public MyNullNamePredefined() : base(null!)
        {
        }

        public override DataTypeClass Class => DataTypeClass.Unknown;

        protected override IDataType New() => new MyNullNamePredefined();
    }

    public class MyInvalidMemberPredefined : ComplexType
    {
        public MyInvalidMemberPredefined() :
            base(nameof(MyInvalidMemberPredefined))
        {
        }

        public IMember<Bool> Member01 = Member.Create<Bool>("Member01");
        public IMember<Bool> Member02 = Member.Create<Bool>("Member01");

        public override DataTypeClass Class => DataTypeClass.Predefined;

        protected override IDataType New() => new MyInvalidMemberPredefined();
    }
}