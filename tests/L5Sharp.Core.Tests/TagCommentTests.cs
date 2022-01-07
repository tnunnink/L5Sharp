using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagCommentTests
    {
        private const string InitialDescription = "This is the initial description";
        
        [Test]
        public void Atomic_Null_ShouldBeExpected()
        {
            var tag = Tag.Create<Dint>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            tag.Comment(null!);

            tag.Description.Should().BeEmpty();
        }
        
        [Test]
        public void Atomic_Empty_ShouldBeExpected()
        {
            var tag = Tag.Create<Dint>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            tag.Comment(string.Empty);

            tag.Description.Should().BeEmpty();
        }

        [Test]
        public void Atomic_ValidComment_ShouldBeExpected()
        {
            var tag = Tag.Create<Dint>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            tag.Comment("This is a test description");

            tag.Description.Should().Be("This is a test description");
        }

        [Test]
        public void Predefined_Null_ShouldBeExpected()
        {
            var tag = Tag.Create<Timer>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            tag.Comment(null!);

            tag.Description.Should().BeEmpty();
        }
        
        [Test]
        public void Predefined_Empty_ShouldBeExpected()
        {
            var tag = Tag.Create<Timer>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            tag.Comment(string.Empty);

            tag.Description.Should().BeEmpty();
        }

        [Test]
        public void Predefined_ValidComment_ShouldBeExpected()
        {
            var tag = Tag.Create<Timer>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            tag.Comment("This is a test description");

            tag.Description.Should().Be("This is a test description");
        }
        
        /*[Test]
        public void SetDescription_String_ShouldUpdateAllElementDescriptionValues()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();
            tag.GetMembers().Select(e => e.Description).Should().AllBeEquivalentTo<string>(null);

            tag.Comment("This is a test");
            tag.Description.Should().Be("This is a test");
            tag.GetMembers().Select(e => e.Description).Should().AllBeEquivalentTo("This is a test");
        }

        [Test]
        public void SetElementDescription_ThenSetTagDescription_TagMemberShouldStillHaveOverridenDescription()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[0];
            element.Comment("Element Description");
            element.Description.Should().Be("Element Description");

            tag.Comment("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");
        }

        [Test]
        public void SetElementDescription_ThenSetTagDescription_ThenGetTheTagMemberAgain_ItShouldRetainTheDescription()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[0];
            element.Comment("Element Description");
            element.Description.Should().Be("Element Description");

            tag.Comment("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");

            element = tag[0];
            element.Description.Should().Be("Element Description");
        }

        [Test]
        public void
            SetElementDescription_ThenSetTagDescription_ThenResetElementDescriptionBack_ItShouldRevertToTagDescription()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            var element = tag[0];
            element.Comment("Element Description");
            element.Description.Should().Be("Element Description");

            tag.Comment("Tag Description");
            tag.Description.Should().Be("Tag Description");
            element.Description.Should().Be("Element Description");

            element.Comment(string.Empty);
            element.Description.Should().Be("Tag Description");
        }

        [Test]
        public void SetElement_Description_ShouldBeExpected()
        {
            var tag = Tag.Build("Test", new Bool()).WithDimensions(5).Create();

            tag[0].Comment("This is a test");

            tag[0].Description.Should().Be("This is a test");
            tag[1].Description.Should().BeNull();
            tag[2].Description.Should().BeNull();
            tag[3].Description.Should().BeNull();
            tag[4].Description.Should().BeNull();
        }*/
    }
}