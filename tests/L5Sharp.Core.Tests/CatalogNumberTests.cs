using System;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class CatalogNumberTests
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
            FluentActions.Invoking(() => new CatalogNumber(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_Empty_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new CatalogNumber(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var number = new CatalogNumber(_fixture.Create<string>());

            number.Should().NotBeNull();
        }

        [Test]
        public void New_ControlLogixBulletin_ShouldHaveExpectedBulletin()
        {
            var number = new CatalogNumber("`1756-L83E");

            number.Bulletin.Should().BeEquivalentTo(Bulletin.ControlLogix);
        }
        
        [Test]
        public void ToString_WhenCalled_shouldBeExpected()
        {
            var catalogNumber = _fixture.Create<string>();
            var number = new CatalogNumber(catalogNumber);

            var value = number.ToString();

            value.Should().Be(catalogNumber);
        }

        [Test]
        public void ImplicitOperator_StringToCatalogNumber_ShouldBeOfTypeCatalogNumber()
        {
            var value = _fixture.Create<string>();
            CatalogNumber number = value;

            number.Should().BeOfType<CatalogNumber>();
            number.ToString().Should().Be(value);
        }
        
        [Test]
        public void ImplicitOperator_CatalogToString_ShouldBeOfTypeBulletin()
        {
            var value = new CatalogNumber(_fixture.Create<string>());
            string number = value;

            number.Should().BeOfType<string>();
            number.Should().Be(value);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<string>();
            var first = new CatalogNumber(value);
            var second = new CatalogNumber(value);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new CatalogNumber(_fixture.Create<string>());

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new CatalogNumber(_fixture.Create<string>());

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<string>();
            var first = new CatalogNumber(value);
            var second = new CatalogNumber(value);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new CatalogNumber(_fixture.Create<string>());

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new CatalogNumber(_fixture.Create<string>());

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var value = _fixture.Create<string>();
            var first = new CatalogNumber(value);
            var second = new CatalogNumber(value);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var value = _fixture.Create<string>();
            var first = new CatalogNumber(value);
            var second = new CatalogNumber(value);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new CatalogNumber(_fixture.Create<string>());

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}