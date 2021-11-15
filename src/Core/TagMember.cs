using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    public class TagMember<TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        private readonly IMember<TDataType> _member;
        private readonly ITagMember<IDataType> _parent;

        internal TagMember(IMember<TDataType> member, ILogixComponent parent)
        {
            _member = member ?? 
                      throw new ArgumentNullException(nameof(member), "Member can not be null");
            _parent = (ITagMember<IDataType>)parent ??
                      throw new ArgumentNullException(nameof(parent), "TagMember must have parent");
        }

        public ComponentName Name => _member.Name;
        
        public string FullName => Parent == null ? Name.ToString()
            : Parent is ITagMember<IDataType> tagMember && !tagMember.Dimensions.AreEmpty ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";
        
        public string DataType => _member.DataType.Name;
        TDataType IMember<TDataType>.DataType => _member.DataType;
        public Dimensions Dimensions => _member.Dimensions;
        public Radix Radix => _member.Radix;
        
        public ExternalAccess ExternalAccess =>
            _member.ExternalAccess.IsMoreRestrictive(_parent.ExternalAccess)
                ? _member.ExternalAccess
                : _parent.ExternalAccess;

        public string Description => _member.Description;

        public ILogixComponent Parent => _parent;

        public TDataType GetData()
        {
            return _member.Dimensions.AreEmpty ? _member.DataType : default;
        }

        public void SetData(IAtomic value)
        {
            if (!(_member.DataType is IAtomic atomic))
                throw new InvalidTagDataException(_member.DataType, value);
            
            atomic.SetValue(value);
        }

        public void SetRadix(Radix radix) => _member.SetRadix(radix);

        public void SetDescription(string description) => _member.SetDescription(description);

        public IEnumerable<string> GetMemberList() => _member.DataType.GetMembers().Select(m => m.Name.ToString());

        public IEnumerable<string> GetDeepMembersList() => _member.DataType.GetMemberNames();

        public IEnumerable<ITagMember<IDataType>> GetMembers() => 
            _member.DataType.GetMembers().Select(m => new TagMember<IDataType>(m, this));

        public ITagMember<IDataType> GetMember(string name)
        {
            var member = _member.DataType.GetMember(name);
            return member != null ? new TagMember<IDataType>(member, this) : null;
        }
        
        public ITagMember<IDataType> GetElement(ushort index)
        {
            return index < Dimensions.Length && _member is IArrayMember<TDataType> arrayMember
                ? new TagMember<IDataType>(arrayMember[index], this) 
                : null;
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression) 
            where TType : IDataType
        {
            var member = expression.Invoke(_member.DataType);
            return member != null ? new TagMember<TType>(member, this) : null;
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value) 
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");
            
            var member = expression.Invoke(_member.DataType);
            member.DataType.SetValue(value);
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");
            
            var member = expression.Invoke(_member.DataType);
            member.SetRadix(radix);
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description) where TAtomic : IAtomic
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Expression can not be null");
            
            var member = expression.Invoke(_member.DataType);
            member.SetDescription(description);
        }

        public void SetElement<TAtomic>(ushort index, TAtomic value) where TAtomic : IAtomic
        {
            if (index >= Elements.Length) return;

            var element = Elements[index];

            if (element.DataType is IAtomic atomic)
                atomic.SetValue(value);
        }

        public void SetElement(ushort index, Radix radix)
        {
            if (index >= Elements.Length) return;
            
            var element = Elements[index];
            element.SetRadix(radix);
        }

        public void SetElement(ushort index, string description)
        {
            if (index >= Elements.Length) return;
            
            var element = Elements[index];
            element.SetDescription(description);
        }

        private static string GetName(ILogixComponent member)
        {
            if (member is ITagMember<IDataType> tagMember)
            {
                return tagMember.Parent is ITagMember<IDataType> parentMember
                    ? parentMember.Dimensions.Length > 0
                        ? $"{GetName(tagMember.Parent)}{member.Name}"
                        : $"{GetName(tagMember.Parent)}.{member.Name}"
                    : member.Name.ToString();
            }

            return member.Name;
        }
    }
}