using System.Net;
using Ardalis.SmartEnum;

namespace L5Sharp.Core
{
    public class Port
    {
        public Port(int id, object address, string type, bool upstream)
        {
            Id = id;
            Address = address;
            Type = type;
            Upstream = upstream;
        }
        
        public int Id { get; }
        public object Address { get; }
        public string Type { get; }
        public bool Upstream { get; }
        public Bus Bus { get; }
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