using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class CircularReferenceExceptionTests
    {
        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var exception = new CircularReferenceException("MyDataType");

            exception.Should().NotBeNull();
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedMessage()
        {
            var exception = new CircularReferenceException("MyDataType");

            exception.Message.Should()
                .Be(
                    "Member can not be same type as parent type 'MyDataType'");
        }   
    }
}