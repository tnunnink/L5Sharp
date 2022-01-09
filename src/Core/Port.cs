namespace L5Sharp.Core
{
    /// <summary>
    /// Represents ta <see cref="IModule"/> port...
    /// </summary>
    public sealed class Port
    {
        /// <summary>
        /// Creates a new <see cref="Port"/> with the provided parameters.
        /// </summary>
        /// <param name="id">The id value tag identifies the port instance.</param>
        /// <param name="address">The string address of the port. This is typically the slot number of IP address.</param>
        /// <param name="type">The type of port.</param>
        /// <param name="upstream">The value indicating content is upstream of the current port. Default is false.</param>
        /// <param name="bus">The <see cref="Bus"/> value of the current port. Default is an empty bus (i.e. no bus).</param>
        public Port(int id, string address, string type, bool upstream = false, Bus bus = default)
        {
            Id = id;
            Address = address;
            Type = type;
            Upstream = upstream;
            Bus = bus;
        }

        /// <summary>
        /// Gets the Id of the <see cref="Port"/>.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Gets the address of the <see cref="Port"/>.
        /// </summary>
        public string Address { get; }
        
        /// <summary>
        /// Gets the type of the <see cref="Port"/>.
        /// </summary>
        public string Type { get; }
        
        /// <summary>
        /// Gets the value indicating whether the there are devices upstream of the current <see cref="Port"/>.
        /// </summary>
        public bool Upstream { get; }
        
        /// <summary>
        /// Gets the <see cref="Bus"/> value of the current <see cref="Port"/>.
        /// </summary>
        public Bus Bus { get; }
    }
}