using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITagMember : IComponent
    {
        public string DataType { get; }
        public Dimensions Dimension { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public string Description { get; }
        object Value { get; }
        public IEnumerable<ITagMember> Members { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
        ITagMember GetMember(string name);
    }
}