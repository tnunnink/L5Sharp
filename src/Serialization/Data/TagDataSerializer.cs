namespace L5Sharp.Serialization.Data;

/// <summary>
/// A static class containing instances of all <see cref="ILogixSerializer{T}"/> that are responsible for serialization
/// of tag data structures. The purpose of this class is so that each serializer can refer to these static properties,
/// and not have to instantiate them within the serializer themselves. Since some reference each other, there are some
/// circular dependencies. For instance, <see cref="ArraySerializer"/> depends on <see cref="StructureSerializer"/>,
/// which in turn may rely on <see cref="ArraySerializer"/>. 
/// </summary>
public static class TagDataSerializer
{
    /// <summary>
    /// Gets the <see cref="AlarmAnalogSerializer"/> instance.
    /// </summary>
    public static readonly AlarmAnalogSerializer AlarmAnalog = new();
    
    /// <summary>
    /// Gets the <see cref="AlarmDataSerializer"/> instance.
    /// </summary>
    public static readonly AlarmDataSerializer AlarmData = new();
    
    /// <summary>
    /// Gets the <see cref="AlarmDigitalSerializer"/> instance.
    /// </summary>
    public static readonly AlarmDigitalSerializer AlarmDigital = new();
    
    /// <summary>
    /// Gets the <see cref="ArrayMemberSerializer"/> instance.
    /// </summary>
    public static readonly ArrayMemberSerializer ArrayMember = new();
    
    /// <summary>
    /// Gets the <see cref="ArraySerializer"/> instance.
    /// </summary>
    public static readonly ArraySerializer Array = new();
    
    /// <summary>
    /// Gets the <see cref="DataValueMemberSerializer"/> instance.
    /// </summary>
    public static readonly DataValueMemberSerializer DataValueMember = new();
    
    /// <summary>
    /// Gets the <see cref="DataValueSerializer"/> instance.
    /// </summary>
    public static readonly DataValueSerializer DataValue = new();
    
    /// <summary>
    /// Gets the <see cref="DecoratedDataSerializer"/> instance.
    /// </summary>
    public static readonly DecoratedDataSerializer DecoratedData = new();
    
    /// <summary>
    /// Gets the <see cref="FormattedDataSerializer"/> instance.
    /// </summary>
    public static readonly FormattedDataSerializer FormattedData = new();
    
    /// <summary>
    /// Gets the <see cref="MemberSerializer"/> instance.
    /// </summary>
    public static readonly MemberSerializer Member = new();
    
    /// <summary>
    /// Gets the <see cref="StringDataSerializer"/> instance.
    /// </summary>
    public static readonly StringDataSerializer StringData = new();
    
    /// <summary>
    /// Gets the <see cref="StringMemberSerializer"/> instance.
    /// </summary>
    public static readonly StringMemberSerializer StringMember = new();
    
    /// <summary>
    /// Gets the <see cref="StringStructureSerializer"/> instance.
    /// </summary>
    public static readonly StringStructureSerializer StringStructure = new();
    
    /// <summary>
    /// Gets the <see cref="StructureMemberSerializer"/> instance.
    /// </summary>
    public static readonly StructureMemberSerializer StructureMember = new();
    
    /// <summary>
    /// Gets the <see cref="StructureSerializer"/> instance.
    /// </summary>
    public static readonly StructureSerializer Structure = new();
}