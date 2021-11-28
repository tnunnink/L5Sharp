using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents the types of data formats produced by a L5X file.
    /// </summary>
    public class DataFormat : SmartEnum<DataFormat>
    {
        private DataFormat(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a verbose formatted data structure. 
        /// </summary>
        public static readonly DataFormat Decorated = new DataFormat("Decorated", 0);
        
        /// <summary>
        /// Represents string formatted data structure.
        /// </summary>
        public static readonly DataFormat String = new DataFormat("String", 2);
        
        /// <summary>
        /// Represents alarm formatted data structure.
        /// </summary>
        public static readonly DataFormat Alarm = new DataFormat("Alarm", 3);
    }
}