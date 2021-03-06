using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class SintTests
    {
        private sbyte _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<sbyte>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new SINT();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new SINT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(SINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be("Logix representation of a System.SByte");
            type.Value.Should().Be(0);
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new SINT(_random);
            
            type.Value.Should().Be(_random);
        }

        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            SINT.MaxValue.Should().Be(sbyte.MaxValue);
        }
        
        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            SINT.MinValue.Should().Be(sbyte.MinValue);
        }

        [Test]
        public void GetValue_AsAtomic_ShouldBeExpected()
        {
            var type = (IAtomicType) new SINT();

            type.Value.Should().Be(0);
        }

        [Test]
        public void SetValue_Null_ShouldBeExpected()
        {
            var type = new SINT();

            FluentActions.Invoking(() => type.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_ValidType_ShouldBeExpected()
        {
            var type = new SINT();

            type.SetValue(_random);

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_NegativeNumber_ShouldBeExpected()
        {
            
        }
        
        [Test]
        public void SetValue_ObjectValidValue_ShouldBeExpected()
        {
            var type = new SINT();

            type.SetValue((object) _random);

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_ObjectSint_ShouldBeExpected()
        {
            var type = new SINT();

            type.SetValue((object) new SINT(_random));

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldBeExpected()
        {
            var type = new SINT();

            type.SetValue(_random.ToString());

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new SINT(_random);

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<DateTime>();
            var type = new SINT();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Format_DefaultRadix_ShouldBeExpected()
        {
            var type = new SINT();

            var format = type.Format();

            format.Should().Be("0");
        }
        
        [Test]
        public void Format_OverloadedRadix_ShouldBeExpected()
        {
            var type = new SINT();

            var format = type.Format(Radix.Binary);

            format.Should().Be("2#0000_0000");
        }
        
        [Test]
        public void Instantiate_WhenCalled_ShouldEqualDefaultInstance()
        {
            var type = new SINT(_random);

            var instance = type.Instantiate();

            instance.Should().BeEquivalentTo(new SINT());
        }

        [Test]
        public void ImplicitOperator_Sint_ShouldBeExpected()
        {
            SINT type = _random;

            type.Value.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_byte_ShouldBeExpected()
        {
            sbyte value = new SINT(_random);

            value.Should().Be(_random);
        }
        
        [Test]
        public void Parse_ValidString_ShouldBeExpected()
        {
            var type = SINT.Parse(_random.ToString());

            type.Value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new SINT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new SINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new SINT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new SINT();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new SINT();
            var second = new SINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new SINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.Value.GetHashCode());
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new SINT();

            type.ToString().Should().Be(type.Value.ToString());
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new SINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new SINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new SINT();
            var second = new SINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}