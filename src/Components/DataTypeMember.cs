using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components;

/// <summary>
/// A component of the <see cref="DataType"/> that makes up the structure of the user defined type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[XmlType(L5XName.Member)]
public class DataTypeMember : LogixComponent<DataTypeMember>
{
    /// <summary>
    /// Creates a new <see cref="DataTypeMember"/> with default values.
    /// </summary>
    public DataTypeMember()
    {
        DataType = string.Empty;
        Dimension = Dimensions.Empty;
        Radix = Radix.Null;
        ExternalAccess = ExternalAccess.ReadWrite;
        Hidden = false;
    }

    /// <summary>
    /// Creates a new <see cref="DataTypeMember"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public DataTypeMember(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The name of the data type of the member.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the member.
    /// Default is <see cref="string.Empty"/>.
    /// Valid value is required for valid import.
    /// </value>
    public string? DataType
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The array dimension of the member.
    /// </summary>
    /// <value>
    /// A <see cref="Core.Dimensions"/> representing the array dimensions of the member.
    /// Default is <see cref="Core.Dimensions.Empty"/>.
    /// Members should not have multidimensional arrays.
    /// </value>
    public Dimensions? Dimension
    {
        get => GetValue<Dimensions>();
        set => SetValue(value);
    }

    /// <summary>
    /// The radix value format of the member.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.Radix"/> representing the value type format of the member.
    /// Default is <see cref="Enums.Radix.Null"/>.
    /// Should be <see cref="Enums.Radix.Null"/> for all non atomic types.
    /// </value>
    public Radix? Radix
    {
        get => GetValue<Radix>();
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the member. 
    /// </summary>
    /// <value>
    /// A <see cref="Enums.ExternalAccess"/> representing read/write access of the member.
    /// Default is <see cref="Enums.ExternalAccess.ReadWrite"/>.
    /// </value>
    public ExternalAccess? ExternalAccess
    {
        get => GetValue<ExternalAccess>();
        set => SetValue(value);
    }

    /// <summary>
    /// An flag indicating whether the member is a hidden backing member for another boolean member of the data type. 
    /// </summary>
    /// <value><c>true</c> if member is a hidden member of the type, <c>false</c> if not, and <c>null</c> if the attribute
    /// does not exist for the underlying element.</value>
    public bool? Hidden
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The name of the hidden member that is the backing member for this data type member. 
    /// </summary>
    /// <value>A <see cref="string"/> representing the name of the hidden member. Logix appends 10 Zs to the hidden
    /// members.
    /// <c>null</c> if the attribute does not exist for the underlying element</value>
    public string? Target
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// The bit number of the hidden member that this boolean member maps to or is backed by.
    /// </summary>
    /// <value>A zero-based integer representing the backing bit of the hidden member which corresponds to this member.
    /// <c>null</c> if the attribute does not exist for the underlying element</value>
    public int? BitNumber
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
}