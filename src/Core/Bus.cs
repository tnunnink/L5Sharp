using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <summary>
    /// A Logix <see cref="Bus"/> is an object that represents a collection of <see cref="IModule"/>.
    /// </summary>
    public sealed class Bus : IEnumerable<IModule>
    {
        private readonly List<IModule> _modules;

        /// <summary>
        /// Creates a new <see cref="Bus"/> with the provided collection.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="modules">A collection of modules to initialize the collection with.</param>
        internal Bus(int size, IEnumerable<IModule> modules)
        {
            var list = modules.ToList();
            
            if (list.Count > size)
                throw new ArgumentException();
            
            _modules = new List<IModule>(size);
            _modules.AddRange(list);
        }

        /// <summary>
        /// Creates a new empty <see cref="Bus"/> with unspecified capacity.
        /// </summary>
        public Bus()
        {
            _modules = new List<IModule>();
        }

        /// <summary>
        /// Creates a new <see cref="Bus"/> value with the specified size parameter.
        /// </summary>
        /// <param name="size">The size of the <see cref="Bus"/>.</param>
        public Bus(int size)
        {
            _modules = new List<IModule>(size);
        }

        /// <summary>
        /// Gets the value of the current <see cref="Bus"/> size.
        /// </summary>
        public int Size => _modules.Capacity;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> has any modules.
        /// </summary>
        public bool IsEmpty => _modules.Any();

        /// <summary>
        /// Adds a <see cref="IModule"/> to the current <see cref="Bus"/>.
        /// </summary>
        /// <param name="module">The <see cref="IModule"/> instance to add.</param>
        public void AddModule(IModule module)
        {
            _modules.Add(module);
        }

        /// <summary>
        /// Adds the provided collection of <see cref="IModule"/> to the <see cref="Bus"/> in the order they are provided.
        /// </summary>
        /// <param name="modules">The collection of <see cref="IModule"/> to add.</param>
        public void AddModules(IEnumerable<IModule> modules)
        {
            _modules.AddRange(modules);
        }

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() => _modules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}