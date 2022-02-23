using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <inheritdoc />
    public class L5XContext : ILogixContext
    {
        private readonly string _fileName;

        /// <summary>
        /// Creates a new <see cref="L5XContext"/> instance with the provided file name.
        /// </summary>
        /// <param name="fileName">The full path the L5X file to load.</param>
        /// <exception cref="ArgumentException">fileName is null or empty string.</exception>
        /// <exception cref="FileNotFoundException">fileName does not exist.</exception>
        /// <exception cref="L5XParseException">The provided L5X document is not valid.</exception>
        public L5XContext(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Filename can not be null or empty");

            if (!File.Exists(fileName))
                throw new FileNotFoundException("The provided file name does not exist", fileName);

            _fileName = fileName;

            var document = XDocument.Load(_fileName);

            L5X = new L5XDocument(document);

            DataTypes = new DataTypeRepository(this);
            Modules = new ModuleRepository(this);
            Tags = new TagRepository(this);
            Programs = new ProgramRepository(this);
            Tasks = new TaskRepository(this);
        }

        internal L5XDocument L5X { get; }

        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision => Revision.Parse(L5X.Root.Attribute(nameof(SchemaRevision))?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision => Revision.Parse(L5X.Root.Attribute(nameof(SoftwareRevision))?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName TargetName => new(L5X.Root.Attribute(nameof(TargetName))?.Value!);

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string TargetType => L5X.Root.Attribute(nameof(TargetType))?.Value!;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext => bool.Parse(L5X.Root.Attribute(nameof(ContainsContext))?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string Owner => L5X.Root.Attribute(nameof(Owner))?.Value!;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate => DateTime.ParseExact(L5X.Root.Attribute(nameof(ExportDate))?.Value,
            "ddd MMM d HH:mm:ss yyyy", CultureInfo.CurrentCulture);

        /// <inheritdoc />
        public IController Controller => L5X.GetComponents<IController>().First().Deserialize<IController>();

        /// <inheritdoc />
        public IRepository<IUserDefined> DataTypes { get; }

        /// <inheritdoc />
        public IRepository<IModule> Modules { get; }

        /// <inheritdoc />
        public IRepository<ITag<IDataType>> Tags { get; }

        /// <inheritdoc />
        public IRepository<IProgram> Programs { get; }

        /// <inheritdoc />
        public IReadOnlyRepository<ITask> Tasks { get; }

        /// <inheritdoc />
        public void Save()
        {
            L5X.Save(_fileName);
        }
        
        /// <summary>
        /// Save the current content of the <see cref="L5XContext"/> to the specified file name.
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            L5X.Save(fileName);
        }
    }
}