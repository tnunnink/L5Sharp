using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Parsers.Tests
{
    [TestFixture]
    public class DataValueParserTests
    {
        private XElement _element;

        [SetUp]
        public void Setup()
        {
            _element = new XElement("DataValue");
            _element.Add(new XAttribute("DataType", "DINT"));
            _element.Add(new XAttribute("Radix", "Decimal"));
            _element.Add(new XAttribute("Value", "32"));
        }
        
        [Test]
        public void Parse_ValidXml_ShouldUpdateParentAsExpected()
        {
            var tag = new Tag("Test", DataType.Dint);
            var parser = new DataValueParser(tag);

            parser.Parse(_element);

            tag.Value.Should().Be(32);
        }
    }
}