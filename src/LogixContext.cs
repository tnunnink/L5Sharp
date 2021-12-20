using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Repositories;
using L5Sharp.Serialization;

namespace L5Sharp
{
    /// <inheritdoc />
    public class LogixContext : ILogixContext
    {
        internal XDocument L5X { get; }

        private LogixContext(XDocument document)
        {
            ValidateFile(document);
            
            L5X = document;
            
            Info = new LogixContextInfo(L5X.Root!);
            DataTypes = new UserDefinedRepository(this);
            Tags = new TagRepository(this);
            Tasks = new TaskRepository(this);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ILogixContext"/> for the provided L5X file name.
        /// </summary>
        /// <param name="fileName">The L5X file name to load into memory.</param>
        /// <returns>A new context instance if the file exists and is a valid L5X file.</returns>
        public static ILogixContext Load(string fileName)
        {
            var document = XDocument.Load(fileName);
            return new LogixContext(document);
        }

        /// <inheritdoc />
        public LogixContextInfo Info { get; }

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

        private static void ValidateFile(XDocument document)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));
            
            //todo get xsd resource and call document.Validate();
        }
    }
}