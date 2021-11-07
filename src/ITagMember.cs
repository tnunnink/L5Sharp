using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    /*public interface ITagMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        string FullName { get; }
        new string DataType { get; }
        object Value { get; }
        public IEnumerable<ITagMember<IDataType>> Members { get; }
        ILogixComponent Parent { get; }
        bool IsValueMember { get; }
        bool IsArrayMember { get; }
        bool IsArrayElement { get; }
        bool IsStructureMember { get; }
        void SetDescription(string description);
        void SetRadix(Radix radix);
        void SetValue(object value);
        ITagMember<IDataType> GetMember(string name);
        IEnumerable<string> GetMembersNames();
    }*/
    
    public interface ITagMember<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        string FullName { get; }
        new string DataType { get; }
        ILogixComponent Parent { get; }
        object GetValue();
        void SetValue(object value);
        IEnumerable<ITagMember<IDataType>> GetMembers();
        IEnumerable<string> GetMembersNames();
        ITagMember<IDataType> GetMember(Func<TDataType, IDataType> expression);
        ITagMember<TType> GetMember<TType>(Func<TDataType, TType> expression) where TType : IDataType;
        void SetRadix(Radix radix);
        void SetDescription(string description);
    }
}