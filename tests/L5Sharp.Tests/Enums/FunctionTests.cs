using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public void IsValid_Null_ShouldBeFalse()
        {
            var result = Function.IsValid(null!);

            result.Should().BeFalse();
        }
        
        [Test]
        public void IsValid_ValidFunction_ShouldBeTrue()
        {
            var result = Function.IsValid("ABS");

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsValid_InvalidFunction_ShouldBeTrue()
        {
            var result = Function.IsValid("FAKE");

            result.Should().BeFalse();
        }
        
        [Test]
        public void ABS_WhenCalled_ShouldNotBeNull()
        {
            Function.ABS.Should().NotBeNull();
        }
        
        [Test]
        public void ACOS_WhenCalled_ShouldNotBeNull()
        {
            Function.ACS.Should().NotBeNull();
        }
        
        [Test]
        public void ASIN_WhenCalled_ShouldNotBeNull()
        {
            Function.ASN.Should().NotBeNull();
        }
        
        [Test]
        public void ATAN_WhenCalled_ShouldNotBeNull()
        {
            Function.ATN.Should().NotBeNull();
        }
        
        [Test]
        public void COS_WhenCalled_ShouldNotBeNull()
        {
            Function.COS.Should().NotBeNull();
        }
        
        [Test]
        public void DEG_WhenCalled_ShouldNotBeNull()
        {
            Function.DEG.Should().NotBeNull();
        }
        
        [Test]
        public void LN_WhenCalled_ShouldNotBeNull()
        {
            Function.LN.Should().NotBeNull();
        }
        
        [Test]
        public void LOG_WhenCalled_ShouldNotBeNull()
        {
            Function.LOG.Should().NotBeNull();
        }
        
        [Test]
        public void RAD_WhenCalled_ShouldNotBeNull()
        {
            Function.RAD.Should().NotBeNull();
        }
        
        [Test]
        public void SIN_WhenCalled_ShouldNotBeNull()
        {
            Function.SIN.Should().NotBeNull();
        }
        
        [Test]
        public void SQRT_WhenCalled_ShouldNotBeNull()
        {
            Function.SQRT.Should().NotBeNull();
        }
        
        [Test]
        public void TAN_WhenCalled_ShouldNotBeNull()
        {
            Function.TAN.Should().NotBeNull();
        }
        
        [Test]
        public void TRUNC_WhenCalled_ShouldNotBeNull()
        {
            Function.TRUNC.Should().NotBeNull();
        }
    }
}