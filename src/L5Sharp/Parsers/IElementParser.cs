using System.Xml.Linq;

namespace L5Sharp.Parsers
{
    internal interface IElementParser
    {
        void Parse(XElement element);
    }
}