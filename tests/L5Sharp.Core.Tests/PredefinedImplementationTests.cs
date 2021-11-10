using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
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

    public class ValidPredefined : Predefined
    {
        public ValidPredefined() :
            base(nameof(ValidPredefined))
        {
            RegisterMemberProperties();
        }

        public IMember<Bool> Member01 => Member.OfType<Bool>(nameof(Member01));
        public IMember<Int> Member02 => Member.OfType<Int>(nameof(Member02));
        public IMember<Dint> Member03 => Member.OfType<Dint>(nameof(Member03));
        public IMember<Real> Member04 => Member.OfType<Real>(nameof(Member04));
        protected override IDataType New()
        {
            return new ValidPredefined();
        }
    }
}