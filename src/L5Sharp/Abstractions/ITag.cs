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
        IComponent Container { get; }
        ITag ChangeTagType(TagType type);
        IEnumerable<string> ListMembers();
    }
}