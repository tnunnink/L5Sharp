using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A wrapper class around an <see cref="XDocument"/> that provided generic methods for getting various
    /// component elements based on component type. Also performs validation on the provided XDocument content.
    /// </summary>
    internal class L5XDocument
    {
        private static readonly XDeclaration DefaultDeclaration = new("1.0", "UTF-8", "yes");
        private const string DefaultRevision = "1.0";
        private const string DateFormat = "ddd MMM d HH:mm:ss yyyy";
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";
        private readonly XDocument _document;
        private readonly L5XIndex _index;
        private readonly L5XSerializers _serializers;

        /// <summary>
        /// Creates a new <see cref="L5XDocument"/> that wraps the provided <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document">The <see cref="XDocument"/> that represents the L5X content.</param>
        internal L5XDocument(XDocument document)
        {
            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools...
            //ValidateFile(document);

            _document = document;
            _index = new L5XIndex(this);
            _serializers = new L5XSerializers(this);
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

            var element = component.Serialize();

            element.Add(new XAttribute(nameof(Use), Use.Target));

            if (component is IController)
            {
                content.Add(element);
                return new L5XDocument(document);
            }

            var controller = new XElement(L5XNames.GetComponentName<IController>());
            var container = new XElement(L5XNames.GetContainerName<TComponent>());

            container.Add(element);
            controller.Add(container);
            content.Add(controller);

            return new L5XDocument(document);
        }

        /// <summary>
        /// Gets the root <see cref="XElement"/> for the current L5X file.
        /// </summary>
        public XElement Content => _document.Root ?? throw new ArgumentNullException(nameof(_document.Root));

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
        /// Gets all <see cref="XElement"/> objects that represent the component element for specified component type.
        /// </summary>
        /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> to get the component elements for.</typeparam>
        /// <returns>A new collection of <see cref="XElement"/> instances that represent the component element for the
        /// specified component type.</returns>
        /// <remarks>
        /// For instance, if the specified component type is an <see cref="IUserDefined"/> component, then the method
        /// will return the elements <see cref="L5XElement.DataType"/>, which corresponds to the component elements
        /// for all user defined data types.
        /// </remarks>
        public IEnumerable<XElement> GetComponents<TComponent>()
            where TComponent : ILogixComponent =>
            _document.Descendants(L5XNames.GetComponentName<TComponent>())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);

        /// <summary>
        /// Gets an <see cref="XElement"/> that represents the containing element for the specified component type.
        /// </summary>
        /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> to get the containing element for.</typeparam>
        /// <returns>A new <see cref="XElement"/> instance that represents the containing element for the specified
        /// component type.</returns>
        /// <remarks>
        /// For instance, if the specified component type is an <see cref="IUserDefined"/> component, then the method
        /// will return the element <see cref="L5XElement.DataTypes"/>, which corresponds to the containing element
        /// for all user defined data type.
        /// </remarks>
        public XElement GetContainer<TComponent>()
            where TComponent : ILogixComponent =>
            _document.Descendants(L5XNames.GetContainerName<TComponent>()).First();

        public L5XIndex Index() => _index;
        
        public L5XSerializers Serializers() => _serializers;

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