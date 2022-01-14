using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <inheritdoc />
    public class LogixContext : ILogixContext
    {
        private readonly string _fileName;
        private const string L5XSchema = "L5Sharp.Resources.L5X.xsd";
        private XDocument L5X { get; }

        /// <summary>
        /// Creates a new <see cref="LogixContext"/> instance with the provided file name.
        /// </summary>
        /// <param name="fileName">The full path the L5X file to load.</param>
        /// <exception cref="ArgumentException">fileName is null or empty string.</exception>
        /// <exception cref="FileNotFoundException">fileName does not exist.</exception>
        /// <exception cref="L5XParseException">The provided L5X document is not valid.</exception>
        public LogixContext(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Filename can not be null or empty");

            if (!File.Exists(fileName))
                throw new FileNotFoundException("The provided file name does not exist", fileName);

            _fileName = fileName;

            var document = XDocument.Load(_fileName);

            //todo need to decide how to get valid schema file.
            //We should probably create our own using exports and xsd generation tools, but that would take a long time.
            //ValidateFile(document);

            L5X = document;

            DataTypes = new UserDefinedRepository(this);
            Tags = new TagRepository(this);
            Tasks = new TaskRepository(this);
        }

        /// <inheritdoc />
        public Revision SchemaRevision => Revision.Parse(L5X.Root?.Attribute(nameof(SchemaRevision))?.Value!);

        /// <inheritdoc />
        public Revision SoftwareRevision => Revision.Parse(L5X.Root?.Attribute(nameof(SoftwareRevision))?.Value!);

        /// <inheritdoc />
        public ComponentName TargetName => new ComponentName(L5X.Root?.Attribute(nameof(TargetName))?.Value!);

        /// <inheritdoc />
        public string TargetType => L5X.Root?.Attribute(nameof(TargetType))?.Value!;

        /// <inheritdoc />
        public bool ContainsContext => bool.Parse(L5X.Root?.Attribute(nameof(ContainsContext))?.Value!);

        /// <inheritdoc />
        public string Owner => L5X.Root?.Attribute(nameof(Owner))?.Value!;

        /// <inheritdoc />
        public DateTime ExportDate => DateTime.ParseExact(L5X.Root?.Attribute(nameof(ExportDate))?.Value,
            "ddd MMM d HH:mm:ss yyyy", CultureInfo.CurrentCulture);

        /// <inheritdoc />
        public IRepository<IUserDefined> DataTypes { get; }

        /// <inheritdoc />
        public IRepository<ITag<IDataType>> Tags { get; }

        /// <inheritdoc />
        public IRepository<IProgram> Programs { get; }

        /// <inheritdoc />
        public IReadOnlyRepository<ITask> Tasks { get; }

        /// <inheritdoc />
        public void Save(string? fileName = null)
        {
            fileName ??= _fileName;

            L5X.Save(fileName);
        }

        internal XElement? GetContainer<TComponent>()
        {
            var containerName = LogixNames.GetContainerName<TComponent>();

            return L5X.Descendants(containerName).FirstOrDefault();
        }

        /// <summary>
        /// Gets all Logix components for the specified component type in the current context.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        internal IEnumerable<XElement> GetComponents<TComponent>()
        {
            var componentName = LogixNames.GetComponentName<TComponent>();

            return L5X.Descendants(componentName);
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
            throw new L5XParseException(e.Message, _fileName, e.Exception);
    }
}