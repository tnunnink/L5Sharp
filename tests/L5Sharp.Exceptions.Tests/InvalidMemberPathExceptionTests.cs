using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class InvalidMemberPathExceptionTests
    {
        [Test]
        public void New_ValidParameters_ShouldNotBeNull()
        {
            var exception = new InvalidMemberPathException("TagName.Member", "BOOL");

            exception.Should().NotBeNull();
        }

        [Test]
        public void New_ValidParameters_ShouldHaveExpectedProperties()
        {
            var exception = new InvalidMemberPathException("TagName.Member", "BOOL");

            exception.TagName.Should().Be("TagName.Member");
            exception.DataType.Should().Be("BOOL");
        }

        [Test]
        public void New_ValidParameters_ShouldHaveExpectedMessage()
        {
            var exception = new InvalidMemberPathException("TagName.Member", "BOOL");

            exception.Message.Should().Be($"The tag name 'TagName.Member' is not a valid member path for type 'BOOL'.");
        }
    }
}