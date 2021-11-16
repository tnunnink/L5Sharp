using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagSimpleArrayTests
    {
        [Test]
        public void New_ArrayOfAtomic_ShouldHaveExpectedElements()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            tag.Should().NotBeNull();
            tag.Dimensions.Length.Should().Be(5);
        }

        [Test]
        public void GetIndexer_ValidIndex_ShouldBeExpected()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[3];

            element.Should().NotBeNull();
            element.Name.Should().Be("[3]");
            element.DataType.Should().Be(nameof(Bool).ToUpper());
            element.ExternalAccess.Should().Be(ExternalAccess.None);
        }

        [Test]
        public void GetElement_InvalidIndex_ShouldBeNull()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[6];

            element.Should().BeNull();
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldUpdateAllElementRadixValues()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();
            tag.GetMembers().Select(e => e.Radix).Should().AllBeEquivalentTo(Radix.Decimal);

            tag.SetRadix(Radix.Binary);

            tag.GetMembers().Select(e => e.Radix).Should().AllBeEquivalentTo(Radix.Binary);
        }

        [Test]
        public void SetDescription_String_ShouldUpdateAllElementDescriptionValues()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();
            tag.GetMembers().Select(e => e.Description).Should().AllBeEquivalentTo<string>(null);

            tag.SetDescription("This is a test");
            tag.Description.Should().Be("This is a test");
            tag.GetMembers().Select(e => e.Description).Should().AllBeEquivalentTo("This is a test");
        }

        [Test]
        public void SetElementDescription_ThenSetTagDescription_TagMemberShouldStillHaveOverridenDescription()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[0];
            element.SetDescription("Element Description");
            element.Description.Should().Be("Element Description");

            tag.SetDescription("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");
        }

        [Test]
        public void
            SetElementDescription_ThenSetTagDescription_ThenGetTheTagMemberAgain_ItShouldRetainTheOverridenDescription()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[0];
            element.SetDescription("Element Description");
            element.Description.Should().Be("Element Description");

            tag.SetDescription("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");

            element = tag[0];
            element.Description.Should().Be("Element Description");
        }

        [Test]
        public void
            SetElementDescription_ThenSetTagDescription_ThenSetTheMemberDescriptionBack_ItShouldRevertToTagDescription()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[0];
            element.SetDescription("Element Description");
            element.Description.Should().Be("Element Description");

            tag.SetDescription("Tag Description");
            tag.Description.Should().Be("Tag Description");
            element.Description.Should().Be("Element Description");

            element.SetDescription(string.Empty);
            element.Description.Should().Be("Tag Description");
        }

        [Test]
        public void SetElement_Description_ShouldBeExpected()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            tag[0].SetDescription("This is a test");

            tag[0].Description.Should().Be("This is a test");
            tag[1].Description.Should().BeNull();
            tag[2].Description.Should().BeNull();
            tag[3].Description.Should().BeNull();
            tag[4].Description.Should().BeNull();
        }

        [Test]
        public void SetElement_Radix_ShouldBeExpected()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            tag[0].SetRadix(Radix.Binary);

            tag[0].Radix.Should().Be(Radix.Binary);
            tag[1].Radix.Should().Be(Radix.Decimal);
            tag[2].Radix.Should().Be(Radix.Decimal);
            tag[3].Radix.Should().Be(Radix.Decimal);
            tag[4].Radix.Should().Be(Radix.Decimal);
        }

        [Test]
        public void SetElement_Atomic_ShouldBeExpected()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            tag[0].SetData(new Bool(true));

            tag[0].DataType.As<Bool>().Should().Be(true);
            tag[1].DataType.As<Bool>().Should().Be(false);
            tag[2].DataType.As<Bool>().Should().Be(false);
            tag[3].DataType.As<Bool>().Should().Be(false);
            tag[4].DataType.As<Bool>().Should().Be(false);
        }
    }
}