using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITagMember<out TDataType> : IComponent, ISetComponentDescription where TDataType : IDataType
    {
        string FullName { get; }
        public TDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        object Value { get; }
        public IEnumerable<ITagMember<IDataType>> Members { get; }
        IComponent Parent { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
        void SetRadix(Radix radix);
        void SetValue(object value);
        ITagMember<IDataType> GetMember(string name);
        IEnumerable<string> GetMembersNames();
    }
}