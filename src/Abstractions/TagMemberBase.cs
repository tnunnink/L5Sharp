using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TagMemberBase<TDataType> : NotificationBase, ITagMember<TDataType> where TDataType : IDataType
    {
        private string _description;
        private Radix _radix;
        private object _value;
        private readonly Dictionary<string, ITagMember<IDataType>> _members =
            new Dictionary<string, ITagMember<IDataType>>();

        protected TagMemberBase(string name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, IComponent parent)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");
            Dimensions = dimensions == null ? Dimensions.Empty : dimensions;
            
            _radix = !(dataType is IPredefined predefined) ? Radix.Null :
                radix == null ? predefined.DefaultRadix : radix;
            
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

        public TDataType DataType { get; }

        public Dimensions Dimensions { get; }

        public virtual Radix Radix => _radix;

        public virtual ExternalAccess ExternalAccess { get; }

        public virtual string Description => _description;

        public virtual object Value => _value;

        public IEnumerable<ITagMember<IDataType>> Members => _members.Values.AsEnumerable();

        public IComponent Parent { get; }

        public bool IsValueMember => DataType is IPredefined {IsAtomic: true} && Dimensions.Length == 0;

        public bool IsArrayMember => Dimensions.Length > 0;

        public bool IsArrayElement => Parent is ITagMember<IDataType> { IsArrayMember: true };

        public bool IsStructureMember =>
            !(DataType is IPredefined { IsAtomic: true }) && !IsArrayMember && _members.Count > 0;

        public virtual void SetDescription(string description)
        {
            SetProperty(ref _description, description, nameof(Description));
        }

        public virtual void SetRadix(Radix radix)
        {
            Validate.Radix(radix, DataType);

            SetProperty(ref _radix, radix, nameof(Radix));

            if (DataType.IsAtomic && _members.Count > 0)
                PropagateValue((m, r) => m.SetRadix(r), _radix);
        }

        public virtual void SetValue(object value)
        {
            if (!(DataType is Predefined { IsAtomic: true } atomic))
                throw new NotConfigurableException(
                    $"Value is not not configurable for type {DataType}. Value is only configurable for atomic types");

            if (!atomic.IsValidValue(value))
                Throw.InvalidTagValueException(value, DataType.Name);

            SetProperty(ref _value, value, nameof(Value));
        }

        public ITagMember<IDataType> GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        public IEnumerable<string> GetMembersNames()
        {
            return GetMemberNames((ITagMember<IDataType>)this);
        }

        internal void UpdateMembers(IEnumerable<ITagMember<IDataType>> tagMembers)
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
            var members = GenerateMembers((ITagMember<IDataType>)this, DataType);

            _members.Clear();

            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        private static IEnumerable<ITagMember<IDataType>> GenerateMembers(ITagMember<IDataType> tagMember,
            IDataType dataType)
        {
            if (tagMember.IsValueMember) return Array.Empty<ITagMember<IDataType>>();

            if (tagMember.IsArrayMember)
                return tagMember.Dimensions.GenerateIndices()
                    .Select(i => new TagMember<IDataType>(tagMember,
                        new Member(i, dataType, Dimensions.Empty, tagMember.Radix, tagMember.ExternalAccess,
                            tagMember.Description)));

            return dataType.Members.Select(m => new TagMember<IDataType>(tagMember, m));
        }

        private void PropagateValue<TProperty>(Action<ITagMember<IDataType>, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Values)
                setter.Invoke(tagMember, value);
        }

        private static IEnumerable<string> GetMemberNames(ITagMember<IDataType> tagMember)
        {
            var names = new List<string>();

            foreach (var member in tagMember.Members)
            {
                names.Add(member.FullName);
                names.AddRange(GetMemberNames(member));
            }

            return names;
        }

        private static string GetName(IComponent member)
        {
            if (member is ITagMember<IDataType> tagMember)
            {
                return tagMember.Parent is ITagMember<IDataType> parentMember
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