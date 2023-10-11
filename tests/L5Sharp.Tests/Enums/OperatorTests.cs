using FluentAssertions;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class OperatorTests
    {
        [Test]
        public void Assignment_WhenCalled_ShouldNotBeNull()
        {
            Operator.Assignment.Should().NotBeNull();
        }
        
        [Test]
        public void Equal_WhenCalled_ShouldNotBeNull()
        {
            Operator.Equal.Should().NotBeNull();
        }
        
        [Test]
        public void NotEqual_WhenCalled_ShouldNotBeNull()
        {
            Operator.NotEqual.Should().NotBeNull();
        }
        
        [Test]
        public void GreaterThan_WhenCalled_ShouldNotBeNull()
        {
            Operator.GreaterThan.Should().NotBeNull();
        }
        
        [Test]
        public void GreaterThanOrEqual_WhenCalled_ShouldNotBeNull()
        {
            Operator.GreaterThanOrEqual.Should().NotBeNull();
        }
        
        [Test]
        public void LessThan_WhenCalled_ShouldNotBeNull()
        {
            Operator.LessThan.Should().NotBeNull();
        }
        
        [Test]
        public void LessThanOrEqual_WhenCalled_ShouldNotBeNull()
        {
            Operator.LessThanOrEqual.Should().NotBeNull();
        }
        
        [Test]
        public void Add_WhenCalled_ShouldNotBeNull()
        {
            Operator.Add.Should().NotBeNull();
        }
        
        [Test]
        public void Subtract_WhenCalled_ShouldNotBeNull()
        {
            Operator.Subtract.Should().NotBeNull();
        }
        
        [Test]
        public void Multiply_WhenCalled_ShouldNotBeNull()
        {
            Operator.Multiply.Should().NotBeNull();
        }
        
        [Test]
        public void Exponent_WhenCalled_ShouldNotBeNull()
        {
            Operator.Exponent.Should().NotBeNull();
        }
        
        [Test]
        public void Divide_WhenCalled_ShouldNotBeNull()
        {
            Operator.Divide.Should().NotBeNull();
        }
        
        [Test]
        public void Modulo_WhenCalled_ShouldNotBeNull()
        {
            Operator.Mod.Should().NotBeNull();
        }
        
        [Test]
        public void And_WhenCalled_ShouldNotBeNull()
        {
            Operator.And.Should().NotBeNull();
        }
        
        [Test]
        public void Or_WhenCalled_ShouldNotBeNull()
        {
            Operator.Or.Should().NotBeNull();
        }
        
        [Test]
        public void Xor_WhenCalled_ShouldNotBeNull()
        {
            Operator.Xor.Should().NotBeNull();
        }
        
        [Test]
        public void Not_WhenCalled_ShouldNotBeNull()
        {
            Operator.Not.Should().NotBeNull();
        }
    }
}