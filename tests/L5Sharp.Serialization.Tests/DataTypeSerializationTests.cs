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
    public class DataTypeSerializationTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\DataTypes.xml");
        private DataTypeSerializer _serializer;
        private XDocument _document;

        [SetUp]
        public void Setup()
        {
            _document = XDocument.Load(_fileName);
            _serializer = new DataTypeSerializer();
        }

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }

        [Test]
        public void Deserialize_WhenCalled_ResultsShouldNotBeNull()
        {
            var element = _document.Descendants(L5XNames.Components.DataType).FirstOrDefault();
            
            var result = _serializer.Deserialize(element);

            result.Should().NotBeNull();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var type = new DataType("TypeName");
            
            var element = _serializer.Serialize(type);

            element.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialise_WhenCalled_ShouldHaveApprovedOutput()
        {
            var type = new DataType("TypeName", "Description of the type");
            
            var element = _serializer.Serialize(type);

            Approvals.VerifyXml(element.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialise_WithMembers_ShouldHaveApprovedOutput()
        {
            var type = new DataType("TypeName", "Description of the type");
            type.AddMember("Member_Bool", DataType.Bool);
            type.AddMember("Member_Sint", DataType.Sint);
            type.AddMember("Member_Int", DataType.Int);
            type.AddMember("Member_Dint", DataType.Dint);
            type.AddMember("Member_Lint", DataType.Lint);
            type.AddMember("Member_Real", DataType.Real);
            
            var element = _serializer.Serialize(type);

            Approvals.VerifyXml(element.ToString());
        }
        
    }
}