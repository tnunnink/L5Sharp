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
    public class TagTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Bool());

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => Tag.New(fixture.Create<string>(), new Bool())).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_NoOverloads_ShouldHaveExpectedDefaults()
        {
            var tag = Tag.New("Test", new Bool());

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.FullName.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Bool).ToUpper());
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
            var tag = Tag.New("Test", new Dint(), new Dimensions(5), Radix.Ascii, ExternalAccess.ReadOnly,
                "This is a test tag", TagUsage.Input, true);

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.DataType.Should().Be(nameof(Dint).ToUpper());
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
            var tag = Tag.New("Test", new Alarm());

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_TwoDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var length = first * second;

            var tag = Tag.New("Test", new Dint(), new Dimensions(first, second));

            tag.Dimensions.Length.Should().Be(length);
        }

        [Test]
        public void New_GenericTag_ShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Bool());
            tag.Should().NotBeNull();
            tag.DataType.Should().Be(nameof(Bool).ToUpper());
        }

        [Test]
        public void SetName_Null_ShouldThrowArgumentException()
        {
            var tag = Tag.New("Test", new Dint());

            FluentActions.Invoking(() => tag.SetName(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var tag = Tag.New("Test", new Dint());
            var fixture = new Fixture();

            FluentActions.Invoking(() => tag.SetName(fixture.Create<string>())).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetName_ValidName_ShouldUpdateName()
        {
            var tag = Tag.New("Test", new Dint());

            tag.SetName("NewName");

            tag.Name.Should().Be("NewName");
        }

        [Test]
        public void SetDescription_Null_ShouldBeExpected()
        {
            var tag = Tag.New("Test", new Dint());

            tag.SetDescription(null);

            tag.Description.Should().Be(null);
        }

        [Test]
        public void SetDescription_StringValue_ShouldBeExpected()
        {
            var tag = Tag.New("Test", new Dint());

            tag.SetDescription("This is a test description");

            tag.Description.Should().Be("This is a test description");
        }

        [Test]
        public void ChangeDataType_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.New("Test", new Bool());

            FluentActions.Invoking(() => tag.ChangeDataType<Timer>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Bool());

            var result = tag.ChangeDataType(new Dint());

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDataType_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(10), Radix.Hex,
                ExternalAccess.ReadOnly, "This is a test");

            var result = tag.ChangeDataType(new Dint());

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(nameof(Dint).ToUpper());
            result.Dimensions.Should().Be(new Dimensions(10));
            result.Radix.Should().Be(Radix.Hex);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            result.Description.Should().Be("This is a test");
        }

        [Test]
        public void ChangeDimensions_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.New("Test", new Bool());

            FluentActions.Invoking(() => tag.ChangeDimensions(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ChangeDimensions_ValidType_ShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Bool());

            var result = tag.ChangeDimensions(new Dimensions(25));

            result.Should().NotBeNull();
        }

        [Test]
        public void ChangeDimensions_ValidType_ShouldHaveExpectedProperties()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(10), Radix.Hex, ExternalAccess.ReadOnly,
                "This is a test");

            var result = tag.ChangeDimensions(new Dimensions(25));

            result.Name.Should().Be("Test");
            result.DataType.Should().Be(nameof(Bool).ToUpper());
            result.Dimensions.Should().Be(new Dimensions(25));
            result.Radix.Should().Be(Radix.Hex);
            result.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            result.Description.Should().Be("This is a test");
        }

        [Test]
        public void SetRadix_NullOnAtomic_ShouldThrowArgumentNullException()
        {
            var tag = Tag.New("Test", new Dint(), new Dimensions(3, 4));

            FluentActions.Invoking(() => tag.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetRadix_ValidRadixOnAtomic_ShouldSetMembersRadix()
        {
            var tag = Tag.New("Test", new Int(), new Dimensions(3, 4));

            tag.SetRadix(Radix.Ascii);

            tag.Radix.Should().Be(Radix.Ascii);
        }

        [Test]
        public void SetValue_DataTypeIsNotAtomic_ShouldThrowNotConfigurableException()
        {
            var fixture = new Fixture();
            var value = fixture.Create<int>();
            var tag = Tag.New("Test", new Timer());

            tag.SetData(new Dint(value));
        }

        [Test]
        public void SetValue_Null_ShouldThrowInvalidTagValueException()
        {
            var tag = Tag.New("Test", new Int());

            FluentActions.Invoking(() => tag.SetData(null)).Should().Throw<InvalidTagDataException>();
        }

        [Test]
        public void SetValue_IsInvalid_ShouldThrowInvalidTagValueException()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.New("Test", new Int());

            FluentActions.Invoking(() => tag.SetData(new Lint(expected))).Should().Throw<InvalidTagDataException>();
        }

        [Test]
        public void SetValue_IsValidBool_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<bool>();
            var tag = Tag.OfType<Bool>("Test");

            tag.SetData(new Bool(expected));

            tag.GetData().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidSint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<byte>();
            var tag = Tag.OfType<Sint>("Test");

            tag.SetData(new Sint(expected));

            tag.GetData().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidInt_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<short>();
            var tag = Tag.OfType<Int>("Test");

            tag.SetData(new Int(expected));

            tag.GetData().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidDint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<int>();
            var tag = Tag.OfType<Dint>("Test");

            tag.SetData(new Dint(expected));

            tag.GetData().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidLint_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<long>();
            var tag = Tag.OfType<Lint>("Test");

            tag.SetData(new Lint(expected));

            tag.GetData().Should().Be(expected);
        }

        [Test]
        public void SetValue_IsValidReal_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var expected = fixture.Create<float>();
            var tag = Tag.OfType<Real>("Test");

            tag.SetData(new Real(expected));

            tag.GetData().Should().Be(expected);
        }

        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.New("Test", new Int());

            FluentActions.Invoking(() => tag.SetExternalAccess(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetExternalAccess_ValidType_ShouldBeExpectedType()
        {
            var tag = Tag.New("Test", new Int());

            tag.SetExternalAccess(ExternalAccess.ReadOnly);

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void SetUsage_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.New("Test", new Int());

            FluentActions.Invoking(() => tag.SetUsage(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetUsage_ValidTypeNonProgramTag_ShouldThrowNotConfigurableException()
        {
            var tag = Tag.New("Test", new Int());

            FluentActions.Invoking(() => tag.SetUsage(TagUsage.Output)).Should().Throw<ComponentNotConfigurableException>();
        }

        [Test]
        public void ListMembers_WhenCalledHasNoMembers_ShouldBeEmpty()
        {
            var tag = Tag.New("Test", new Bool());

            var members = tag.GetMembersList();

            members.Should().BeEmpty();
        }

        [Test]
        public void ListMembers_WhenCalledHasMembers_ShouldNotBeEmpty()
        {
            var tag = Tag.New("Test", new Counter());

            var members = tag.GetMembersList();

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
        public void GetMembers_ByString_SameMember_DataShouldReferenceSameInstance()
        {
            var tag = Tag.OfType<Timer>("Test");
            
            var first = tag.GetMember("ACC");
            var second = tag.GetMember("ACC");

            first.GetData().Should().BeSameAs(second.GetData());
        }
        
        [Test]
        public void GetMembers_ByReference_SameMember_DataShouldReferenceSameInstance()
        {
            var tag = Tag.OfType<Timer>("Test");
            
            var first = tag.GetMember(d => d.ACC);
            var second = tag.GetMember(d => d.ACC);

            first.GetData().Should().BeSameAs(second.GetData());
        }
        
        
        [Test]
        public void SetMember_ValidNameHasMember_ShouldNotBeNull()
        {
            var tag = Tag.OfType<Timer>("Test");
            
            tag.SetMember(t => t.ACC, new Dint(5001));

            tag.GetMember(t => t.ACC).GetData().As<Dint>().Should().Be(5001);
        }
        
        [Test]
        public void GetMember_GenericTagType_ShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Timer());

            var value = tag.GetMember("PRE").GetData().As<Dint>();

            value.Should().NotBeNull();
        }

        [Test]
        public void GetMember_NullMemberHasMember_ShouldThrowArgumentNullException()
        {
            var tag = Tag.New("Test", new Timer());

            FluentActions.Invoking(() => tag.GetMember(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetMember_InvalidMemberHasMember_MemberShouldBeNull()
        {
            var tag = Tag.New("Test", new Timer());

            var member = tag.GetMember("Invalid");

            member.Should().BeNull();
        }

        [Test]
        public void ChangeTagType_ValidTypeBase_TagShouldNotBeNull()
        {
            var tag = Tag.New("Test", new Timer());

            var result = tag.ChangeTagType(TagType.Base);

            result.Should().NotBeNull();
        }
    }
}