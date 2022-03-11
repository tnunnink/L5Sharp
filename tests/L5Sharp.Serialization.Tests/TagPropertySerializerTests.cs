using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Serialization.Components;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TagPropertySerializerTests
    {
        private TagPropertySerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new TagPropertySerializer(L5XElement.Comment.ToString());
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var value = new KeyValuePair<TagName, string>(new TagName("TagName.MemberName"), "this is a test comment");

            var xml = _serializer.Serialize(value);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Bool_ShouldBeApproved()
        {
            var value = new KeyValuePair<TagName, string>(new TagName("TagName.MemberName"), "this is a test comment");

            var xml = _serializer.Serialize(value);

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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>()
                .WithMessage($"Element 'Invalid' not valid for the serializer {_serializer.GetType()}.");
        }

        [Test]
        public void Deserialize_Valid_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetSimpleCommentsTag()).Descendants("Comment").First();

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ValidBool_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetSimpleCommentsTag()).Descendants("Comment").First();

            var component = _serializer.Deserialize(element);
            
            component.Key.Should().Be("Test.0");
            component.Value.Should().Be("This is a test");
        }

        private static string GetSimpleCommentsTag()
        {
            return
                @"<Tag Name=""Test"" TagType=""Base"" DataType=""SINT"" Radix=""Hex"" Constant=""false"" ExternalAccess=""None"">
                <Comments>
                <Comment Operand="".0"">
                <![CDATA[This is a test]]>
                </Comment>
                </Comments>
                <Data Format=""L5K"">
                <![CDATA[5]]>
                </Data>
                <Data Format=""Decorated"">
                <DataValue DataType=""SINT"" Radix=""Hex"" Value=""16#05""/>
                </Data>
                </Tag>";
        }
    }
}