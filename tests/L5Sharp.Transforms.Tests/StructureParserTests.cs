using NUnit.Framework;

namespace L5Sharp.Transforms.Tests
{
    [TestFixture]
    public class StructureParserTests
    {
        /*private XElement _element;

        [SetUp]
        public void Setup()
        {
            _element = new XElement("Structure", new XAttribute("DataType", "Atomics"));
            _element.Add(new XElement("DataValueMember",
                new XAttribute("Name", "Member"),
                new XAttribute("DataType", "BOOL"),
                new XAttribute("Value", "true")));
            var array = new XElement("ArrayMember",
                new XAttribute("Name", "INTArray"),
                new XAttribute("DataType", "INT"),
                new XAttribute("Dimensions", "1"),
                new XAttribute("Radix", "Decimal"));
            array.Add(new XElement("Element",
                    new XAttribute("Index", "[0]"),
                    new XAttribute("Value", "23")));
            _element.Add(array);
        }

        [Test]
        public void Parse_ValidXml_ShouldUpdateParentAsExpected()
        {
            var tag = new Tag("Test", new DataType("Atomics"));
            var parser = new StructureParser(tag);

            parser.Parse(_element);

            tag.Tags.Should().HaveCount(2);
            
            var first = tag.Tags.First();
            first.Name.Should().Be("Member");
            first.DataType.Should().Be(DataType.Bool);
            first.Radix.Should().Be(Radix.Decimal);
            first.Value.Should().Be(true);
            
            var second = tag.Tags.Single(t => t.Name == "INTArray");
            second.Name.Should().Be("INTArray");
            second.DataType.Should().Be(DataType.Int);
            second.Radix.Should().Be(Radix.Decimal);
            second.Dimensions.Length.Should().Be(1);
            second.Value.Should().Be(null);
            second.Tags.Should().HaveCount(1);
        }*/
    }
}