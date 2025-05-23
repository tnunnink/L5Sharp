namespace L5Sharp.Core;

internal class TagBaseAtomicBuilder<TAtomic>(Tag instance) : TagBaseBuilder<ITagBaseAtomicBuilder<TAtomic>>(instance),
    ITagBaseAtomicBuilder<TAtomic> where TAtomic : AtomicData, new()
{
    public ITagBaseAtomicBuilder<TAtomic> WithValue(TAtomic value)
    {
        Instance.Value = value;
        return this;
    }

    protected override ITagBaseAtomicBuilder<TAtomic> GetBuilder()
    {
        return this;
    }
}