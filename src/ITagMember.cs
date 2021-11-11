using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITagMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        string FullName { get; }
        new string DataType { get; }
        ILogixComponent Parent { get; }
        TDataType GetData();
        void SetData(IDataType data);
        IEnumerable<string> GetMembersList();
        IEnumerable<string> GetDeepMembersList();
        IEnumerable<ITagMember<IDataType>> GetMembers();
        ITagMember<IDataType> GetMember(string name);
        ITagMember<IDataType> GetElement(ushort index);
        
        ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType;
        
        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic;

        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic;

        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description)
            where TAtomic : IAtomic;
        
        void SetElement<TAtomic>(ushort index, TAtomic value) where TAtomic : IAtomic;
        void SetElement(ushort index, Radix radix);
        void SetElement(ushort index, string description);
    }
}