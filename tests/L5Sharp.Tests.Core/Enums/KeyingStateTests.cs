using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class KeyingStateTests
    {
        [Test]
        public void ExactMatch_WhenCalled_ShouldNotBeNull()
        {
            ElectronicKeying.ExactMatch.Should().NotBeNull();
        }
        
        [Test]
        public void CompatibleModule_WhenCalled_ShouldNotBeNull()
        {
            ElectronicKeying.CompatibleModule.Should().NotBeNull();
        }
        
        [Test]
        public void Custom_WhenCalled_ShouldNotBeNull()
        {
            ElectronicKeying.Custom.Should().NotBeNull();
        }
        
        [Test]
        public void Disabled_WhenCalled_ShouldNotBeNull()
        {
            ElectronicKeying.Disabled.Should().NotBeNull();
        }
    }
}