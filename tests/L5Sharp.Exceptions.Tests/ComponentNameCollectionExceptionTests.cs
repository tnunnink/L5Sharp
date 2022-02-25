using FluentAssertions;
using L5Sharp.Atomics;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class ComponentNameCollectionExceptionTests
    {
        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var exception = new ComponentNameCollisionException("Name", typeof(Bool));

            exception.Should().NotBeNull();
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedMessage()
        {
            var exception = new ComponentNameCollisionException("Name", typeof(IDataType));

            exception.Message.Should()
                .Be(
                    "The IDataType component with name 'Name' already exists in the current context. Component names must be unique");
        }
        
        [Test]
        public void New_Valid_ShouldHaveExpectedName()
        {
            var exception = new ComponentNameCollisionException("Name", typeof(IDataType));

            exception.Name.Should().Be("Name");
        }
    }
}