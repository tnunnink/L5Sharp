using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = new Tag("Test", DataType.Bool);

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => new Tag(fixture.Create<string>(), DataType.Bool)).Should()
                .Throw<InvalidNameException>();
        }
        
        [Test]
        public void New_NoDataType_ShouldHaveExpectedDefaults()
        {
            var tag = new Tag("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.DataType.Should().BeNull();
            tag.Dimension.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Null);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Value.Should().BeNull();
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.AliasFor.Should().BeEmpty();
            tag.Constant.Should().BeFalse();
        }

        [Test]
        public void New_Counter_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", DataType.Counter);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(7);
            tag.Members.Any(t => t.Name == "PRE").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ACC").Should().BeTrue();
            tag.Members.Any(t => t.Name == "CU").Should().BeTrue();
            tag.Members.Any(t => t.Name == "CD").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DN").Should().BeTrue();
            tag.Members.Any(t => t.Name == "OV").Should().BeTrue();
            tag.Members.Any(t => t.Name == "UN").Should().BeTrue();
        }

        [Test]
        public void New_TwoDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var length = first * second;
            
            var tag = new Tag("Test", DataType.Dint, new Dimensions(first, second));

            tag.Dimension.Length.Should().Be(length);
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldSetMembersRadix()
        {
            var tag = new Tag("Test", DataType.Dint, new Dimensions(3, 4));

            tag.Radix = Radix.Ascii;

            tag.Members.All(t => t.Radix == Radix.Ascii).Should().BeTrue();
        }

        [Test]
        public void New_GenericTag_ShouldNotBeNull()
        {

        }
    }
}