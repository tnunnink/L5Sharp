using System;
using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    public class TagMember<TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        private readonly IMember<TDataType> _member;
        private readonly TagMember<IDataType> _parent;
        private string _description;

        internal TagMember(IMember<TDataType> member, ILogixComponent parent)
        {
            _member = member ?? 
                      throw new ArgumentNullException(nameof(member), "Member can not be null");
            _parent = (TagMember<IDataType>)parent ??
                      throw new ArgumentNullException(nameof(parent), "TagMember must have parent");
        }

        public string Name => _member.Name;
        
        public string FullName => Parent == null ? Name
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

        public IMember<TDataType>[] Elements => _member.Elements;

        public string Description => string.IsNullOrEmpty(_description)
            ? $"{_parent.Description} {_member?.Description}"
            : _description;

        public ILogixComponent Parent => _parent;

        public IAtomic GetData()
        {
            return _member.DataType is IAtomic atomic ? atomic : null;
        }

        public void SetData(IAtomic value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!(_member.DataType is IAtomic atomic))
                throw new ComponentNotConfigurableException("Value", GetType(),
                    "Tag member is not atomic. Can only set values for atomic types");

            atomic.SetValue(value);
        }

        public void SetRadix(Radix radix) => _member.SetRadix(radix);

        public void SetDescription(string description)
        {
            _description = description;
        }

        public IEnumerable<string> GetMembersNames()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITagMember<IDataType>> GetMembers()
        {
            throw new NotImplementedException();
        }

        public ITagMember<IDataType> GetMember(string name)
        {
            var member = _member.DataType.GetMember(name);
            return member != null ? new TagMember<IDataType>(member, this) : null;
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression) 
            where TType : IDataType
        {
            var member = expression.Invoke(_member.DataType);
            return member != null ? new TagMember<TType>(member, this) : null;
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, TAtomic value) where TAtomic : IAtomic
        {
            throw new NotImplementedException();
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, Radix radix)
            where TAtomic : IAtomic
        {
            throw new NotImplementedException();
        }

        public void SetMember<TAtomic>(Func<TDataType, IMember<TAtomic>> expression, string description) where TAtomic : IAtomic
        {
            throw new NotImplementedException();
        }

        private static string GetName(ILogixComponent member)
        {
            if (member is ITagMember<IDataType> tagMember)
            {
                return tagMember.Parent is ITagMember<IDataType> parentMember
                    ? parentMember.Dimensions.Length > 0
                        ? $"{GetName(tagMember.Parent)}{member.Name}"
                        : $"{GetName(tagMember.Parent)}.{member.Name}"
                    : member.Name;
            }

            return member.Name;
        }
    }
}