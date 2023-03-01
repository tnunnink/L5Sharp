using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class TagMember : ILogixTag
    {
        private readonly Tag _tag;

        internal TagMember(TagName? tagName, ILogixType data, Tag tag)
        {
            TagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }

        /// <inheritdoc />
        public TagName TagName { get; }

        /// <inheritdoc />
        public ILogixType Data { get; private set; }

        /// <inheritdoc />
        public string DataType => Data.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => Data is ArrayType<ILogixType> array ? array.Dimensions : Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix => Data is AtomicType atomic ? atomic.Radix : Radix.Null;

        /// <inheritdoc />
        public MemberType MemberType => MemberType.FromType(Data);

        /// <summary>
        /// The overriden string comment of the tag member, if one exists. Empty string if not.
        /// </summary>
        /// <value>A <see cref="string"/> containing the tag member comment.</value>
        public string Comment
        {
            get => _tag.Comments.TryGetValue(TagName.Operand, out var comment) ? comment : string.Empty;
            set => _tag.Comments[TagName.Operand] = value;
        }

        /// <summary>
        /// The units of the tag member. This appears to only apply to module defined tags...
        /// </summary>
        /// <value>A <see cref="string"/> representing the scaled units of the tag member.</value>
        public string Unit
        {
            get => _tag.Units.TryGetValue(TagName.Operand, out var units) ? units : string.Empty;
            set => _tag.Units[TagName.Operand] = value;
        }

        /// <inheritdoc />
        public TagMember? Member(TagName tagName)
        {
            var member = Data.FindMember(tagName);

            return member is not null
                ? new TagMember(TagName.Combine(TagName, tagName), member.DataType, _tag)
                : null;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members()
        {
            var members = new List<TagMember>();

            foreach (var member in Data.FindMembers())
            {
                var tagName = TagName.Combine(TagName, member.Name);
                var tagMember = new TagMember(tagName, member.DataType, _tag);
                members.Add(tagMember);
                members.AddRange(tagMember.Members());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members(Predicate<TagName> predicate)
        {
            var members = new List<TagMember>();

            foreach (var member in Data.FindMembers())
            {
                var tagName = TagName.Combine(TagName, member.Name);
                var tagMember = new TagMember(tagName, member.DataType, _tag);

                if (predicate.Invoke(tagMember.TagName))
                    members.Add(tagMember);

                members.AddRange(tagMember.Members(predicate));
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members(Predicate<TagMember> predicate)
        {
            var members = new List<TagMember>();

            foreach (var member in Data.FindMembers())
            {
                var tagName = TagName.Combine(TagName, member.Name);
                var tagMember = new TagMember(tagName, member.DataType, _tag);

                if (predicate.Invoke(tagMember))
                    members.Add(tagMember);

                members.AddRange(tagMember.Members(predicate));
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> MembersOf(TagName tagName)
        {
            var member = Data.FindMember(tagName);
            
            var tagMember = member is not null
                ? new TagMember(TagName.Combine(TagName, tagName), member.DataType, _tag)
                : null;
            
            return tagMember is not null ? tagMember.Members() : Enumerable.Empty<TagMember>();
        }

        /// <inheritdoc />
        public bool SetValue(AtomicType atomicType)
        {
            if (Data is not AtomicType) return false;
            Data = atomicType;
            return true;
        }
    }
}