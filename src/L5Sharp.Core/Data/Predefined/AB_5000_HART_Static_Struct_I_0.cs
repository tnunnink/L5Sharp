using System.Xml.Linq;

// Auto-generated type definition
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>AB_5000_HART_Static_Struct_I_0</c> data type structure.
/// </summary>
[LogixData("AB:5000_HART_Static_Struct:I:0")]
public sealed partial class AB_5000_HART_Static_Struct_I_0 : StructureData
{
    /// <summary>
    /// Creates a new <see cref="AB_5000_HART_Static_Struct_I_0"/> instance initialized with default values.
    /// </summary>
    public AB_5000_HART_Static_Struct_I_0() : base("AB:5000_HART_Static_Struct:I:0")
    {
        Fault = new BOOL();
        PVUnit = new USINT();
        HARTRevision = new USINT();
        HARTTagName = new AB_5000_String32_Struct_I_0();
        Descriptor = new AB_5000_String16_Struct_I_0();
        PVAtSignal4 = new REAL();
        PVAtSignal20 = new REAL();
        AdditionalDeviceStatus = new ArrayData<SINT>(25);
    }
    
    /// <summary>
    /// Creates a new <see cref="AB_5000_HART_Static_Struct_I_0"/> instance initialized with the provided element.
    /// </summary>
    public AB_5000_HART_Static_Struct_I_0(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>Fault</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public BOOL Fault
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVUnit</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public USINT PVUnit
    {
        get => GetMember<USINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HARTRevision</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public USINT HARTRevision
    {
        get => GetMember<USINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>HARTTagName</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public AB_5000_String32_Struct_I_0 HARTTagName
    {
        get => GetMember<AB_5000_String32_Struct_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Descriptor</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public AB_5000_String16_Struct_I_0 Descriptor
    {
        get => GetMember<AB_5000_String16_Struct_I_0>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVAtSignal4</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public REAL PVAtSignal4
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>PVAtSignal20</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public REAL PVAtSignal20
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>AdditionalDeviceStatus</c> member of the <see cref="AB_5000_HART_Static_Struct_I_0"/> data type.
    /// </summary>
    public ArrayData<SINT> AdditionalDeviceStatus
    {
        get => GetArray<SINT>();
        set => SetArray(value);
    }
}