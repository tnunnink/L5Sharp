using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Common;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Elements;

/// <summary>
/// An element of the <see cref="DataType"/> that makes up the structure of the user defined type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[L5XType(L5XName.Member)]
public class DataTypeMember : LogixElement
{
    /// <summary>
    /// Creates a new <see cref="DataTypeMember"/> with default values.
    /// </summary>
    public DataTypeMember()
    {
        Name = string.Empty;
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
    /// The unique name of the <c>Member</c>.
    /// </summary>
    /// <value>A <see cref="string"/> representing the component name. This property is required for valid elements.</value>
    public string Name
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the <c>Member</c>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public string? Description
    {
        get => GetProperty<string>();
        set => SetDescription(value);
    }

    /// <summary>
    /// The name of the data type of the <c>Member</c>.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the data type name of the member. Default is <see cref="string.Empty"/>.
    /// This property is required for valid elements.
    /// </value>
    public string DataType
    {
        get => GetRequiredValue<string>();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The array dimension of the <c>Member</c>.
    /// </summary>
    /// <value>
    /// A <see cref="Dimensions"/> representing the array dimensions of the member. Default is <see cref="Dimensions.Empty"/>.
    /// </value>
    public Dimensions? Dimension
    {
        get => GetValue<Dimensions>();
        set => SetValue(value);
    }

    /// <summary>
    /// The radix value format of the <c>Member</c>.
    /// </summary>
    /// <value>
    /// A <see cref="Enums.Radix"/> representing the value type format of the member. Default is <see cref="Enums.Radix.Null"/>.
    /// </value>
    public Radix? Radix
    {
        get => GetValue<Radix>();
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the <c>Member</c>. 
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
    /// An flag indicating whether the <c>Member</c> is a hidden backing member for another boolean member of the data type. 
    /// </summary>
    /// <value><c>true</c> if member is a hidden member of the type, <c>false</c> if not, and <c>null</c> if the attribute
    /// does not exist for the underlying element.</value>
    /// <remarks>
    /// This attribute is not required for valid elements and importing to work. Logix will internally
    /// generate backing members for boolean type members.
    /// </remarks>
    public bool? Hidden
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// The name of the hidden member that is the backing member for this <c>Member</c>. 
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of the hidden member.
    /// <c>null</c> if the attribute does not exist for the underlying element
    /// </value>
    /// <remarks>
    /// This attribute is not required for valid elements and importing to work. Logix will internally
    /// generate backing members for boolean type members. Logix appends 10 Zs to the hidden
    /// members. 
    /// </remarks>
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
    /// <remarks>
    /// This attribute is not required for valid elements and importing to work. Logix will internally
    /// generate backing members for boolean type members.
    /// </remarks>
    public int? BitNumber
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
}