using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    public class DataType : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets the value of the <see cref="DataTypeFamily"/> for the <see cref="IDataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating whether the data type belongs to the string family or has no family.
        /// </value>
        public DataTypeFamily Family { get; set; } = DataTypeFamily.None;
        
        /// <summary>
        /// Gets the value of the <see cref="DataTypeClass"/> for the <see cref="IDataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating the class for which the current data type belongs.
        /// This could be atomic, user, predefined, etc.
        /// </value>
        public DataTypeClass Class => DataTypeClass.User;

        /// <summary>
        /// 
        /// </summary>
        public List<DataTypeMember> Members { get; set; } = new();
    }
}