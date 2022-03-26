using System;
using AutoFixture;
using FluentAssertions;
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
        public void New_InvalidName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();
            FluentActions.Invoking(() => new Parameter<BOOL>(fixture.Create<string>(), new BOOL())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Parameter<BOOL>(null!, new BOOL())).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_EmptyName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Parameter<BOOL>(string.Empty, new BOOL())).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void New_AtomicType_ShouldNotBeNull()
        {
            var parameter = new Parameter<BOOL>("Test", new BOOL());

            parameter.Should().NotBeNull();
        }

        [Test]
        public void New_PredefinedType_ShouldNoteBeNull()
        {
            var parameter = new Parameter<TIMER>("Test", new TIMER());

            parameter.Should().NotBeNull();
        }

        [Test]
        public void New_UserType_ShouldNotBeNull()
        {
            var dataType = new UserDefined("Test");
            var parameter = new Parameter<IDataType>("Test", dataType);

            parameter.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldBeExpectedProperties()
        {
            var parameter = new Parameter<DINT>("Test", new DINT());

            parameter.Name.Should().Be("Test");
            parameter.Description.Should().BeEmpty();
            parameter.DataType.Should().Be(new DINT());
            parameter.Dimensions.Should().Be(Dimensions.Empty);
            parameter.Radix.Should().Be(Radix.Decimal);
            parameter.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            parameter.Usage.Should().Be(TagUsage.Input);
            parameter.TagType.Should().Be(TagType.Base);
            parameter.Required.Should().BeFalse();
            parameter.Visible.Should().BeFalse();
            parameter.Alias.Should().Be(TagName.Empty);
            parameter.Constant.Should().BeFalse();
            parameter.Default.Should().BeEquivalentTo(new DINT());
        }

        [Test]
        public void New_Overloaded_ShouldBeExpected()
        {
            var parameter = new Parameter<DINT>("Test", new DINT(34), TagUsage.Output, Radix.Hex,
                ExternalAccess.ReadOnly, true, true, true, "This is a test");

            parameter.Name.Should().Be("Test");
            parameter.Description.Should().Be("This is a test");
            parameter.DataType.Should().BeOfType<DINT>();
            parameter.Dimensions.Should().Be(Dimensions.Empty);
            parameter.Radix.Should().Be(Radix.Hex);
            parameter.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            parameter.Usage.Should().Be(TagUsage.Output);
            parameter.TagType.Should().Be(TagType.Alias);
            parameter.Required.Should().BeTrue();
            parameter.Visible.Should().BeTrue();
            parameter.Alias.Should().Be(new TagName("LocalTag"));
            parameter.Constant.Should().BeTrue();
            parameter.Default.Should().BeEquivalentTo(new DINT(34));
        }

        [Test]
        public void New_SimpleArray_ShouldBeExpectedProperties()
        {
            var parameter = new Parameter<IArrayType<DINT>>("Test", new ArrayType<DINT>(10));

            parameter.Name.Should().Be("Test");
            parameter.DataType.Should().BeOfType<ArrayType<DINT>>();
            parameter.Dimensions.Should().BeEquivalentTo(new Dimensions(10));
            parameter.Usage.Should().Be(TagUsage.InOut);
            parameter.Default.Should().BeNull();
        }

        [Test]
        public void New_Predefined_ShouldHaveInOutUsage()
        {
            var parameter = new Parameter<TIMER>("Test", new TIMER());

            parameter.Usage.Should().Be(TagUsage.InOut);
        }

        [Test]
        public void New_Array_ShouldHaveInOutUsage()
        {
            var parameter = new Parameter<IArrayType<DINT>>("Test", new ArrayType<DINT>(10));

            parameter.Usage.Should().Be(TagUsage.InOut);
        }
        
        [Test]
        public void New_Predefined_RequiredShouldBeTrue()
        {
            var parameter = new Parameter<TIMER>("Test", new TIMER());

            parameter.Required.Should().BeTrue();
        }

        [Test]
        public void New_Array_RequiredShouldBeTrue()
        {
            var parameter = new Parameter<IArrayType<DINT>>("Test", new ArrayType<DINT>(10));

            parameter.Required.Should().BeTrue();
        }
        
        [Test]
        public void New_Predefined_VisibleShouldBeTrue()
        {
            var parameter = new Parameter<TIMER>("Test", new TIMER());

            parameter.Required.Should().BeTrue();
        }

        [Test]
        public void New_Array_VisibleShouldBeTrue()
        {
            var parameter = new Parameter<IArrayType<DINT>>("Test", new ArrayType<DINT>(10));

            parameter.Required.Should().BeTrue();
        }
    }
}