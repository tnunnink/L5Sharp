using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class TagMember : ITagMember
    {
        private readonly IDataType _dataType;
        private Radix _radix;
        private object _value;
        private readonly Dictionary<string, TagMember> _members = new Dictionary<string, TagMember>();

        /// <summary>
        /// Base constructor. Initialized fields if provided otherwise will opt to parent or default parameters. 
        /// </summary>
        /// <param name="parent">The parent Tag member instance</param>
        /// <param name="name">The name of the current member instance</param>
        /// <param name="dataType">The data type of the current member instance. If null will assume parent type</param>
        /// <param name="dimensions">The dimensions of the current member instance. If null will assume parent Empty</param>
        /// <param name="radix">The radix of the current member instance. If null will assume Default</param>
        /// <param name="description">The description/comment of the current member instance. Will append parent description</param>
        /// <param name="value">The value of the current member instance</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Based on testing, the following rules appear to be true:
        /// 1. The member's Data Type gets set by the data type definition. For arrays, assumes the parents type.
        /// 2. The member's Radix (Style) gets set by the data type member (but can be overridden).
        /// 3. The member's External Access is inherited from the parent/base tag
        /// 4. The member's Description (by default) is a concatenation of the parent and member description
        /// </remarks>
        internal TagMember(ITagMember parent, string name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, string description = null, object value = null)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Name = name ?? throw new ArgumentNullException(nameof(name)) ;
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Dimension = dimensions ?? Dimensions.Empty;
            _radix = radix ?? parent.Radix;
            ExternalAccess = parent.ExternalAccess;
            Description = string.IsNullOrEmpty(description)
                ? parent.Description
                : $"{parent.Description} {description}";
            _value = value ?? _dataType.DefaultValue;

            Instantiate();
        }

        /// <summary>
        /// Constructor that helps initialize members using the provided data type member and parent tag member
        /// </summary>
        /// <param name="parent">The parent tag member instance</param>
        /// <param name="member">The data type member that defines the properties for this member</param>
        internal TagMember(ITagMember parent, IMember member) :
            this(parent, member.Name, member.DataType, new Dimensions(member.Dimension), member.Radix,
                member.Description)
        {
        }

        public string FullName => Parent == null ? Name
            : IsArrayElement ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";

        public string Name { get; }
        public string DataType => _dataType.Name;
        public Dimensions Dimension { get; }

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

        public ExternalAccess ExternalAccess { get; internal set; }
        public string Description { get; set; }

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

        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();
        public bool IsValueMember => Value != null && _dataType.IsAtomic;
        public bool IsArrayMember => Dimension.Length > 0;
        public bool IsArrayElement => Parent.IsArrayMember;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;
        public ITagMember GetMember(string name)
        {
            return Members.SingleOrDefault(m => m.Name == name);
        }

        private ITagMember Parent { get; }

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

        private void PropagatePropertyValue<TProperty>(Action<TagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Values)
                setter.Invoke(tagMember, value);
        }
        
        private static string GetName(IComponent member)
        {
            if (member is TagMember tagMember)
            {
                return tagMember.Parent != null
                    ? tagMember.Parent.IsArrayElement
                        ? $"{GetName(tagMember.Parent)}{member.Name}"
                        : $"{GetName(tagMember.Parent)}.{member.Name}"
                    : member.Name;
            }

            return member.Name;
        }
    }
}