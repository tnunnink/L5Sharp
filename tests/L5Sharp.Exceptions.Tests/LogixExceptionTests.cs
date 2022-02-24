using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Exceptions.Tests
{
    [TestFixture]
    public class LogixExceptionTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var exception = new LogixException();

            exception.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveExpectedMessage()
        {
            var exception = new LogixException();

            exception.Message.Should().Be("...");
        }
        
        [Test]
        public void New_Message_ShouldHaveExpectedMessage()
        {
            var exception = new LogixException("This is the reason it failed.");

            exception.Message.Should().Be("This is the reason it failed.");
        }
        
        [Test]
        public void New_InnerException_ShouldHaveExpectedInnerException()
        {
            var exception = new LogixException("This is the reason it failed.", new ArgumentException());
            
            exception.InnerException.Should().BeOfType<ArgumentException>();
        }
    }
}