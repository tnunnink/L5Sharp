using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Exceptions;
using L5Sharp.Helpers;

namespace L5Sharp
{
    internal class L5X
    {
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";
        private readonly XDocument _document;

        public L5X(XDocument document)
        {
            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools, but that would take a long time.
            //ValidateFile(document);
            
            _document = document;
        }

        public XElement Root => _document.Root!;
        
        public XElement GetContainer<TComponent>()
        {
            var containerName = LogixNames.GetContainerName<TComponent>();

            return _document.Descendants(containerName).First();
        }
        
        public IEnumerable<XElement> GetComponents<TComponent>()
        {
            var componentName = LogixNames.GetComponentName<TComponent>();

            return _document.Descendants(componentName);
        }

        public void Save(string fileName)
        {
            _document.Save(fileName);
        }
        
        private void ValidateFile(XDocument document)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(L5XSchema);

            if (stream is null)
                throw new InvalidOperationException(
                    $"Could not load embedded resource '{L5XSchema} from current assembly.");

            var schema = XmlSchema.Read(stream, null);
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(schema);

            document.Validate(schemaSet, ValidationEventHandler);
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e) =>
            throw new XmlException();
    }
}