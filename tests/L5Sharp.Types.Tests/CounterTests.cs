using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class CounterTests
    {
        [Test]
        public void Constructor_WhenCalled_ShouldNotBeNull()
        {
            var type = new COUNTER();
            
            type.Should().NotBeNull();
        }
        
        [Test]
        public void Constructor_ValidDint_ShouldHaveExpectedPREValue()
        {
            var type = new COUNTER(new DINT(5000));
            
            type.PRE.DataType.Value.Should().Be(5000);
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new COUNTER();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeEqualToDefault()
        {
            var type = new COUNTER();

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().BeEquivalentTo(new COUNTER());
        }

        [Test]
        public void Members_ShouldNotBeNull()
        {
            var type = new COUNTER();
            type.DN.Should().NotBeNull();
            type.CD.Should().NotBeNull();
            type.CU.Should().NotBeNull();
            type.OV.Should().NotBeNull();
            type.UN.Should().NotBeNull();
            type.ACC.Should().NotBeNull();
            type.PRE.Should().NotBeNull();
        }
    }
}