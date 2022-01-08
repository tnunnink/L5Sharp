namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Bus
    {
        internal Bus(byte size, float baud = 0)
        {
            Size = size;
            Baud = baud;
        }
        
        /// <summary>
        /// Gets the size of the 
        /// </summary>
        public byte Size { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public float Baud { get; }
    }
}