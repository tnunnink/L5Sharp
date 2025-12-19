using System;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// An element of the <see cref="DataType"/> that makes up the structure of the user-defined type.
/// </summary>
/// <footer>
/// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
/// `Logix 5000 Controllers Import/Export`</a> for more information.
/// </footer>
[LogixElement(L5XName.Member)]
public class DataTypeMember : LogixObject<DataTypeMember>
{
    /// <summary>
    /// Creates a new <see cref="DataTypeMember"/> with default values.
    /// </summary>
    public DataTypeMember() : base(L5XName.Member)
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
        get => GetRequiredValue();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The description of the <c>Member</c>.
    /// </summary>
    /// <value>A <see cref="string"/> containing the component description if it exists; Otherwise, <c>null</c>.</value>
    public string? Description
    {
        get => GetProperty();
        set => SetProperty(value);
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
        get => GetRequiredValue();
        set => SetRequiredValue(value);
    }

    /// <summary>
    /// The array dimension of the <c>Member</c>.
    /// </summary>
    /// <value>
    /// A <see cref="Dimensions"/> representing the array dimensions of the member. Default is <see cref="Dimensions.Empty"/>.
    /// </value>
    public Dimensions Dimension
    {
        get => GetValue(Dimensions.Parse) ?? Dimensions.Empty;
        set => SetValue(value);
    }

    /// <summary>
    /// The radix value format of the <c>Member</c>.
    /// </summary>
    /// <value>
    /// A <see cref="Core.Radix"/> representing the value type format of the member. Default is <see cref="L5Sharp.Core.Radix.Null"/>.
    /// </value>
    public Radix? Radix
    {
        get => GetValue(Radix.Parse);
        set => SetValue(value?.Value);
    }

    /// <summary>
    /// The external access of the <c>Member</c>. 
    /// </summary>
    /// <value>
    /// A <see cref="Core.ExternalAccess"/> representing read/write access of the member.
    /// Default is <see cref="L5Sharp.Core.ExternalAccess.ReadWrite"/>.
    /// </value>
    public ExternalAccess? ExternalAccess
    {
        get => GetValue(ExternalAccess.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// A flag indicating whether the <c>Member</c> is a hidden backing member for another boolean member of the data type. 
    /// </summary>
    /// <value><c>true</c> if member is a hidden member of the type, <c>false</c> if not, and <c>null</c> if the attribute
    /// does not exist for the underlying element.</value>
    /// <remarks>
    /// This attribute is not required for valid elements and importing to work. Logix will internally
    /// generate backing members for boolean type members.
    /// </remarks>
    public bool Hidden
    {
        get => GetValue(bool.Parse);
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
        get => GetValue();
        set => SetValue(value);
    }

    /// <summary>
    /// The bit number of the hidden member that this boolean member maps to or is backed by.
    /// </summary>
    /// <value>A zero-based integer representing the backing a bit of the hidden member which corresponds to this member.
    /// <c>null</c> if the attribute does not exist for the underlying element</value>
    /// <remarks>
    /// This attribute is not required for valid elements and importing to work. Logix will internally
    /// generate backing members for boolean type members.
    /// </remarks>
    public int? BitNumber
    {
        get => GetValue(int.Parse);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the parent data type component that this member is contained by. If this member is not contained or
    /// attached to a L5X document, this returns null.
    /// </summary>
    /// <value>A <see cref="Core.DataType"/> representing the parent type for this member.</value>
    public DataType? Parent => GetAncestor<DataType>();

    /// <summary>
    /// Gets the <see cref="Core.DataType"/> component that defines the structure of this <see cref="DataTypeMember"/>.
    /// </summary>
    /// <remarks>
    /// This uses the attached L5X to retrieve the data type component this member references by name. If this member is
    /// not attached or the L5X does not container the data type reference, this will return a default component with the
    /// name specified by this member.
    /// </remarks>
    public DataType Definition => GetDefinition();

    /// <summary>
    /// Converts the current instance of <see cref="DataTypeMember"/> to a <see cref="LogixMember"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="LogixMember"/> based on the properties of the <see cref="DataTypeMember"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the Name or DataType is null or empty.</exception>
    public LogixMember ToMember()
    {
        var isArray = Dimension.Length > 0;

        //If the type is registered, we can create the instance using the registered factory.
        if (LogixType.TryCreate(DataType, out var registered))
        {
            var value = isArray ? ArrayData.New(registered, Dimension) : registered;
            return new LogixMember(Name, value);
        }

        //If not, we can try to get the data type definition from the l5X if attached
        //and use that to recursively build up the complex type.
        var data = GetDefinition().ToData();
        var structure = isArray ? ArrayData.New(data, Dimension) : data;
        return new LogixMember(Name, structure);
    }

    /// <summary>
    /// Attempts to get the <see cref="Core.DataType"/> object that represents this member's type definition.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private DataType GetDefinition()
    {
        if (TryGetDocument(out var doc) && doc.TryGet<DataType>(DataType, out var definition))
        {
            return definition;
        }

        return new DataType(DataType);
    }

    /// <inheritdoc />
    public override string ToString() => Name;
}