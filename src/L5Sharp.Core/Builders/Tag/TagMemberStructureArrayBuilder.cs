using System;

namespace L5Sharp.Core;

internal class TagMemberStructureArrayBuilder(Tag member) : ITagMemberStructureArrayBuilder
{
    public ITagMemberStructureArrayBuilder WithElement(int index, Action<ITagMemberStructureBuilder> action)
    {
        var element = member[$"[{index}]"];
        var builder = new TagMemberStructureBuilder(element);
        action.Invoke(builder);
        return this;
    }

    public ITagMemberStructureArrayBuilder WithDescription(string description)
    {
        member.Description = description;
        return this;
    }
}