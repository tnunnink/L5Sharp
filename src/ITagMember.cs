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
        IAtomic GetData();
        void SetData(IAtomic value);
        void SetRadix(Radix radix);
        void SetDescription(string description);
        IEnumerable<string> GetMembersNames();
        IEnumerable<ITagMember<IDataType>> GetMembers();
        ITagMember<IDataType> GetMember(string name);
        
        ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression)
            where TType : IDataType;
        
        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value)
            where TAtomic : IAtomic;
        
        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic;
        
        void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description)
            where TAtomic : IAtomic;
    }
}