using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void Create_ValidName_ShouldNotBeNull()
        {
            var parameter = Parameter.Create("Test", new Bool());

            parameter.Should().NotBeNull();
        }

        [Test]
        public void Create_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => Parameter.Create(fixture.Create<string>(), new Bool())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Parameter.Create(null, new Bool())).Should()
                .Throw<ArgumentException>();
        }
        
        [Test]
        public void Create_EmptyName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Parameter.Create(string.Empty, new Bool())).Should()
                .Throw<ArgumentException>();
        }
        
        [Test]
        public void Create_GenericType_ShouldNotBeNull()
        {
            var parameter = Parameter.Create<Bool>("Test");

            parameter.Should().NotBeNull();
        }

        [Test]
        public void Create_UserType_ShouldNotBeNull()
        {
            var dataType = new UserDefined("Test");
            var parameter = Parameter.Create("Test", dataType);

            parameter.Should().NotBeNull();
        }

        [Test]
        public void Create_Defaults_ShouldBeExpected()
        {
            var parameter = Parameter.Create<Dint>("Test");

            parameter.Name.Should().Be("Test");
            parameter.DataType.Should().Be(new Dint());
            parameter.Dimension.Should().Be(Dimensions.Empty);
            parameter.Radix.Should().Be(Radix.Decimal);
            parameter.Required.Should().BeFalse();
            parameter.Visible.Should().BeFalse();
            parameter.Usage.Should().Be(TagUsage.Input);
            parameter.TagType.Should().Be(TagType.Base);
            parameter.Alias.Should().BeNull();
            parameter.Description.Should().BeNull();
            parameter.Constant.Should().BeFalse();
            parameter.Default.Should().Be(new Dint());
        }
    }
}