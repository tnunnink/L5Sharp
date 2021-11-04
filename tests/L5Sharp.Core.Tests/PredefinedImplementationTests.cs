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

        private static IEnumerable<Member> GenerateMembers()
        {
            return new List<Member>
            {
                new("Member01", Logix.DataType.Bool),
                new("Member02", Logix.DataType.Bool),
                new("Member03", Logix.DataType.Bool),
                new("Member04", Logix.DataType.Bool),
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