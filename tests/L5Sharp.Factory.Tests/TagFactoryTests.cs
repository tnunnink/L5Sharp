using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Factories;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Factory.Tests
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
        public void Create_Name_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_NameAndType_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new Bool());

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_NameAndTypeAndDimensions_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", new Bool(), new Dimensions(5));

            tag.Should().NotBeNull();
        }
    }
}