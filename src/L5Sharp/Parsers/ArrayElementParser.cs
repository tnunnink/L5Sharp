using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class ArrayElementParser : IElementParser
    {
        private readonly Tag _parent;

        public ArrayElementParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value); //todo validate
            var dimension = Dimensions.Parse(element.Attribute(nameof(Dimensions))?.Value); //todo validate
            var radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value); //todo validate

            var indices = element.Descendants("Element");
            
            foreach (var index in indices)
            {
                var name = $"{_parent.Name}{index.Attribute("Index")?.Value}";
                var value = index.Attribute("Value")?.Value;
                
                var tag = new Tag(_parent, name, dataType, value: value);
                _parent.AddTag(tag);

                if (!index.HasElements || index.Elements().First().Name != "Structure") continue;
                var parser = new StructureParser(tag);
                parser.Parse(index.Elements().First());
            }
        }
    }
}