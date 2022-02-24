using System;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Factories;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Extensions
{
    [TestFixture]
    public class ElementExtensionsTests
    {
        [Test]
        public void GetComponentName_NoNameElement_ShouldThrowInvalidOperationException()
        {
            var element = new XElement("Test", new XAttribute("Property", "TestName"));

            FluentActions.Invoking(() => element.GetComponentName()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetComponentName_ValidElement_ShouldBeExpectedName()
        {
            var element = new XElement("Test", new XAttribute("Name", "TestName"));

            var name = element.GetComponentName();

            name.Should().Be("TestName");
        }

        [Test]
        public void GetDataType_ElementWithUnknowableType_ShouldBeUndefined()
        {
            var element = new XElement("Test", new XAttribute("DataType", "Invalid"));

            var type = element.GetDataTypeName();

            type.Should().Be("Invalid");
        }

        [Test]
        public void GetDataType_ElementWithAtomicType_ShouldBeExpectedType()
        {
            var element = new XElement("Test", new XAttribute("DataType", "BOOL"));

            var type = element.GetDataTypeName();

            type.Should().Be("BOOL");
        }

        [Test]
        public void GetValue_PropertyDoesNotExists_ShouldBeNull()
        {
            var element = new XElement("Test", new XAttribute("Radix", "Decimal"));

            var externalAccess = element.GetAttribute<IMember<IDataType>, ExternalAccess>(m => m.ExternalAccess);

            externalAccess.Should().BeNull();
        }

        [Test]
        public void GetValue_PropertyExists_ShouldBeExpectedValue()
        {
            var element = new XElement("Test", new XAttribute("Radix", "Decimal"));

            var radix = element.GetAttribute<IMember<IDataType>, Radix>(m => m.Radix);

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void AddAttribute_NullInstance_ShouldThrowArgumentNullException()
        {
            var element = new XElement("Test");

            FluentActions.Invoking(() => element.AddAttribute<string, int>(null!, c => c.Length))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void AddAttribute_NonMemberExpression_ShouldThrowArgumentException()
        {
            var element = new XElement("Test");
            const string instance = "This is a string";

            FluentActions.Invoking(() => element.AddAttribute(instance, c => c.Substring(0, 1)))
                .Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void AddAttribute_EmptyValue_ShouldThrowArgumentException()
        {
            var element = new XElement("Test");
            var member = Member.Create<Int>("Test");
            
            element.AddAttribute(member, m => m.Description);

            element.Attributes().Should().HaveCount(1);
            element.Elements().Should().HaveCount(0);
        }

        [Test]
        public void AddAttribute_ValidMember_ShouldHaveExpectedNameAndValue()
        {
            var element = new XElement("Test");
            var dt = DateTime.UnixEpoch;

            element.AddAttribute(dt, t => t.Year);

            element.Attribute("Year").Should().NotBeNull();
            element.Attribute("Year")?.Value.Should().Be("1970");
        }
        
        [Test]
        public void AddElement_ValidMember_ShouldHaveExpectedNameAndValue()
        {
            var element = new XElement("Test");
            var dt = DateTime.UnixEpoch;

            element.AddElement(dt, t => t.Year);

            element.Element("Year").Should().NotBeNull();
            element.Element("Year")?.Value.Should().Be("1970");
        }
        
        [Test]
        public void AddAttribute_ValidMemberNameOverride_ShouldHaveExpectedNameAndValue()
        {
            var element = new XElement("Test");
            var dt = DateTime.UnixEpoch;

            element.AddAttribute(dt, t => t.Year, nameOverride: "CurrentYear");

            element.Attribute("CurrentYear").Should().NotBeNull();
            element.Attribute("CurrentYear")?.Value.Should().Be("1970");
        }
    }
}