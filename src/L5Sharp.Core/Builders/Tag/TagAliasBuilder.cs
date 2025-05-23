namespace L5Sharp.Core;

internal class TagAliasBuilder(Tag instance) : ITagAliasBuilder
{
    public Tag Build()
    {
        return instance;
    }

    public ITagBuilder WithDescription(string description)
    {
        instance.Description = description;
        return this;
    }
}