using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TagBase : ComponentBase, ITag
    {
        private IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private TagUsage _usage;
        private object _value;
        private readonly Dictionary<string, TagMember> _members = new Dictionary<string, TagMember>();

        protected TagBase(string name, IDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, IComponent parent, TagUsage usage, bool constant)
            : base(name, description)
        {
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");

            if (_dataType is DataType userDefined)
                userDefined.MemberUpdated += OnDataTypeMemberUpdated;

            Parent = parent;
            Usage = usage != null ? usage : TagUsage.Null;
            Dimensions = dimensions == null ? Dimensions.Empty : dimensions;
            Radix = radix == null ? dataType.DefaultRadix : radix;
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            Constant = constant;
            if (_dataType.IsAtomic)
                Value = _dataType.DefaultValue;
        }

        public abstract TagType TagType { get; }

        public Scope Scope => Parent == null ? Scope.Null
            : Parent is IController ? Scope.Controller
            : Parent is IProgram ? Scope.Program
            : throw new InvalidOperationException(
                $"Scope can not be determined by container type '{Parent.GetType()}'");

        public TagUsage Usage
        {
            get => _usage;
            set => _usage = value;
        }

        public bool Constant { get; set; }
        public IComponent Parent { get; }
        public string FullName => Name;
        public string DataType => _dataType?.Name;

        public Dimensions Dimensions
        {
            get => _dimensions;
            set
            {
                _dimensions = value;
                InstantiateMembers(_dataType);
            }
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                Validate.Radix(value, _dataType);

                _radix = value;

                if (_dataType.IsAtomic)
                    PropagatePropertyValue((t, v) => t.Radix = v, _radix);
            }
        }

        public ExternalAccess ExternalAccess { get; set; }

        public object Value
        {
            get => _value;
            set
            {
                if (!(_dataType is Predefined { IsAtomic: true } predefined))
                    throw new NotConfigurableException(
                        $"Radix property is not not configurable for type {_dataType}. Radix is only configurable for atomic types");

                
                if (!predefined.IsValidValue(value))
                    Throw.InvalidTagValueException(value, _dataType.Name);

                _value = value;
            }
        }

        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();
        public bool IsValueMember => Value != null && _dataType is { IsAtomic: true };
        public bool IsArrayMember => Dimensions.Length > 0;
        public bool IsArrayElement => false;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;
        
        public void UpdateDataType(IDataType dataType)
        {
            if (_dataType is DataType type)
                type.MemberUpdated -= OnDataTypeMemberUpdated;

            if (dataType is DataType userDefined)
                userDefined.MemberUpdated += OnDataTypeMemberUpdated;
            
            _dataType = dataType;
            
            InstantiateMembers(_dataType);
        } 

        public ITag ChangeTagType(TagType type)
        {
            return type.Create(Name, _dataType);
        }

        public ITagMember GetMember(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Member name can not be null or empty");

            _members.TryGetValue(name, out var member);
            return member;
        }

        public IEnumerable<string> ListMembers()
        {
            return GetMemberNames(this);
        }

        private static IEnumerable<string> GetMemberNames(ITagMember member)
        {
            var names = new List<string>();

            foreach (var m in member.Members)
            {
                var name = m.IsArrayElement ? $"{member.Name}{m.Name}" : $"{member.Name}.{m.Name}";
                names.Add(name);
                names.AddRange(GetMemberNames(m));
            }

            return names;
        }

        private void InstantiateMembers(IDataType dataType)
        {
            _members.Clear();
            var members = TagMember.GenerateMembers(this, dataType);
            foreach (var member in members)
                _members.Add(member.Name, member); 
        }

        private void PropagatePropertyValue<TProperty>(Action<TagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Values.Cast<TagMember>())
                setter.Invoke(tagMember, value);
        }
        
        private void OnDataTypeMemberUpdated(object sender, EventArgs e)
        {
            InstantiateMembers(_dataType);
        }
    }
}