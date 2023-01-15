using System;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Extensions;
using NUnit.Framework;

namespace L5Sharp.Tests.Extensions
{
    [TestFixture]
    public class ElementExtensionsTests
    {
        [Test]
        public void GetComponentName_NoNameElement_ShouldThrowInvalidOperationException()
        {
            var element = new XElement("Test", new XAttribute("Property", "TestName"));

            FluentActions.Invoking(() => element.ComponentName()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetComponentName_ValidElement_ShouldBeExpectedName()
        {
            var element = new XElement("Test", new XAttribute("Name", "TestName"));

            var name = element.ComponentName();

            name.Should().Be("TestName");
        }

        [Test]
        public void GetDataType_ElementWithUnknowableType_ShouldBeUndefined()
        {
            var element = new XElement("Test", new XAttribute("DataType", "Invalid"));

            var type = element.DataTypeName();

            type.Should().Be("Invalid");
        }

        [Test]
        public void GetDataType_ElementWithAtomicType_ShouldBeExpectedType()
        {
            var element = new XElement("Test", new XAttribute("DataType", "BOOL"));

            var type = element.DataTypeName();

            type.Should().Be("BOOL");
        }
    }
}