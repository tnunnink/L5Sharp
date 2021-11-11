using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagSimpleTests
    {
        [Test]
        public void New_BoolValidName_ShouldBeExpected()
        {
            var tag = Tag.New("Test", new Bool());

            tag.Should().NotBeNull();
            tag.DataType.Should().Be("BOOL");
            tag.Scope.Should().Be(Scope.Null);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.TagType.Should().Be(TagType.Base);
            tag.GetData().As<Bool>().Value.Should().BeFalse();
        }

        [Test]
        public void New_SintValidName_ShouldBeExpectedDefaults()
        {
            var tag = Tag.OfType<Sint>("Test");
            
            tag.Should().NotBeNull();
            tag.DataType.Should().Be("SINT");
            tag.Scope.Should().Be(Scope.Null);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.TagType.Should().Be(TagType.Base);
            tag.GetData().Value.Should().Be(0);
        }

        [Test]
        public void GetData_TagWithValue_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.OfType("Test", new Dint(expected));

            var value = tag.GetData();

            value.Should().Be(expected);
        }
        
        [Test]
        public void SetData_ValidValue_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.OfType<Dint>("Test");

            tag.SetData(new Dint(expected));

            tag.GetData().Should().Be(expected);
        }
    }
}