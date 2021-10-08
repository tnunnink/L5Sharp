namespace L5Sharp.Core
{
    public class Port
    {
        public Port(int id, int slot, string address, string type, bool upstream)
        {
            Id = id;
            Slot = slot;
            Address = address;
            Type = type;
            Upstream = upstream;
        }

        public int Id { get; }
        public int Slot { get; }
        public string Address { get; }
        public string Type { get; }
        public bool Upstream { get; }
    }
}