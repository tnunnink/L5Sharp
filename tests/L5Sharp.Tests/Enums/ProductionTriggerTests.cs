using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class ProductionTriggerTests
    {
        [Test]
        public void Application_WhenCalled_ShouldNotBeNull()
        {
            ProductionTrigger.Application.Should().NotBeNull();
        }
        
        [Test]
        public void Cyclic_WhenCalled_ShouldNotBeNull()
        {
            ProductionTrigger.Cyclic.Should().NotBeNull();
        }
        
        [Test]
        public void Cos_WhenCalled_ShouldNotBeNull()
        {
            ProductionTrigger.Cos.Should().NotBeNull();
        }
    }
}