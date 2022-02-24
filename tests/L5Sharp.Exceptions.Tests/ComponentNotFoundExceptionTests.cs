using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class ComponentNotFoundExceptionTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var exception = new ComponentNotFoundException("MyComponent", typeof(IModule));

            exception.Should().NotBeNull();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectedMessage()
        {
            var exception = new ComponentNotFoundException("MyComponent", typeof(IModule));

            exception.Message.Should()
                .Be("The IModule component with name 'MyComponent' was not found in the current context.");
        }
    }
}