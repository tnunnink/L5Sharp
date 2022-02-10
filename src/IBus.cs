using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    public interface IBus : IEnumerable<Module>
    {
        /// <summary>
        /// Gets the type indicating which port types can be added to the <see cref="IBus"/>.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the number of Modules in the <see cref="IBus"/>.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Gets the value of the current <see cref="IBus"/> size.
        /// </summary>
        public byte Size { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IBus"/> is empty or has not Modules.
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IBus"/> is full and can not add new Modules.
        /// </summary>
        public bool IsFull { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IBus"/> is of fixed size or unbounded.
        /// </summary>
        public bool IsFixed { get; }

        public void Add(Module module);
        
        public bool Remove(Module module);

        public string DetermineAddress(string address);

        public string NextAvailableAddress();
        
        public bool IsAvailableAddress(string address);
    }
}