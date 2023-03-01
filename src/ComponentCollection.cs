using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;

namespace L5Sharp
{
    /// <summary>
    /// A generic collection implementation ....
    /// </summary>
    /// <typeparam name="TComponent">The logix component type for which the collection represents.</typeparam>
    public class ComponentCollection<TComponent> : ILogixComponentCollection<TComponent>
        where TComponent : ILogixComponent
    {
        private readonly XContainer _container;
        private readonly L5X _l5X;
        private readonly XName _name;
        private readonly ILogixSerializer<TComponent> _serializer;

        /// <summary>
        /// Creates a new collection over the provided <see cref="XContainer"/>.
        /// </summary>
        /// <param name="container">The root container element containing the specified component type.</param>
        /// <param name="l5X">The <see cref="L5X"/> content element.</param>
        internal ComponentCollection(XContainer container, L5X l5X)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _l5X = l5X ?? throw new ArgumentNullException(nameof(l5X));
            _name = typeof(TComponent).GetLogixName();
            _serializer = LogixSerializer.GetSerializer<TComponent>();
        }

        /// <inheritdoc />
        public void Add(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            if (!component.Name.IsComponentName())
                throw new ArgumentException($"The provided component name '{component.Name}' is not valid.");

            if (_container.Elements().Any(e => e.LogixName() == component.Name))
                throw new InvalidOperationException(
                    $"A component with the same name '{component.Name}' already exists in the collection.");
            
            //todo we need to figure out if container is null, and if so, we need to create/normalize the L5X

            _container.Add(component);
        }

        /// <inheritdoc />
        public bool Contains(string name) => _container.Descendants(_name).Any(e => e.LogixName() == name);

        /// <inheritdoc />
        public TComponent? Find(string name)
        {
            var component = _container.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);
            return component is not null ? _serializer.Deserialize(component) : default;
        }

        /// <inheritdoc />
        public TComponent Get(string name)
        {
            var result = _container.Descendants(_name).SingleOrDefault(e => e.LogixName() == name);

            if (result is null)
                throw new InvalidOperationException($"No component with name '{name}' was found in the collection.");
            
            return _serializer.Deserialize(result);
        }

        /// <inheritdoc />
        public bool Remove(string name)
        {
            var component = _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == name);
            if (component is null) return false;
            component.Remove();
            return true;
        }

        /// <inheritdoc />
        public bool Replace(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
                    
            var target = _container.Descendants(_name).SingleOrDefault(c => c.LogixName() == component.Name);
            if (target is null) return false;
            target.ReplaceWith(component);
            return true;
        }

        /// <inheritdoc />
        public IEnumerator<TComponent> GetEnumerator() => 
            _container.Descendants(_name).Select(e => _serializer.Deserialize(e)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}