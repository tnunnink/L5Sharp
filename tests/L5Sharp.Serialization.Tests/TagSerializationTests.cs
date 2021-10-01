using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Primitives;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TagSerializationTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }
        
        /*[Test]
        public void Serialize_Tag_ShouldNotBeNull()
        {
            var tag = new Tag("TestTag", DataType.Counter);

            var element = tag.Serialize();

            element.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Tag_ShouldHaveApprovedOutput()
        {
            var tag = new Tag("TestTag", DataType.Counter);

            var element = tag.Serialize();

            Approvals.VerifyXml(element.ToString());
        }*/
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Materialize_Tag_ShouldHaveApprovedOutput()
        {
            var element = XDocument.Load(_fileName).Descendants(nameof(Tag))
                .FirstOrDefault(x => x.Attribute(L5XNames.Name)?.Value == "array");

            var tag = Tag.Materialize(element, new Controller("Test"));
            tag.Should().NotBeNull();
            tag.Name.Should().Be("array");
            tag.DataType.Should().Be(DataType.Dint);

            var result = tag.Serialize();
            Approvals.VerifyXml(result.ToString());
        }
    }
}