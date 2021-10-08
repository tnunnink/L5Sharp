using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class DataTypeSerializationTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
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
        public void Deserialize_ArrayType_ResultShouldExpectedProperties()
        {
            var element = _document.Descendants(L5XNames.Components.DataType)
                .FirstOrDefault(x => x.Attribute("Name")?.Value == "ArrayType");
            
            var result = _serializer.Deserialize(element);

            result.Should().NotBeNull();
            result.Name.Should().Be("ArrayType");
            result.Class.Should().Be(DataTypeClass.User);
            result.Family.Should().Be(DataTypeFamily.None);
            result.Members.Should().NotBeEmpty();
            result.Members.Should().Contain(m => m.Name == "BoolArray");
            result.Members.Should().Contain(m => m.Name == "SintArray");
            result.Members.Should().Contain(m => m.Name == "IntArray");
            result.Members.Should().Contain(m => m.Name == "DintArray");
            result.Members.Should().Contain(m => m.Name == "LintArray");
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
            type.AddMember("Member_Bool", Predefined.Bool);
            type.AddMember("Member_Sint", Predefined.Sint);
            type.AddMember("Member_Int", Predefined.Int);
            type.AddMember("Member_Dint", Predefined.Dint);
            type.AddMember("Member_Lint", Predefined.Lint);
            type.AddMember("Member_Real", Predefined.Real);
            
            var element = _serializer.Serialize(type);

            Approvals.VerifyXml(element.ToString());
        }
        
    }
}