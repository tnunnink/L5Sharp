using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public class TagMember<TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        private readonly IMember<TDataType> _member;
        private readonly TagMember<IDataType> _parent;
        private string _description;
        private readonly List<TagMember<IDataType>> _members = new List<TagMember<IDataType>>();

        internal TagMember(IMember<TDataType> member, ILogixComponent parent)
        {
            _member = member ?? throw new ArgumentNullException(nameof(member), "Member can not be null");
            _parent = (TagMember<IDataType>) parent;

            if (member.DataType is IAtomic && member.Dimensions.Length == 0) return;
            
            //todo the question is should we instantiate all member now or just create them on request.
            //1. Pro Could conserve memory on request. Could be like lazy loading
            //2. Question is would that cause issues anywhere else?
        }

        public string Name => _member.Name;
        
        public string FullName => Parent == null ? Name
            : Parent is ITagMember<IDataType> { Dimensions.Length > 0: true } ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";

        public string DataType => _member.DataType.Name;

        TDataType IMember<TDataType>.DataType => _member.DataType;

        public Dimensions Dimensions => _member.Dimensions;
        public Radix Radix => _member.Radix;
        
        public ExternalAccess ExternalAccess =>
            _member.ExternalAccess.IsMoreRestrictive(_parent.ExternalAccess)
                ? _member.ExternalAccess
                : _parent.ExternalAccess;

        public string Description => string.IsNullOrEmpty(_description)
            ? $"{_parent.Description} {_member?.Description}"
            : _description;

        public ILogixComponent Parent => _parent;

        public object GetValue()
        {
            return _member.DataType is IAtomic atomic ? atomic.GetValue() : null;
        }

        public void SetValue(object value)
        {
            if (!(_member.DataType is IAtomic atomic))
                throw new ComponentNotConfigurableException(nameof(DataType), typeof(TagMember<TDataType>),
                    $"'{Name}' is not an atomic type. Value is only configurable for atomics");

            atomic.SetValue(value);
        }

        public IEnumerable<ITagMember<IDataType>> GetMembers()
        {
            return _member.DataType.Members.Select(m => new TagMember<IDataType>(m, this));
        }

        public ITagMember<IDataType> GetMember(Func<TDataType, IMember<IDataType>> expression)
        {
            return new TagMember<IDataType>(expression.Invoke(_member.DataType), this);
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, IMember<TType>> expression) where TType : IDataType
        {
            return new TagMember<TType>(expression.Invoke(_member.DataType), this);
        }

        public ITagMember<TType> GetMember<TType>(Func<TDataType, TType> expression) where TType : IDataType
        {
            throw new NotImplementedException();
        }

        public void SetRadix(Radix radix)
        {
            throw new NotImplementedException();
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public IEnumerable<string> GetMembersNames()
        {
            throw new NotImplementedException();
        }

        public ITagMember<IDataType> GetMember(Func<TDataType, IDataType> expression)
        {
            throw new NotImplementedException();
        }

        private void PropagateValue<TProperty>(Action<IMember<IDataType>, TProperty> setter, TProperty value)
        {
            foreach (var member in _member.DataType.Members)
                setter.Invoke(member, value);
        }

        private static IEnumerable<string> GetMemberNames(IDataType dataType)
        {
            var names = new List<string>();

            foreach (var member in dataType.Members)
            {
                names.Add(member.Name);
                names.AddRange(GetMemberNames(member.DataType));
            }

            return names;
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