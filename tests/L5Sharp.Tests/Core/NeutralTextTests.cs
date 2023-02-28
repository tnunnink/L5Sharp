using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class NeutralTextTests
    {
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
        public void New_SomeString_ShouldNotBeNull()
        {
            var fixture = new Fixture();
            
            var text = new NeutralText(fixture.Create<string>());

            text.Should().NotBeNull();
        }

        [Test]
        public void Empty_WhenCalled_ShouldBeEmptyString()
        {
            var text = NeutralText.Empty;

            text.Should().BeEquivalentTo(new NeutralText(string.Empty));
        }

        [Test]
        public void IsBalanced_UnbalancedBrackets_ShouldFalse()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)OTE(Output);");

            text.IsBalanced.Should().BeFalse();
        }

        [Test]
        public void New_ValidText_ShouldNotBeNull()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            text.Should().NotBeNull();
        }

        [Test]
        public void Instructions_SimpleTextWithMultipleInstruction_ShouldHaveExpectedCount()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            var instructions = text.Instructions();

            instructions.Should().HaveCount(3);
        }

        [Test]
        public void TagNames_SimpleTextWithMultipleTags_ShouldHaveExpectedCount()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            var tagNames = text.TagNames();

            tagNames.Should().HaveCount(3);
        }
    }
}