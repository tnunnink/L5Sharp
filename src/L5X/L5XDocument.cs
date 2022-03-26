using System;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A wrapper class around an <see cref="XDocument"/> that provided generic methods for getting various
    /// component elements based on component type. Also performs validation on the provided L5X XDocument.
    /// </summary>
    internal class L5XDocument
    {
        private static readonly XDeclaration DefaultDeclaration = new("1.0", "UTF-8", "yes");
        private const string DefaultRevision = "1.0";
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy";
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";
        private readonly XDocument _document;

        /// <summary>
        /// Creates a new <see cref="L5XDocument"/> that wraps the provided <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document">The <see cref="XDocument"/> that represents the L5X content.</param>
        internal L5XDocument(XDocument document)
        {
            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools...
            //ValidateFile(document);

            _document = document ?? throw new ArgumentNullException(nameof(document));
            Components = new L5XComponents(this);
            Containers = new L5XContainers(this);
            Index = new L5XIndex(this);
            Serializers = new L5XSerializers(this);
        }

        /// <summary>
        /// Creates a new <see cref="L5XDocument"/> with the provided logic component as the target.
        /// </summary>
        /// <param name="component">The logix component instance for which to create the target context.</param>
        /// <typeparam name="TComponent">The logix component type.</typeparam>
        /// <returns>A new <see cref="L5XDocument"/> that represents a target context for the provided component.</returns>
        internal static L5XDocument Create<TComponent>(TComponent component)
            where TComponent : ILogixComponent
        {
            var document = new XDocument(DefaultDeclaration);

            var content = new XElement(L5XElement.RSLogix5000Content.ToString());
            content.Add(new XAttribute(nameof(SchemaRevision), DefaultRevision));
            content.Add(new XAttribute(nameof(SoftwareRevision), DefaultRevision));
            content.Add(new XAttribute(nameof(TargetName), component.Name));
            content.Add(new XAttribute(nameof(TargetType), L5XNames.GetComponentName<TComponent>()));
            content.Add(new XAttribute(nameof(ContainsContext), component is not IController));
            content.Add(new XAttribute(nameof(Owner), Environment.UserName));
            content.Add(new XAttribute(nameof(ExportDate), DateTime.Now.ToString(DateFormat)));

            var controller = component is IController
                ? component.Serialize()
                : new XElement(L5XElement.Controller.ToString());

            if (component is not IController)
            {
                var container = new XElement(L5XNames.GetContainerName<TComponent>());
                
                //how to add component and dependencies...?
                //var element = component.Serialize();
                //element.Add(new XAttribute(nameof(Use), Use.Target));
                //container.Add(element);
                
                controller.Add(container);
            }
            
            content.Add(controller);

            return new L5XDocument(document);
        }

        /// <summary>
        /// Gets the root <see cref="XElement"/> for the current L5X file.
        /// </summary>
        public XElement Content => _document.Root ?? throw new ArgumentNullException(nameof(_document.Root));

        /// <summary>
        /// Gets the controller <see cref="XElement"/> for the current L5X file.
        /// </summary>
        public XElement? Controller => _document.Root?.Element(L5XElement.Controller.ToString());

        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision => Revision.Parse(Content.Attribute(nameof(SchemaRevision))?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision => Revision.Parse(Content.Attribute(nameof(SoftwareRevision))?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName TargetName => new(Content.Attribute(nameof(TargetName))?.Value!);

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string TargetType => Content.Attribute(nameof(TargetType))?.Value!;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext => bool.Parse(Content.Attribute(nameof(ContainsContext))?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string Owner => Content.Attribute(nameof(Owner))?.Value!;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate => DateTime.ParseExact(Content.Attribute(nameof(ExportDate))?.Value, DateFormat,
            CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets the <see cref="L5XComponents"/> for the current <see cref="L5XDocument"/>
        /// </summary>
        public L5XComponents Components { get; }

        /// <summary>
        /// Gets the <see cref="L5XContainers"/> for the current <see cref="L5XDocument"/>
        /// </summary>
        public L5XContainers Containers { get; }

        /// <summary>
        /// Gets the <see cref="L5XIndex"/> for the current <see cref="L5XDocument"/>
        /// </summary>
        public L5XIndex Index { get; }

        /// <summary>
        /// Gets the <see cref="L5XSerializers"/> for the current <see cref="L5XDocument"/>
        /// </summary>
        public L5XSerializers Serializers { get; }

        /// <summary>
        /// Saves the current L5X file to the specified file path.
        /// </summary>
        /// <param name="fileName">The full path for which to save the current L5X content.</param>
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