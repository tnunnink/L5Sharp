using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITagMember : IMember
    {
        string FullName { get; }
        object Value { get; }
        public IEnumerable<ITagMember> Members { get; }
        ILogixComponent Parent { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
        void SetDescription(string description);
        void SetRadix(Radix radix);
        void SetValue(object value);
        ITagMember GetMember(string name);
        IEnumerable<string> GetMembersNames();
    }
    
    public interface ITagMember<out TDataType> : ITagMember where TDataType : IDataType
    {
        
    }
}