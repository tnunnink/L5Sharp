using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class CommentsTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var comments = new Comments();

            comments.Should().NotBeNull();
        }

        [Test]
        public void New_Overloaded_ShouldContainedExpected()
        {
            var collection = new List<KeyValuePair<TagName, string>>()
            {
                new("SomeTag.Member1", "This is a test comment #1"),
                new("SomeTag.Member2", "This is a test comment #2"),
                new("SomeTag.Member3", "This is a test comment #3"),
            };

            var comments = new Comments(collection);

            comments.Should().HaveCount(3);
        }

        [Test]
        public void Enumerate_WhenPerformed_ShouldNotBeNull()
        {
            var comments = new Comments(GetComments());

            foreach (var comment in comments)
            {
                comment.Should().NotBeNull();
            }
        }

        [Test]
        public void GetEnumerator_AsEnumerable_ShouldNotBeNull()
        {
            var comments = (IEnumerable)new Comments(GetComments());

            var enumerator = comments.GetEnumerator();

            enumerator.Should().NotBeNull();
        }

        [Test]
        public void Get_Null_ShouldThrowArgumentNullException()
        {
            var comments = new Comments(GetComments());

            FluentActions.Invoking(() => comments.Get(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Get_NonExisting_ShouldBeEmpty()
        {
            var comments = new Comments(GetComments());

            var comment = comments.Get("TagName.Member");

            comment.Should().BeEmpty();
        }
        
        [Test]
        public void Get_ExistingMember_ShouldBeExpected()
        {
            var comments = new Comments(GetComments());

            var comment = comments.Get("SomeTag.Member1");

            comment.Should().NotBeNull();
            comment.Should().Be("This is a test comment #1");
        }
        
        [Test]
        public void ContainsComment_Null_ShouldBeFalse()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsValue(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsComment_Empty_ShouldBeFalse()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsValue(string.Empty);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ContainsComment_DoesNotExist_ShouldBeFalse()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsValue("This is a test comment");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsComment_Exists_ShouldBeTrue()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsValue("This is a test comment #1");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsTag_Null_ShouldBeFalse()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsTag(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsTag_DoesNotExist_ShouldBeFalse()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsTag("TagName.Member");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsTag_Exists_ShouldBeTrue()
        {
            var comments = new Comments(GetComments());

            var result = comments.ContainsTag("SomeTag.Member1");

            result.Should().BeTrue();
        }

        [Test]
        public void Apply_NullTagName_ShouldThrowArgumentNullException()
        {
            var comments = new Comments(GetComments());
            
            FluentActions.Invoking(() => comments.Apply(null!, "This is a test")).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Apply_NullComment_ShouldNoLongerContainTagName()
        {
            var comments = new Comments(GetComments());

            comments.Apply("SomeTag.Member2", null);

            comments.Should().HaveCount(2);
            comments.Should().NotContain(c => c.Key == "SomeTag.Member2");
        }

        [Test]
        public void Apply_EmptyComment_ShouldNotLongerContainTagName()
        {
            var comments = new Comments(GetComments());
            
            comments.Apply("SomeTag.Member3", string.Empty);

            comments.Should().HaveCount(2);
            comments.Should().NotContain(c => c.Key == "SomeTag.Member3");
        }

        [Test]
        public void Apply_NewTagName_ShouldHaveExpectedCount()
        {
            var comments = new Comments(GetComments());
            
            comments.Apply("SomeTag.Member", "This is a test");

            comments.Should().HaveCount(4);
        }

        [Test]
        public void Apply_ExistingTagName_ShouldUpdateExistingValue()
        {
            var comments = new Comments(GetComments());
            
            comments.Apply("SomeTag.Member1", "This is an updated comment");

            comments.Should().HaveCount(3);
            comments.Get("SomeTag.Member1").Should().Be("This is an updated comment");
        }

        [Test]
        public void Reset_NullTagNAme_ShouldThrowArgumentNullException()
        {
            var comments = new Comments(GetComments());

            FluentActions.Invoking(() => comments.Reset(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Reset_NonExistingName_ShouldBeFalse()
        {
            var comments = new Comments(GetComments());

            var removed = comments.Reset("TagName.Member");

            removed.Should().BeFalse();
        }

        [Test]
        public void Reset_ValidName_ShouldBeTrueAndNoLongerExist()
        {
            var comments = new Comments(GetComments());

            var removed = comments.Reset("SomeTag.Member1");

            removed.Should().BeTrue();
            comments.Should().NotContain(c => c.Key == "SomeTag.Member1");
        }

        private static IEnumerable<KeyValuePair<TagName, string>> GetComments()
        {
            return new List<KeyValuePair<TagName, string>>
            {
                new("SomeTag.Member1", "This is a test comment #1"),
                new("SomeTag.Member2", "This is a test comment #2"),
                new("SomeTag.Member3", "This is a test comment #3"),
            };
        }
    }
}