using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Creators;
using L5Sharp.Predefined;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagCommentTests
    {
        private const string InitialDescription = "This is the initial description";
        
        [Test]
        public void Comment_Null_ShouldShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<Dint>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            FluentActions.Invoking(() => tag.Comment(null!)).Should().Throw<ArgumentNullException>();
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
        public void Predefined_Null_ShouldThrowArgumentNullException()
        {
            var tag = Tag.Create<Timer>("Test", description: InitialDescription);
            
            tag.Description.Should().Be(InitialDescription);

            FluentActions.Invoking(() => tag.Comment(null!)).Should().Throw<ArgumentNullException>();
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

        [Test]
        public void ArrayOfAtomic_ValidComment_ShouldPropagate()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            tag.Comment("This is a test");
            tag.Description.Should().Be("This is a test");
            tag.Members().Select(e => e.Description).Should().AllBeEquivalentTo("This is a test");
        }
        
        [Test]
        public void ArrayOfPredefined_ValidComment_ShouldPropagate()
        {
            var tag = Tag.Create<Timer>("Test", new Dimensions(5));

            tag.Comment("This is a test");
            tag.Description.Should().Be("This is a test");
            tag.Members().Select(e => e.Description).Should().AllBeEquivalentTo("This is a test");
        }

        [Test]
        public void ArrayOfAtomic_SetSingleElement_ShouldBeExpected()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            tag[0].Comment("This is a test");

            tag[0].Description.Should().Be("This is a test");
            tag[1].Description.Should().BeEmpty();
            tag[2].Description.Should().BeEmpty();
            tag[3].Description.Should().BeEmpty();
            tag[4].Description.Should().BeEmpty();
        }

        [Test]
        public void ArrayOfAtomic_SetElementThenTagComment_TagMemberShouldStillHaveOverridenDescription()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            var element = tag[0];
            element.Comment("Element Description");
            element.Description.Should().Be("Element Description");

            tag.Comment("Tag Description");
            tag.Description.Should().Be("Tag Description");

            element.Description.Should().Be("Element Description");
        }

        [Test]
        public void ArrayOfAtomic_SetElementDescription_ElementShouldRetainCommentAfterGettingNewInstance()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            var e1 = tag[0];
            e1.Comment("Element Description");
            e1.Description.Should().Be("Element Description");

            var e2 = tag[0];
            e2.Description.Should().Be("Element Description");
        }

        [Test]
        public void ArrayOfAtomic_ResettingElementDescription_ShouldRevertToParentTagDescription()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            var element = tag[0];
            element.Comment("Element Description");
            element.Description.Should().Be("Element Description");

            tag.Comment("Tag Description");
            tag.Description.Should().Be("Tag Description");
            element.Description.Should().Be("Element Description");

            element.Comment(string.Empty);
            element.Description.Should().Be("Tag Description");
        }
    }
}