using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Tests.Types.Atomics
{
    [TestFixture]
    public class BoolTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new BOOL();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new BOOL();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(BOOL).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Should().Be(false);
        }

        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new BOOL(true);

            type.Should().Be(true);
        }

        [Test]
        public void New_IntZeroOverload_ShouldBeFalse()
        {
            var type = new BOOL(0);

            type.Should().Be(false);
        }

        [Test]
        public void New_IntPositiveOverload_ShouldBeTrue()
        {
            var type = new BOOL(1);

            type.Should().Be(true);
        }

        [Test]
        public void New_IntNegativeOverload_ShouldBeTrue()
        {
            var type = new BOOL(-1);

            type.Should().Be(true);
        }

        [Test]
        public void SetValue_ValidValue_ShouldReturnExpected()
        {
            var fixture = new Fixture();
            var value = fixture.Create<bool>();

            BOOL type = value;

            type.Should().Be(value);
        }

        [Test]
        public void Format_DefaultRadix_ShouldBeExpected()
        {
            var type = new BOOL();

            var format = type.ToString();

            format.Should().Be("0");
        }
        
        [Test]
        public void Format_OverloadedRadix_ShouldBeExpected()
        {
            var type = new BOOL();

            var format = type.ToString(Radix.Binary);

            format.Should().Be("2#0");
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            BOOL type = true;

            type.Should().Be(true);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            bool value = new BOOL();

            value.Should().Be(false);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new BOOL();
            var second = new BOOL();

            var result = first.Equals(second);

            result.Should().Be(true);
        }

        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new BOOL();

            var result = first.Equals(first);

            result.Should().Be(true);
        }


        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new BOOL();

            var result = first.Equals(null);

            result.Should().Be(false);
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new BOOL();
            var second = new BOOL();

            var result = first.Equals((object)second);

            result.Should().Be(true);
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new BOOL();

            var result = first.Equals((object)first);

            result.Should().Be(true);
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new BOOL();

            var result = first.Equals((object)null);

            result.Should().Be(false);
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new BOOL();
            var second = new BOOL();

            var result = first == second;

            result.Should().Be(true);
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new BOOL();
            var second = new BOOL();

            var result = first != second;

            result.Should().Be(false);
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfValue()
        {
            var type = new BOOL();

            var hash = type.GetHashCode();

            hash.Should().Be(false.GetHashCode());
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeValue()
        {
            var type = new BOOL();

            type.ToString().Should().Be("0");
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new BOOL();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new BOOL();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new BOOL();
            var second = new BOOL();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}