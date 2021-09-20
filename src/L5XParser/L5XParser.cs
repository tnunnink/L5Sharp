using System.Collections.Generic;
using System.Xml.Linq;
using L5XParser.Model;

namespace L5XParser
{
    public class L5XParser
    {
        private readonly XDocument _document;

        public L5XParser(string fileName)
        {
            _document = XDocument.Load(fileName);
        }

        public IEnumerable<Module> GetModules()
        {
            var elements = _document.Descendants("Module");

            foreach (var element in elements)
                yield return Module.Parse(element);
        }
        
        public IEnumerable<Rung> GetRungs()
        {
            var elements = _document.Descendants("Rung");

            foreach (var element in elements)
                yield return Rung.Parse(element);
        }
        
        public IEnumerable<Rung> GetRungsWithTag(string tagName)
        {
            var elements = _document.Descendants("Rung");

            foreach (var element in elements)
            {
                var rung =  Rung.Parse(element);
                if (rung.ContainsTagName(tagName))
                    yield return rung;
            }
        }
    }
}