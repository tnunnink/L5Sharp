using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class CommentSerializerTests
    {
        private CommentSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new CommentSerializer("TagName");
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var comments = new Comments();
            comments.Set(new TagName("TagName.MemberName"), "this is a test comment");

            var xml = _serializer.Serialize(comments);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var comments = new Comments();
            comments.Set(new TagName("TagName.MemberName"), "this is a test comment");

            var xml = _serializer.Serialize(comments);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_InvalidElementName_ShouldThrowArgumentException()
        {
            const string xml = @"<Invalid></Invalid>";
            var element = XElement.Parse(xml);

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_Valid_ShouldNotBeNull()
        {
            const string xml =  @"<Comments>
                <Comment Operand="".0"">
                <![CDATA[This is a test]]>
                </Comment>
                </Comments>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBool_ShouldHaveExpectedProperties()
        {
            const string xml =  @"<Comments>
                <Comment Operand="".0"">
                <![CDATA[This is a test]]>
                </Comment>
                </Comments>";

            var element = XElement.Parse(xml);

            var component = _serializer.Deserialize(element);

            var comment = component.Get("TagName.0");
            comment.Should().NotBeNull();
            comment.Should().Be("This is a test");
        }
    }
}