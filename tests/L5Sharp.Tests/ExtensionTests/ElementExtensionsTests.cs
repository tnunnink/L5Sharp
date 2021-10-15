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
    public class ElementExtensionsTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }

        [Test]
        public void Contains_DataTypeKnownType_ShouldNotBeNull()
        {
            
        }

        [Test]
        public void GetFirst_WhenCalled_ShouldBeExpected()
        {
            var element = XElement.Load(_fileName);

            var type = element.GetFirst<DataType>("ArrayType");

            type.Should().NotBeNull();
        }

        [Test]
        public void GetValue_WhenCalled_ShouldReturnExpectedValue()
        {
            var element = XElement.Load(_fileName);
            var target = element.Descendants("DataType").FirstOrDefault();

            var value = target.GetValue<IDataType>(t => t.Class);

            value.Should().NotBeNull();
        }

        [Test]
        public void ToAttribute_ValidComponent_ShouldHaveExpectedNameAndValue()
        {
            var component = new DataType("Test");

            var attribute = component.ToAttribute(c => c.Name);

            attribute.Should().NotBeNull();
            attribute.Name.ToString().Should().Be("Name");
            attribute.Should().HaveValue("Test");
        }
    }
}