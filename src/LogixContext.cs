using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Repositories;
using L5Sharp.Serialization;

namespace L5Sharp
{
    public class LogixContext
    {
        private LogixContext(XDocument document)
        {
            L5X = new L5X(document);
            Serializer = new SerializationContext(this);
            TypeRegistry = new LogixTypeRegistry(this);

            DataTypes = new UserDefinedRepository(this);
            Tags = new TagRepository(this);
        }

        /// <summary>
        /// Creates a new Logix Context for the provided L5X file.
        /// </summary>
        /// <param name="fileName">The full file name and path to the L5X.</param>
        public LogixContext(string fileName) : this(XDocument.Load(fileName))
        {
        }

        internal L5X L5X { get; }
        internal SerializationContext Serializer { get; }
        internal LogixTypeRegistry TypeRegistry { get; }

        public string SchemaRevision => L5X.SchemaRevision;
        public string SoftwareRevision => L5X.SoftwareRevision;
        public string TargetName => L5X.TargetName;
        public string TargetType => L5X.TargetType;
        public string ContainsContext => L5X.ContainsContext;
        public string Owner => L5X.Owner;

        public IUserDefinedRepository DataTypes { get; }
        public ITagRepository Tags { get; }


        public void Save(string fileName)
        {
            L5X.Save(fileName);
        }

        /*internal IXMaterializer<T> GetFactory<T>() where T : ILogixComponent
        {
            var type = _factories.Keys.SingleOrDefault(t => t == typeof(T));

            if (type == null)
                throw new InvalidOperationException($"Factory not defined for component of type '{typeof(T)}'");

            return (IXMaterializer<T>)_factories[type];
        }

        private void InitializeFactories()
        {
            _factories.Add(typeof(IUserDefined), new UserDefinedMaterializer(this));
            _factories.Add(typeof(IMember<IDataType>), new MemberMaterializer(this));
            _factories.Add(typeof(ITag<IDataType>), new TagMaterializer(this));
        }*/

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