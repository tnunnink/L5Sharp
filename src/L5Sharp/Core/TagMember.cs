using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    internal class TagMember : ITagMember
    {
        private readonly ITagMember _parent;
        private readonly IDataType _dataType;
        private Radix _radix;
        private string _description;
        private bool _descriptionOverriden = false;
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
        private TagMember(ITagMember parent, string name, IDataType dataType,
            Dimensions dimensions = null, Radix radix = null, string description = null,
            object value = null)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));

            Name = name;
            Dimensions = dimensions != null ? dimensions : Dimensions.Empty;
            Radix = radix != null ? radix : dataType.DefaultRadix;
            _description = string.IsNullOrEmpty(description)
                ? parent.Description
                : $"{parent.Description} {description}";
            Value = value ?? dataType.DefaultValue;

            var members = GenerateMembers(this, dataType);
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        /// <summary>
        /// Constructor that helps initialize members using the provided data type member and parent tag member
        /// </summary>
        /// <param name="parent">The parent tag member instance</param>
        /// <param name="member">The data type member that defines the properties for this member</param>
        private TagMember(ITagMember parent, IMember member) :
            this(parent, member.Name, member.DataType, new Dimensions(member.Dimension), member.Radix,
                member.Description, member.DataType.DefaultValue)
        {
        }

        public string FullName => _parent == null ? Name
            : IsArrayElement ? $"{GetName(_parent)}{Name}"
            : $"{GetName(_parent)}.{Name}";
        
        public string Name { get; }

        public string DataType => _dataType.Name;
        public Dimensions Dimensions { get; }

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

        public ExternalAccess ExternalAccess => _parent.ExternalAccess;

        public string Description
        {
            get => _description;
            set => _description = _descriptionOverriden ? value : $"{_parent.Description} {value}";
        }

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
        public bool IsValueMember => _value != null && _dataType.IsAtomic;
        public bool IsArrayMember => Dimensions.Length > 0;
        public bool IsArrayElement => _parent.IsArrayMember;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;

        public ITagMember GetMember(string name)
        {
            _members.TryGetValue(name, out var member);
            return member;
        }

        public static IEnumerable<TagMember> GenerateMembers(ITagMember tagMember, IDataType dataType)
        {
            if (tagMember.IsValueMember) return Array.Empty<TagMember>();

            if (tagMember.IsArrayMember)
                return tagMember.Dimensions.GenerateIndices()
                    .Select(i => new TagMember(tagMember, i, dataType));

            return dataType.Members.Select(m => new TagMember(tagMember, m));
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
                return tagMember._parent != null
                    ? tagMember._parent.IsArrayElement
                        ? $"{GetName(tagMember._parent)}{member.Name}"
                        : $"{GetName(tagMember._parent)}.{member.Name}"
                    : member.Name;
            }

            return member.Name;
        }
    }
}