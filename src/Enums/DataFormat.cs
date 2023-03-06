using L5Sharp.Types.Predefined;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents the types of data formats produced by a L5X file.
    /// </summary>
    public class DataFormat : LogixEnum<DataFormat, string>
    {
        private DataFormat(string name, string value) : base(name, value)
        {
        }
        
        /// <summary>
        /// Gets the data format for the specified <see cref="ILogixType"/>.
        /// </summary>
        /// <param name="type">The data type instance to determine the type for.</param>
        /// <returns>The <see cref="DataFormat"/> value for the specified type.
        /// Note that L5K is not going to be returned by this method because we are not currently supporting it.
        /// </returns>
        public static DataFormat FromDataType(ILogixType type)
        {
            return type switch
            {
                ALARM_DIGITAL => Alarm,
                ALARM_ANALOG => Alarm,
                STRING => String,
                _ => Decorated
            };
        }

        /// <summary>
        /// Represents a common verbose formatted data structure. 
        /// </summary>
        public static readonly DataFormat Decorated = new(nameof(Decorated), nameof(Decorated));
        
        /// <summary>
        /// Represents String formatted data structure.
        /// </summary>
        public static readonly DataFormat String = new(nameof(String), nameof(String));
        
        /// <summary>
        /// Represents Alarm formatted data structure.
        /// </summary>
        public static readonly DataFormat Alarm = new(nameof(Alarm), nameof(Alarm));
        
        /// <summary>
        /// Represents Message formatted data structure.
        /// </summary>
        public static readonly DataFormat Message = new(nameof(Message), nameof(Message));
        
        /// <summary>
        /// Represents L5K formatted data structure.
        /// </summary>
        public static readonly DataFormat L5K = new(nameof(L5K), nameof(L5K));
    }
}