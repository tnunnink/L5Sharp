using System.Collections.Generic;

namespace L5Sharp.Abstractions
{
    public interface ITagMember : IMember
    {
        object Value { get; }
        public ITagMember Parent { get; }
        public IEnumerable<ITagMember> Members { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
    }
}