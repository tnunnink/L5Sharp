using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class TagUsageTests
    {
        [Test]
        public void Null_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Null.Should().NotBeNull();
        }
        
        [Test]
        public void Normal_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Normal.Should().NotBeNull();
        }
        
        [Test]
        public void Local_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Local.Should().NotBeNull();
        }
        
        [Test]
        public void Public_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Public.Should().NotBeNull();
        }
        
        [Test]
        public void Input_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Input.Should().NotBeNull();
        }
        
        [Test]
        public void Output_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Output.Should().NotBeNull();
        }
        
        [Test]
        public void InOut_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.InOut.Should().NotBeNull();
        }
        
        [Test]
        public void Static_WhenCalled_ShouldNotBeNull()
        {
            TagUsage.Static.Should().NotBeNull();
        }
        
        [Test]
        public void AoiDefault_AtomicType_ShouldBeInput()
        {
            var usage = TagUsage.Default(new BOOL());

            usage.Should().Be(TagUsage.Input);
        }
        
        [Test]
        public void AoiDefault_ComplexType_ShouldBeInOut()
        {
            var usage = TagUsage.Default(new TIMER());

            usage.Should().Be(TagUsage.InOut);
        }
    }
}