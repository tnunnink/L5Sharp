using AutoFixture;
using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class OperandTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }
        [Test]
        public void New_Null_ShouldThrowArgumentException()
        {
            
        }
        
        [Test]
        public void New_NonNull_ShouldNotBeNull()
        {
            Operand operand = 0;

            operand.Should().NotBeNull();
        }
        
        [Test]
        public void New_Valid_ShouldBeExpected()
        {
            Operand operand = 0;

            operand.Should().Be(0);
        }

        [Test]
        public void IsValue_int_ShouldBeTrue()
        {
            Operand operand = 0;

            operand.IsValue.Should().BeTrue();
        }

        [Test]
        public void IsValue_Sint_ShouldBeTrue()
        {
            Operand operand = new SINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_Int_ShouldBeTrue()
        {
            Operand operand = new INT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_Dint_ShouldBeTrue()
        {
            Operand operand = new DINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_Lint_ShouldBeTrue()
        {
            Operand operand = new LINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_USint_ShouldBeTrue()
        {
            Operand operand = new USINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_UInt_ShouldBeTrue()
        {
            Operand operand = new UINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_UDint_ShouldBeTrue()
        {
            Operand operand = new UDINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_ULint_ShouldBeTrue()
        {
            Operand operand = new ULINT();

            operand.IsValue.Should().BeTrue();
        }
        
        [Test]
        public void IsValue_Real_ShouldBeTrue()
        {
            Operand operand = new REAL();

            operand.IsValue.Should().BeTrue();
        }

        [Test]
        public void IsReference_TagName_ShouldBeTrue()
        {
            Operand operand = new TagName("MyTag.MemberName[0].Bit");

            operand.IsReference.Should().BeTrue();
        }
    }
}