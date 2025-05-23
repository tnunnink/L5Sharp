namespace L5Sharp.Core;

internal class TagMemberAtomicBuilder<TAtomic, TBuilder>(Tag member, TBuilder parent)
    : ITagMemberAtomicBuilder<TAtomic, TBuilder> where TAtomic : AtomicData, new()
{
    public TBuilder WithValue(TAtomic value)
    {
        member.Value = value;
        return parent;
    }

    public ITagMemberAtomicBuilder<TAtomic, TBuilder> WithDescription(string description)
    {
        member.Description = description;
        return this;
    }
}