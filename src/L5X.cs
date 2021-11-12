using System.Runtime.CompilerServices;
using System.Xml.Linq;

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

        public string SchemaRevision => _content.Attribute(nameof(SchemaRevision))?.Value;

        public string SoftwareRevision => _content.Attribute(nameof(SoftwareRevision))?.Value;

        public string TargetName => _content.Attribute(nameof(TargetName))?.Value;

        public string TargetType => _content.Attribute(nameof(TargetType))?.Value;

        public string ContainsContext => _content.Attribute(nameof(ContainsContext))?.Value;

        public string Owner => _content.Attribute(nameof(Owner))?.Value;

        public XElement DataTypes => _content.Element(LogixNames.Controller)?.Element(LogixNames.DataTypes);

        public XElement Modules => _content.Element(LogixNames.Controller)?.Element(LogixNames.Modules);

        public XElement Instructions =>
            _content.Element(LogixNames.Controller)?.Element(LogixNames.AddOnInstructionDefinitions);

        public XElement Tags => _content.Element(LogixNames.Controller)?.Element(LogixNames.Tags);
        public XElement Programs => _content.Element(LogixNames.Controller)?.Element(LogixNames.Programs);

        public XElement Tasks => _content.Element(LogixNames.Controller)?.Element(LogixNames.Tasks);

        public void Save(string fileName)
        {
            _document.Save(fileName);
        }
    }
}