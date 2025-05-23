namespace L5Sharp.Core;

internal class TagBaseTypeBuilder(string name) : ITagBaseTypeBuilder
{
    public ITagBaseAtomicBuilder<TAtomic> AsAtomic<TAtomic>() where TAtomic : AtomicData, new()
    {
        var value = new TAtomic();
        var instance = new Tag(name, value);
        return new TagBaseAtomicBuilder<TAtomic>(instance);
    }

    public ITagBaseStructureBuilder AsStructure(string dataType)
    {
        var value = LogixData.Create(dataType);
        var instance = new Tag(name, value);
        return new TagBaseStructureBuilder(instance);
    }

    public ITagBaseAtomicArrayBuilder<TAtomic> AsArray<TAtomic>(Dimensions dimensions) where TAtomic : AtomicData, new()
    {
        var value = ArrayData.New<TAtomic>(dimensions);
        var instance = new Tag(name, value);
        return new TagBaseAtomicArrayBuilder<TAtomic>(instance);
    }

    public ITagBaseStructureArrayBuilder AsArray(string dataType, Dimensions dimensions)
    {
        var value = new ArrayData<LogixData>(dataType, dimensions);
        var instance = new Tag(name, value);
        return new TagBaseStructureArrayBuilder(instance);
    }

    public ITagAliasBuilder AsAlias(TagName alias)
    {
        var instance = new Tag
        {
            Name = name,
            AliasFor = alias,
            TagType = TagType.Alias
        };

        return new TagAliasBuilder(instance);
    }
}