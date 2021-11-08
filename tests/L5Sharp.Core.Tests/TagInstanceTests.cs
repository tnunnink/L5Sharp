using System.Linq;
using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagInstanceTests
    {
        [Test]
        public void String_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.OfType<String>("Test");

            tag.Should().NotBeNull();
            tag.GetMembers().Should().HaveCount(2);
            tag.GetMembers().Any(t => t.Name == "LEN").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "DATA").Should().BeTrue();
        }
        
        [Test]
        public void Timer_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.OfType<Timer>("Test");

            tag.Should().NotBeNull();
            tag.GetMembers().Should().HaveCount(5);
            tag.GetMembers().Any(t => t.Name == "PRE").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "ACC").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "EN").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "TT").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "DN").Should().BeTrue();
        }


        [Test]
        public void Counter_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.OfType<Counter>("Test");

            tag.Should().NotBeNull();
            tag.GetMembers().Should().HaveCount(7);
            tag.GetMembers().Any(t => t.Name == "PRE").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "ACC").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "CU").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "CD").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "DN").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "OV").Should().BeTrue();
            tag.GetMembers().Any(t => t.Name == "UN").Should().BeTrue();
        }
    }
}