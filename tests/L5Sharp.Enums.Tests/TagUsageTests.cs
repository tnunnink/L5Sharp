using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Core;
using L5Sharp.Predefined;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
            var usage = TagUsage.AoiDefault(new Bool());

            usage.Should().Be(TagUsage.Input);
        }
        
        [Test]
        public void AoiDefault_ComplexType_ShouldBeInOut()
        {
            var usage = TagUsage.AoiDefault(new Timer());

            usage.Should().Be(TagUsage.InOut);
        }
        
        [Test]
        public void AoiDefault_ArrayType_ShouldBeInOut()
        {
            var usage = TagUsage.AoiDefault(new ArrayType<Dint>(10, new Dint()));

            usage.Should().Be(TagUsage.InOut);
        }
    }
}