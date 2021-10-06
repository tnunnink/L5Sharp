using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enumerations;
using L5Sharp.Extensions;
using L5Sharp.Repositories;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class LogixContext
    {
        private readonly XDocument _document;

        private LogixContext(XDocument document)
        {
            DataTypes = new DataTypeRepository(this);
        }
        
        public LogixContext(IComponent component, Revision revision)
        {
            _document = GenerateRoot();
        }
        
        public LogixContext(string fileName)
        {
            _document = XDocument.Load(fileName);
        }
        
        public IDataTypeRepository DataTypes { get; }

        internal XElement Content => _document.Root;

        public void RegisterDataType(IDataType dataType)
        {
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            if (Content.Contains<DataType>(dataType.Name))
                Throw.ComponentNameCollisionException(dataType.Name, typeof(IDataType));
            
            Content.Container<DataType>().Add(dataType.Serialize());
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