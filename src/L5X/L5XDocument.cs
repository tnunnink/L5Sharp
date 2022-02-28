using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A wrapper class around an <see cref="XDocument"/> that provided generic methods for getting various
    /// component elements based on component type. Also performs validation on the provided XDocument content.
    /// </summary>
    public class L5XDocument
    {
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";
        private readonly XDocument _document;

        /// <summary>
        /// Creates a new <see cref="L5XDocument"/> that wraps the provided <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document">The <see cref="XDocument"/> that represents the L5X content.</param>
        public L5XDocument(XDocument document)
        {
            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools, but that would take a long time.
            //ValidateFile(document);

            _document = document;
        }

        /// <summary>
        /// Gets the root <see cref="XElement"/> for the current L5X file.
        /// </summary>
        public XElement Content => _document.Root!;

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
            _document.Descendants(L5XNames.GetComponentName<TComponent>());

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