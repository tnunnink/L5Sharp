using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components;

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
    /// The name of the member.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the name of the member.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The description of the member.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the description of the member.
    /// Default is <see cref="string.Empty"/>.
    /// </value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The name of the data type of the member.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the member.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// The array dimension of the member.
    /// </summary>
    /// <value>
    /// A <see cref="Core.Dimensions"/> representing the array dimensions of the member.
    /// Default is <see cref="Core.Dimensions.Empty"/>.
    /// Members should not have multidimensional arrays.
    /// </value>
    public Dimensions Dimension { get; set; } = Dimensions.Empty;

    /// <summary>
    /// The radix value format of the member.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.Radix"/> representing the value type format of the member.
    /// Default is <see cref="Enums.Radix.Null"/>.
    /// Should be <see cref="Enums.Radix.Null"/> for all non atomic types.
    /// </value>
    public Radix Radix { get; set; } = Radix.Null;

    /// <summary>
    /// The external access of the member. 
    /// </summary>
    /// <value>
    /// A <see cref="Enums.ExternalAccess"/> representing read/write access of the member.
    /// Default is <see cref="Enums.ExternalAccess.ReadWrite"/>.
    /// </value>
    public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;
}