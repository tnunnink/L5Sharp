using System;

namespace L5Sharp.Core;

internal class TagMemberTypeBuilder<TBuilder>(Tag instance, string name, TBuilder parent)
    : ITagMemberTypeBuilder<TBuilder>
{
    public ITagMemberAtomicBuilder<TAtomic, TBuilder> AsAtomic<TAtomic>() where TAtomic : AtomicData, new()
    {
        var value = new TAtomic();
        instance.AddMember(name, value);
        var member = instance[name];
        return new TagMemberAtomicBuilder<TAtomic, TBuilder>(member, parent);
    }

    public TBuilder AsStructure(string dataType, Action<ITagMemberStructureBuilder> action)
    {
        var value = LogixType.CreateOrDefault(dataType);
        instance.AddMember(name, value);
        var member = instance[name];
        var builder = new TagMemberStructureBuilder(member);
        action.Invoke(builder);
        return parent;
    }

    public TBuilder AsArray<TAtomic>(Dimensions dimensions, Action<ITagMemberAtomicArrayBuilder<TAtomic>> action)
        where TAtomic : AtomicData, new()
    {
        var value = ArrayData.New<TAtomic>(dimensions);
        instance.AddMember(name, value);
        var member = instance[name];
        var builder = new TagMemberAtomicArrayBuilder<TAtomic>(member);
        action.Invoke(builder);
        return parent;
    }

    public TBuilder AsArray(string dataType, Dimensions dimensions, Action<ITagMemberStructureArrayBuilder> action)
    {
        var value = ArrayData.New(dataType, dimensions);
        instance.AddMember(name, value);
        var member = instance[name];
        var builder = new TagMemberStructureArrayBuilder(member);
        action.Invoke(builder);
        return parent;
    }
}