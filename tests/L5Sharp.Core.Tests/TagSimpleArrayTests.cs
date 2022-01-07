using System;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagSimpleArrayTests
    {
        [Test]
        public void Create_ArrayOfAtomic_ShouldHaveExpectedElements()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            tag.Should().NotBeNull();
            tag.Dimensions.Length.Should().Be(5);
        }

        [Test]
        public void Create_ArrayOfComplex_ShouldHaveExpectedElements()
        {
            var tag = Tag.Create<Timer>("Test", new Dimensions(5));

            tag.Should().NotBeNull();
            tag.Dimensions.Length.Should().Be(5);
        }
        
        [Test]
        public void Create_ArrayOfUserDefined_ShouldHaveExpectedElements()
        {
            var tag = Tag.Create<MyNestedType>("Test", new Dimensions(5));

            tag.Should().NotBeNull();
            tag.Dimensions.Length.Should().Be(5);
        }

        [Test]
        public void Create_TwoDimensionalArray_ShouldHaveExpectedCount()
        {
            var tag = Tag.Create<Int>("Test", new Dimensions(2, 3));

            tag.Dimensions.Length.Should().Be(6);
        }

        [Test]
        public void GetIndexer_OneDimensionalValidIndex_ShouldBeExpected()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            var element = tag[3];

            element.Should().NotBeNull();
            element.Name.Should().Be("[3]");
            element.DataType.Should().BeOfType<Bool>();
        }

        [Test]
        public void GetIndexer_OneDimensionalInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            FluentActions.Invoking(() => tag[5]).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void GetIndexer_TwoDimensionalValidIndex_ShouldBeExpected()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(2, 3));

            var element = tag[1, 2];

            element.Should().NotBeNull();
            element.Name.Should().Be("[1,2]");
            element.DataType.Should().BeOfType<Bool>();
            element.TagName.Should().Be("Test[1,2]");
        }

        [Test]
        public void GetIndexer_TwoDimensionalInvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(2, 3));

            FluentActions.Invoking(() => tag[2, 3]).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}