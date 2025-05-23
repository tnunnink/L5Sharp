namespace L5Sharp.Core;

internal class TagBaseStructureBuilder(Tag instance)
    : TagBaseBuilder<ITagBaseStructureBuilder>(instance), ITagBaseStructureBuilder
{
    public ITagMemberTypeBuilder<ITagBaseStructureBuilder> AddMember(string name)
    {
        return new TagMemberTypeBuilder<ITagBaseStructureBuilder>(Instance, name, this);
    }

    public ITagBaseStructureBuilder WithValue(TagName name, LogixData value)
    {
        Instance[name].Value = value;
        return this;
    }

    protected override ITagBaseStructureBuilder GetBuilder()
    {
        return this;
    }
}