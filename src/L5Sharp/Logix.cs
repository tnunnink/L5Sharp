using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class Logix
    {
        private readonly XDocument _document;
        
        public Logix(string fileName)
        {
            _document = XDocument.Load(fileName);
        }

        public Logix(IController controller)
        {
            
        }

        public XElement Content => _document.Root;

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