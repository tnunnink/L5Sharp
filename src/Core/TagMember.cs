using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    internal class TagMember : ITagMember
    {
        private readonly ITagMember _parent;
        private readonly IMember _member;
        private Radix _radix;
        private string _description;
        private object _value;
        private readonly Dictionary<string, TagMember> _members = new Dictionary<string, TagMember>();

        /// <summary>
        /// Base constructor. Initialized fields if provided otherwise will opt to parent or default parameters. 
        /// </summary>
        /// <param name="parent">The parent Tag member instance</param>
        /// <param name="member"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Based on testing, the following rules appear to be true:
        /// 1. The member's Data Type gets set by the data type definition. For arrays, assumes the parents type.
        /// 2. The member's Radix (Style) gets set by the data type member (but can be overridden).
        /// 3. The member's External Access is inherited from the parent/base tag
        /// 4. The member's Description (by default) is a concatenation of the parent and member description
        /// </remarks>
        private TagMember(ITagMember parent, IMember member)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent), "Parent can not be null");
            _member = member ?? throw new ArgumentNullException(nameof(member), "Backing member can not be null");
            
            if (member.DataType is DataType userDefined)
                userDefined.PropertyChanged += OnDataTypePropertyChanged;

            _radix = member.Radix;
            _value = member.DataType.DefaultValue;
            
            var children = GenerateMembers(this, member.DataType);
            foreach (var child in children)
                _members.Add(child.Name, child);
        }

        public string FullName => _parent == null ? Name
            : IsArrayElement ? $"{GetName(_parent)}{Name}"
            : $"{GetName(_parent)}.{Name}";

        public string Name => _member.Name;

        public string DataType => _member.DataType.Name;

        public Dimensions Dimensions => new Dimensions(_member.Dimension);

        public Radix Radix
        {
            get => _radix;
            set
            {
                Validate.Radix(value, _member.DataType);

                _radix = value;

                if (_member.DataType.IsAtomic)
                    PropagatePropertyValue((t, v) => t.Radix = v, _radix);
            }
        }

        public ExternalAccess ExternalAccess => _member.ExternalAccess.IsMoreRestrictive(_parent.ExternalAccess)
            ? _member.ExternalAccess
            : _parent.ExternalAccess;

        public string Description
        {
            get => string.IsNullOrEmpty(_description) ? $"{_parent.Description} {_member.Description}" : _description;
            set => _description = value;
        }

        public object Value
        {
            get => _value;
            set
            {
                if (!(_member.DataType is Predefined { IsAtomic: true } predefined))
                    throw new NotConfigurableException(
                        $"Value is not not configurable for type {_member.DataType}. Value is only configurable for atomic types");

                if (!predefined.IsValidValue(value))
                    Throw.InvalidTagValueException(value, _member.DataType.Name);

                _value = value;
            }
        }

        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();

        public bool IsValueMember => _value != null && _member.DataType.IsAtomic;

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
                    .Select(i => new TagMember(tagMember,
                        new ReadOnlyMember(i, dataType, 0, tagMember.Radix, tagMember.ExternalAccess,
                            tagMember.Description)));

            return dataType.Members.Select(m => new TagMember(tagMember, new ReadOnlyMember(m)));
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

        private void OnDataTypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!(sender is DataType userDefined)) return;
            
            var members = GenerateMembers(this, userDefined);
            _members.Clear();
            foreach (var member in members)
                _members.Add(member.Name, member);
        }
    }
}