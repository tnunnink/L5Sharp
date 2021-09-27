using L5Sharp.Enumerations;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
{
    public interface ITag
    {
        string Name { get; }
        IDataType DataType { get; }
        Dimensions Dimensions { get; }
        Radix Radix { get; }
        ExternalAccess ExternalAccess { get; }
        string Description { get; }
        TagType TagType { get; }
        TagUsage Usage { get; }
        Scope Scope { get; }
        Program Program { get; }
        string AliasFor { get; }
        bool Constant { get; }
        object Value { get; }
    }
}