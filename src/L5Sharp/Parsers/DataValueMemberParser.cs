using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class DataValueMemberParser : IElementParser
    {
        private readonly Tag _parent;

        public DataValueMemberParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            var name = element.Attribute(nameof(_parent.Name))?.Value;
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(_parent.DataType))?.Value);
            var radix = Radix.FromName(element.Attribute(nameof(_parent.Radix))?.Value);
            var value = element.Attribute(nameof(_parent.Value))?.Value;

            var tag = new Tag(_parent, name, dataType, radix: radix, value: value);
            _parent.AddTag(tag);
        }
    }
}