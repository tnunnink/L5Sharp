using AutoFixture;
using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Tests.Common
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

            revision.Major.Should().Be("1");
            revision.Minor.Should().Be("0");
        }

        [Test]
        public void New_ValidString_ShouldHaveExpectedValue()
        {
            var revision = new Revision("1.2");

            revision.Should().NotBeNull();
            revision.Should().Be("1.2");
        }

        [Test]
        public void New_NoMajorVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => new Revision(".12")).Should().Throw<FormatException>();
        }
        
        [Test]
        public void New_NoMinorVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => new Revision("1")).Should().Throw<FormatException>();
        }
        
        [Test]
        public void New_NullString_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new Revision(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_EmptyString_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new Revision(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_MajorMinor_ShouldHaveExpectedValues()
        {
            var revision = new Revision(1, 23);

            revision.Should().Be("1.23");
        }

        [Test]
        public void New_DoubleValue_ShouldHaveExpectedValues()
        {
            var revision = new Revision(1.23);

            revision.Should().Be("1.23");
        }

        [Test]
        public void Equals_EqualRevision_ShouldBeTrue()
        {
            var first = new Revision();
            var second = new Revision();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void Equals_EqualString_ShouldBeTrue()
        {
            var first = new Revision();

            // ReSharper disable once SuspiciousTypeConversion.Global
            var result = first.Equals("1.0");

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
            var expected = "1.0".GetHashCode();
            var revision = new Revision();

            var hash = revision.GetHashCode();

            hash.Should().Be(expected);
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

        [Test]
        public void ToString_WhenCalled_ShouldBeExpectedValue()
        {
            var revision = new Revision();

            var result = revision.ToString();

            result.Should().Be("1.0");
        }

        [Test]
        public void Operator_StringToRevision_ShouldBeExpectedValue()
        {
            Revision revision = "1.23";

            revision.Should().Be("1.23");
        }

        [Test]
        public void Operator_RevisionToString_ShouldHaveExpectedValue()
        {
            var revision = new Revision();

            string result = revision;

            result.Should().Be("1.0");
        }

        [Test]
        public void Operator_Double_ShouldBeExpectedValue()
        {
            Revision revision = 1.23;

            revision.Should().Be("1.23");
        }
    }
}