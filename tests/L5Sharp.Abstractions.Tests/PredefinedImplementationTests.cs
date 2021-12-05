using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Abstractions.Tests
{
    [TestFixture]
    public class PredefinedImplementationTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new ValidPredefined();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_WhenCalled_ShouldBePredefined()
        {
            var type = new ValidPredefined();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }
        
    }

    public class ValidPredefined : ComplexType
    {
        public ValidPredefined() :
            base(nameof(ValidPredefined))
        {
        }

        public IMember<Bool> Member01 => Member.Create<Bool>(nameof(Member01));
        public IMember<Int> Member02 => Member.Create<Int>(nameof(Member02));
        public IMember<Dint> Member03 => Member.Create<Dint>(nameof(Member03));
        public IMember<Real> Member04 => Member.Create<Real>(nameof(Member04));
        public override DataTypeClass Class => DataTypeClass.User;

        protected override IDataType New()
        {
            return new ValidPredefined();
        }
    }
}