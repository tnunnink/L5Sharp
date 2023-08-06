using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests.Enums
{
    [TestFixture]
    public class TagTypeTests
    {
        [Test]
        public void Base_WhenCalled_ShouldNotBeNull()
        {
            TagType.Base.Should().NotBeNull();
        }
        
        [Test]
        public void Alias_WhenCalled_ShouldNotBeNull()
        {
            TagType.Alias.Should().NotBeNull();
        }
        
        [Test]
        public void Produced_WhenCalled_ShouldNotBeNull()
        {
            TagType.Produced.Should().NotBeNull();
        }
        
        [Test]
        public void Consumed_WhenCalled_ShouldNotBeNull()
        {
            TagType.Consumed.Should().NotBeNull();
        }
    }
}