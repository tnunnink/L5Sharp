using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class L5X
    {
        private readonly XDocument _document;
        private readonly Dictionary<Type, string> _containerNames = new Dictionary<Type, string>
        {
            { typeof(DataType), L5XNames.Containers.DataTypes },
        };

        public L5X(string fileName)
        {
            _document = XDocument.Load(fileName);
        }

        public bool Contains<T>(string name) where T : INamedComponent
        {
            var component = typeof(T).Name;
            return _document.Descendants(component).FirstOrDefault(x => x.GetName() == name) != null;
        }
        
        public T Get<T>(string name) where T : INamedComponent
        {
            var component = typeof(T).Name;
            return _document.Descendants(component).FirstOrDefault(x => x.GetName() == name).Deserialize<T>();
        }
        
        public XElement GetContainer<T>() where T : INamedComponent
        {
            var containerName = _containerNames[typeof(T)];
            return _document.Descendants(containerName).FirstOrDefault();
        }

        private static XDocument GenerateRoot()
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement(L5XNames.Containers.RSLogix5000Content);
            root.Add(new XAttribute("SchemaRevision", "1.0"));
            root.Add(new XAttribute("SoftwareRevision", "1.0"));
            root.Add(new XAttribute("TargetName", "1.0"));
            root.Add(new XAttribute("TargetType", "1.0"));
            root.Add(new XAttribute("SchemaRevision", "1.0"));
            
            return new XDocument(declaration);
        }
    }
}