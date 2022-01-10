using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all Logix <see cref="DataTypeFamily"/> options for a given <see cref="IDataType"/>.
    /// Valid options are None and String.
    /// </summary>
    public sealed class DataTypeFamily : SmartEnum<DataTypeFamily, string>
    {
        private DataTypeFamily(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents no specific data type family.
        /// All <see cref="IDataType"/> objects except <see cref="IStringType"/> will have this option.
        /// </summary>
        public static readonly DataTypeFamily None = new(nameof(None), "NoFamily");
        
        
        /// <summary>
        /// Represents a string family. Only <see cref="IStringType"/> objects will have this option.
        /// </summary>
        public static readonly DataTypeFamily String = new(nameof(String), "StringFamily");
    }
}