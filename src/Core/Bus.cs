using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <summary>
    /// A Logix <see cref="Bus"/> is an object that represents a collection of <see cref="IModule"/>.
    /// </summary>
    public sealed class Bus : IEnumerable<IModule>
    {
        private readonly List<IModule?> _modules;

        /// <summary>
        /// Creates a new <see cref="Bus"/> with the optional size and baud rate.
        /// </summary>
        /// <param name="size">The size of the Bus. This sets the capacity of the collection.</param>
        /// <param name="baud">The baud rate of the Bus.</param>
        public Bus(int? size = default, float? baud = default)
        {
            Size = size;
            Baud = baud;

            _modules = size is not null ? new List<IModule?>(new IModule[size.Value]) : new List<IModule?>();
        }

        /// <summary>
        /// Gets the number of Modules in the current <see cref="Bus"/>.
        /// </summary>
        /// <remarks>
        /// The count represents the number of non null slots in the <see cref="Bus"/>.
        /// </remarks>
        public int Count => _modules.Count(m => m is not null);

        /// <summary>
        /// Gets the value of the current <see cref="Bus"/> size.
        /// </summary>
        /// <remarks>
        /// Is the Size is null, the <see cref="Bus"/> represents an unbounded collection of Modules. This
        /// is typical for Ethernet based Bus networks. If Size has a value, the <see cref="Bus"/> represents a
        /// fixed length collection and will contain null references for empty slots or indices.
        /// </remarks>
        public int? Size { get; }

        /// <summary>
        /// Gets the value of the Baud rate for the <see cref="Bus"/> connection.
        /// </summary>
        public float? Baud { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> has any modules.
        /// </summary>
        public bool IsEmpty => _modules.All(m => m is null);

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is full.
        /// </summary>
        public bool IsFull => Size is not null && _modules.All(m => m is not null);

        /// <summary>
        /// Gets a value indicating whether the <see cref="Bus"/> is a backplane containing a fixed size.
        /// </summary>
        /// <remarks>
        /// When <see cref="Size"/> is not null then we are assuming the Bus represents a backplane or a fixed length
        /// collection of Modules that are assignable by index or slot number. If null, then we assume this is a network
        /// type Bus having an unconstrained length of Modules.
        /// </remarks>
        /// <seealso cref="IsEthernet"/>
        public bool IsBackplane => Size is not null;

        /// <summary>
        /// Gets a valid indicating whether the <see cref="Bus"/> is a set of Ethernet based devices and that
        /// the size is unconstrained.
        /// </summary>
        /// <remarks>
        /// When <see cref="Size"/> is not null then we are assuming the Bus represents a backplane or a fixed length
        /// collection of Modules that are assignable by index or slot number. If null, then we assume this is a Ethernet
        /// type Bus having an unconstrained length of Modules.
        /// </remarks>
        /// <seealso cref="IsBackplane"/>
        public bool IsEthernet => Size is null;

        /// <summary>
        /// Gets a <see cref="IModule"/> at the specified slot number.
        /// </summary>
        /// <param name="slot">The slot number at which to get the Module.</param>
        /// <returns>
        /// For Ethernet type Bus objects, returns a Module at the specified index if valid; otherwise throws exception.
        /// For Backplane type Bus objects, returns the Module instance in the specified slot if it exists; otherwise null. 
        /// </returns>
        /// <remarks>
        /// This getter is mostly for backplane type Bus objects as they will have an initialized set of indices after
        /// construction. Accessing a module by index for Ethernet type Bus may result in <see cref="ArgumentOutOfRangeException"/>
        /// if the specified index is not within range of the current collection count.
        /// </remarks>
        public IModule? this[int slot] => _modules[slot];

        /// <summary>
        /// Gets a <see cref="IModule"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the Module to get.</param>
        /// <returns>
        /// The <see cref="IModule"/> instance with the specified name if one exists on the Bus; otherwise, null. 
        /// </returns>
        public IModule? this[ComponentName name] => _modules.FirstOrDefault(m => m is not null && m.Name == name);

        /// <summary>
        /// Adds a <see cref="IModule"/> to the current <see cref="Bus"/>.
        /// </summary>
        /// <param name="module">The <see cref="IModule"/> instance to add.</param>
        /// <exception cref="ArgumentNullException">modules is null.</exception>
        /// <exception cref="ComponentNameCollisionException">module name is already defined on the collection.</exception>
        /// <exception cref="ArgumentException">module does not have an upstream port defined
        /// -or- module is not ethernet based and the slot number could not be parsed.</exception>
        /// <exception cref="InvalidOperationException">Bus is full
        /// -or- the specified slot is already taken by another module. </exception>
        /// <exception cref="ArgumentOutOfRangeException">module slot number is out of range.</exception>
        /// <remarks>
        /// 
        /// </remarks>
        public void Add(IModule? module) => AddModuleInternal(module);

        /// <summary>
        /// Adds the provided collection of <see cref="IModule"/> to the <see cref="Bus"/> in the order they are provided.
        /// </summary>
        /// <param name="modules">The collection of <see cref="IModule"/> to add.</param>
        /// <exception cref="ArgumentNullException">modules is null.</exception>
        /// <remarks>
        /// 
        /// </remarks>
        public void AddMany(IEnumerable<IModule?> modules)
        {
            if (modules is null)
                throw new ArgumentNullException(nameof(modules));

            foreach (var module in modules)
                AddModuleInternal(module);
        }

        /// <summary>
        /// Determines if the current <see cref="Bus"/> contains a <see cref="IModule"/> with the specified name.
        /// </summary>
        /// <param name="moduleName">The name of the Module to search for.</param>
        public bool Contains(string moduleName) => _modules.Any(m => m is not null && m.Name == moduleName);

        /// <summary>
        /// Removes the <see cref="IModule"/> with the specified name from the <see cref="Bus"/>.
        /// </summary>
        /// <param name="moduleName">The name of the Module to remove from the Bus.</param>
        /// <returns>true is the Module was found and removed; false if it was not found.</returns>
        public bool Remove(string moduleName) => _modules.RemoveAll(m => m is not null && m.Name == moduleName) == 1;

        /// <summary>
        /// Removes the <see cref="IModule"/> at the specified slot number from the <see cref="Bus"/>.
        /// </summary>
        /// <param name="slot">The slot of the Module to remove from the Bus.</param>
        public void RemoveAt(int slot)
        {
            if (slot < 0 || slot >= Size)
                throw new ArgumentOutOfRangeException(nameof(slot),
                    "The provided module slot number is out of range for the current Bus size.");

            _modules[slot] = null;
        }

        /// <summary>
        /// Attempts to add the provided <see cref="IModule"/> to the <see cref="Bus"/>.
        /// </summary>
        /// <param name="module">The Module to add.</param>
        /// <returns>true if the Module was added to the Bus; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">when module is null.</exception>
        public bool TryAdd(IModule? module)
        {
            if (module is null)
                throw new ArgumentNullException(nameof(module));

            var port = module.Ports.FirstOrDefault(p => p.Upstream);

            if (port is null || Contains(module.Name) || IsFull)
                return false;

            if (port.Type == "Ethernet")
            {
                _modules.Add(module);
                return true;
            }

            if (!int.TryParse(port.Address, out var slot) || !IsValidSlot(slot)) return false;
            _modules[slot] = module;
            return true;
        }

        private void AddModuleInternal(IModule? module)
        {
            if (module is null)
                throw new ArgumentNullException(nameof(module));

            if (IsFull)
                throw new InvalidOperationException(
                    "The current Bus is full and can not add new modules. Remove modules before adding further items.");

            if (Contains(module.Name))
                throw new ComponentNameCollisionException(module.Name, module.GetType());

            var port = module.Ports.FirstOrDefault(p => p is not null && p.Upstream);

            if (port is null)
                throw new ArgumentException(
                    "The provided Module does not have an upstream port." +
                    " In order to add a Module to the current Bus, it must have an upstream port defined");

            ValidatePort(port);

            if (port.Type == "Ethernet")
            {
                _modules.Add(module);
                return;
            }

            var slot = GetSlotNumber(port);
            _modules[slot] = module;
        }

        private static int GetSlotNumber(Port port) => int.Parse(port.Address);

        private bool IsValidSlot(int? slot) => slot > 0 && slot < Size && _modules[slot.Value] is null;

        private void ValidatePort(Port port)
        {
            if (IsEthernet)
            {
                if (port.Type != "Ethernet")
                    throw new ArgumentException("Can only add Modules with a port type of Ethernet to the current Bus");
            }

            if (!int.TryParse(port.Address, out var slot))
                throw new ArgumentException(
                    $"Could not determine the slot number for the provided Module Address '{port.Address}'");

            if (slot < 0 || slot >= Size)
                throw new ArgumentOutOfRangeException(nameof(slot),
                    "The provided Module slot number is out of range for the current Bus size.");

            if (_modules[slot] is not null)
                throw new InvalidOperationException(
                    $"The current Bus already has a module at the specified slot number {slot}");
        }

        /// <inheritdoc />
        public IEnumerator<IModule> GetEnumerator() => _modules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}