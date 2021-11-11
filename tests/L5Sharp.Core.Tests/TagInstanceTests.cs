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
            tag.GetMember(m => m.LEN).Should().NotBeNull();
            tag.GetMember(m => m.DATA).Should().NotBeNull();
        }
        
        [Test]
        public void Timer_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.OfType<Timer>("Test");

            tag.Should().NotBeNull();
            tag.GetMember(m => m.PRE).Should().NotBeNull();
            tag.GetMember(m => m.ACC).Should().NotBeNull();
            tag.GetMember(m => m.TT).Should().NotBeNull();
            tag.GetMember(m => m.EN).Should().NotBeNull();
            tag.GetMember(m => m.DN).Should().NotBeNull();
        }
        
        [Test]
        public void Counter_ValidParameters_ShouldHaveValidMembers()
        {
            var tag = Tag.OfType<Counter>("Test");

            tag.Should().NotBeNull();
            tag.GetMember(m => m.PRE).Should().NotBeNull();
            tag.GetMember(m => m.ACC).Should().NotBeNull();
            tag.GetMember(m => m.CU).Should().NotBeNull();
            tag.GetMember(m => m.CD).Should().NotBeNull();
            tag.GetMember(m => m.DN).Should().NotBeNull();
            tag.GetMember(m => m.OV).Should().NotBeNull();
            tag.GetMember(m => m.UN).Should().NotBeNull();
        }
    }
}