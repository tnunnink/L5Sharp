using System;
using System.IO;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp;
using NUnit.Framework;

namespace L5SharpTests
{
    [TestFixture]
    public class L5XTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private readonly string _masterTemplate = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Gen5.xml");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_masterTemplate);
        }

        [Test]
        public void New_ValidFile_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            doc.Should().NotBeNull();
        }
        
        [Test]
        public void DataTypes_Get_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var dataTypes = doc.DataTypes;

            dataTypes.Should().NotBeNull();
        }
        
        [Test]
        public void DataTypes_Get_NameShouldBeDataTypes()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var dataTypes = doc.DataTypes;
            
            dataTypes.Name.ToString().Should().Be("DataTypes");
        }
        
        [Test]
        public void Modules_Get_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var component = doc.Modules;

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Instructions_Get_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var component = doc.Instructions;

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Tags_Get_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var component = doc.Tags;

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Programs_Get_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var component = doc.Programs;

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Tasks_Get_ShouldNotBeNull()
        {
            var doc = new L5X(XDocument.Load(_masterTemplate));

            var component = doc.Tasks;

            component.Should().NotBeNull();
        }
    }
}