using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var controller = new Controller("Test_Controller");

            controller.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Controller("This is Invalid !@#$")).Should()
                .Throw<ComponentNameInvalidException>();
        }
    }
}