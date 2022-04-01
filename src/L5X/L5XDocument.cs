using System;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Serialization.Components;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A wrapper class around an <see cref="XDocument"/> that provided generic methods for getting various
    /// component elements based on component type. Also performs validation on the provided L5X XDocument.
    /// </summary>
    internal class L5XDocument : IRevertibleChangeTracking
    {
        private static readonly XDeclaration DefaultDeclaration = new("1.0", "UTF-8", "yes");
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";
        private readonly XElement _source;

        /// <summary>
        /// Creates a new <see cref="L5XDocument"/> that wraps the provided <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document">The <see cref="XDocument"/> that represents the L5X content.</param>
        internal L5XDocument(XDocument document)
        {
            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools...
            //ValidateFile(document);
            
            _source = document.Root ?? throw new ArgumentNullException(nameof(document));
            Content = new XElement(_source);
            Info = new L5XInfo(Content);
            Components = new L5XComponents(this);
            Containers = new L5XContainers(this);
            Index = new L5XIndex(this);
            Serializers = new L5XSerializers(this);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="controller"></param>
       /// <returns></returns>
        internal static L5XDocument Create(IController controller)
        {
            var document = new XDocument(DefaultDeclaration);

            var content = L5XInfo.New(controller.Name, nameof(Controller));

            var serializer = new ControllerSerializer();
            var element = serializer.Serialize(controller);
            content.Add(element);
            
            document.Add(content);

            return new L5XDocument(document);
        }

        /// <summary>
        /// Creates a <see cref="L5XDocument"/> with just a root RSLogix5000Content element and default values.
        /// </summary>
        /// <returns>A <see cref="L5XDocument"/></returns>
        public static L5XDocument Empty()
        {
            var document = new XDocument(DefaultDeclaration);
            
            document.Add(L5XInfo.Default());

            return new L5XDocument(document);
        }

        /// <summary>
        /// Gets the controller <see cref="XElement"/> for the current L5X file.
        /// </summary>
        public XElement? Controller => Content.Element(L5XElement.Controller.ToString());

        /// <summary>
        /// Gets the root content <see cref="XElement"/> of the L5X file.
        /// </summary>
        public XElement Content { get; private set; }

        /// <summary>
        /// Gets the <see cref="L5XComponents"/> for the current <see cref="L5XDocument"/>
        /// </summary>
        public L5XComponents Components { get; }

        /// <summary>
        /// Gets the <see cref="L5XContainers"/> for the current <see cref="L5XDocument"/>.
        /// </summary>
        public L5XContainers Containers { get; }

        /// <summary>
        /// Gets the <see cref="L5XIndex"/> for the current <see cref="L5XDocument"/>.
        /// </summary>
        public L5XIndex Index { get; }

        /// <summary>
        /// Gets the <see cref="L5XInfo"/> for the current <see cref="L5XDocument"/>.
        /// </summary>
        public L5XInfo Info { get; }

        /// <summary>
        /// Indicates whether the current document has changes.
        /// </summary>
        public bool IsChanged => Content.ToString() != _source.ToString();

        /// <summary>
        /// Gets the <see cref="L5XSerializers"/> for the current <see cref="L5XDocument"/>.
        /// </summary>
        public L5XSerializers Serializers { get; }

        /// <summary>
        /// Saves the current L5X file to the specified file path.
        /// </summary>
        /// <param name="fileName">The full path for which to save the current L5X content.</param>
        public void Save(string fileName)
        {
            AcceptChanges();

            var document = new XDocument(DefaultDeclaration, _source);
            
            document.Save(fileName);
        }

        public void AcceptChanges()
        {
            _source.ReplaceWith(Content);
            Index.Run();
        }

        public void RejectChanges() => Content = new XElement(_source);

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