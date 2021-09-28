using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;

namespace L5Sharp.Parsers
{
    public class DecoratedDataParser : IElementParser
    {
        private readonly Dictionary<string, Func<Tag, IElementParser>> _parsers = 
            new Dictionary<string, Func<Tag, IElementParser>>
        {
            { "DataValue", t => new DataValueParser(t) },
            { "Array", t => new DataValueParser(t) },
            { "Structure", t => new DataValueParser(t) }
        };

        private readonly Tag _parent;

        public DecoratedDataParser(Tag parent)
        {
            _parent = parent;
        }

        public void Parse(XElement element)
        {
            var target = element.Descendants().First();
            var type = target.Name.ToString();

            if (!_parsers.ContainsKey(type))
                throw new InvalidOperationException();
            
            var parser = _parsers[type].Invoke(_parent);
            parser.Parse(target);
        }
    }
}