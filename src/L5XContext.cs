using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <inheritdoc />
    public class L5XContext : ILogixContext
    {
        /// <summary>
        /// Creates a new <see cref="L5XContext"/> instance with the provided <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document">The XDocument instance that represents the loaded L5X.</param>
        private L5XContext(XDocument document)
        {
            L5X = new L5XDocument(document);
            DataTypes = new DataTypeRepository(this);
            Modules = new ModuleRepository(this);
            Tags = new TagRepository(this);
            Programs = new ProgramRepository(this);
            Tasks = new TaskRepository(this);
        }

        /// <summary>
        /// Creates a new <see cref="L5XContext"/> with the provided string path.
        /// </summary>
        /// <param name="fileName">The full path, including file name, to the L5X file to load.</param>
        /// <returns>A new <see cref="L5XContext"/> instance.</returns>
        /// <exception cref="ArgumentException">The string is null or empty.</exception>
        public static L5XContext Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Filename can not be null or empty");
            
            var document = XDocument.Load(fileName);

            return new L5XContext(document);
        }

        /// <summary>
        /// Creates a new <see cref="L5XContext"/> with the provided L5X string content.
        /// </summary>
        /// <param name="content">The string that contains the L5X content to parse.</param>
        /// <returns>A new <see cref="L5XContext"/> instance.</returns>
        /// <exception cref="ArgumentException">The string is null or empty.</exception>
        public static L5XContext Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Filename can not be null or empty");

            var document = XDocument.Parse(content);

            return new L5XContext(document);
        }

        /// <summary>
        /// Gets the underlying <see cref="L5XDocument"/> that the current context represents. 
        /// </summary>
        public L5XDocument L5X { get; }

        /// <summary>
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        public Revision SchemaRevision => Revision.Parse(L5X.Content.Attribute(nameof(SchemaRevision))?.Value!);

        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        public Revision SoftwareRevision => Revision.Parse(L5X.Content.Attribute(nameof(SoftwareRevision))?.Value!);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public ComponentName TargetName => new(L5X.Content.Attribute(nameof(TargetName))?.Value!);

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string TargetType => L5X.Content.Attribute(nameof(TargetType))?.Value!;

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool ContainsContext => bool.Parse(L5X.Content.Attribute(nameof(ContainsContext))?.Value!);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string Owner => L5X.Content.Attribute(nameof(Owner))?.Value!;

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime ExportDate => DateTime.ParseExact(L5X.Content.Attribute(nameof(ExportDate))?.Value,
            "ddd MMM d HH:mm:ss yyyy", CultureInfo.CurrentCulture);

        /// <inheritdoc />
        public IController Controller => L5X.GetComponents<IController>().First().Deserialize<IController>();

        /// <inheritdoc />
        public IRepository<IUserDefined> DataTypes { get; }

        /// <inheritdoc />
        public IModuleRepository Modules { get; }

        /// <inheritdoc />
        public IRepository<ITag<IDataType>> Tags { get; }

        /// <inheritdoc />
        public IRepository<IProgram> Programs { get; }

        /// <inheritdoc />
        public IReadOnlyRepository<ITask> Tasks { get; }

        /// <inheritdoc />
        public override string ToString() => L5X.Content.ToString();
    }
}