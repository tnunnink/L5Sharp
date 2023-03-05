using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp
{
    /// <summary>
    /// A generic collection implementation ....
    /// </summary>
    /// <typeparam name="TComponent">The logix component type for which the collection represents.</typeparam>
    internal class ComponentCollection<TComponent> : ILogixComponentCollection<TComponent>
        where TComponent : class, ILogixComponent
    {
        private readonly L5X _l5X;
        private readonly XName _name;
        private readonly ILogixSerializer<TComponent> _serializer;
        private XContainer? _container;

        /// <summary>
        /// Creates a new collection over the provided <see cref="XContainer"/>.
        /// </summary>
        /// <param name="l5X">The <see cref="L5X"/> content element.</param>
        /// <param name="container"></param>
        internal ComponentCollection(L5X l5X, XContainer? container)
        {
            _l5X = l5X ?? throw new ArgumentNullException(nameof(l5X));

            _name = typeof(TComponent).GetLogixName();
            _serializer = LogixSerializer.GetSerializer<TComponent>();
            _container = container;
        }

        /// <inheritdoc />
        public void Add(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (!component.Name.IsComponentName())
                throw new ArgumentException($"The provided component name '{component.Name}' is not valid.");

            if (_container is null)
            {
                _l5X.Normalize();
                _container = _l5X.GetContainer(_name);
            }

            if (_container is not null && _container.Elements().Any(e => e.LogixName() == component.Name))
                throw new InvalidOperationException(
                    $"A component with the name '{component.Name}' already exists in the collection.");

            var element = _serializer.Serialize(component);

            if (_l5X.ContainsContext == true && component.Name != _l5X.TargetName)
                element.Add(new XAttribute(L5XName.Use, Use.Context));

            _container?.Add(element);
        }

        /// <inheritdoc />
        public bool Contains(string name) =>
            _container is not null && _container.Descendants(_name).Any(e => e.LogixName() == name);

        /// <inheritdoc />
        public TComponent? Find(string name)
        {
            var component = _container?.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);
            return component != null ? _serializer.Deserialize(component) : default;
        }

        /// <inheritdoc />
        public TComponent Get(string name)
        {
            if (_container is null)
                throw new InvalidOperationException(
                    $"No container exists for the current component type {typeof(TComponent)}");
            
            var result = _container.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);

            if (result is null)
                throw new InvalidOperationException($"No component with name '{name}' was found in the collection.");

            return _serializer.Deserialize(result);
        }

        /// <inheritdoc />
        public bool Remove(string name)
        {
            var component = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == name);
            if (component is null) return false;
            component.Remove();
            return true;
        }

        /// <inheritdoc />
        public bool Replace(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var target = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name);
            if (target is null) return false;

            var element = _serializer.Serialize(component);

            if (_l5X.ContainsContext == true && component.Name != _l5X.TargetName)
                element.Add(new XAttribute(L5XName.Use, Use.Context));

            target.ReplaceWith(element);
            return true;
        }

        public void Upsert(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            var element = _serializer.Serialize(component);
            if (_l5X.ContainsContext == true && component.Name != _l5X.TargetName)
                element.Add(new XAttribute(L5XName.Use, Use.Context));

            var target = _container?.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name);
            
            if (target is not null)
            {
                target.ReplaceWith(element);
                return;
            }

            if (!component.Name.IsComponentName())
                throw new ArgumentException($"The provided component name '{component.Name}' is not valid.");

            if (_container is null)
            {
                _l5X.Normalize();
                _container = _l5X.GetContainer(_name);
            }
                
            _container?.Add(element);
        }

        /// <inheritdoc />
        public IEnumerator<TComponent> GetEnumerator() =>
            _container?.Descendants(_name).Select(e => _serializer.Deserialize(e)).GetEnumerator() 
            ?? Enumerable.Empty<TComponent>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}