using System.Xml.Linq;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Types.Predefined;

/// <summary>
/// A predefined or built in data type used with message instructions. 
/// </summary>
public sealed class MESSAGE : StructureType
{
    /// <summary>
    /// Creates a new <see cref="MESSAGE"/> data type instance.
    /// </summary>
    public MESSAGE() : base(nameof(MESSAGE))
    {
    }
    
    /// <inheritdoc />
    public MESSAGE(XElement element) : base(element)
    {
    
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>  /// <summary>
    /// Gets the <see cref="MessageType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string MessageType
    {
        get => GetValue<string>() ?? throw new L5XException(Element);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="RequestedLength"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public int RequestedLength
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectedFlag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public int ConnectedFlag  
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ConnectionPath"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? ConnectionPath 
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CommTypeCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public int CommTypeCode
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ServiceCode"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? ServiceCode
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="ObjectType"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? ObjectType 
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="TargetObject"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? TargetObject
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="AttributeNumber"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? AttributeNumber
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LocalIndex"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public int LocalIndex 
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="DestinationTag"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public string? DestinationTag 
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="CacheConnections"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public bool CacheConnections
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the <see cref="LargePacketUsage"/> value of the <see cref="MESSAGE"/> parameters.
    /// </summary>
    public bool LargePacketUsage
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    /*/// <summary>
    /// Gets the <see cref="Flags"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT Flags { get; set; } = new(Radix.Hex);

    /// <summary>
    /// Gets the <see cref="EW"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL EW { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="ER"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL DN { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="DN"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL ER { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="ST"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL ST { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="EN"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL EN { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="TO"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL TO { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="EN_CC"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public BOOL EN_CC { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="ERR"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT ERR { get; set; } = new(Radix.Hex);

    /// <summary>
    /// Gets the <see cref="EXERR"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public DINT EXERR { get; set; } = new(Radix.Hex);

    /// <summary>
    /// Gets the <see cref="ERR_SRC"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public SINT ERR_SRC { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="DN_LEN"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT DN_LEN { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="REQ_LEN"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT REQ_LEN { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="DestinationLink"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT DestinationLink { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="DestinationNode"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT DestinationNode { get; set; } = new(Radix.Octal);

    /// <summary>
    /// Gets the <see cref="SourceLink"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT SourceLink { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="MessageClass"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT MessageClass { get; set; } = new(Radix.Hex);

    /// <summary>
    /// Gets the <see cref="Attribute"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public INT Attribute { get; set; } = new(Radix.Hex);

    /// <summary>
    /// Gets the <see cref="Instance"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public DINT Instance { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="LocalIndex"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public DINT LocalIndex { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="Channel"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public SINT Channel { get; set; } = new(Radix.Ascii);

    /// <summary>
    /// Gets the <see cref="Rack"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public SINT Rack { get; set; } = new(Radix.Octal);

    /// <summary>
    /// Gets the <see cref="Group"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public SINT Group { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="Slot"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public SINT Slot { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="Path"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public STRING Path { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="RemoteIndex"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public DINT RemoteIndex { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="RemoteElement"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public STRING RemoteElement { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="UnconnectedTimeout"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public DINT UnconnectedTimeout { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="ConnectionRate"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public DINT ConnectionRate { get; set; } = new();

    /// <summary>
    /// Gets the <see cref="TimeoutMultiplier"/> member of the <see cref="MESSAGE"/> data type.
    /// </summary>
    public SINT TimeoutMultiplier { get; set; } = new();*/
}