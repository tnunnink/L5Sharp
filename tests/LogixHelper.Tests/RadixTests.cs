using FluentAssertions;
using LogixHelper.Enumerations;
using NUnit.Framework;

namespace LogixHelper.Tests
{
    [TestFixture]
    public class RadixTests
    {
        [Test]
        public void Null_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Null;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Null);
        }
        
        [Test]
        public void General_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.General;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.General);
        }
        
        [Test]
        public void Binary_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Binary;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Binary);
        }
        
        [Test]
        public void Octal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Octal;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Octal);
        }
        
        [Test]
        public void Decimal_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Decimal;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Hex_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Hex;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Hex);
        }
        
        [Test]
        public void Exponential_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Exponential;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Exponential);
        }
        
        [Test]
        public void Float_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Float;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Float);
        }
        
        [Test]
        public void Ascii_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Ascii;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Ascii);
        }
        
        [Test]
        public void Unicode_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Unicode;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Unicode);
        }
        
        [Test]
        public void DateTime_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTime;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTime);
        }
        
        [Test]
        public void DateTimeNs_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTimeNs;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTimeNs);
        }
        
        [Test]
        public void UseTypeStyle_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.UseTypeStyle;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.UseTypeStyle);
        }
    }
}