using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixAsciiTests
    {
        [Test]
        public void Ascii_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Ascii;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Ascii);
        }
        
        [Test]
        public void Format_AsciiValidSint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Sint(20));

            result.Should().Be("$14");
        }

        [Test]
        public void Format_Ascii_ShouldBeExpected()
        {
            var ascii = Radix.Ascii.Format(new Dint(123456));

            ascii.Should().Be("$00$00$00p");
        }

        [Test]
        public void Format_AsciiValidInt_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Int(20));

            result.Should().Be("$00$14");
        }
        
        [Test]
        public void Format_AsciiValidDint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Dint(20));

            result.Should().Be("$00$00$00$14");
        }

        [Test]
        public void Format_AsciiValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.Ascii;

            var result = radix.Format(new Lint(20));

            result.Should().Be("$00$00$00$00$00$00$00$14");
        }

        [Test]
        public void Testing()
        {
            var list = new List<char>();
            for (var i = 0; i < 300; i++)
            {
                var c = Convert.ToChar(i);
                list.Add(c);
            }

            list.Should().NotBeEmpty();
        }
    }
}