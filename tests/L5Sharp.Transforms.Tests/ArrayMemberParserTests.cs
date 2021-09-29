using NUnit.Framework;

namespace L5Sharp.Transforms.Tests
{
    [TestFixture]
    public class ArrayMemberParserTests
    {
        /*private XElement _element;

        [SetUp]
        public void Setup()
        {
            _element = new XElement("ArrayMember");
            _element.Add(new XAttribute("Name", "MemberArray"));
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
            var parser = new ArrayMemberParser(tag);

            parser.Parse(_element);

            tag.Tags.Should().HaveCount(1);
            var member = tag.Tags.First();
            member.Name.Should().Be("MemberArray");
            member.Tags.Should().HaveCount(10);
            member.DataType.Should().Be(DataType.Int);
            member.Dimensions.Length.Should().Be(10);
            member.Radix.Should().Be(Radix.Decimal);
            member.Value.Should().Be(null);
        }*/
    }
}