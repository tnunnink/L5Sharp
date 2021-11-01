using System.Net;

namespace L5Sharp.Core
{
    public class Port
    {
        internal Port(int id, byte slot, IPAddress address, string type, bool upstream)
        {
            Id = id;
            Slot = slot;
            Address = address;
            Type = type;
            Upstream = upstream;
        }
        
        public int Id { get; }
        public byte Slot { get; }
        public IPAddress Address { get; }
        public string Type { get; }
        public bool Upstream { get; }
    }
}