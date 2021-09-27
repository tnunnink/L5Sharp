using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Primitives;

namespace L5Sharp.Helpers
{

    internal interface ITagDataParser
    {
        IEnumerable<ITag> ParseData(XElement element);
    }
    
    internal class StructureParser : ITagDataParser
    {
        public IEnumerable<ITag> ParseData(XElement element)
        {
            //expecting the element to be the structure root
            
            //iterate through each descendent and generate a tag
            //if it is a data value member then se return and new tag
            //if it is a array member

            return null;
        }
    }

    public class TagDataParser
    {
        private readonly XElement _element;
        private readonly XElement _decorated;
        private readonly XElement _alarm;

        public TagDataParser(XElement element)
        {
            _element = element;
            
            _decorated = element.Descendants("Data")
                .SingleOrDefault(x => x.HasAttributes && x.FirstAttribute.Value == "Decorated");
            
            _alarm = element.Descendants("Data")
                .SingleOrDefault(x => x.HasAttributes && x.FirstAttribute.Value == "Alarm");
        }
    }
}