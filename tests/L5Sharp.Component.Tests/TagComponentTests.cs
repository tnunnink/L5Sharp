using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Component.Tests
{
    public class TagComponentTests
    {
        [Test]
        public void Create_NameAndGenericType_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_NameAndTyped_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_NameAndTypeProvided_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new Bool(true));

            tag.Should().NotBeNull();
            tag.Value.Should().Be(true);
        }

        [Test]
        public void Create_WithDimensions_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
        }
    }
}