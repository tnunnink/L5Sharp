using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class NeutralTextTests
    {
        [Test]
        public void New_DefaultConstructor_ShouldNotBeNull()
        {
            var text = new NeutralText();

            text.Should().NotBeNull();
        }

        [Test]
        public void New_NullText_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new NeutralText(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_EmptyString_ShouldBeEqualToEmpty()
        {
            var text = new NeutralText(string.Empty);

            text.Should().BeEquivalentTo(NeutralText.Empty);
        }

        [Test]
        public void New_UnbalancedBrackets_ShouldThrowFormatException()
        {
            FluentActions.Invoking(() => new NeutralText("[XIC(SomeBit),XIO(AnotherBit)OTE(Output);"))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void New_ValidText_ShouldNotBeNull()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            text.Should().NotBeNull();
        }

        [Test]
        public void New_ValidText_ShouldHaveExpectedInstructions()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            text.Instructions.Should().HaveCount(3);
        }
    }
}