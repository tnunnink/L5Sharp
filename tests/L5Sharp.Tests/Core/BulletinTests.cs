using System;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class BulletinTests
    {
        [Test]
        public void New_InvalidLength_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new Bulletin(12345)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ValidLength_ShouldNotBeNull()
        {
            var bulletin = new Bulletin(1234);

            bulletin.Should().NotBeNull();
        }

        [Test]
        public void New_EmptyString_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Bulletin(string.Empty)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_InvalidLength_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Bulletin("12310")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_NonByteString_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Bulletin("A43X")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_NullString_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Bulletin(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_ValidString_ShouldNotBeNull()
        {
            var bulletin = new Bulletin("1756");

            bulletin.Should().NotBeNull();
        }

        [Test]
        public void ControlLogix_WhenCalled_shouldBeExpected()
        {
            var bulletin = Bulletin.ControlLogix;

            bulletin.Should().Be(new Bulletin(1756));
        }
        
        [Test]
        public void CompactLogix_WhenCalled_shouldBeExpected()
        {
            var bulletin = Bulletin.CompactLogix;

            bulletin.Should().Be(new Bulletin(1769));
        }
        
        [Test]
        public void SoftLogix_WhenCalled_shouldBeExpected()
        {
            var bulletin = Bulletin.SoftLogix;

            bulletin.Should().Be(new Bulletin(1789));
        }
        
        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new Bulletin(1234);
            var second = new Bulletin(1234);

            var result = first.CompareTo(second);

            result.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Different_ShouldNotBeZero()
        {
            var first = new Bulletin(1234);
            var second = new Bulletin(4321);

            var result = first.CompareTo(second);

            result.Should().NotBe(0);
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var first = new Bulletin(1234);

            var result = first.CompareTo(first);

            result.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var first = new Bulletin(1234);

            var result = first.CompareTo(null!);

            result.Should().Be(1);
        }

        [Test]
        public void ToString_WhenCalled_shouldBeExpected()
        {
            var bulletin = new Bulletin(1234);

            var value = bulletin.ToString();

            value.Should().Be("1234");
        }

        [Test]
        public void ImplicitOperator_Short_ShouldBeOfTypeString()
        {
            var bulletin = new Bulletin(1234);

            short value = bulletin;
            
            value.Should().Be(1234);
        }
        
        
        [Test]
        public void ImplicitOperator_Bulletin_ShouldBeOfTypeBulletin()
        {
            Bulletin bulletin = 1234;

            bulletin.Should().BeOfType<Bulletin>();
            bulletin.Should().Be(1234);
        }
        
        [Test]
        public void ImplicitOperator_string_ShouldBeOfTypeBulletin()
        {
            Bulletin bulletin = "1234";

            bulletin.Should().BeOfType<Bulletin>();
            bulletin.Should().Be(1234);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bulletin(1756);
            var second = new Bulletin(1756);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Bulletin(1756);

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new Bulletin(1756);

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bulletin(1756);
            var second = new Bulletin(1756);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Bulletin(1756);

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Bulletin(1756);

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bulletin(1756);
            var second = new Bulletin(1756);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Bulletin(1756);
            var second = new Bulletin(1756);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new Bulletin(1756);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}