using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TagMemberBase : NotificationBase, ITagMember
    {
        private string _description;
        private Radix _radix;
        private object _value;
        private readonly Dictionary<string, ITagMember> _members =
            new Dictionary<string, ITagMember>();

        protected TagMemberBase(string name, IDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, ILogixComponent parent)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");
            Dimensions = dimensions == null ? Dimensions.Empty : dimensions;
            
            _radix = radix != null ? radix.IsValidForType(DataType) ? 
                    radix : throw new RadixNotSupportedException(radix, DataType)
                : DataType is IPredefined predefined ? predefined.DefaultRadix : Radix.Null;
            
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            _description = description;
            Parent = parent;

            if (dataType is IPredefined p)
                _value = p.DefaultValue;
            
            RegisterType();
        }

        public virtual string FullName => Parent == null ? Name
            : IsArrayElement ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";

        public virtual string Name { get; }

        public IDataType DataType { get; }

        public Dimensions Dimensions { get; }

        public virtual Radix Radix => _radix;

        public virtual ExternalAccess ExternalAccess { get; }

        public virtual string Description => _description;

        public virtual object Value => _value;

        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();

        public ILogixComponent Parent { get; }

        public bool IsValueMember => DataType is IPredefined {IsAtomic: true} && Dimensions.Length == 0;

        public bool IsArrayMember => Dimensions.Length > 0;

        public bool IsArrayElement => Parent is ITagMember { IsArrayMember: true };

        public bool IsStructureMember =>
            !(DataType is IPredefined { IsAtomic: true }) && !IsArrayMember && _members.Count > 0;

        public virtual void SetDescription(string description)
        {
            SetProperty(ref _description, description, nameof(Description));
        }

        public virtual void SetRadix(Radix radix)
        {
            Validate.Radix(radix, DataType);

            var changed = SetProperty(ref _radix, radix, nameof(Radix));
            
            if (changed && DataType.IsAtomic && _members.Count > 0)
                PropagateValue((m, r) => m.SetRadix(r), _radix);
        }

        public virtual void SetValue(object value)
        {
            if (!(DataType is Predefined { IsAtomic: true } atomic))
                throw new ComponentNotConfigurableException(nameof(Value), typeof(TagMember),
                    $"'{Name}' is not an atomic type. Value is only configurable for atomics");

            if (!atomic.IsValidValue(value))
                throw new InvalidTagValueException(value, DataType.GetType());

            SetProperty(ref _value, value, nameof(Value));
        }

        public ITagMember GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        public IEnumerable<string> GetMembersNames()
        {
            return GetMemberNames(this);
        }

        internal void UpdateMembers(IEnumerable<ITagMember> tagMembers)
        {
            foreach (var tagMember in tagMembers)
            {
                if (!_members.ContainsKey(tagMember.Name))
                    throw new InvalidOperationException();

                var member = _members[tagMember.Name];

                if (!member.DataType.Equals(tagMember.DataType))
                    throw new InvalidOperationException();
                
                if (!member.Dimensions.Equals(tagMember.Dimensions))
                    throw new InvalidOperationException();

                member.SetRadix(tagMember.Radix);
                member.SetValue(tagMember.Value);
                member.SetDescription(tagMember.Description);
            }
        }

        protected void InstantiateMembers()
        {
            var members = GenerateMembers(this, DataType);

            _members.Clear();

            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        private static IEnumerable<ITagMember> GenerateMembers(ITagMember tagMember, IDataType dataType)
        {
            if (tagMember.IsValueMember) return Array.Empty<ITagMember>();

            if (tagMember.IsArrayMember)
                return tagMember.Dimensions.GenerateIndices()
                    .Select(i => new TagMember(tagMember,
                        new Member(i, dataType, Dimensions.Empty, tagMember.Radix, tagMember.ExternalAccess,
                            tagMember.Description)));

            return dataType.Members.Select(m => new TagMember(tagMember, m));
        }

        private void PropagateValue<TProperty>(Action<ITagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Values)
                setter.Invoke(tagMember, value);
        }

        private static IEnumerable<string> GetMemberNames(ITagMember tagMember)
        {
            var names = new List<string>();

            foreach (var member in tagMember.Members)
            {
                names.Add(member.FullName);
                names.AddRange(GetMemberNames(member));
            }

            return names;
        }

        private static string GetName(ILogixComponent member)
        {
            if (member is ITagMember tagMember)
            {
                return tagMember.Parent is ITagMember parentMember
                    ? parentMember.IsArrayElement
                        ? $"{GetName(tagMember.Parent)}{member.Name}"
                        : $"{GetName(tagMember.Parent)}.{member.Name}"
                    : member.Name;
            }

            return member.Name;
        }

        private void RegisterType()
        {
            if (!(DataType is DataType userDefined)) return;
            userDefined.PropertyChanged -= OnDataTypePropertyChanged;
            userDefined.PropertyChanged += OnDataTypePropertyChanged;
        }

        private void OnDataTypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            InstantiateMembers();
        }
    }
}