using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class ULintTests
    {
        private ulong _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<ulong>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new ULINT();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new ULINT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(ULINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Description.Should().Be("Logix representation of a System.UInt64");
            type.Value.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new ULINT(_random);
            
            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            ULINT.MaxValue.Should().Be(ulong.MaxValue);
        }
        
        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            ULINT.MinValue.Should().Be(ulong.MinValue);
        }
        
        [Test]
        public void GetValue_AsAtomic_ShouldBeExpected()
        {
            var type = (IAtomicType) new ULINT();

            type.Value.Should().Be(0);
        }

        [Test]
        public void SetValue_Null_ShouldBeExpected()
        {
            var type = new ULINT();

            FluentActions.Invoking(() => type.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_ValidShort_ShouldBeExpected()
        {
            var type = new ULINT();

            type.SetValue(_random);

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_ValidByte_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = new ULINT();

            type.SetValue(value);

            type.Value.Should().Be(value);
        }

        [Test]
        public void SetValue_SameType_ShouldBeExpected()
        {
            var type = new ULINT();

            type.SetValue(new ULINT(_random));

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_SameTypeAsObject_ShouldBeExpected()
        {
            var type = new ULINT();

            type.SetValue((object)new ULINT(_random));

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_ULint_ShouldBeExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<byte>();
            var type = new ULINT();

            type.SetValue(new ULINT(value));

            type.Value.Should().Be(value);
        }

        [Test]
        public void SetValue_ValidObjectValue_ShouldBeExpected()
        {
            var type = new ULINT();

            type.SetValue((object) _random);

            type.Value.Should().Be(_random);
        }

        [Test]
        public void SetValue_ValidStringValue_ShouldBeExpected()
        {
            var type = new ULINT();

            type.SetValue(_random.ToString());

            type.Value.Should().Be(_random);
        }
        
        [Test]
        public void SetValue_InvalidString_ShouldThrowArgumentException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new ULINT(_random);

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetValue_InvalidType_ShouldBeZero()
        {
            var fixture = new Fixture();
            var value = fixture.Create<DateTime>();
            var type = new ULINT();

            FluentActions.Invoking(() => type.SetValue(value)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Format_DefaultRadix_ShouldBeExpected()
        {
            var type = new ULINT();

            var format = type.Format();

            format.Should().Be("0");
        }
        
        [Test]
        public void Format_OverloadedRadix_ShouldBeExpected()
        {
            var type = new ULINT();

            var format = type.Format(Radix.Binary);

            format.Should().Be("2#0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000");
        }
        
        [Test]
        public void Instantiate_WhenCalled_ShouldEqualDefaultInstance()
        {
            var type = new ULINT(_random);

            var instance = type.Instantiate();

            instance.Should().BeEquivalentTo(new ULINT());
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            ULINT type = _random;

            type.Value.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            ulong value = new ULINT(_random);

            value.Should().Be(_random);
        }
        
        [Test]
        public void ImplicitOperator_ValidString_ShouldBeExpected()
        {
            ULINT type = _random.ToString();

            type.Value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ULINT();
            var second = new ULINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new ULINT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new ULINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ULINT();
            var second = new ULINT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new ULINT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new ULINT();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ULINT();
            var second = new ULINT();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new ULINT();
            var second = new ULINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new ULINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.Value.GetHashCode());
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new ULINT();

            type.ToString().Should().Be(type.Value.ToString());
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new ULINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new ULINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new ULINT();
            var second = new ULINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}