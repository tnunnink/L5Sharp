using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

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
            var dataType = new DataType("Test");
            var parameter = Parameter.Create("Test", dataType);

            parameter.Should().NotBeNull();
        }

        [Test]
        public void Create_Defaults_ShouldBeExpected()
        {
            var parameter = Parameter.Create<Dint>("Test");

            parameter.Name.Should().Be("Test");
            parameter.DataType.Should().Be(new Dint());
            parameter.Dimensions.Should().Be(Dimensions.Empty);
            parameter.Radix.Should().Be(Radix.Decimal);
            parameter.Required.Should().BeFalse();
            parameter.Visible.Should().BeFalse();
            parameter.Usage.Should().Be(TagUsage.Input);
            parameter.TagType.Should().Be(TagType.Base);
            parameter.Alias.Should().BeNull();
            parameter.Description.Should().BeNull();
            parameter.Constant.Should().BeFalse();
            parameter.Elements.Should().BeEmpty();
            parameter.Default.Should().Be(new Dint());
        }

        [Test]
        public void SetName_ValidName_ShouldBeExpectedNae()
        {
            var parameter = Parameter.Create<Dint>("Test");
            
            parameter.SetName("Different");

            parameter.Name.Should().Be("Different");
        }

        [Test]
        public void SetName_Null_ShouldThrowArgumentException()
        {
            var parameter = Parameter.Create<Sint>("Test");
            
            FluentActions.Invoking(() => parameter.SetName(null)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetName_Empty_ShouldThrowArgumentException()
        {
            var parameter = Parameter.Create<Sint>("Test");
            
            FluentActions.Invoking(() => parameter.SetName(string.Empty)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetName_Invalid_ShouldThrowComponentNameInvalidException()
        {
            var parameter = Parameter.Create<Sint>("Test");
            
            FluentActions.Invoking(() => parameter.SetName("Not_Valid#01")).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetDescription_String_ShouldUpdateDescription()
        {
            var parameter = Parameter.Create<Sint>("Test");
            
            parameter.SetDescription("This is a test");

            parameter.Description.Should().Be("This is a test");
        }
        
        [Test]
        public void SetDescription_Empty_ShouldUpdateDescription()
        {
            var parameter = Parameter.Create<Sint>("Test");
            
            parameter.SetDescription(string.Empty);

            parameter.Description.Should().Be(string.Empty);
        }

        [Test]
        public void SetUsage_ValidUsageForType_ShouldUpdateUsage()
        {
            var parameter = Parameter.Create<String>("Test");
            
            parameter.SetUsage(TagUsage.Output);

            parameter.Usage.Should().Be(TagUsage.Output);
        }
        
        [Test]
        public void SetUsage_InvalidUsageForType_ShouldThrowInvalidOperationException()
        {
            var parameter = Parameter.Create<Dint>("Test");
            
            FluentActions.Invoking(() => parameter.SetUsage(TagUsage.InOut)).Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void SetUsage_Null_ShouldThrowArgumentNullException()
        {
            var parameter = Parameter.Create<String>("Test");
            
            FluentActions.Invoking(() => parameter.SetUsage(null)).Should().Throw<ArgumentNullException>();
        }
        
        
        [Test]
        public void SetUsage_NotInputOutputOrInOut_ShouldThrowArgumentException()
        {
            var parameter = Parameter.Create<String>("Test");
            
            FluentActions.Invoking(() => parameter.SetUsage(TagUsage.Normal)).Should().Throw<ArgumentException>();
        }
    }
}