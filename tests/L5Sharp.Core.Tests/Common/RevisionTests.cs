using AutoFixture;
using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Core.Tests.Common
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
        public void New_Default_ShouldNotBeNull()
        {
            var revision = new Revision();

            revision.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var revision = new Revision();

            revision.Major.Should().Be(1);
            revision.Minor.Should().Be(0);
        }

        [Test]
        public void New_MajorMinor_ShouldHaveExpectedValues()
        {
            var revision = new Revision(1, 23);

            revision.Should().Be(1.23);
        }
        
        [Test]
        public void New_DoubleValue_ShouldHaveExpectedValues()
        {
            var revision = new Revision(1.23);

            revision.Should().Be(1.23);
        }

        [Test]
        public void Equals_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_Same_ShouldBeTrue()
        {
            var first = new Revision();

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = new Revision();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var revision = new Revision();

            var hash = revision.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void OperatorEquals_Equal_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_Equal_ShouldBeFalse()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first != second;

            result.Should().BeFalse();
        }
        
        [Test]
        public void OperatorGreaterThan_FirstGreaterThanSecond_ShouldBeTrue()
        {
            var first = new Revision(1.2);
            var second = new Revision(1.1);

            var result = first > second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorLessThan_FirstGreaterThanSecond_ShouldBeFalse()
        {
            var first = new Revision(1.2);
            var second = new Revision(1.1);

            var result = first < second;

            result.Should().BeFalse();
        }
        
        [Test]
        public void OperatorGreaterThanOrEqual_FirstGreaterThanSecond_ShouldBeTrue()
        {
            var first = new Revision(1.2);
            var second = new Revision(1.1);

            var result = first >= second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorLessThanOrEqual_FirstGreaterThanSecond_ShouldBeFalse()
        {
            var first = new Revision(1.2);
            var second = new Revision(1.1);

            var result = first <= second;

            result.Should().BeFalse();
        }
    }
}