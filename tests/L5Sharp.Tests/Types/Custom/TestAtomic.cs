using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Tests.Types.Custom;

public sealed class TestAtomic : AtomicType
{
    private readonly int _value;
    
    public TestAtomic(int value)
    {
        _value = value;
    }

    public override string Name => nameof(TestAtomic);
    public override LogixType Set(LogixType type) => type;
    public override Radix Radix => Radix.Decimal;
    public override byte[] GetBytes() => BitConverter.GetBytes(_value);
}