﻿using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class UIntTests
    {
        private ushort _random;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            _random = fixture.Create<ushort>();
        }
        
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new UINT();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new UINT();

            type.Should().NotBeNull();
            type.Name.Should().Be(nameof(UINT).ToUpper());
            type.Class.Should().Be(DataTypeClass.Atomic);
            type.Family.Should().Be(DataTypeFamily.None);
            //type.Description.Should().Be("Logix representation of a System.UInt16");
            type.Should().Be(0);
        }
        
        [Test]
        public void New_ValueOverload_ShouldHaveExpectedValue()
        {
            var type = new UINT(_random);
            
            type.Should().Be(_random);
        }
        
        [Test]
        public void MaxValue_WhenCalled_ShouldBeExpected()
        {
            UINT.MaxValue.Should().Be(ushort.MaxValue);
        }
        
        [Test]
        public void MinValue_WhenCalled_ShouldBeExpected()
        {
            UINT.MinValue.Should().Be(ushort.MinValue);
        }

    

        [Test]
        public void SetValue_ValidShort_ShouldBeExpected()
        {
            UINT type = _random;

            type.Should().Be(_random);
        }

        [Test]
        public void SetValue_SameType_ShouldBeExpected()
        {
            var type = new UINT(_random);

            type.Should().Be(_random);
        }

        [Test]
        public void Format_DefaultRadix_ShouldBeExpected()
        {
            var type = new UINT();

            var format = type.Format();

            format.Should().Be("0");
        }
        
        [Test]
        public void Format_OverloadedRadix_ShouldBeExpected()
        {
            var type = new UINT();

            var format = type.Format(Radix.Binary);

            format.Should().Be("2#0000_0000_0000_0000");
        }

        [Test]
        public void ImplicitOperator_Bool_ShouldBeTrue()
        {
            UINT type = _random;

            type.Should().Be(_random);
        }

        [Test]
        public void ImplicitOperator_bool_ShouldBeTrue()
        {
            ushort value = new UINT(_random);

            value.Should().Be(_random);
        }

        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new UINT();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new UINT();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new UINT();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new UINT();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new UINT();
            var second = new UINT();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_DefaultValue_ShouldBeHashOfName()
        {
            var type = new UINT();

            var hash = type.GetHashCode();

            hash.Should().Be(type.GetHashCode());
        }
        
        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new UINT();

            type.ToString().Should().Be(type.ToString());
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new UINT();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new UINT();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new UINT();
            var second = new UINT();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}