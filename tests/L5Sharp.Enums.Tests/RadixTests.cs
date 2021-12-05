using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixTests
    {
        [Test]
        public void Default_UserDefined_ShouldBeNullRadix()
        {
            var userDefined = UserDefined.Create("Test", "This is a test");

            var radix = Radix.Default(userDefined);

            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Default_Real_ShouldBeDecimalRadix()
        {
            var type = new Real();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void Default_Bool_ShouldBeDecimalRadix()
        {
            var type = new Bool();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Sint_ShouldBeDecimalRadix()
        {
            var type = new Sint();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Int_ShouldBeDecimalRadix()
        {
            var type = new Int();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }


        [Test]
        public void Default_Dint_ShouldBeDecimalRadix()
        {
            var type = new Dint();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Default_Lint_ShouldBeDecimalRadix()
        {
            var type = new Lint();

            var radix = Radix.Default(type);

            radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void Infer_BinaryValue_ShouldBeBinaryRadix()
        {
            var input = "";

            var radix = Radix.Infer(input);

            radix.Should().Be(Radix.Binary);
        }

        [Test]
        public void ParseValue_ValidBinary_ShouldBeExpectedValue()
        {
            const string value = "2#0000_0101";

            var parsed = Radix.ParseValue(value);

            parsed.Should().Be(5);
        }

        [Test]
        public void ParseValue_ValidOctal_ShouldBeExpectedValue()
        {
            const string value = "8#014";

            var parsed = (int)Radix.ParseValue(value);

            parsed.Should().Be(20);
        }

        [Test]
        public void ByteToInt()
        {
            byte b = 20;

            int i = b;

            i.Should().Be(20);
        }
    }
}