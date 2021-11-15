using System;
using System.IO;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TagSerializationTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private TagSerializer _serializer;
        private XDocument _document;

        [SetUp]
        public void Setup()
        {
            _document = XDocument.Load(_fileName);
            _serializer = new TagSerializer();
        }

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }
        
        /*[Test]
        public void Deserialize_SimpleBool_ResultsShouldNotBeNull()
        {
            var element = _document.Descendants(LogixNames.Components.Tag)
                .FirstOrDefault(x => x.Attribute("Name")?.Value == "SimpleBool");
            
            var result = _serializer.Deserialize(element);

            result.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_TestAlarmTag_ResultsShouldNotBeNull()
        {
            var element = _document.Descendants(LogixNames.Components.Tag)
                .FirstOrDefault(x => x.Attribute("Name")?.Value == "TestAlarmTag");
            
            var result = _serializer.Deserialize(element);

            result.Should().NotBeNull();
        }*/
        
        [Test]
        public void Serialize_Tag_ShouldNotBeNull()
        {
            var tag = Tag.Create<Counter>("TestTag");

            var element = tag.Serialize();

            element.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Tag_ShouldHaveApprovedOutput()
        {
            var tag = Tag.Create("TestTag", new Counter());

            var element = tag.Serialize();

            Approvals.VerifyXml(element.ToString());
        }
        
        /*[Test]
        [UseReporter(typeof(DiffReporter))]
        public void Materialize_Tag_ShouldHaveApprovedOutput()
        {
            var element = XDocument.Load(_fileName).Descendants(nameof(Tag))
                .FirstOrDefault(x => x.Attribute(L5XNames.Name)?.Value == "array");

            var tag = Tag.Materialize(element, new Controller("Test"));
            tag.Should().NotBeNull();
            tag.Name.Should().Be("array");
            tag.Logix.DataType.Should().Be(Logix.DataType.Dint);

            var result = tag.Serialize();
            Approvals.VerifyXml(result.ToString());
        }*/
    }
}