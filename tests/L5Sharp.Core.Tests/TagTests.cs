using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            tag.Should().NotBeNull();
        }

        [Test]
        public void Array()
        {
            var tag = new Tag<Bool>[]
            {
                new("Test", new Bool()),
                
            };
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => new Tag<IDataType>(fixture.Create<string>(), new Bool())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_NoOverloads_ShouldHaveExpectedDefaults()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.FullName.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Bool));
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Description.Should().BeNull();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.Parent.Should().Be(null);
            tag.Constant.Should().BeFalse();
        }

        [Test]
        public void New_AllOverloads_ShouldHaveExpectedProperties()
        {
            var tag = new Tag<IDataType>("Test", new Dint(), new Dimensions(5), Radix.Ascii, ExternalAccess.ReadOnly,
                "This is a test tag", TagUsage.Input, true);

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Dint));
            tag.Dimensions.Should().Be(new Dimensions(5));
            tag.Radix.Should().Be(Radix.Ascii);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            tag.Description.Should().Be("This is a test tag");
            tag.Usage.Should().Be(TagUsage.Input);
            tag.Constant.Should().BeTrue();
        }

        [Test]
        public void New_Alarm_ShouldHaveValidMembers()
        {
            var tag = new Tag<IDataType>("Test", new Alarm());

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_TwoDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var length = first * second;

            var tag = new Tag<IDataType>("Test", new Dint(), new Dimensions(first, second));

            tag.Dimensions.Length.Should().Be(length);
        }

        [Test]
        public void New_GenericTag_ShouldNotBeNull()
        {
            var tag = new Tag<IDataType>("Test", new Bool());
            tag.Should().NotBeNull();
            tag.DataType.Should().Be(nameof(Bool));
        }

        [Test]
        public void SetName_Null_ShouldThrowArgumentException()
        {
            var tag = new Tag<IDataType>("Test", new Dint());

            FluentActions.Invoking(() => tag.SetName(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var tag = new Tag<IDataType>("Test", new Dint());
            var fixture = new Fixture();

            FluentActions.Invoking(() => tag.SetName(fixture.Create<string>())).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetName_ValidName_ShouldUpdateName()
        {
            var tag = new Tag<IDataType>("Test", new Dint());

            tag.SetName("NewName");

            tag.Name.Should().Be("NewName");
        }

        [Test]
        public void SetDescription_Null_ShouldBeExpected()
        {
            var tag = new Tag<IDataType>("Test", new Dint());

            tag.SetDescription(null);

            tag.Description.Should().Be(null);
        }

        [Test]
        public void SetDescription_StringValue_ShouldBeExpected()
        {
            var tag = new Tag<IDataType>("Test", new Dint());

            tag.SetDescription("This is a test description");

            tag.Description.Should().Be("This is a test description");
        }

        [Test]
        public void ChangeDataType_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            FluentActions.Invoking(() => tag.ChangeDataType<Timer>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldNotBeNull()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            var result = tag.ChangeDataType(new Dint());

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = new Tag<IDataType>("Test", new Bool(), dimensions: new Dimensions(10), radix: Radix.Hex,
                externalAccess: ExternalAccess.ReadOnly, description: "This is a test");

            var result = tag.ChangeDataType(new Dint());

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(nameof(Dint));
            result.Dimensions.Should().Be(new Dimensions(10));
            result.Radix.Should().Be(Radix.Hex);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            result.Description.Should().Be("This is a test");
        }

        [Test]
        public void ChangeDimensions_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            FluentActions.Invoking(() => tag.ChangeDimensions(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDimensions_ValidType_ShouldNotBeNull()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            var result = tag.ChangeDimensions(new Dimensions(25));

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDimensions_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = new Tag<IDataType>("Test", new Bool(), new Dimensions(10), Radix.Hex, ExternalAccess.ReadOnly,
                "This is a test");

            var result = tag.ChangeDimensions(new Dimensions(25));

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(nameof(Bool));
            result.Dimensions.Should().Be(new Dimensions(25));
            result.Radix.Should().Be(Radix.Hex);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            result.Description.Should().Be("This is a test");
        }

        [Test]
        public void SetRadix_NullOnAtomic_ShouldThrowArgumentNullException()
        {
            var tag = new Tag<IDataType>("Test", new Dint(), new Dimensions(3, 4));

            FluentActions.Invoking(() => tag.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetRadix_ValidRadixOnAtomic_ShouldSetMembersRadix()
        {
            var tag = new Tag<IDataType>("Test", new Int(), new Dimensions(3, 4));

            tag.SetRadix(Radix.Ascii);

            tag.Radix.Should().Be(Radix.Ascii);
        }

        [Test]
        public void SetValue_DataTypeIsNotAtomic_ShouldThrowNotConfigurableException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var tag = new Tag<IDataType>("Test", new Timer());

            FluentActions.Invoking(() => tag.SetValue(new Dint(value)))
                .Should().Throw<ComponentNotConfigurableException>();
        }

        [Test]
        public void SetValue_Null_ShouldThrowInvalidTagValueException()
        {
            var tag = new Tag<IDataType>("Test", new Int());

            FluentActions.Invoking(() => tag.SetValue(null)).Should().Throw<InvalidTagValueException>();
        }

        [Test]
        public void SetValue_IsInvalid_ShouldThrowInvalidTagValueException()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = new Tag<IDataType>("Test", new Int());

            FluentActions.Invoking(() => tag.SetValue(new Lint(expected))).Should().Throw<InvalidTagValueException>();
        }

        [Test]
        public void SetValue_IsValidBool_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<bool>();
            var tag = Tag.OfType<Bool>("Test");

            tag.SetValue(new Bool(expected));

            tag.GetValue().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidSint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<byte>();
            var tag = Tag.OfType<Sint>("Test");

            tag.SetValue(new Sint(expected));

            tag.GetValue().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidInt_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<short>();
            var tag = Tag.OfType<Int>("Test");

            tag.SetValue(new Int(expected));

            tag.GetValue().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidDint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.OfType<Dint>("Test");

            tag.SetValue(new Dint(expected));

            tag.GetValue().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidLint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.OfType<Lint>("Test");

            tag.SetValue(new Lint(expected));

            tag.GetValue().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidReal_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<float>();
            var tag = Tag.OfType<Real>("Test");

            tag.SetValue(new Real(expected));

            tag.GetValue().Should().Be(expected);
        }

        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag<IDataType>("Test", new Int());

            FluentActions.Invoking(() => tag.SetExternalAccess(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetExternalAccess_ValidType_ShouldBeExpectedType()
        {
            var tag = new Tag<IDataType>("Test", new Int());

            tag.SetExternalAccess(ExternalAccess.ReadOnly);

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void SetUsage_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag<IDataType>("Test", new Int());

            FluentActions.Invoking(() => tag.SetUsage(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetUsage_ValidTypeNonProgramTag_ShouldThrowNotConfigurableException()
        {
            var tag = new Tag<IDataType>("Test", new Int());

            FluentActions.Invoking(() => tag.SetUsage(TagUsage.Output)).Should().Throw<ComponentNotConfigurableException>();
        }

        [Test]
        public void SetUsage_ValidTypeProgramTag_ShouldThrowNotConfigurableException()
        {
            var tag = new Tag<IDataType>("Test", new Int(), parent: new Program("Program"));

            tag.SetUsage(TagUsage.Output);

            tag.Usage.Should().Be(TagUsage.Output);
        }

        [Test]
        public void ListMembers_WhenCalledHasNoMembers_ShouldBeEmpty()
        {
            var tag = new Tag<IDataType>("Test", new Bool());

            var members = tag.GetMembersNames();

            members.Should().BeEmpty();
        }

        [Test]
        public void ListMembers_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = new Tag<IDataType>("Test", new Counter());

            var members = tag.GetMembersNames();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetMember_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.OfType<Timer>("Test");

            var member = tag.GetMember(t => t.PRE);

            member.Should().NotBeNull();
        }
        
        [Test]
        public void GetMember_GenericTagType_ShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Timer());

            var value = tag.GetMember("PRE").GetValue().As<Dint>();

            value.Should().NotBeNull();
        }

        [Test]
        public void GetMember_NullMemberHasMember_ShouldThrowArgumentNullException()
        {
            var tag = new Tag<IDataType>("Test", new Timer());

            FluentActions.Invoking(() => tag.GetMember(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetMember_InvalidMemberHasMember_MemberShouldBeNull()
        {
            var tag = new Tag<IDataType>("Test", new Timer());

            var member = tag.GetMember("Invalid");

            member.Should().BeNull();
        }

        [Test]
        public void ChangeTagType_ValidTypeBase_TagShouldNotBeNull()
        {
            var tag = new Tag<IDataType>("Test", new Timer());

            var result = tag.ChangeTagType(TagType.Base);

            result.Should().NotBeNull();
        }
    }
}