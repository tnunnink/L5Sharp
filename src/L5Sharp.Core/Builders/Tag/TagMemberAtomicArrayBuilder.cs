namespace L5Sharp.Core;

internal class TagMemberAtomicArrayBuilder<TAtomic>(Tag member)
    : ITagMemberAtomicArrayBuilder<TAtomic> where TAtomic : AtomicData
{
    public ITagMemberAtomicArrayBuilder<TAtomic> WithElement(int index, TAtomic value)
    {
        member[$"[{index}]"].Value = value;
        return this;
    }

    public ITagMemberAtomicArrayBuilder<TAtomic> WithDescription(string description)
    {
        member.Description = description;
        return this;
    }
}