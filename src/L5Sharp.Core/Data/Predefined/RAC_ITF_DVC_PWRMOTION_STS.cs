using System.Xml.Linq;
// Auto-generated file
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart

namespace L5Sharp.Core;

/// <summary>
/// Represents a <c>RAC_ITF_DVC_PWRMOTION_STS</c> data type structure.
/// </summary>
[LogixData("RAC_ITF_DVC_PWRMOTION_STS")]
public sealed partial class RAC_ITF_DVC_PWRMOTION_STS : StructureData
{
    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> instance initialized with default values.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_STS() : base("RAC_ITF_DVC_PWRMOTION_STS")
    {
        eState = new DINT();
        FirstWarning = new RAC_EVENT();
        FirstFault = new RAC_EVENT();
        eCmdFail = new DINT();
        bSts = new INT();
        Physical = new BOOL();
        Virtual = new BOOL();
        Connected = new BOOL();
        Available = new BOOL();
        Warning = new BOOL();
        Faulted = new BOOL();
        Ready = new BOOL();
        Active = new BOOL();
        ZeroSpeed = new BOOL();
        NoMotion = new BOOL();
    }

    /// <summary>
    /// Creates a new <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> instance initialized with the provided element.
    /// </summary>
    public RAC_ITF_DVC_PWRMOTION_STS(XElement element) : base(element)
    {
    }

    /// <summary>
    /// The <c>eState</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public DINT eState
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FirstWarning</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public RAC_EVENT FirstWarning
    {
        get => GetMember<RAC_EVENT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>FirstFault</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public RAC_EVENT FirstFault
    {
        get => GetMember<RAC_EVENT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>eCmdFail</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public DINT eCmdFail
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>bSts</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public INT bSts
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Physical</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Physical
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Virtual</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Virtual
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Connected</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Connected
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Available</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Available
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Warning</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Warning
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Faulted</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Faulted
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Ready</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Ready
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>Active</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL Active
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>ZeroSpeed</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL ZeroSpeed
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    /// <summary>
    /// The <c>NoMotion</c> member of the <see cref="RAC_ITF_DVC_PWRMOTION_STS"/> data type.
    /// </summary>
    public BOOL NoMotion
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

}
