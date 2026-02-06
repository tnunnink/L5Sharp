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
        ExternalAccess = Access.ReadWrite;
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
    /// Creates a new <see cref="DataTypeMember"/> initialized with the specified name and data type.
    /// </summary>
    /// <param name="name">The unique name of the member.</param>
    /// <param name="dataType">The name of the data type of the member.</param>
    public DataTypeMember(string name, string dataType) : this()
    {
        Name = name;
        DataType = dataType;
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
    /// A <see cref="Access"/> representing read/write access of the member.
    /// Default is <see cref="Access.ReadWrite"/>.
    /// </value>
    public Access? ExternalAccess
    {
        get => GetValue(Access.Parse);
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
    /// Attempts to retrieve the <see cref="DataType"/> definition associated with this member.
    /// </summary>
    /// <param name="definition">When this method returns, contains the <see cref="DataType"/> associated with this member if found; otherwise, null.</param>
    /// <returns>True if the definition is successfully retrieved; otherwise, false.</returns>
    /// <remarks>Typically, all member types are defined</remarks>
    public bool TryGetDefinition(out DataType definition)
    {
        // If this member has access to the document and can find the definition, return that.
        if (TryGetDocument(out var doc) && doc.TryGet(DataType, out definition))
        {
            return true;
        }

        definition = null!;
        return false;
    }

    /// <summary>
    /// Converts this <see cref="DataTypeMember"/> to a <see cref="LogixMember"/> instance with the appropriate
    /// data type and dimensions.
    /// </summary>
    /// <returns>
    /// A <see cref="LogixMember"/> containing the member name and corresponding data instance.
    /// The data instance is created based on the member's data type definition and dimension configuration.
    /// If the type is defined in the current L5X context, uses that definition. If the type is a registered predefined type,
    /// creates an instance of that type. Otherwise, creates a default <see cref="StructureData"/> instance.
    /// For members with dimensions, returns an <see cref="ArrayData"/> containing the appropriate data type.
    /// </returns>
    public LogixMember ToMember()
    {
        // Typically, a member type will be defined in the current L5X context if available.
        if (TryGetDefinition(out var definition))
        {
            var instance = definition.ToData();
            return new LogixMember(Name, Dimension.Length > 0 ? ArrayData.New(instance, Dimension) : instance);
        }

        // See if the type is registered (atomic/predefined or other registered user-defined).
        if (LogixType.TryCreate(DataType, out var registered))
        {
            return new LogixMember(Name, Dimension.Length > 0 ? ArrayData.New(registered, Dimension) : registered);
        }

        // At this point the definition has to be some custom type that is not available.
        // Just return a default structure or array based on the member config.
        var structure = new StructureData(DataType);
        return new LogixMember(Name, Dimension.Length > 0 ? ArrayData.New(structure, Dimension) : structure);
    }

    /// <summary>
    /// Calculates the total size in bytes occupied by this member.
    /// </summary>
    /// <returns>
    /// An <see cref="int"/> representing the size in bytes. Returns 0 for bit members that are backed by hidden members.
    /// For array members, returns the size of the element type multiplied by the total dimension length.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the member's data type is a complex type that is not defined in the current L5X context
    /// and cannot be resolved.
    /// </exception>
    /// <remarks>
    /// The calculation logic varies based on the member type:
    /// <list type="bullet">
    /// <item>Bit-backed members (those with Target and BitNumber) return 0 as they don't occupy additional space.</item>
    /// <item>Atomic types (BOOL, SINT, USINT, INT, UINT, DINT, UDINT, REAL, TIME32, LINT, ULINT, LREAL) use predefined sizes (1, 2, 4, or 8 bytes).</item>
    /// <item>Array members multiply the element type size by the total dimension length.</item>
    /// <item>Complex (structure) types require their definition to be available to determine size recursively.</item>
    /// </list>
    /// </remarks>
    public int GetSize()
    {
        // We need to handle bit members that have a backing member field.
        if (Target is not null && BitNumber is not null)
            return 0;

        // If the member is an atomic type, we can determine the size from the type name.
        if (LogixType.IsAtomic(DataType))
            return Dimension > 0 ? AtomicSize(DataType) * Dimension : AtomicSize(DataType);

        // This is a complex type. We need the nested definition to know.
        if (TryGetDefinition(out var definition))
            return definition.GetSize();

        // If nothing else, see if the type is a predefined or registered type that we can compute the size for.
        if (LogixType.TryCreate(DataType, out var registered))
            return registered.GetSize();

        // We can't know the size of an undefined complex type. Report as error instead of silently miscalculating.
        throw new InvalidOperationException($"Unable to determine size of undefined type {DataType} for member {Name}");

        // Get the size of the atomic type.
        int AtomicSize(string name)
        {
            return name switch
            {
                nameof(BOOL) => 0,
                nameof(SINT) or nameof(USINT) => 1,
                nameof(INT) or nameof(UINT) => 2,
                nameof(DINT) or nameof(UDINT) or nameof(REAL) or nameof(TIME32) => 4,
                _ => 8
            };
        }
    }

    /// <inheritdoc />
    public override string ToString() => Name;
}