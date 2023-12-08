using System.Xml.Linq;

// ReSharper disable InconsistentNaming RSLogix naming

namespace L5Sharp.Core;

/// <summary>
/// A predefined or built in data type used with counter instructions. 
/// </summary>
public sealed class COUNTER : StructureType
{
    /// <summary>
    /// Creates a new <see cref="COUNTER"/> data type instance.
    /// </summary>
    public COUNTER() : base(nameof(COUNTER))
    {
        PRE = new DINT();
        ACC = new DINT();
        CU = new BOOL();
        CD = new BOOL();
        DN = new BOOL();
        OV = new BOOL();
        UN = new BOOL();
    }

    /// <inheritdoc />
    public COUNTER(XElement element) : base(element)
    {
    }

    /// <inheritdoc />
    public override DataTypeClass Class => DataTypeClass.Predefined;

    /// <summary>
    /// Gets the <see cref="PRE"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public DINT PRE
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="ACC"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public DINT ACC
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="CU"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL CU
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="CD"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL CD
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="DN"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL DN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="OV"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL OV
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// Gets the <see cref="UN"/> member of the <see cref="COUNTER"/> data type.
    /// </summary>
    public BOOL UN
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }
}