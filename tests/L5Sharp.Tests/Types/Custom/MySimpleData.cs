using System.Xml.Linq;

namespace L5Sharp.Tests.Types.Custom;

public class MySimpleData : ComplexData
{
    public MySimpleData() : base(nameof(MySimpleData))
    {
        M1 = new BOOL();
        M2 = new SINT();
        M3 = new INT();
        M4 = new DINT();
        M5 = new LINT();
        M6 = new REAL();
    }

    public MySimpleData(XElement element) : base(element)
    {
    }

    public BOOL M1
    {
        get => GetMember<BOOL>();
        set => SetMember(value);
    }

    public SINT M2
    {
        get => GetMember<SINT>();
        set => SetMember(value);
    }

    public INT M3
    {
        get => GetMember<INT>();
        set => SetMember(value);
    }

    public DINT M4
    {
        get => GetMember<DINT>();
        set => SetMember(value);
    }

    public LINT M5
    {
        get => GetMember<LINT>();
        set => SetMember(value);
    }

    public REAL M6
    {
        get => GetMember<REAL>();
        set => SetMember(value);
    }
}