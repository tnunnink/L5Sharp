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
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));

            tag.Elements.Should().NotBeNull();
            tag.Elements.Should().HaveCount(5);
            tag.Elements.Should().AllBeOfType<Member<IDataType>>();
            tag.Elements.Select(e => e.DataType).Should().AllBeOfType<Bool>();
            tag.Elements.Select(e => e.DataType).Should().AllBeEquivalentTo(new Bool());
        }

        [Test]
        public void GetElement_ValidIndex_ShouldBeExpected()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));

            var element = tag.GetElement(3);

            element.Should().NotBeNull();
            element.Name.Should().Be("[3]");
            element.DataType.Should().Be("BOOL");
            element.FullName.Should().Be("Test[3]");
            element.Elements.Should().BeEmpty();
            element.Parent.Should().Be(tag);
            element.ExternalAccess.Should().Be(ExternalAccess.None);
        }
        
        [Test]
        public void GetElement_InvalidIndex_ShouldBeNull()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));

            var element = tag.GetElement(6);

            element.Should().BeNull();
        }
        
        [Test]
        public void SetRadix_ValidRadix_ShouldUpdateAllElementRadixValues()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));
            tag.Elements.Select(e => e.Radix).Should().AllBeEquivalentTo(Radix.Decimal);

            tag.SetRadix(Radix.Binary);

            tag.Elements.Select(e => e.Radix).Should().AllBeEquivalentTo(Radix.Binary);
        }
        
        [Test]
        public void SetDescription_String_ShouldUpdateAllElementDescriptionValues()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));
            tag.Elements.Select(e => e.Description).Should().AllBeEquivalentTo<string>(null);
            
            tag.SetDescription("This is a test");
            tag.Description.Should().Be("This is a test");
            tag.Elements.Select(e => e.Description).Should().AllBeEquivalentTo("This is a test");
        }
        
        [Test]
        public void SetElementDescription_ThenSetTagDescription_TagMemberShouldStillHaveOverridenDescription()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));

            var element = tag.GetElement(0);
            element.SetDescription("Element Description");
            element.Description.Should().Be("Element Description");
            
            tag.SetDescription("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");
        }
        
        [Test]
        public void SetElementDescription_ThenSetTagDescription_ThenGetTheTagMemberAgain_ItShouldRetainTheOverridenDescription()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));

            var element = tag.GetElement(0);
            element.SetDescription("Element Description");
            element.Description.Should().Be("Element Description");
            
            tag.SetDescription("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");

            element = tag.GetElement(0);
            element.Description.Should().Be("Element Description");
        }
        
        [Test]
        public void SetElementDescription_ThenSetTagDescription_ThenSetTheMemberDescriptionBack_ItShouldRevertToTagDescription()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));

            var element = tag.GetElement(0);
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
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));
            
            tag.SetElement(0, "This is a test");

            tag.Elements[0].Description.Should().Be("This is a test");
            tag.Elements[1].Description.Should().BeNull();
            tag.Elements[2].Description.Should().BeNull();
            tag.Elements[3].Description.Should().BeNull();
            tag.Elements[4].Description.Should().BeNull();
        }

        [Test]
        public void SetElement_Radix_ShouldBeExpected()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));
            
            tag.SetElement(0, Radix.Binary);

            tag.Elements[0].Radix.Should().Be(Radix.Binary);
            tag.Elements[1].Radix.Should().Be(Radix.Decimal);
            tag.Elements[2].Radix.Should().Be(Radix.Decimal);
            tag.Elements[3].Radix.Should().Be(Radix.Decimal);
            tag.Elements[4].Radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void SetElement_Atomic_ShouldBeExpected()
        {
            var tag = Tag.New("Test", new Bool(), new Dimensions(5));
            
            tag.SetElement(0, new Bool(true));

            tag.Elements[0].DataType.As<Bool>().Should().Be(true);
            tag.Elements[1].DataType.As<Bool>().Should().Be(false);
            tag.Elements[2].DataType.As<Bool>().Should().Be(false);
            tag.Elements[3].DataType.As<Bool>().Should().Be(false);
            tag.Elements[4].DataType.As<Bool>().Should().Be(false);
        }
    }
}