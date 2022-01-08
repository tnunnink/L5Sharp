namespace L5Sharp.Core
{
    /// <summary>
    /// Represents ta <see cref="IModule"/> port...
    /// </summary>
    public class Port
    {
        internal Port(int id, string address, string type, bool upstream, Bus? bus = null)
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
        /// Gets the value indicating whether the...
        /// </summary>
        public bool Upstream { get; }
        
        /// <summary>
        /// Gets the <see cref="Bus"/> value of the current <see cref="Port"/>.
        /// </summary>
        public Bus? Bus { get; }
    }
}