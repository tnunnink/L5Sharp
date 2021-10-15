using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Factories;
using L5Sharp.Repositories;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class LogixContext
    {
        private readonly XDocument _document;
        private readonly Dictionary<Type, IComponentFactory> _factories = new Dictionary<Type, IComponentFactory>();

        private readonly Dictionary<Type, IComponentCache> _cache = new Dictionary<Type, IComponentCache>
        {
            { typeof(IDataType), new ComponentCache<IDataType>() },
            { typeof(ITag), new ComponentCache<ITag>() }
        };

        private LogixContext(XDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            //todo validate document?

            _document = document;
            
            _factories.Add(typeof(IDataType), new DataTypeFactory(this));
        }

        public LogixContext(string fileName) : this(XDocument.Load(fileName))
        {
        }

        public LogixContext(IComponent component, Revision revision) : this(GenerateContent(component, revision))
        {
        }

        public void Save(string fileName)
        {
            _document.Save(fileName);
        }


        public string SchemaRevision => Content.Attribute(nameof(SchemaRevision))?.Value;

        public string SoftwareRevision => Content.Attribute(nameof(SoftwareRevision))?.Value;

        public string TargetName => Content.Attribute(nameof(TargetName))?.Value;

        public string TargetType => Content.Attribute(nameof(TargetType))?.Value;

        public string ContainsContext => Content.Attribute(nameof(ContainsContext))?.Value;

        public string Owner => Content.Attribute(nameof(Owner))?.Value;

        public IDataTypeRepository DataTypes => new DataTypeRepository(this);

        internal XElement Content => _document.Root;

        internal IComponentCache<T> GetCache<T>() where T : IComponent
        {
            var type = typeof(T);

            if (!_cache.ContainsKey(type))
                throw new InvalidOperationException($"Cache not defined for component of type '{type}'");

            return (IComponentCache<T>)_cache[type];
        }

        internal IComponentFactory<T> GetFactory<T>() where T : IComponent
        {
            var type = typeof(T);

            if (!_factories.ContainsKey(type))
                throw new InvalidOperationException($"Serializer not defined for component of type '{type}'");

            return (IComponentFactory<T>)_factories[type];
        }

        private static XDocument GenerateContent(IComponent component, Revision revision)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement(LogixNames.Containers.RSLogix5000Content);
            root.Add(new XAttribute("SchemaRevision", "1.0"));
            root.Add(new XAttribute("SoftwareRevision", revision.ToString()));
            root.Add(new XAttribute("TargetName", component.Name));
            root.Add(new XAttribute("TargetType", component.GetType().Name));
            root.Add(new XAttribute("ContainsContext", component.GetType() != typeof(IController)));
            root.Add(new XAttribute("Owner", $"{Environment.UserName}, {Environment.UserDomainName}"));
            root.Add(new XAttribute("ExportDate", DateTime.Now));
            root.Add(new XAttribute("ExportOptions", ""));

            if (component is IController) return new XDocument(declaration, root);

            var controllerElement = new XElement(LogixNames.Components.Controller);
            //todo add other properties needed
            root.Add(controllerElement);

            return new XDocument(declaration, root);
        }
    }
}