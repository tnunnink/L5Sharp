using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all known tag data formats.
    /// </summary>
    /// <remarks>
    /// This is not a enumeration found in the L5X but one that we created to capture possible data format options
    /// and provide an easy way to determine how the tag data should be serialized.
    /// </remarks>
    public class TagDataFormat : SmartEnum<TagDataFormat, string>
    {
        private TagDataFormat(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Gets the data format for the specified <see cref="IDataType"/>.
        /// </summary>
        /// <param name="dataType">The data type instance to determine the type for.</param>
        /// <returns>The <see cref="TagDataFormat"/> value for the specified type.
        /// Note that L5X is not going to be returned by this method because we are not currently supporting it.
        /// </returns>
        public static TagDataFormat FromDataType(IDataType dataType)
        {
            return dataType switch
            {
                Types.Alarm => Alarm,
                Types.String => String,
                _ => Decorated
            };
        }

        /// <summary>
        /// Represents the standard decorated or verbose data format for the tags.
        /// </summary>
        public static readonly TagDataFormat Decorated = new(nameof(Decorated), nameof(Decorated));
        
        /// <summary>
        /// Represents the alarm data format used with the <see cref="Alarm"/> type.
        /// </summary>
        public static readonly TagDataFormat Alarm = new(nameof(Alarm), nameof(Alarm));
        
        /// <summary>
        /// Represents the string data format used with <see cref="String"/> types.
        /// </summary>
        public static readonly TagDataFormat String = new(nameof(String), nameof(String));
        
        /// <summary>
        /// Represents L5K data format. This format is not used by the library.
        /// </summary>
        public static readonly TagDataFormat L5K = new(nameof(L5K), nameof(L5K));
    }
}