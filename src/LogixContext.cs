using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <inheritdoc />
    public class LogixContext : ILogixContext
    {
        private readonly string _fileName;

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

            L5X = new L5X(document);

            DataTypes = new DataTypeRepository(this);
            Modules = new ModuleRepository(this);
            Tags = new TagRepository(this);
            Programs = new ProgramRepository(this);
            Tasks = new TaskRepository(this);
        }

        internal L5X L5X { get; }

        /// <inheritdoc />
        public Revision SchemaRevision => Revision.Parse(L5X.Root.Attribute(nameof(SchemaRevision))?.Value!);

        /// <inheritdoc />
        public Revision SoftwareRevision => Revision.Parse(L5X.Root.Attribute(nameof(SoftwareRevision))?.Value!);

        /// <inheritdoc />
        public ComponentName TargetName => new(L5X.Root.Attribute(nameof(TargetName))?.Value!);

        /// <inheritdoc />
        public string TargetType => L5X.Root.Attribute(nameof(TargetType))?.Value!;

        /// <inheritdoc />
        public bool ContainsContext => bool.Parse(L5X.Root.Attribute(nameof(ContainsContext))?.Value!);

        /// <inheritdoc />
        public string Owner => L5X.Root.Attribute(nameof(Owner))?.Value!;

        /// <inheritdoc />
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
        public void Save(string? fileName = null)
        {
            fileName ??= _fileName;
            L5X.Save(fileName);
        }
    }
}