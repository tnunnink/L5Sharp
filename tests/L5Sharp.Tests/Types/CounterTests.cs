using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;

namespace L5Sharp.Tests.Types
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
            
            type.PRE.Should().Be(5000);
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new COUNTER();
            
            type.Class.Should().Be(DataTypeClass.Predefined);
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