using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class StructureMemberParser
    {
        private readonly Tag _parent;

        public StructureMemberParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            var name = element.Attribute(nameof(_parent.Name))?.Value;
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(_parent.DataType))?.Value);

            var tag = new Tag(_parent, name, dataType);
            _parent.AddTag(tag);

            var parser = new StructureParser(tag);
            parser.Parse(element);
        }
    }
}