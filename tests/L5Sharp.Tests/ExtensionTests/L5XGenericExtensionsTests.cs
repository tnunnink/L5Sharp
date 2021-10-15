using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Tests.ExtensionTests
{
    [TestFixture]
    public class L5XGenericExtensionsTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }

        [Test]
        public void GetValue_WhenCalled_ShouldReturnExpectedValue()
        {
            var element = XElement.Load(_fileName);
            var target = element.Descendants("DataType").FirstOrDefault();

            var value = target.GetValue<IDataType, DataTypeClass>(t => t.Class, v => DataTypeClass.FromName(v));

            value.Should().NotBeNull();
        }
        
        [Test]
        public void GetFirst_WhenCalled_ShouldBeExpected()
        {
            var element = XElement.Load(_fileName);

            var type = element.Descendants("DataTypes").Single().Nodes().FirstOrDefault();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void ToXAttribute_ValidComponent_ShouldHaveExpectedNameAndValue()
        {
            var component = new DataType("Test");

            var attribute = component.ToXAttribute(c => c.Name);

            attribute.Should().NotBeNull();
            attribute.Name.ToString().Should().Be("Name");
            attribute.Should().HaveValue("Test");
        }
    }
}