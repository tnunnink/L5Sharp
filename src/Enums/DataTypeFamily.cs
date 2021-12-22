using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all Logix DataTypeFamily options for a given <see cref="IDataType"/>.
    /// </summary>
    public sealed class DataTypeFamily : SmartEnum<DataTypeFamily>
    {
        private DataTypeFamily(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents no specific data type family.
        /// All <see cref="IDataType"/> objects except <see cref="IStringType"/> will have this option.
        /// </summary>
        public static readonly DataTypeFamily None = new DataTypeFamily("NoFamily", 0);
        
        
        /// <summary>
        /// Represents a string family. Only <see cref="IStringType"/> objects will have this option.
        /// </summary>
        public static readonly DataTypeFamily String = new DataTypeFamily("StringFamily", 1);
    }
}