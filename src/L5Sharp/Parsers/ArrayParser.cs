using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class ArrayParser : IElementParser
    {
        private const string BaseElementName = "Array";
        private const string ArrayElementName = "Element";
        private const string ArrayIndexName = "Element";
        private const string StructureName = "Structure";
        private readonly Tag _parent;

        public ArrayParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            if (!element.Name.ToString().Contains(BaseElementName))
                throw new InvalidOperationException("Element name does not contain expected value Array ");
            
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value);
            if (!Equals(dataType, _parent.DataType))
                throw new InvalidOperationException("Array type does not match parent type");
            
            var dimension = Dimensions.Parse(element.Attribute(nameof(Dimensions))?.Value);
            if (!Equals(dimension, _parent.Dimensions))
                throw new InvalidOperationException("Array dimensions does not match parent dimensions");
            
            var radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            if (!Equals(radix, _parent.Radix))
                throw new InvalidOperationException("Array radix does not match parent radix");
            
            var indices = element.Descendants(ArrayElementName);
            
            foreach (var index in indices)
            {
                var name = index.Attribute(ArrayIndexName)?.Value;
                
                var value = dataType is Predefined predefined && index.Attribute(nameof(_parent.Value)) != null
                    ? predefined.ParseValue(index.Attribute(nameof(_parent.Value))?.Value)
                    : null;

                var tag = new Tag(_parent, name, dataType, value: value);
                _parent.AddTag(tag);

                if (!index.HasElements || index.Elements().First().Name != StructureName) continue;
                var parser = new StructureParser(tag);
                parser.Parse(index.Elements().First());
            }
        }
    }
}