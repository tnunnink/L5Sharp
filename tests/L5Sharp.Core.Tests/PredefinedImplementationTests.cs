using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
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

        private static IEnumerable<ReadOnlyMember> GenerateMembers()
        {
            return new List<ReadOnlyMember>
            {
                ReadOnlyMember.New("Member01", Bool),
                ReadOnlyMember.New("Member02", Bool),
                ReadOnlyMember.New("Member03", Bool),
                ReadOnlyMember.New("Member04", Bool)
            };
        }

        public IMember Member01 => Members.SingleOrDefault(m => m.Name == nameof(Member01));
        public IMember Member02 => Members.SingleOrDefault(m => m.Name == nameof(Member02));
        public IMember Member03 => Members.SingleOrDefault(m => m.Name == nameof(Member03));
        public IMember Member04 => Members.SingleOrDefault(m => m.Name == nameof(Member04));

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