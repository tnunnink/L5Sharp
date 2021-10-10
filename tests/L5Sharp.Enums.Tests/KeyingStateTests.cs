using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class KeyingStateTests
    {
        [Test]
        public void ExactMatch_WhenCalled_ShouldNotBeNull()
        {
            KeyingState.ExactMatch.Should().NotBeNull();
        }
        
        [Test]
        public void CompatibleModule_WhenCalled_ShouldNotBeNull()
        {
            KeyingState.CompatibleModule.Should().NotBeNull();
        }
        
        [Test]
        public void Custom_WhenCalled_ShouldNotBeNull()
        {
            KeyingState.Custom.Should().NotBeNull();
        }
        
        [Test]
        public void Disabled_WhenCalled_ShouldNotBeNull()
        {
            KeyingState.Disabled.Should().NotBeNull();
        }
    }
}