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
            var tag = Tag.Create<Bool>("Test", 5);

            tag.Should().NotBeNull();
            tag.Dimensions.Length.Should().Be(5);
        }

        [Test]
        public void Create_ArrayOfComplex_ShouldHaveExpectedElements()
        {
            var tag = Tag.Create<Timer>("Test", 5);

            tag.Should().NotBeNull();
            tag.Dimensions.Length.Should().Be(5);
        }

        [Test]
        public void GetIndexer_ValidIndex_ShouldBeExpected()
        {
            var tag = Tag.Create<Bool>("Test", 5);

            var element = tag[3];

            element.Should().NotBeNull();
            element?.Name.Should().Be("[3]");
            element?.DataType.Should().Be(nameof(Bool).ToUpper());
        }

        [Test]
        public void GetIndexer_InvalidIndex_ShouldBeNull()
        {
            var tag = Tag.Create<Bool>("Test", 5);

            var element = tag[6];

            element.Should().BeNull();
        }
    }
}