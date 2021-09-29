using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;

[assembly: InternalsVisibleTo("L5Sharp.Parsers.Tests")]

namespace L5Sharp.Parsers
{
    internal class DataValueParser : IElementParser
    {
        private readonly Tag _parent;

        public DataValueParser(Tag parent)
        {
            _parent = parent;
        }
        
        /// <summary>
        /// Parse the data value element to update the parent tag value
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Parse(XElement element)
        {
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value);
            if (!Equals(dataType, _parent.DataType))
                throw new InvalidOperationException("Data value type does not match parent type");

            var radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            if (!Equals(radix, _parent.Radix))
                throw new InvalidOperationException("Data value radix does not match parent radix");

            if (!(dataType is Predefined predefined))
                throw new InvalidOperationException("Data value type is not a predefined atomic type");

            _parent.Value = predefined.ParseValue(element.Attribute(nameof(_parent.Value))?.Value);
        }
    }
}