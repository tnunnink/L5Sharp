using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagSimpleTests
    {
        [Test]
        public void Create_ValidTagName_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_InvalidTagName_ShouldThrowComponentNameInvalidException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Tag.Create(fixture.Create<string>(), (IDataType)new Bool())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Create_Bool_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.Create<Bool>("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<Bool>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.IsValueMember.Should().BeTrue();
            tag.IsStructureMember.Should().BeFalse();
            tag.IsArrayMember.Should().BeFalse();
        }

        [Test]
        public void Create_Sint_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.Create<Sint>("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<Sint>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.IsValueMember.Should().BeTrue();
            tag.IsStructureMember.Should().BeFalse();
            tag.IsArrayMember.Should().BeFalse();
        }

        [Test]
        public void Create_Real_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.Create<Real>("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.TagName.Should().Be("Test");
            tag.DataType.Should().BeOfType<Real>();
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Float);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Constant.Should().BeFalse();
            tag.IsValueMember.Should().BeTrue();
            tag.IsStructureMember.Should().BeFalse();
            tag.IsArrayMember.Should().BeFalse();
        }

        [Test]
        public void Create_UserDefined_ShouldNotBeNull()
        {
            var dataType = new UserDefined("UserDefined", "This is a test", new List<IMember<IDataType>>
            {
                Member.Create("TestMember", new Counter())
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
            var tag = Tag.Create("Test", new Dint(expected));

            var value = tag.Value;

            value.Should().Be(expected);
        }

        [Test]
        public void Value_String_ShouldBeExpectedValue()
        {
            const string expected = "This is a test";
            var tag = Tag.Create("Test", new String(expected));

            var value = tag.Value;

            value.Should().Be(expected);
        }

        [Test]
        public void Value_Complex_ShouldBeExpectedValue()
        {
            var tag = Tag.Create("Test", new Timer());

            var value = tag.Value;

            value.Should().BeNull();
        }

        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create("Test", (IDataType)new Int());

            FluentActions.Invoking(() => tag.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetValue_InvalidDataType_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create("Test", new Timer());

            FluentActions.Invoking(() => tag.SetValue(new Dint())).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void SetValue_IsValidBool_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<bool>();
            var tag = Tag.Create<Bool>("Test");

            tag.SetValue(new Bool(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidSint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<byte>();
            var tag = Tag.Create<Sint>("Test");

            tag.SetValue(new Sint(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidInt_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<short>();
            var tag = Tag.Create<Int>("Test");

            tag.SetValue(new Int(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidDint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create<Dint>("Test");

            tag.SetValue(new Dint(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidLint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.Create<Lint>("Test");

            tag.SetValue(new Lint(expected));

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidReal_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<float>();
            var tag = Tag.Create<Real>("Test");

            tag.SetValue(new Real(expected));

            tag.Value.Should().Be(expected);
        }
        
        [Test]
        public void TrySetValue_Null_ShouldBeFalse()
        {
            var tag = Tag.Create<Int>("Test");

            var result = tag.TrySetValue(null!);

            result.Should().BeFalse();
        }
        
        [Test]
        public void TrySetValue_NonAtomic_ShouldBeFalse()
        {
            var tag = Tag.Create<Timer>("Test");

            var result = tag.TrySetValue(new Dint());

            result.Should().BeFalse();
        }

        [Test]
        public void TrySetValue_InvalidDataType_ShouldBeFalse()
        {
            var tag = Tag.Create<Int>("Test");

            var result = tag.TrySetValue(new Bool());

            result.Should().BeFalse();
        }

        [Test]
        public void TrySetValue_ValidValue_ShouldBeTrue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.Create<Dint>("Test");

            var result = tag.TrySetValue(new Dint(expected));

            result.Should().BeTrue();
            tag.Value.Should().Be(expected);
        }
        
        [Test]
        public void Contains_Member_ShouldBeFalse()
        {
            var tag = Tag.Create<Bool>("Test");

            var result = tag.Contains("Member");

            result.Should().BeFalse();
        }

        [Test]
        public void IndexGetter_NonArray_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create<Bool>("Test");

            FluentActions.Invoking(() => tag[1]).Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void NameGetter_NonArray_ShouldThrowInvalidOperationException()
        {
            var tag = Tag.Create<Bool>("Test");

            FluentActions.Invoking(() => tag["Member"]).Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void GetTagNames_HasNoMembers_ShouldBeEmpty()
        {
            var tag = Tag.Create("Test", new Bool());

            var members = tag.GetTagNames();

            members.Should().BeEmpty();
        }
    }
}