namespace L5Sharp.Core;

internal class TagMemberStructureBuilder(Tag member) : ITagMemberStructureBuilder
{
    public ITagMemberTypeBuilder<ITagMemberStructureBuilder> AddMember(string name)
    {
        return new TagMemberTypeBuilder<ITagMemberStructureBuilder>(member, name, this);
    }

    public ITagMemberStructureBuilder WithValue(TagName name, LogixData value)
    {
        member[name].Value = value;
        return this;
    }

    public ITagMemberStructureBuilder WithDescription(string description)
    {
        member.Description = description;
        return this;
    }
}