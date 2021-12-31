using System;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;
using NUnit.Framework;

namespace L5Sharp.Extensions.Tests
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

            var type = element.GetDataType();

            type.Should().BeOfType<Undefined>();
        }

        [Test]
        public void GetDataType_ElementWithValidType_ShouldBeExpectedType()
        {
            var element = new XElement("Test", new XAttribute("DataType", "BOOL"));

            var type = element.GetDataType();

            type.Should().BeOfType<Bool>();
        }

        [Test]
        public void GetValue_NonPropertyExpression_ShouldThrowInvalidOperationException()
        {
            var element = new XElement("Test", new XAttribute("Radix", "Decimal"));

            FluentActions.Invoking(() => element.GetValue<IMember<IDataType>, object>(m => m[0])).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void GetValue_PropertyDoesNotExists_ShouldBeNull()
        {
            var element = new XElement("Test", new XAttribute("Radix", "Decimal"));

            var externalAccess = element.GetValue<IMember<IDataType>, ExternalAccess>(m => m.ExternalAccess);

            externalAccess.Should().BeNull();
        }

        [Test]
        public void GetValue_PropertyExists_ShouldBeExpectedValue()
        {
            var element = new XElement("Test", new XAttribute("Radix", "Decimal"));

            var radix = element.GetValue<IMember<IDataType>, Radix>(m => m.Radix);

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Decimal);
        }
        
        [Test]
        public void Add_NullInstance_ShouldThrowArgumentNullException()
        {
            var element = new XElement("Test");

            FluentActions.Invoking(() => element.AddValue<string, int>(null!, c => c.Length))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Add_NonMemberExpression_ShouldThrowArgumentException()
        {
            var element = new XElement("Test");
            const string instance = "This is a string";

            FluentActions.Invoking(() => element.AddValue(instance, c => c.Substring(0, 1)))
                .Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Add_EmptyValue_ShouldThrowArgumentException()
        {
            var element = new XElement("Test");
            var member = Member.Create<Int>("Test");
            
            element.AddValue(member, m => m.Description);

            element.Attributes().Should().HaveCount(0);
            element.Elements().Should().HaveCount(0);
        }

        [Test]
        public void Add_ValidMember_ShouldHaveExpectedNameAndValue()
        {
            var element = new XElement("Test");
            var dt = DateTime.UnixEpoch;

            element.AddValue(dt, t => t.Year);

            element.Attribute("Year").Should().NotBeNull();
            element.Attribute("Year")?.Value.Should().Be("1970");
        }
        
        [Test]
        public void Add_ValidMemberIsElementOverride_ShouldHaveExpectedNameAndValue()
        {
            var element = new XElement("Test");
            var dt = DateTime.UnixEpoch;

            element.AddValue(dt, t => t.Year, true);

            element.Element("Year").Should().NotBeNull();
            element.Element("Year")?.Value.Should().Be("1970");
        }
        
        [Test]
        public void Add_ValidMemberNameOverride_ShouldHaveExpectedNameAndValue()
        {
            var element = new XElement("Test");
            var dt = DateTime.UnixEpoch;

            element.AddValue(dt, t => t.Year, nameOverride: "CurrentYear");

            element.Attribute("CurrentYear").Should().NotBeNull();
            element.Attribute("CurrentYear")?.Value.Should().Be("1970");
        }
    }
}