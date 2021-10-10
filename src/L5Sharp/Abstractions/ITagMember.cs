using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITagMember : IComponent
    {
        string FullName { get; }
        public string DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; set; }
        public ExternalAccess ExternalAccess { get; }
        object Value { get; set; }
        public IEnumerable<ITagMember> Members { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
        ITagMember GetMember(string name);
    }
}