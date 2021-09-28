using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class ArrayMemberParser : IElementParser
    {
        private readonly Tag _parent;

        public ArrayMemberParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            var name = element.Attribute(nameof(_parent.Name))?.Value;
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value);
            var dimensions = Dimensions.Parse(element.Attribute(nameof(Dimensions))?.Value);
            var radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);

            var tag = new Tag(_parent, name, dataType, dimensions, radix);

            var parser = new ArrayElementParser(tag);
            parser.Parse(element);
        }
    }
}