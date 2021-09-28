using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class ArrayParser : IElementParser
    {
        private readonly Tag _parent;

        public ArrayParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value); //todo validate - should match parent
            var dimension = Dimensions.Parse(element.Attribute(nameof(Dimensions))?.Value); //todo validate
            var radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value); //todo validate

            var indices = element.Descendants("Element");
            
            foreach (var index in indices)
            {
                var name = index.Attribute("Index")?.Value;
                
                var value = dataType is Predefined predefined && index.Attribute(nameof(_parent.Value)) != null
                    ? predefined.ParseValue(index.Attribute(nameof(_parent.Value))?.Value)
                    : null;

                var tag = new Tag(_parent, name, dataType, value: value);
                _parent.AddTag(tag);

                if (!index.HasElements || index.Elements().First().Name != "Structure") continue;
                var parser = new StructureParser(tag);
                parser.Parse(index.Elements().First());
            }
        }
    }
}