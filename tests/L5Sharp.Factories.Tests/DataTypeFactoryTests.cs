using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Factories.Tests
{
    [TestFixture]
    public class DataTypeFactoryTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private XDocument _document;
        private DataTypeFactory _factory;

        [SetUp]
        public void Setup()
        {
            _document = XDocument.Load(_fileName);
            
            _factory = new DataTypeFactory(null);
        }
        
        [Test]
       public void Create_WhenCalled_ResultsShouldNotBeNull()
       {
           var element = _document.Descendants(LogixNames.GetComponentName<IDataType>()).FirstOrDefault();
           
           var result = _factory.Create(element);

           result.Should().NotBeNull();
       }
       
       [Test]
       public void Create_ArrayType_ResultShouldExpectedProperties()
       {
           var element = _document.Descendants(LogixNames.GetComponentName<IUserDefined>())
               .FirstOrDefault(x => x.Attribute("Name")?.Value == "ArrayType");
           
           var result = _factory.Create(element);

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
    }
}