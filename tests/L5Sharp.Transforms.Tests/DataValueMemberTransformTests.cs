using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Loaders;
using NUnit.Framework;

namespace L5Sharp.Transforms.Tests
{
    public class DataValueMemberTransformTests
    {
        private XElement _element;
        private static string _fileName;

        [SetUp]
        public void Setup()
        {
            
            _fileName = Path.Combine(Environment.CurrentDirectory, @"TestData\Test.xml");
            _element = new XElement("DataValueMember");
            _element.Add(new XAttribute("Name", "MemberName"));
            _element.Add(new XAttribute("DataType", "REAL"));
            _element.Add(new XAttribute("Radix", "Float"));
            _element.Add(new XAttribute("Value", "34.76"));
        }

        [Test]
        public void TestDataFileExists()
        {
            FileAssert.Exists(_fileName);
        }

        [Test]
        public void Perform_SimpleXElement_ResultShouldHaveExpectedAttributes()
        {
            var transform = new DataValueMemberTransform();

            var result = transform.Normalize(_element);

            result.Name.ToString().Should().Be("TagMember");
            result.Should().HaveAttribute("Name", "MemberName");
            result.Should().HaveAttribute("DataType", "REAL");
            result.Should().HaveAttribute("Radix", "Float");
            result.Should().HaveAttribute("Value", "34.76");
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Perform_TestFile_ResultShouldHaveApprovedOutput()
        {
            var doc = XDocument.Load(_fileName);
            var element = doc.Descendants("Tag").FirstOrDefault(t => t.Attribute("Name")?.Value == "DINT")?
                .Descendants("DataValueMember").FirstOrDefault();
            var transform = new DataValueMemberTransform();

            var result = transform.Normalize(element);
            
            Approvals.VerifyXml(result.ToString());
        }
    }
}