using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagInstanceTests
    {
        [Test]
        public void String_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", Logix.DataType.String);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(2);
            tag.Members.Any(t => t.Name == "LEN").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DATA").Should().BeTrue();
        }
        
        [Test]
        public void Timer_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", Logix.DataType.Timer);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(5);
            tag.Members.Any(t => t.Name == "PRE").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ACC").Should().BeTrue();
            tag.Members.Any(t => t.Name == "EN").Should().BeTrue();
            tag.Members.Any(t => t.Name == "TT").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DN").Should().BeTrue();
        }


        [Test]
        public void Counter_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", Logix.DataType.Counter);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(7);
            tag.Members.Any(t => t.Name == "PRE").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ACC").Should().BeTrue();
            tag.Members.Any(t => t.Name == "CU").Should().BeTrue();
            tag.Members.Any(t => t.Name == "CD").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DN").Should().BeTrue();
            tag.Members.Any(t => t.Name == "OV").Should().BeTrue();
            tag.Members.Any(t => t.Name == "UN").Should().BeTrue();
        }
    }
}