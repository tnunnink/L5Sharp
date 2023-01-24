using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TagMember : ILogixTagMember
    {
        protected readonly Member _member;
        private readonly ILogixTagMember _parent;
        private readonly Tag _tag;

        internal TagMember(Member member, ILogixTagMember parent, Tag tag)
        {
            _member = member;
            _parent = parent;
            _tag = tag;
        }
        
        /// <inheritdoc />
        public string Name => _member.Name;

        /// <inheritdoc />
        public string Description => _member.Description;

        /// <inheritdoc />
        public string DataType => _member.DataType.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => _member.Dimensions;

        /// <inheritdoc />
        public Radix Radix => _member.Radix;
        
        /// <inheritdoc />
        [XmlIgnore]
        public ExternalAccess ExternalAccess =>
            ExternalAccess.MostRestrictive(_member.ExternalAccess, _parent.ExternalAccess);

        /// <inheritdoc />
        [XmlIgnore]
        public TagName TagName => TagName.Combine(_parent.TagName, Name);

        /// <inheritdoc />
        [XmlIgnore]
        public MemberType MemberType => MemberType.FromType(_member.DataType);
        
        /// <summary>
        /// The overriden string comment of the tag member, if one exists. Empty string if not.
        /// </summary>
        /// <value>A <see cref="string"/> containing the tag member comment.</value>
        [XmlIgnore]
        public string Comment
        {
            get => _tag.Comments.TryGetValue(TagName.Operand, out var comment) ? comment: string.Empty;
            set => _tag.Comments[TagName.Operand] = value;
        }

        /// <summary>
        /// The units of the tag member. This appears to only apply to module defined tags...
        /// </summary>
        /// <value>A <see cref="string"/> representing the scaled units of the tag member.</value>
        [XmlIgnore]
        public string Units
        {
            get => _tag.Units.TryGetValue(TagName.Operand, out var units) ? units: string.Empty;
            set => _tag.Units[TagName.Operand] = value;
        }

        /// <summary>
        /// Gets a descendent tag member relative to the current tag member.
        /// </summary>
        /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
        /// <returns>A <see cref="TagMember"/> representing the child member instance.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public TagMember? Member(TagName tagName)
        {
            Check.NotNullOrEmpty(tagName);

            var childName = tagName.Members.First();
            var childMember = GetMembers(_member.DataType).SingleOrDefault(m => m.Name == childName);

            if (childMember is null) return null;

            var tagMember = new TagMember(childMember, this, _tag);

            var next = TagName.Combine(tagName.Members.Skip(1));

            return next.IsEmpty ? tagMember : tagMember.Member(next);
        }

        /// <summary>
        /// Gets all descendent tag members relative to the current tag member.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
        public IEnumerable<TagMember> Members()
        {
            var members = new List<TagMember>();

            foreach (var member in GetMembers(_member.DataType))
            {
                var tagMember = new TagMember(member, this, _tag);
                members.Add(tagMember);
                members.AddRange(tagMember.Members());
            }

            return members;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName">The full <see cref="Core.TagName"/> path of the member to get.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="TagMember"/> objects.</returns>
        public IEnumerable<TagMember> Members(TagName tagName)
        {
            var tagMember = Descendent(tagName);
            return tagMember is not null ? tagMember.Members() : Enumerable.Empty<TagMember>();
        }

        /// <summary>
        /// Gets the underlying atomic value of the <see cref="TagMember"/>,
        /// if it is an <see cref="AtomicType"/> member.
        /// </summary>
        /// <returns>A <see cref="AtomicType"/> representing the value of the member if the <see cref="MemberType"/>
        /// is a value member; Otherwise, null.</returns>
        /// <seealso cref="GetValue{TAtomic}"/>
        public AtomicType? GetValue() => _member.DataType as AtomicType;

        /// <summary>
        /// Gets the underlying atomic value of the <see cref="TagMember"/> as the a strongly typed <see cref="AtomicType"/>.
        /// If the member's data is not the specified type, returns null.
        /// </summary>
        /// <returns>A <see cref="AtomicType"/> representing the value of the member if the <see cref="MemberType"/>
        /// is a value member; Otherwise, null.</returns>
        /// <seealso cref="GetValue"/>
        public TAtomic? GetValue<TAtomic>() where TAtomic : AtomicType => _member.DataType as TAtomic;
        
        /// <inheritdoc />
        public void SetValue(AtomicType atomicType)
        {
            if (_member.DataType is not AtomicType)
                throw new InvalidOperationException(
                    $"The underlying member type {_member.DataType.Name} is not a {typeof(AtomicType)}. The member must be a value member to set the value.");

            _member.DataType = atomicType;
        }

        /// <inheritdoc />
        public bool TrySetValue(AtomicType atomicType)
        {
            if (_member.DataType is not AtomicType)
                return false;

            _member.DataType = atomicType;
            return true;
        }

        private TagMember? Descendent(TagName tagName)
        {
            Check.NotNullOrEmpty(tagName);
            
            var childName = tagName.Members.First();
            var childMember = GetMembers(_member.DataType).SingleOrDefault(m => m.Name == childName);

            if (childMember is null) return null;

            var tagMember = new TagMember(childMember, this, _tag);

            var next = TagName.Combine(tagName.Members.Skip(1));

            return next.IsEmpty ? tagMember : tagMember.Member(next);
        }

        private static IEnumerable<Member> GetMembers(ILogixType dataType)
        {
            switch (dataType)
            {
                case StructureType structureType:
                    structureType.Members();
                    break;
                case ArrayType<ILogixType> arrayType:
                    return arrayType.Members();
            }

            return Enumerable.Empty<Member>();
        }
    }
}