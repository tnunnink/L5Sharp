using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TagMember : ILogixMember
    {
        private readonly Member _member;
        private readonly TagMember _parent;
        private readonly Tag _tag;

        internal TagMember(Member member, TagMember parent, Tag tag)
        {
            _member = member;
            _parent = parent;
            _tag = tag;
        }

        /// <summary>
        /// The full (dot-down) tag name of the <see cref="TagMember"/>.
        /// </summary>
        /// <value>A <see cref="TagName"/> value type representing the tag name of the member.</value>
        public TagName TagName => TagName.Combine(_parent.TagName, Name);

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
        public ExternalAccess ExternalAccess =>
            ExternalAccess.MostRestrictive(_member.ExternalAccess, _parent.ExternalAccess);

        /// <summary>
        /// 
        /// </summary>
        public MemberType MemberType => MemberType.FromType(_member.DataType);

        /// <summary>
        /// The overriden string comment of the tag member, if one exists. Empty string if not.
        /// </summary>
        /// <value>A <see cref="string"/> containing the tag member comment.</value>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// The units of the tag member. This appears to only apply to module defined tags...
        /// </summary>
        /// <value>A <see cref="string"/> representing the scaled units of the tag member.</value>
        public string Units { get; set; } = string.Empty;

        public TagMember Member(TagName tagName)
        {
            /*if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var path = tagName.Base.Equals(_tag.Name) ? tagName.Path : tagName.ToString();

            var members = GetMembers(_member.DataType).ToList();

            if (!members.Any())
                throw new InvalidOperationException(tagName, _member.DataType.Name);

            var current = this;

            foreach (var member in members.Select(m => new TagMember(m, current, _tag)))
                current = member;

            return current;*/
            throw new NotImplementedException();
        }

        public IEnumerable<TagMember> Members()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TagMember> Members(TagName tagName)
        {
            throw new System.NotImplementedException();
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


        public void SetValue(AtomicType atomicType)
        {
            if (_member.DataType is not AtomicType)
                throw new InvalidOperationException(
                    $"The underlying member type {_member.DataType.Name} is not a {typeof(AtomicType)}. The member must be a value member to set the value.");

            _member.DataType = atomicType;
        }

        public bool TrySetValue(AtomicType atomicType)
        {
            if (_member.DataType is not AtomicType)
                return false;

            _member.DataType = atomicType;
            return true;
        }

        private static IEnumerable<Member> GetMembers(ILogixType dataType)
        {
            switch (dataType)
            {
                case StructureType structureType:
                    structureType.Members();
                    break;
                case ArrayType arrayType:
                    return arrayType.Members();
            }

            return Enumerable.Empty<Member>();
        }
    }
}