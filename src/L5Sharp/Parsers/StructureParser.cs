using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class StructureParser : IElementParser
    {
        private readonly Dictionary<string, Func<Tag, IElementParser>> _parsers = 
            new Dictionary<string, Func<Tag, IElementParser>>
            {
                { "DataValueMember", t => new DataValueMemberParser(t) },
                { "ArrayMember", t => new ArrayMemberParser(t) },
                { "StructureMember", t => new DataValueParser(t) }
            };
        
        private readonly Tag _parent;

        public StructureParser(Tag parent)
        {
            _parent = parent;
        }
        
        public void Parse(XElement element)
        {
            //todo just validate here. It should match parent
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(_parent.DataType))?.Value); 

            var children = element.Elements(); 

            foreach (var child in children)
            {
                var name = child.Name.ToString();
                if (!_parsers.ContainsKey(name)) continue;
                var parser = _parsers[name].Invoke(_parent);
                parser.Parse(child);
            }
        }
    }
}