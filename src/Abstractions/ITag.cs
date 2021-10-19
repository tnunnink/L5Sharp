using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITag : ITagMember
    {
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; }
        IComponent Parent { get; }
        void UpdateDataType(IDataType dataType);
        ITag ChangeTagType(TagType type);
        IEnumerable<string> ListMembersNames();
    }

    public interface ITag<T> : ITag where T : IDataType, new()
    {
        ITag<T> As();
    }
}