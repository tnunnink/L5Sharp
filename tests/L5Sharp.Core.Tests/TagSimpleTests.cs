/*using System;
using System.Collections.Generic;
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
    public class TagSimpleTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Tag.Create(fixture.Create<string>(), (IDataType)new Bool())).Should()
                .Throw<ComponentNameInvalidException>();
        }
        
        [Test]
        public void New_BoolValidName_ShouldBeExpected()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            tag.Should().NotBeNull();
            tag.Name.ToString().Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Bool).ToUpper());
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Description.Should().BeNull();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.Constant.Should().BeFalse();
            tag.GetData().As<Bool>().Value.Should().BeFalse();
        }

        [Test]
        public void New_SintValidName_ShouldBeExpectedDefaults()
        {
            var tag = Tag.Create<Sint>("Test");
            
            tag.Should().NotBeNull();
            tag.Name.ToString().Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Sint).ToUpper());
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Description.Should().BeNull();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.Constant.Should().BeFalse();
            tag.GetData().Value.Should().Be(0);
        }
        
        [Test]
        public void New_RealValidName_ShouldBeExpectedDefaults()
        {
            var tag = Tag.Create<Real>("Test");
            
            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Real).ToUpper());
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Float);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Description.Should().BeNull();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.Constant.Should().BeFalse();
            tag.GetData().Value.Should().Be(0);
        }
        
        [Test]
        public void New_UserDefined_ShouldNotBeNull()
        {
            var dataType = new UserDefined("UserDefined", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create("TestMember", new Counter())
            });
            
            var tag = Tag.Create("Test", dataType);

            tag.Should().NotBeNull();
            tag.DataType.Should().Be(dataType.Name);
            tag.Should().BeOfType<Tag<IDataType>>();
        }

        [Test]
        public void GetData_TagWithValue_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create("Test", new Dint(expected));

            var value = tag.GetData().Value;

            value.Should().Be(expected);
        }

        [Test]
        public void SetName_Null_ShouldThrowArgumentException()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            FluentActions.Invoking(() => tag.SetName(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());
            var fixture = new Fixture();

            FluentActions.Invoking(() => tag.SetName(fixture.Create<string>())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetName_ValidName_ShouldUpdateName()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            tag.SetName("NewName");

            tag.Name.Should().Be("NewName");
        }

        [Test]
        public void SetDescription_Null_ShouldBeExpected()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            tag.Comment(null);

            tag.Description.Should().Be(null);
        }

        [Test]
        public void SetDescription_StringValue_ShouldBeExpected()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            tag.Comment("This is a test description");

            tag.Description.Should().Be("This is a test description");
        }

        [Test]
        public void SetDimensions_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            FluentActions.Invoking(() => tag.SetDimensions(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetDimensions_ValidDimensions_ShouldHaveUpdatedDimensions()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            tag.SetDimensions(new Dimensions(25));

            tag.Dimensions.Length.Should().Be(25);
        }

        [Test]
        public void SetDimensions_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = Tag.Build("Test", new Bool())
                .WithDimensions(10)
                .Create();

            tag.SetDimensions(new Dimensions(25));

            tag.Name.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Bool).ToUpper());
            tag.Dimensions.Should().Be(new Dimensions(25));
        }

        /*[Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            FluentActions.Invoking(() => tag.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetRadix_Invalid_shouldThrowRadixNotSupportedException()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            FluentActions.Invoking(() => tag.SetRadix(Radix.Float)).Should().Throw<RadixNotSupportedException>();
        }

        [Test]
        public void SetRadix_Valid_ShouldUpdateRadix()
        {
            var tag = Tag.Create("Test", (IDataType)new Dint());

            tag.SetRadix(Radix.Binary);

            tag.Radix.Should().Be(Radix.Binary);
        }#1#

        [Test]
        public void SetData_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Int());

            FluentActions.Invoking(() => tag.SetData(null)).Should().Throw<ArgumentNullException>();
        }

        /*[Test]
        public void SetData_InvalidDataType_ShouldThrowInvalidTagDataException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var tag = Tag.Create("Test", (IDataType)new Timer());

            FluentActions.Invoking(() => tag.SetData(new Dint(value))).Should().Throw<InvalidTagDataException>();
        }#1#

        [Test]
        public void SetData_IsDifferentAtomicType_ShouldThrowInvalidTagValueException()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.Create("Test", (IDataType)new Int());

            FluentActions.Invoking(() => tag.SetData(new Lint(expected))).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetData_IsValidBool_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<bool>();
            var tag = Tag.Create<Bool>("Test");

            tag.SetData(new Bool(expected));

            tag.GetData().Value.Should().Be(expected);
        }

        [Test]
        public void SetData_IsValidSint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<byte>();
            var tag = Tag.Create<Sint>("Test");

            tag.SetData(new Sint(expected));

            tag.GetData().Value.Should().Be(expected);
        }

        [Test]
        public void SetData_IsValidInt_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<short>();
            var tag = Tag.Create<Int>("Test");

            tag.SetData(new Int(expected));

            tag.GetData().Value.Should().Be(expected);
        }

        [Test]
        public void SetData_IsValidDint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create<Dint>("Test");

            tag.SetData(new Dint(expected));

            tag.GetData().Value.Should().Be(expected);
        }

        [Test]
        public void SetData_IsValidLint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.Create<Lint>("Test");

            tag.SetData(new Lint(expected));

            tag.GetData().Value.Should().Be(expected);
        }

        [Test]
        public void SetData_IsValidReal_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<float>();
            var tag = Tag.Create<Real>("Test");

            tag.SetData(new Real(expected));

            tag.GetData().Value.Should().Be(expected);
        }

        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Int());

            FluentActions.Invoking(() => tag.SetExternalAccess(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetExternalAccess_ValidType_ShouldBeExpectedType()
        {
            var tag = Tag.Create("Test", (IDataType)new Int());

            tag.SetExternalAccess(ExternalAccess.ReadOnly);

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void SetUsage_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Int());

            FluentActions.Invoking(() => tag.SetUsage(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetUsage_ValidTypeNonProgramTag_ShouldThrowNotConfigurableException()
        {
            var tag = Tag.Create("Test", (IDataType)new Int());

            FluentActions.Invoking(() => tag.SetUsage(TagUsage.Output)).Should()
                .Throw<ComponentNotConfigurableException>();
        }

        [Test]
        public void GetMemberList_WhenCalledHasNoMembers_ShouldBeEmpty()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            var members = tag.GetMemberNames();

            members.Should().BeEmpty();
        }

        [Test]
        public void ChangeDataType_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            FluentActions.Invoking(() => tag.ChangeDataType<Timer>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldNotBeNull()
        {
            var tag = Tag.Create("Test", (IDataType)new Bool());

            var result = tag.ChangeDataType(new Dint());

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = Tag.Create("Test", new Bool());

            var result = tag.ChangeDataType(new Dint());

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(nameof(Dint).ToUpper());
        }
    }
}*/