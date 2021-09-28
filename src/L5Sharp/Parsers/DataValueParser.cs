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
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="element"></param>
        /// <exception cref="XmlException"></exception>
        /// <example>
        ///  <DataValue DataType="INT" Radix="Decimal" Value="0"/>
        /// </example>
        public void Parse(XElement element)
        {
            var dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value);
            if (!Equals(dataType, _parent.DataType))
                throw new XmlException();

            var radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value);
            if (radix != _parent.Radix)
                throw new XmlException();

            if (!(dataType is Predefined predefined))
                throw new InvalidOperationException();

            _parent.Value = predefined.ParseValue(element.Attribute(nameof(_parent.Value))?.Value);
        }
    }
}