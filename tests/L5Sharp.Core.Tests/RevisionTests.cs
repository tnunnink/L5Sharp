using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class RevisionTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 127));
        }

        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var revision = new Revision();

            revision.Should().NotBeNull();
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedDefaults()
        {
            var revision = new Revision();

            revision.Major.Should().Be(1);
            revision.Minor.Should().Be(0);
        }

        [Test]
        public void EqualsTyped_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void EqualsTyped_Same_ShouldBeTrue()
        {
            var first = new Revision();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void EqualsTyped_Null_ShouldBeFalse()
        {
            var first = new Revision();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void EqualsObject_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void EqualsObject_Same_ShouldBeTrue()
        {
            var first = new Revision();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void EqualsObject_Null_ShouldBeFalse()
        {
            var first = new Revision();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void EqualsOperator_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEqualsOperator_Equal_ShouldBeFalse()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var revision = new Revision();

            var hash = revision.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}