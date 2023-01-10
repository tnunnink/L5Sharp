using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Creators;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagComplexArrayTests
    {
        [Test]
        public void New_TwoDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var length = first * second;

            var tag = Tag.Create<DINT>("Test", new Dimensions(first, second));

            tag.Dimensions.Length.Should().Be(length);
        }

        [Test]
        public void Index_TwoDimensionalArray_ValidIndex_ShouldNotBeNull()
        {
            var fixture = new Fixture();
            var tag = Tag.Create<DINT>("Test", new Dimensions(fixture.Create<ushort>(), fixture.Create<ushort>()));

            var element = tag[0, 0];

            element.Should().NotBeNull();
        }

        [Test]
        public void Index_TwoDimensionalArray_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var fixture = new Fixture();
            var tag = Tag.Create<DINT>("Test", new Dimensions(fixture.Create<ushort>(), fixture.Create<ushort>()));

            FluentActions.Invoking(() => tag[0, -1]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void New_ThreeDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var third = fixture.Create<ushort>();
            var length = first * second * third;

            var tag = Tag.Create<DINT>("Test", new Dimensions(first, second, third));

            tag.Dimensions.Length.Should().Be(length);
        }

        [Test]
        public void Index_ThreeDimensionalArray_ValidIndex_ShouldNotBeNull()
        {
            var tag = Tag.Create<DINT>("Test", new Dimensions(5, 5, 5));

            var element = tag[0, 0, 0];

            element.Should().NotBeNull();
        }

        [Test]
        public void Index_ThreeDimensionalArray_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var tag = Tag.Create<DINT>("Test", new Dimensions(5, 5, 5));

            FluentActions.Invoking(() => tag[0, -1, 0]).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}