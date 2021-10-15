using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Factories;
using L5Sharp.Repositories;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class LogixContext
    {
        private readonly XDocument _document;
        private readonly XElement _content;
        private readonly Dictionary<Type, IComponentFactory> _factories = new Dictionary<Type, IComponentFactory>();
        private readonly Dictionary<Type, IComponentCache> _cache = new Dictionary<Type, IComponentCache>();

        private LogixContext(XDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            //todo validate document?

            _document = document;
            _content = document.Root;
            
            InitializeCache();
            InitializeFactories();
        }

        public LogixContext(string fileName) : this(XDocument.Load(fileName))
        {
        }

        public LogixContext(IComponent component, Revision revision) : this(GenerateContent(component, revision))
        {
        }

        public string SchemaRevision => _content.Attribute(nameof(SchemaRevision))?.Value;

        public string SoftwareRevision => _content.Attribute(nameof(SoftwareRevision))?.Value;

        public string TargetName => _content.Attribute(nameof(TargetName))?.Value;

        public string TargetType => _content.Attribute(nameof(TargetType))?.Value;

        public string ContainsContext => _content.Attribute(nameof(ContainsContext))?.Value;

        public string Owner => _content.Attribute(nameof(Owner))?.Value;

        public IDataTypeRepository DataTypes => new DataTypeRepository(this);

        public void Save(string fileName)
        {
            _document.Save(fileName);
        }

        public void ClearCache()
        {
            foreach (var cache in _cache)
                cache.Value.Clear();
        }

        internal IComponentCache<T> GetCache<T>() where T : IComponent
        {
            var type = _cache.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(T)));
            
            if (type == null)
                throw new InvalidOperationException($"Cache not defined for component of type '{typeof(T)}'");

            return (IComponentCache<T>)_cache[type];
        }

        internal IComponentFactory<T> GetFactory<T>() where T : IComponent
        {
            var type = _factories.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(T)));
            
            if (type == null)
                throw new InvalidOperationException($"Factory not defined for component of type '{typeof(T)}'");

            return (IComponentFactory<T>)_factories[type];
        }

        internal XElement GetContainer<T>() where T : IComponent
        {
            var container = LogixNames.GetContainerName<T>();
            return _content.Descendants(container).FirstOrDefault();
        }

        private void InitializeCache()
        {
            _cache.Add(typeof(IDataType), new ComponentCache<IDataType>());
            _cache.Add(typeof(ITag), new ComponentCache<ITag>());
        }
        
        private void InitializeFactories()
        {
            _factories.Add(typeof(IDataType), new DataTypeFactory(this));
            _factories.Add(typeof(IMember), new MemberFactory(this));
        }

        private static XDocument GenerateContent(IComponent component, Revision revision)
        {
            var declaration = new XDeclaration("1.0", "UTF-8", "yes");
            var root = new XElement(LogixNames.GetContainerName<LogixContext>());
            root.Add(new XAttribute("SchemaRevision", "1.0"));
            root.Add(new XAttribute("SoftwareRevision", revision.ToString()));
            root.Add(new XAttribute("TargetName", component.Name));
            root.Add(new XAttribute("TargetType", component.GetType().Name));
            root.Add(new XAttribute("ContainsContext", component.GetType() != typeof(IController)));
            root.Add(new XAttribute("Owner", $"{Environment.UserName}, {Environment.UserDomainName}"));
            root.Add(new XAttribute("ExportDate", DateTime.Now));
            root.Add(new XAttribute("ExportOptions", ""));

            if (component is IController) return new XDocument(declaration, root);

            var controllerElement = new XElement(LogixNames.GetComponentName<IController>());
            //todo add other properties needed
            root.Add(controllerElement);

            return new XDocument(declaration, root);
        }
    }
}