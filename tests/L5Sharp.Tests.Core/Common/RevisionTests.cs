using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class RevisionTests
    {
        private Fixture? _fixture;

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
            revision.Build.Should().Be(0);
            revision.ToString().Should().Be("1.0");
        }

        [Test]
        public void New_MajorMinor_ShouldHaveExpectedValues()
        {
            var revision = new Revision(10, 5);

            revision.Major.Should().Be(10);
            revision.Minor.Should().Be(5);
            revision.Build.Should().Be(0);
            revision.ToString().Should().Be("10.5");
        }
        
        [Test]
        public void New_MajorMinorBuild_ShouldHaveExpectedValues()
        {
            var revision = new Revision(10, 5, 13);

            revision.Major.Should().Be(10);
            revision.Minor.Should().Be(5);
            revision.Build.Should().Be(13);
            revision.ToString().Should().Be("10.5.13");
        }
        
        [Test]
        public void New_DoubleValue_ShouldHaveExpectedValues()
        {
            var revision = new Revision(1.23);

            revision.Major.Should().Be(1);
            revision.Minor.Should().Be(23);
            revision.Build.Should().Be(0);
            revision.ToString().Should().Be("1.23");
        }

        [Test]
        public void Parse_ValidMajorMinor_ShouldBeExpected()
        {
            var revision = Revision.Parse("1.23");
            
            revision.Major.Should().Be(1);
            revision.Minor.Should().Be(23);
            revision.Build.Should().Be(0);
            revision.ToString().Should().Be("1.23");
        }
        
        [Test]
        public void Parse_ValidMajorMinorBuild_ShouldBeExpected()
        {
            var revision = Revision.Parse("1.23.45");
            
            revision.Major.Should().Be(1);
            revision.Minor.Should().Be(23);
            revision.Build.Should().Be(45);
            revision.ToString().Should().Be("1.23.45");
        }
        
        /// <summary>
        /// Issue #42: This is according to Rockwell website.
        /// Since leading zeros don't imact the value or the revision we treat each part as a numeric value
        /// and not a string. 
        /// </summary>
        [Test]
        public void Parse_ValidWithLeadingZeros_ShouldBeExpected()
        {
            var revision = Revision.Parse("7.010.001");
            
            revision.Major.Should().Be(7);
            revision.Minor.Should().Be(10);
            revision.Build.Should().Be(1);
            revision.ToString().Should().Be("7.10.1");
        }

        [Test]
        public void Parse_NoMajorVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Revision.Parse(".12")).Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_NoMinorVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Revision.Parse("1")).Should().Throw<FormatException>();
        }
        
        [Test]
        public void Parse_InvalidMajorVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Revision.Parse("1234567.12")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Parse_InvalidMinorVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Revision.Parse("12.1234567")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Parse_InvalidBuildVersion_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => Revision.Parse("12.1.234567")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_NullString_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Revision.Parse(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_EmptyString_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => Revision.Parse(string.Empty)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void TryParse_ValidMajorMinor_ShouldBeExpected()
        {
            var revision = Revision.TryParse("1.23");
            
            revision.Should().NotBeNull();
            revision?.Major.Should().Be(1);
            revision?.Minor.Should().Be(23);
            revision?.Build.Should().Be(0);
            revision?.ToString().Should().Be("1.23");
        }
        
        [Test]
        public void TryParse_ValidMajorMinorBuild_ShouldBeExpected()
        {
            var revision = Revision.TryParse("1.23.45");
            
            revision.Should().NotBeNull();
            revision?.Major.Should().Be(1);
            revision?.Minor.Should().Be(23);
            revision?.Build.Should().Be(45);
            revision?.ToString().Should().Be("1.23.45");
        }
        
        [Test]
        public void TryParse_NoMajorVersion_ShouldThrowFormatException()
        {
            var revision = Revision.TryParse(".12");
            
            revision.Should().BeNull();
        }

        [Test]
        public void TryParse_NoMinorVersion_ShouldThrowFormatException()
        {
            var revision = Revision.TryParse("1");
            
            revision.Should().BeNull();
        }
        
        [Test]
        public void TryParse_InvalidMajorVersion_ShouldThrowFormatException()
        {
            var revision = Revision.TryParse("1234567.12");
            
            revision.Should().BeNull();
        }
        
        [Test]
        public void TryParse_InvalidMinorVersion_ShouldThrowFormatException()
        {
            var revision = Revision.TryParse("12.1234567");
            
            revision.Should().BeNull();
        }
        
        [Test]
        public void TryParse_InvalidBuildVersion_ShouldThrowFormatException()
        {
            var revision = Revision.TryParse("12.1.234567");
            
            revision.Should().BeNull();
        }

        [Test]
        public void TryParse_NullString_ShouldThrowArgumentException()
        {
            var revision = Revision.TryParse(null!);
            
            revision.Should().BeNull();
        }

        [Test]
        public void TryParse_EmptyString_ShouldThrowArgumentException()
        {
            var revision = Revision.TryParse(string.Empty);
            
            revision.Should().BeNull();
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
        public void OperatorGreaterThan_FirstGreaterThanSecondWithBuild_ShouldBeTrue()
        {
            var first = new Revision(1, 1, 2);
            var second = new Revision(1, 1, 1);

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
        public void OperatorLessThan_FirstGreaterThanSecondWithBuild_ShouldBeFalse()
        {
            var first = new Revision(1, 1, 2);
            var second = new Revision(1, 1, 1);

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