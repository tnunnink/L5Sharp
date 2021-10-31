using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Bool);

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => new Tag(fixture.Create<string>(), Predefined.Bool)).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_NoOverloads_ShouldHaveExpectedDefaults()
        {
            var tag = new Tag("Test", Predefined.Bool);

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.FullName.Should().Be("Test");
            tag.DataType.Should().Be(Predefined.Bool);
            tag.Dimensions.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Value.Should().Be(false);
            tag.Description.Should().BeNull();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.Parent.Should().Be(null);
            tag.Constant.Should().BeFalse();
            tag.IsValueMember.Should().BeTrue();
            tag.IsArrayMember.Should().BeFalse();
            tag.IsArrayElement.Should().BeFalse();
            tag.IsStructureMember.Should().BeFalse();
        }

        [Test]
        public void New_AllOverloads_ShouldHaveExpectedProperties()
        {
            var tag = new Tag("Test", Predefined.Dint, new Dimensions(5), Radix.Ascii, ExternalAccess.ReadOnly,
                "This is a test tag", TagUsage.Input, true);

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.DataType.Should().Be(Predefined.Dint);
            tag.Dimensions.Should().Be(new Dimensions(5));
            tag.Radix.Should().Be(Radix.Ascii);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            tag.Value.Should().Be(0);
            tag.Description.Should().Be("This is a test tag");
            tag.Usage.Should().Be(TagUsage.Input);
            tag.Constant.Should().BeTrue();
        }

        [Test]
        public void New_Alarm_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", Predefined.Alarm);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(24);
            tag.Members.Any(t => t.Name == "EnableIn").Should().BeTrue();
            tag.Members.Any(t => t.Name == "In").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HHLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LLLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "Deadband").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPosLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCNegLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPeriod").Should().BeTrue();
            tag.Members.Any(t => t.Name == "EnableOut").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HHAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LLAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPosAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCNegAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROC").Should().BeTrue();
            tag.Members.Any(t => t.Name == "Status").Should().BeTrue();
            tag.Members.Any(t => t.Name == "InstructFault").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DeadbandInv").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPosLimitInv").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCNegLimitInv").Should().BeTrue();
        }

        [Test]
        public void New_TwoDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var length = first * second;

            var tag = new Tag("Test", Predefined.Dint, new Dimensions(first, second));

            tag.Dimensions.Length.Should().Be(length);
        }

        [Test]
        public void New_GenericTag_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Bool);
            tag.Should().NotBeNull();
            tag.DataType.Should().Be(Predefined.Bool);
        }

        [Test]
        public void SetName_Null_ShouldThrowArgumentException()
        {
            var tag = new Tag("Test", Predefined.Dint);

            FluentActions.Invoking(() => tag.SetName(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var tag = new Tag("Test", Predefined.Dint);
            var fixture = new Fixture();

            FluentActions.Invoking(() => tag.SetName(fixture.Create<string>())).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetName_ValidName_ShouldUpdateName()
        {
            var tag = new Tag("Test", Predefined.Dint);

            tag.SetName("NewName");

            tag.Name.Should().Be("NewName");
        }

        [Test]
        public void SetDescription_Null_ShouldBeExpected()
        {
            var tag = new Tag("Test", Predefined.Dint);

            tag.SetDescription(null);

            tag.Description.Should().Be(null);
        }

        [Test]
        public void SetDescription_StringValue_ShouldBeExpected()
        {
            var tag = new Tag("Test", Predefined.Dint);

            tag.SetDescription("This is a test description");

            tag.Description.Should().Be("This is a test description");
        }

        [Test]
        public void ChangeDataType_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag("Test", Predefined.Bool);

            FluentActions.Invoking(() => tag.ChangeDataType(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Bool);

            var result = tag.ChangeDataType(Predefined.Dint);

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = new Tag("Test", Predefined.Bool, dimensions: new Dimensions(10), radix: Radix.Hex,
                externalAccess: ExternalAccess.ReadOnly, description: "This is a test");

            var result = tag.ChangeDataType(Predefined.Dint);

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(Predefined.Dint);
            result.Dimensions.Should().Be(new Dimensions(10));
            result.Radix.Should().Be(Radix.Hex);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            result.Description.Should().Be("This is a test");
        }

        [Test]
        public void ChangeDimensions_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag("Test", Predefined.Bool);

            FluentActions.Invoking(() => tag.ChangeDimensions(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDimensions_ValidType_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Bool);

            var result = tag.ChangeDimensions(new Dimensions(25));

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDimensions_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = new Tag("Test", Predefined.Bool, new Dimensions(10), Radix.Hex, ExternalAccess.ReadOnly,
                "This is a test");

            var result = tag.ChangeDimensions(new Dimensions(25));

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(Predefined.Bool);
            result.Dimensions.Should().Be(new Dimensions(25));
            result.Radix.Should().Be(Radix.Hex);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            result.Description.Should().Be("This is a test");
        }

        [Test]
        public void SetRadix_NullOnAtomic_ShouldThrowArgumentNullException()
        {
            var tag = new Tag("Test", Predefined.Dint, new Dimensions(3, 4));

            FluentActions.Invoking(() => tag.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetRadix_ValidRadixOnAtomic_ShouldSetMembersRadix()
        {
            var tag = new Tag("Test", Predefined.Int, new Dimensions(3, 4));

            tag.SetRadix(Radix.Ascii);

            tag.Radix.Should().Be(Radix.Ascii);
            tag.Members.All(t => t.Radix == Radix.Ascii).Should().BeTrue();
        }

        [Test]
        public void SetValue_DataTypeIsNotAtomic_ShouldThrowNotConfigurableException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var tag = new Tag("Test", Predefined.Timer);

            FluentActions.Invoking(() => tag.SetValue(value)).Should().Throw<ComponentNotConfigurableException>();
        }

        [Test]
        public void SetValue_Null_ShouldThrowInvalidTagValueException()
        {
            var tag = new Tag("Test", Predefined.Int);

            FluentActions.Invoking(() => tag.SetValue(null)).Should().Throw<InvalidTagValueException>();
        }

        [Test]
        public void SetValue_IsInvalid_ShouldThrowInvalidTagValueException()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = new Tag("Test", Predefined.Int);

            FluentActions.Invoking(() => tag.SetValue(expected)).Should().Throw<InvalidTagValueException>();
        }

        [Test]
        public void SetValue_IsValidBool_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<bool>();
            var tag = new Tag("Test", Predefined.Bool);

            tag.SetValue(expected);

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidSint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<byte>();
            var tag = new Tag("Test", Predefined.Sint);

            tag.SetValue(expected);

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidInt_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<short>();
            var tag = new Tag("Test", Predefined.Int);

            tag.SetValue(expected);

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidDint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = new Tag("Test", Predefined.Dint);

            tag.SetValue(expected);

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidLint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = new Tag("Test", Predefined.Lint);

            tag.SetValue(expected);

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidReal_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<float>();
            var tag = new Tag("Test", Predefined.Real);

            tag.SetValue(expected);

            tag.Value.Should().Be(expected);
        }

        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag("Test", Predefined.Int);

            FluentActions.Invoking(() => tag.SetExternalAccess(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetExternalAccess_ValidType_ShouldBeExpectedType()
        {
            var tag = new Tag("Test", Predefined.Int);

            tag.SetExternalAccess(ExternalAccess.ReadOnly);

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void SetUsage_Null_ShouldThrowArgumentNullException()
        {
            var tag = new Tag("Test", Predefined.Int);

            FluentActions.Invoking(() => tag.SetUsage(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetUsage_ValidTypeNonProgramTag_ShouldThrowNotConfigurableException()
        {
            var tag = new Tag("Test", Predefined.Int);

            FluentActions.Invoking(() => tag.SetUsage(TagUsage.Output)).Should().Throw<ComponentNotConfigurableException>();
        }

        [Test]
        public void SetUsage_ValidTypeProgramTag_ShouldThrowNotConfigurableException()
        {
            var tag = new Tag("Test", Predefined.Int, parent: new Program("Program"));

            tag.SetUsage(TagUsage.Output);

            tag.Usage.Should().Be(TagUsage.Output);
        }

        [Test]
        public void ListMembers_WhenCalledHasNoMembers_ShouldBeEmpty()
        {
            var tag = new Tag("Test", Predefined.Bool);

            var members = tag.GetMembersNames();

            members.Should().BeEmpty();
        }

        [Test]
        public void ListMembers_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = new Tag("Test", Predefined.Counter);

            var members = tag.GetMembersNames();

            members.Should().NotBeEmpty();
        }

        [Test]
        public void GetMember_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Timer);

            var member = tag.GetMember("PRE");

            member.Should().NotBeNull();
        }

        [Test]
        public void GetMember_NullMemberHasMember_ShouldThrowArgumentNullException()
        {
            var tag = new Tag("Test", Predefined.Timer);

            FluentActions.Invoking(() => tag.GetMember(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetMember_InvalidMemberHasMember_MemberShouldBeNull()
        {
            var tag = new Tag("Test", Predefined.Timer);

            var member = tag.GetMember("Invalid");

            member.Should().BeNull();
        }

        [Test]
        public void ChangeTagType_ValidTypeBase_TagShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Timer);

            var result = tag.ChangeTagType(TagType.Base);

            result.Should().NotBeNull();
        }
    }
}