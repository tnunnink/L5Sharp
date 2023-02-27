using System.Collections.Generic;
using L5Sharp.Attributes;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(DataTypeSerializer))]
    public class DataType : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets the value of the <see cref="DataTypeFamily"/> for the <see cref="DataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating whether the data type belongs to the string family or has no family.
        /// </value>
        public DataTypeFamily Family { get; set; } = DataTypeFamily.None;

        /// <summary>
        /// Gets the value of the <see cref="DataTypeClass"/> for the <see cref="DataType"/> instance.
        /// </summary>
        /// <value>
        /// An enum value indicating the class for which the current data type belongs.
        /// This could be atomic, user, predefined, etc.
        /// </value>
        public DataTypeClass Class => DataTypeClass.User;

        /// <summary>
        /// A collection of <see cref="DataTypeMember"/> objects that together make up the structure of the
        /// <see cref="DataType"/> component.
        /// </summary>
        public List<DataTypeMember> Members { get; set; } = new();
    }
}