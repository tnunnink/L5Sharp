using System;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Repositories;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class LogixContext
    {
        private readonly XDocument _document;
        private XElement Content => _document.Root;

        private LogixContext(XDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            
            //todo validate document?
            
            _document = document;
        }

        public LogixContext(string fileName) : this(XDocument.Load(fileName))
        {
        }

        public string SchemaRevision => Content.Attribute(nameof(SchemaRevision))?.Value;
        public string SoftwareRevision => Content.Attribute(nameof(SoftwareRevision))?.Value;
        public string TargetName => Content.Attribute(nameof(TargetName))?.Value;
        public string TargetType => Content.Attribute(nameof(TargetType))?.Value;
        public string ContainsContext => Content.Attribute(nameof(ContainsContext))?.Value;
        public string Owner => Content.Attribute(nameof(Owner))?.Value;

        public IDataTypeRepository DataTypes => new DataTypeRepository(Content.Container<IDataType>());

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