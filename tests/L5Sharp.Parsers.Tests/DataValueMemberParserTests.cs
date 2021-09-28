using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Parsers.Tests
{
    public class DataValueMemberParserTests
    {
        private XElement _element;

        [SetUp]
        public void Setup()
        {
            _element = new XElement("DataValueMember");
            _element.Add(new XAttribute("Name", "MemberName"));
            _element.Add(new XAttribute("DataType", "REAL"));
            _element.Add(new XAttribute("Radix", "Float"));
            _element.Add(new XAttribute("Value", "34.76"));
        }
        
        [Test]
        public void Parse_ValidXml_ShouldUpdateParentAsExpected()
        {
            var tag = new Tag("Test", DataType.Real);
            var parser = new DataValueMemberParser(tag);

            parser.Parse(_element);

            tag.Tags.Should().HaveCount(1);
            var member = tag.Tags.First();
            member.DataType.Should().Be(DataType.Real);
            member.Radix.Should().Be(Radix.Float);
            member.Value.Should().Be(34.76f);
        }
    }
}