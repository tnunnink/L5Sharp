using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
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
            var controller = Controller.Create("Test", ProcessorType.L74, new Revision(32, 01));

            controller.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => Controller.Create(fixture.Create<string>(), ProcessorType.L71, new Revision()))
                .Should().Throw<ComponentNameInvalidException>();
        }
    }
}