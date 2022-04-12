using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Creators;
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
        public void New_ValidArguments_ShouldNotBeNull()
        {
            var tag = new Tag<BOOL>("Test", new BOOL());

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Tag<BOOL>(null!, new BOOL())).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullDataType_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Tag<BOOL>("Test", null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedProperties()
        {
            var tag = new Tag<BOOL>("Test", new BOOL(), Radix.Binary, ExternalAccess.ReadOnly, "This is a test",
                TagUsage.Input);

            tag.Name.Should().Be("Test");
            tag.DataType.Should().BeOfType<BOOL>();
            tag.Dimensions.Should().BeEquivalentTo(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Binary);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            tag.Description.Should().Be("This is a test");
            tag.Usage.Should().Be(TagUsage.Input);
            tag.Root.Should().BeSameAs(tag);
            tag.Parent.Should().BeNull();
            tag.MemberType.Should().Be(MemberType.ValueMember);
            tag.TagName.Should().Be("Test");
            tag.Value.Should().Be(false);
        }

        [Test]
        public void Create_ValidTagName_ShouldNotBeNull()
        {
            var tag = Tag.Create<BOOL>("Test");

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_InvalidTagName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Tag.Create(fixture.Create<string>(), (IDataType)new BOOL())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Create_Bool_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.Create<BOOL>("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<BOOL>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.MemberType.Should().Be(MemberType.ValueMember);
        }

        [Test]
        public void Create_Sint_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.Create<SINT>("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<SINT>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.MemberType.Should().Be(MemberType.ValueMember);
        }

        [Test]
        public void Create_Real_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.Create<REAL>("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<REAL>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Float);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.MemberType.Should().Be(MemberType.ValueMember);
        }

        [Test]
        public void Create_UserDefined_ShouldNotBeNull()
        {
            var dataType = new UserDefined("UserDefined", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create("TestMember", new COUNTER())
            });

            var tag = Tag.Create("Test", dataType);

            tag.Should().NotBeNull();
            tag.DataType.Should().BeOfType<UserDefined>();
            tag.Should().BeOfType<Tag<UserDefined>>();
        }

        [Test]
        public void Value_Atomic_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create("Test", new DINT(expected));

            var value = tag.Value;

            value.Should().Be(expected);
        }

        [Test]
        public void Value_String_ShouldBeExpectedValue()
        {
            const string expected = "This is a test";
            var tag = Tag.Create("Test", new STRING(expected));

            var value = tag.Value;

            value.Should().Be(expected);
        }

        [Test]
        public void Value_Complex_ShouldBeExpectedValue()
        {
            var tag = Tag.Create("Test", new TIMER());

            var value = tag.Value;

            value.Should().BeNull();
        }

        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new INT());

            FluentActions.Invoking(() => tag.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_InvalidDataType_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create("Test", new TIMER());

            FluentActions.Invoking(() => tag.SetValue(new DINT())).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void SetValue_IsValidBool_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<bool>();
            var tag = Tag.Create<BOOL>("Test");

            tag.SetValue(new BOOL(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidSint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<sbyte>();
            var tag = Tag.Create<SINT>("Test");

            tag.SetValue(new SINT(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidInt_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<short>();
            var tag = Tag.Create<INT>("Test");

            tag.SetValue(new INT(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidDint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create<DINT>("Test");

            tag.SetValue(new DINT(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidLint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.Create<LINT>("Test");

            tag.SetValue(new LINT(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidReal_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<float>();
            var tag = Tag.Create<REAL>("Test");

            tag.SetValue(new REAL(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void TrySetValue_Null_ShouldBeFalse()
        {
            var tag = Tag.Create<INT>("Test");

            var result = tag.TrySetValue(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void TrySetValue_NonAtomic_ShouldBeFalse()
        {
            var tag = Tag.Create<TIMER>("Test");

            var result = tag.TrySetValue(new DINT());

            result.Should().BeFalse();
        }

        [Test]
        public void TrySetValue_InvalidDataType_ShouldBeFalse()
        {
            var tag = Tag.Create<INT>("Test");

            var result = tag.TrySetValue(new BOOL());

            result.Should().BeFalse();
        }

        [Test]
        public void TrySetValue_ValidValue_ShouldBeTrue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create<DINT>("Test");

            var result = tag.TrySetValue(new DINT(expected));

            result.Should().BeTrue();
            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetData_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<DINT>("Test");

            FluentActions.Invoking(() => tag.SetData(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetData_InvalidType_ShouldNotSetValue()
        {
            var tag = Tag.Create<DINT>("Test");

            tag.SetData(new TIMER(5000));

            tag.Value.Should().Be(0);
        }

        [Test]
        public void SetData_ValidType_ShouldUpdateValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create<DINT>("Test");

            tag.SetData(new DINT(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void HasMember_Member_ShouldBeFalse()
        {
            var tag = Tag.Create<BOOL>("Test");

            var result = tag.Contains("Member");

            result.Should().BeFalse();
        }

        [Test]
        public void Index_NonArray_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create<BOOL>("Test");

            FluentActions.Invoking(() => tag[1]).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Member_InvalidMember_ShouldThrowInvalidMemberPathException()
        {
            var tag = Tag.Create<BOOL>("Test");

            FluentActions.Invoking(() => tag.Member("Member")).Should().Throw<InvalidMemberPathException>();
        }

        [Test]
        public void GetTagNames_HasNoMembers_ShouldBeEmpty()
        {
            var tag = Tag.Create("Test", new BOOL());

            var members = tag.TagNames();

            members.Should().BeEmpty();
        }
    }
}