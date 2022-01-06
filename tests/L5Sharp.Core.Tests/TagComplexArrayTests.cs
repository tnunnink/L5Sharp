/*using AutoFixture;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
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

            var tag = Tag.Create<Dint>("Test", new Dimensions(first, second));

            tag.Dimensions.Length.Should().Be(length);
        }
    }
}*/