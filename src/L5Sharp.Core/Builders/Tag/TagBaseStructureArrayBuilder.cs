using System;

namespace L5Sharp.Core;

internal class TagBaseStructureArrayBuilder(Tag instance)
    : TagBaseBuilder<ITagBaseStructureArrayBuilder>(instance), ITagBaseStructureArrayBuilder
{
    public ITagBaseStructureArrayBuilder WithElement(int index, Action<ITagMemberStructureBuilder> action)
    {
        var element = Instance[$"[{index}]"];
        var builder = new TagMemberStructureBuilder(element);
        action.Invoke(builder);
        return this;
    }

    protected override ITagBaseStructureArrayBuilder GetBuilder()
    {
        return this;
    }
}