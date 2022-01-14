using System;
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
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Controller(null!, ProcessorType.L71, new Revision()))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();
            
            FluentActions.Invoking(() => new Controller(fixture.Create<string>(), ProcessorType.L71, new Revision()))
                .Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var controller = new Controller("Test", ProcessorType.L74, new Revision(32, 01));

            controller.Should().NotBeNull();
        }
    }
}