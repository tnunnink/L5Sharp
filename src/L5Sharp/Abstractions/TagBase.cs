using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class TagBase : ComponentBase, ITag
    {
        private readonly IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private TagUsage _usage;
        private object _value;
        private readonly Dictionary<string, TagMember> _members = new Dictionary<string, TagMember>();

        protected TagBase(string name, IDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description, IComponent container, TagUsage usage, bool constant)
            : base(name, description)
        {
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType), "DataType can not be null");

            Container = container;
            Usage = usage;
            Dimensions = dimensions == null ? Dimensions.Empty : dimensions;
            Radix = radix == null ? dataType.DefaultRadix : radix;
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            Constant = constant;
            if (_dataType.IsAtomic)
                Value = _dataType.DefaultValue;
        }

        public abstract TagType TagType { get; }

        public Scope Scope => Container == null ? Scope.Null
            : Container is Controller ? Scope.Controller
            : Container is IProgram ? Scope.Program
            : throw new InvalidOperationException(
                $"Scope can not be determined by container type '{Container.GetType()}'");

        public TagUsage Usage
        {
            get => _usage;
            set => _usage = value;
        }

        public bool Constant { get; set; }
        public IComponent Container { get; }
        public string FullName => Name;
        public string DataType => _dataType?.Name;

        public Dimensions Dimensions
        {
            get => _dimensions;
            set
            {
                _dimensions = value;
                Instantiate();
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
                if (!_dataType.IsValidValue(value))
                    Throw.InvalidTagValueException(value, _dataType.Name);
                    
                _value = value;
            }
        }

        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();
        public bool IsValueMember => Value != null && _dataType is { IsAtomic: true };
        public bool IsArrayMember => Dimensions.Length > 0;
        public bool IsArrayElement => false;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;

        public ITag ChangeTagType(TagType type)
        {
            throw new System.NotImplementedException();
        }

        public ITagMember GetMember(string name)
        {
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

        private void Instantiate()
        {
            _members.Clear();
            var members = TagMember.GenerateMembers(this, _dataType);
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        private void PropagatePropertyValue<TProperty>(Action<TagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Values.Cast<TagMember>())
                setter.Invoke(tagMember, value);
        }
    }
}