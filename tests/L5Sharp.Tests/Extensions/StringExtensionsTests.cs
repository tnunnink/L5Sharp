using AutoFixture;
using FluentAssertions;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void IsEmpty_Empty_ShouldBeTrue()
        {
            string.Empty.IsEmpty().Should().BeTrue();
        }
        
        [Test]
        public void IsEmpty_NotEmpty_ShouldBeFalse()
        {
            var fixture = new Fixture();
            fixture.Create<string>().IsEmpty().Should().BeFalse();
        }
        
        [Test]
        public void HasDecimalFormat_Integer_ShouldBeTrue()
        {
            var fixture = new Fixture();

            var number = fixture.Create<int>().ToString();

            number.HasDecimalFormat().Should().BeTrue();
        }
        
        [Test]
        public void HasDecimalFormat_Empty_ShouldBeFalse()
        {
            var number = string.Empty;

            number.HasDecimalFormat().Should().BeFalse();
        }
        
        [Test]
        public void HasDecimalFormat_Negative_ShouldBeTrue()
        {
            const string number = "-123";

            number.HasDecimalFormat().Should().BeTrue();
        }

        [Test]
        public void HasDecimalFormat_Decimal_ShouldBeFalse()
        {
            const string number = "12.3";

            number.HasDecimalFormat().Should().BeFalse();
        }
        
        [Test]
        public void HasDecimalFormat_Letter_ShouldBeFalse()
        {
            const string number = "3A";

            number.HasDecimalFormat().Should().BeFalse();
        }

        [Test]
        public void HasFloatFormat_Float_ShouldBeTrue()
        {
            const string number = "1.4325";

            number.HasFloatFormat().Should().BeTrue();
        }
        
        [Test]
        public void HasFloatFormat_ZeroPointZero_ShouldBeTrue()
        {
            const string number = "0.0";

            number.HasFloatFormat().Should().BeTrue();
        }
        
        [Test]
        public void HasFloatFormat_Zero_ShouldBeFalse()
        {
            const string number = "0";

            number.HasFloatFormat().Should().BeFalse();
        }
        
        [Test]
        public void HasFloatFormat_Exponential_ShouldBeFalse()
        {
            const string number = "1.234e+002";

            number.HasFloatFormat().Should().BeFalse();
        }

        [Test]
        public void IsBalanced_EmptyString_ShouldBeTrue()
        {
            var input = string.Empty;

            input.IsBalanced('(', ')').Should().BeTrue();
        }
        
        [Test]
        public void IsBalanced_UnBalanced_ShouldBeFalse()
        {
            const string input = "(This is a test";

            input.IsBalanced('(', ')').Should().BeFalse();
        }
        
        [Test]
        public void IsBalanced_Balanced_ShouldBeTrue()
        {
            const string input = "(This is a test)";

            input.IsBalanced('(', ')').Should().BeTrue();
        }
        
        [Test]
        public void IsBalanced_NestedBalanced_ShouldBeTrue()
        {
            const string input = "(This is () (a test))";

            input.IsBalanced('(', ')').Should().BeTrue();
        }
        
        [Test]
        public void IsBalanced_NestedUnBalanced_ShouldBeFalse()
        {
            const string input = "(This is a() (test)";

            input.IsBalanced('(', ')').Should().BeFalse();
        }
        
        [Test]
        public void IsBalanced_ClosingBeforeOpening_ShouldBeFalse()
        {
            const string input = "This is a ) (test";

            input.IsBalanced('(', ')').Should().BeFalse();
        }
    }
}