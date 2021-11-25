using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5SharpTests")]

namespace L5Sharp
{
    internal class L5X
    {
        private readonly XDocument _document;
        private readonly XElement _content;

        public L5X(XDocument document)
        {
            _document = document;
            _content = document.Root;
        }
        
        public XElement GetFirst(Func<XElement, bool> predicate)
        {
            return _content.Descendants().FirstOrDefault(predicate);
        }
        
        public XElement GetSingle(Func<XElement, bool> predicate)
        {
            return _content.Descendants().SingleOrDefault(predicate);
        }
        
        public IEnumerable<XElement> GetAll(Func<XElement, bool> predicate)
        {
            return _content.Descendants().Where(predicate);
        }

        public XElement GetContainer<TComponent>()
        {
            var name = LogixNames.GetContainerName<TComponent>();
            return _content.Descendants(name).FirstOrDefault();
        }
        
        public IEnumerable<XElement> GetComponents<TComponent>()
        {
            var name = LogixNames.GetComponentName<TComponent>();
            return _content.Descendants(name);
        }

        public void Save(string fileName)
        {
            _document.Save(fileName);
        }
    }
}