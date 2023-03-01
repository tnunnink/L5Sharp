﻿using System.Xml.Serialization;
using L5Sharp.Attributes;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    /// <summary>
    /// A component of the <see cref="DataType"/> that makes up the structure of the user defined type.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [XmlType(L5XName.Member)]
    [LogixSerializer(typeof(DataTypeMemberSerializer))]
    public class DataTypeMember
    {
        /// <summary>
        /// The name of the <see cref="DataTypeMember"/>.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// The data type name of the <see cref="DataTypeMember"/>.
        /// </summary>
        public string DataType { get; set; } = string.Empty;
        
        /// <summary>
        /// The dimensions of the <see cref="DataTypeMember"/>.
        /// </summary>
        public Dimensions Dimension { get; set; } = Dimensions.Empty;
        
        /// <summary>
        /// The radix format of the <see cref="DataTypeMember"/>.
        /// </summary>
        public Radix Radix { get; set; } = Radix.Null;
        
        /// <summary>
        /// The external access of the <see cref="DataTypeMember"/>.
        /// </summary>
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;
        
        /// <summary>
        /// The description of the <see cref="DataTypeMember"/>.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}