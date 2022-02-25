using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Predefined;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class ControlTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new Control();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new Control();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new Control();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new Control());
        }
    }
}