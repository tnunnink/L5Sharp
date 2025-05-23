namespace L5Sharp.Core;

internal class TagBaseAtomicArrayBuilder<TAtomic>(Tag instance)
    : TagBaseBuilder<ITagBaseAtomicArrayBuilder<TAtomic>>(instance),
        ITagBaseAtomicArrayBuilder<TAtomic> where TAtomic : AtomicData, new()
{
    /*public ITagBaseArrayBuilder WithStructure(int index, Action<ITagMemberStructureBuilder> action)
    {
        var element = instance[$"[{index}]"];
        var builder = new TagMemberStructureBuilder(element);
        action.Invoke(builder);
        return this;
    }*/

    public ITagBaseAtomicArrayBuilder<TAtomic> WithElement(int index, TAtomic value)
    {
        Instance[$"[{index}]"].Value = value;
        return this;
    }

    protected override ITagBaseAtomicArrayBuilder<TAtomic> GetBuilder()
    {
        return this;
    }
}