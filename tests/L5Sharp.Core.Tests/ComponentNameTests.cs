using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ComponentNameTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var name = new ComponentName("Test");

            name.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidName_ShouldBeExpected()
        {
            var name = new ComponentName("Test");

            name.ToString().Should().Be("Test");
        }

        [Test]
        public void New_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new ComponentName(fixture.Create<string>())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_EmptyString_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ComponentName(string.Empty)).Should()
                .Throw<ArgumentException>();   
        }
        
        [Test]
        public void New_Null_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new ComponentName(null)).Should()
                .Throw<ArgumentException>();   
        }

        [Test]
        public void Set_Empty_ShouldThrowArgumentException()
        {
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = string.Empty).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Set_ValidName_ShouldBeExpectedValue()
        {
            var name = new ComponentName("Test");

            name = "New";

            name.ToString().Should().Be("New");
        }
        
        [Test]
        public void Set_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = fixture.Create<string>()).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Set_StartsWithNumber_ShouldThrowComponentNameInvalidException()
        {
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = "1Test").Should().Throw<ComponentNameInvalidException>();
        }
        
        [Test]
        public void Set_StartsWithUnderscore_ShouldThrowComponentNameInvalidException()
        {
            var name = new ComponentName("Test");

            FluentActions.Invoking(() => name = "_Test").Should().Throw<ComponentNameInvalidException>();
        }
    }
}