using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Repositories;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp
{
    /// <inheritdoc />
    public class LogixContext : ILogixContext
    {
        internal L5X L5X { get; }

        private LogixContext(XDocument document)
        {
            L5X = new L5X(document);

            Info = new L5XInfo(document.Root);
            DataTypes = new UserDefinedRepository(this);
            Tags = new TagRepository(this);
            Tasks = new TaskRepository(this);
        }

        /// <summary>
        /// Creates a new Logix Context for the provided L5X file.
        /// </summary>
        /// <param name="fileName">The full file name and path to the L5X.</param>
        private LogixContext(string fileName) : this(XDocument.Load(fileName))
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ILogixContext"/> for the provided L5X file name.
        /// </summary>
        /// <param name="fileName">The L5X file name to load into memory.</param>
        /// <returns>A new context instance if the file exists and is a valid L5X file.</returns>
        public static ILogixContext Load(string fileName)
        {
            return new LogixContext(fileName);
        }

        /// <inheritdoc />
        public L5XInfo Info { get; }
        
        /// <inheritdoc />
        public IUserDefinedRepository DataTypes { get; }

        /// <inheritdoc />
        public ITagRepository Tags { get; }

        /// <inheritdoc />
        public ITaskRepository Tasks { get; }
        
        /// <inheritdoc />
        public void Save(string fileName)
        {
            L5X.Save(fileName);
        }

        private static XDocument GenerateContent(ILogixComponent logixComponent, Revision revision)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement(LogixNames.RsLogix5000Content);
            root.Add(new XAttribute("SchemaRevision", "1.0"));
            root.Add(new XAttribute("SoftwareRevision", revision.ToString()));
            root.Add(new XAttribute("TargetName", logixComponent.Name));
            root.Add(new XAttribute("TargetType", logixComponent.GetType().Name));
            root.Add(new XAttribute("ContainsContext", logixComponent.GetType() != typeof(IController)));
            root.Add(new XAttribute("Owner", $"{Environment.UserName}, {Environment.UserDomainName}"));
            root.Add(new XAttribute("ExportDate", DateTime.Now));
            root.Add(new XAttribute("ExportOptions", ""));

            if (logixComponent is IController) return new XDocument(declaration, root);

            var controllerElement = new XElement(LogixNames.Controller);
            //todo add other properties needed
            root.Add(controllerElement);

            return new XDocument(declaration, root);
        }
    }
}