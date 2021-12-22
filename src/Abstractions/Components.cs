using System;
using System.Collections;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Exceptions;

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// Represents a collection of <see cref="ILogixComponent"/> objects.
    /// </summary>
    /// <remarks>
    /// Base collection for all component collections that is used to encapsulate the functionality around maintaining
    /// collections of Logix components. 
    /// </remarks>
    /// <typeparam name="TComponent">The type of component the current collection represents.</typeparam>
    public class Components<TComponent> : IComponents<TComponent>
        where TComponent : ILogixComponent
    {
        /// <summary>
        /// The primary collection of <see cref="TComponent"/> objects.
        /// </summary>
        protected readonly Dictionary<string, TComponent> Collection = new();

        /// <summary>
        /// Creates a new instance of <see cref="ComponentCollection{TComponent}"/> for the specified
        /// <see cref="ILogixComponent"/> with the provided collection of components. 
        /// </summary>
        /// <param name="components">A collection of components to initialize the collection with.</param>
        internal Components(IEnumerable<TComponent>? components = null)
        {
            if (components is not null)
                RegisterComponents(components);
        }

        /// <inheritdoc />
        public int Count => Collection.Count;

        /// <inheritdoc />
        public bool Contains(ComponentName name) => Collection.ContainsKey(name);

        /// <inheritdoc />
        public TComponent? Get(ComponentName name) => Collection.TryGetValue(name, out var component) ? component : default;
        

        /// <summary>
        /// Initializes the base collection with the provided components collection.
        /// </summary>
        /// <param name="components">A collection of components to initialize the collection with.</param>
        /// <exception cref="ArgumentNullException">If the provided component is null.</exception>
        /// <exception cref="ComponentNameCollisionException">If a duplicate name is encountered.</exception>
        private void RegisterComponents(IEnumerable<TComponent> components)
        {
            foreach (var component in components)
            {
                if (component is null)
                    throw new ArgumentNullException(nameof(component));

                if (Collection.ContainsKey(component.Name))
                    throw new ComponentNameCollisionException(component.Name, component.GetType());

                Collection.Add(component.Name, component);
            }
        }

        /// <inheritdoc />
        public IEnumerator<TComponent> GetEnumerator()
        {
            return Collection.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}