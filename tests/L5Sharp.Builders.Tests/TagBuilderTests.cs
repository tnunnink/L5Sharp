using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Builders.Tests
{
    [TestFixture]
    public class TagBuilderTests
    {
        [Test]
        public void WithDimensions_Valid_ShouldCreateTagWithDimensions()
        {
            var builder = new TagBuilder<IDataType>("Test", new Bool());

            builder.WithDimensions(5);

            var tag = builder.Create();
            tag.Dimensions.Length.Should().Be(5);
        }
        
        [Test]
        public void OfType_WhenCalled_ShouldBeExpected()
        {
            var builder = new TagBuilder<IDataType>("Test", new Bool());

            builder.WithRadix(Radix.Binary);

            var tag = builder.Create();
            tag.Radix.Should().Be(Radix.Binary);
        }
        
        [Test]
        public void WithDimension_WhenCalled_ShouldBeExpected()
        {
            var builder = new TagBuilder<IDataType>("Test", new Bool());

            builder.WithAccess(ExternalAccess.ReadOnly);

            var tag = builder.Create();
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

    }
}