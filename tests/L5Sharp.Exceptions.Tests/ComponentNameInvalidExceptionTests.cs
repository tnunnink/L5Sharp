using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class ComponentNameInvalidExceptionTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var exception = new ComponentNameInvalidException("!#0923Name");

            exception.Should().NotBeNull();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectedMessage()
        {
            var exception = new ComponentNameInvalidException("!#0923Name");

            exception.Message.Should()
                .Be(
                    "Name '!#0923Name' is not valid. Name must contain only alphanumeric or '_', start with a letter or '_', and be less than 40 characters");
        }
    }
}