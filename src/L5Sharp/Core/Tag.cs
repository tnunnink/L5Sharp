using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Tag : ITagMember
    {
        private string _name;
        private IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;
        private string _description;
        private TagUsage _usage;
        private object _value;
        private readonly Dictionary<string, TagMember> _members = new Dictionary<string, TagMember>();

        public Tag(string name, IDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, TagType tagType = null, TagUsage usage = null,
            string description = null, Scope scope = null, string aliasFor = null, bool constant = false)
        {
            Name = name;
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            _dimensions = dimensions ?? Dimensions.Empty;

            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            TagType = tagType ?? TagType.Base;
            AliasFor = aliasFor ?? string.Empty;
            Description = description ?? string.Empty;
            Constant = constant;
            Scope = scope ?? Scope.Null;
            Usage = usage != null ? usage : scope == Scope.Program ? TagUsage.Local : TagUsage.Null;

            _value = _dataType.Default;

            Instantiate();
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                Validate.Name(value);
                _name = value;
            }
        }

        public string DataType => _dataType?.Name;

        public Dimensions Dimension
        {
            get => _dimensions;
            set
            {
                _dimensions = value;

                _members.Clear();
                Instantiate();
            }
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                if (!_dataType.IsAtomic) return;

                if (!_dataType.SupportsRadix(value))
                    Throw.RadixNotSupportedException(value, _dataType);

                _radix = value;

                PropagatePropertyValue((t, v) => t.Radix = v, _radix);
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                _externalAccess = value;

                PropagatePropertyValue((t, v) => t.ExternalAccess = v, _externalAccess);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;

                PropagatePropertyValue((t, v) => t.Description = v, _description);
            }
        }

        public object Value
        {
            get => _value;
            set
            {
                if (!IsValueMember)
                    throw new InvalidOperationException();

                if (!(_dataType is Predefined predefined))
                    throw new InvalidOperationException();

                if (!predefined.IsValidValue(value))
                    throw new InvalidOperationException();

                _value = value;
            }
        }

        public TagType TagType { get; set; }

        public string AliasFor { get; set; }

        public bool Constant { get; set; }

        public TagUsage Usage
        {
            get => _usage;
            set
            {
                if (Scope == Scope.Controller) return;
                _usage = value;
            }
        }

        public Scope Scope { get; internal set; }
        public object ForceValue { get; set; }
        public bool CanForce { get; set; }
        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();

        public bool IsValueMember => Value != null && _dataType is { IsAtomic: true };
        public bool IsArrayMember => Dimension.Length > 0;
        public bool IsArrayElement => false;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;
        public ITagMember GetMember(string name)
        {
            return Members.SingleOrDefault(m => m.Name == name);
        }

        public IEnumerable<string> ListMembers()
        {
            return GetMemberNames(this);
        }

        public void ChangeType(IDataType dataType)
        {
            _dataType = dataType;
            _value = dataType.Default;
            Radix = Radix.Default(dataType);
            Instantiate();
        }

        private void Instantiate()
        {
            if (IsValueMember) return; //todo unless we decide that value members have members?

            _members.Clear();

            if (IsArrayMember)
            {
                var indices = Dimension.GenerateIndices();
                var arrayMembers = indices.Select(i => new TagMember(this, i, _dataType)).ToList();
                foreach (var member in arrayMembers)
                    _members.Add(member.Name, member);
                return;
            }

            var members = _dataType.Members.Select(m => new TagMember(this, m));
            foreach (var member in members)
                _members.Add(member.Name, member);
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

        private void PropagatePropertyValue<TProperty>(Action<TagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Values.Cast<TagMember>())
                setter.Invoke(tagMember, value);
        }
    }

    public class Tag<T> : Tag where T : IDataType, new()
    {
        private Tag(string name, IDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, TagType tagType = null, TagUsage usage = null,
            string description = null, Scope scope = null, string aliasFor = null, bool constant = false) : base(name,
            dataType, dimensions, radix, externalAccess, tagType, usage, description, scope, aliasFor, constant)
        {
        }

        public Tag(string name, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, TagType tagType = null, TagUsage usage = null,
            string description = null, Scope scope = null, string aliasFor = null, bool constant = false) : this(name,
            new T(), dimensions, radix, externalAccess, tagType, usage, description, scope, aliasFor, constant)
        {
        }

        public ITagMember GetMember<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("");

            var propertyName = memberExpression.Member.Name;

            return Members.SingleOrDefault(m => m.Name == propertyName);
        }
    }
}