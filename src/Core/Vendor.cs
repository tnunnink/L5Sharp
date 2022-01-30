namespace L5Sharp.Core
{
    public class Vendor
    {
        private Vendor(ushort vendorId, string vendorName)
        {
            Id = vendorId;
            Name = vendorName;
        }
        
        /// <summary>
        /// Gets the value that uniquely identifies the <see cref="Vendor"/>. 
        /// </summary>
        public ushort Id { get; }
        
        /// <summary>
        /// Gets the value that represents the <see cref="Vendor"/> name.
        /// </summary>
        public string Name { get; }
        

        public static Vendor Unkown => new(0, string.Empty);
    }
}