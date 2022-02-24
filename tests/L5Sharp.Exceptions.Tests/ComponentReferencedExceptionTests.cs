using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class ComponentReferencedExceptionTests
    {
        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var exception = new ComponentReferencedException("MyType", typeof(IDataType));

            exception.Should().NotBeNull();
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedMessage()
        {
            var exception = new ComponentReferencedException("MyType", typeof(IDataType));

            exception.Message.Should()
                .Be(
                    "The IDataType component with name 'MyType' is currently referenced in the context. Can not remove referenced components.");
        }
    }
}