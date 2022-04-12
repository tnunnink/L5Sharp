using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <inheritdoc />
    internal sealed class ComponentQuery<TComponent> : IComponentQuery<TComponent>
        where TComponent : ILogixComponent
    {
        private readonly IEnumerable<XElement> _components;
        private readonly IL5XSerializer<TComponent> _serializer;
        private readonly StringComparer _comparer;
        
        public ComponentQuery(IEnumerable<XElement> components, IL5XSerializer<TComponent> serializer)
        {
            _components = components ?? throw new ArgumentNullException(nameof(components));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _comparer = StringComparer.OrdinalIgnoreCase;
        }

        /// <inheritdoc />
        public IEnumerable<TComponent> All() => _components.Select(e => _serializer.Deserialize(e));

        /// <inheritdoc />
        public bool Any() => _components.Any();

        /// <inheritdoc />
        public bool Any(string name) =>
            _components.Any(e => _comparer.Equals(e.ComponentName(), name));

        /// <inheritdoc />
        public int Count() => _components.Count();

        /// <inheritdoc />
        public TComponent? Find(string name)
        {
            var component = _components.FirstOrDefault(e => _comparer.Equals(e.ComponentName(), name));
            return component is not null ? _serializer.Deserialize(component) : default;
        }

        /// <inheritdoc />
        public IEnumerable<TComponent> Find(IEnumerable<string> names)
        {
            var components = _components.Where(e => names.Contains(e.ComponentName(), _comparer));
            return components.Select(e => _serializer.Deserialize(e));
        }

        /// <inheritdoc />
        public TComponent First() => _serializer.Deserialize(_components.First());

        /// <inheritdoc />
        public TComponent? FirstOrDefault()
        {
            var component = _components.FirstOrDefault();
            return component is not null ? _serializer.Deserialize(component) : default;
        }

        /// <inheritdoc />
        public TComponent Get(string name) =>
            _serializer.Deserialize(_components.Single(e => _comparer.Equals(e.ComponentName(), name)));

        /// <inheritdoc />
        public IEnumerable<ComponentName> Names() =>
            _components.Select(e => new ComponentName(e.ComponentName()));

        /// <inheritdoc />
        public IEnumerable<TComponent> Take(int count) =>
            _components.Take(count).Select(e => _serializer.Deserialize(e));
    }
}