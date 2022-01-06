using Ardalis.SmartEnum;

namespace L5Sharp.Core
{
    public class Port
    {
        internal Port(int id, string address, string type, bool upstream)
        {
            Id = id;
            Address = address;
            Type = type;
            Upstream = upstream;
        }
        
        /// <summary>
        /// Gets the ID for the <see cref="Port"/>.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Gets the address for the <see cref="Port"/>.
        /// </summary>
        public string Address { get; }
        
        /// <summary>
        /// Gets the port type for the <see cref="Port"/>.
        /// </summary>
        public string Type { get; }
        
        /// <summary>
        /// Gets the value indicating whether the
        /// </summary>
        public bool Upstream { get; }
        
        public Bus? Bus { get; }
    }

    public class Bus
    {
        public byte Size { get; }
        public Baud Baud { get; }
    }

    public sealed class Baud : SmartEnum<Baud>
    {
        private Baud(string name, int value) : base(name, value)
        {
        }

        public static readonly Baud _57_6 = new Baud("57.6", 0);
        public static readonly Baud _115_2 = new Baud("115.2", 1);
        public static readonly Baud _230_4 = new Baud("230.4", 2);
    }
}