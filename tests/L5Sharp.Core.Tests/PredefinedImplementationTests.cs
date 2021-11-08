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
    public class PredefinedImplementationTests : Predefined
    {
        public PredefinedImplementationTests() :
            base(nameof(PredefinedImplementationTests), DataTypeFamily.None, GenerateMembers())
        {
        }

        private static IEnumerable<Member<IDataType>> GenerateMembers()
        {
            return new List<Member<IDataType>>
            {
                new("Member01", new Bool()),
                new("Member02", new Bool()),
                new("Member03", new Bool()),
                new("Member04", new Bool()),
            };
        }

        public IMember<Bool> Member01 => GetMember<Bool>(nameof(Member01));
        public IMember<Bool> Member02 => GetMember<Bool>(nameof(Member02));
        public IMember<Bool> Member03 => GetMember<Bool>(nameof(Member03));
        public IMember<Bool> Member04 => GetMember<Bool>(nameof(Member04));

        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new PredefinedImplementationTests();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_WhenCalled_ShouldBePredefined()
        {
            var type = new PredefinedImplementationTests();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }
    }
}