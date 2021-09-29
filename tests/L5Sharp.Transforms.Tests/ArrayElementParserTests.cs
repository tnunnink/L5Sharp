using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Transforms.Tests
{
    [TestFixture]
    public class ArrayElementParserTests
    {
        /*private XElement _element;

        [SetUp]
        public void Setup()
        {
            _element = new XElement("Array");
            _element.Add(new XAttribute("DataType", "INT"));
            _element.Add(new XAttribute("Dimensions", "10"));
            _element.Add(new XAttribute("Radix", "Decimal"));

            for (var i = 1; i <= 10; i++)
                _element.Add(
                    new XElement("Element", new XAttribute("Index", $"[{i}]"), new XAttribute("Value", i)));
        }
        
        [Test]
        public void Parse_ValidXml_ShouldUpdateParentAsExpected()
        {
            var tag = new Tag("Test", DataType.Real, new Dimensions(10));
            var parser = new ArrayParser(tag);

            parser.Parse(_element);

            tag.Tags.Should().HaveCount(10);
            var member = tag.Tags.First();
            member.DataType.Should().Be(DataType.Int);
            member.Radix.Should().Be(Radix.Decimal);
            member.Value.Should().Be(1);
        }*/
    }
}