namespace L5Sharp.Core
{
    /// <summary>
    /// A set of properties that defines a <see cref="Port"/> for a given <see cref="IModule"/>. 
    /// </summary>
    /// <remarks>
    /// This class is used by a <see cref="IModule"/> to instantiate valid <see cref="Port"/> instance on construction.
    /// </remarks>
    public sealed class PortDefinition
    {
        /// <summary>
        /// Creates a new <see cref="PortDefinition"/> with the provided parameters.
        /// </summary>
        /// <param name="id">The unique id of the port.</param>
        /// <param name="type">The type of the port (e.g. ICP, Ethernet, etc.)</param>
        /// <param name="upstream">Flag indicating whether the port is up or down stream.</param>
        /// <param name="address">The address of the port (slot or IP).</param>
        /// <param name="busSize">The size of the port's bus or chassis if known.</param>
        /// <param name="downstreamOnly">Indicates whether the port is only available for downstream connection.</param>
        public PortDefinition(int id, string type, bool upstream, string? address = null, int busSize = default,
            bool downstreamOnly = default)
        {
            Id = id;
            Type = type;
            Upstream = upstream;
            Address = address ?? string.Empty;
            BusSize = busSize;
            DownstreamOnly = downstreamOnly;
        }

        /// <summary>
        /// Gets the unique id of the port for the current <see cref="PortDefinition"/>
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the port type of the <see cref="PortDefinition"/>
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the value indicating whether the port is an upstream or downstream connection. 
        /// </summary>
        public bool Upstream { get; set; }

        /// <summary>
        /// Gets a value indicating whether the port only support a downstream bus connection.
        /// </summary>
        public bool DownstreamOnly { get; }

        /// <summary>
        /// Gets the address of the port.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets the size of the port bus.
        /// </summary>
        public int BusSize { get; set; }
    }
}