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
    public class TagMember : ILogixTag
    {
        internal TagMember(Member member, ILogixTag tag, ILogixTag parent)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            TagName = TagName.Combine(parent.TagName, member.Name);
            Data = member.DataType;
            Root = tag ?? throw new ArgumentNullException(nameof(tag));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <inheritdoc />
        public TagName TagName { get; }

        /// <inheritdoc />
        public string Description => !string.IsNullOrEmpty(Comment) ? Comment : Parent.Description;

        /// <inheritdoc />
        public virtual ILogixType Data { get; private set; }

        /// <inheritdoc />
        public string DataType => Data.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => Data is ILogixArray<ILogixType> array ? array.Dimensions : Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix => Data is AtomicType atomic ? atomic.Radix : Radix.Null;

        /// <inheritdoc />
        public AtomicType? Value
        {
            get => Data as AtomicType;
            set
            {
                if (Data is not AtomicType)
                    throw new InvalidOperationException(
                        $"Tag type is {Data.GetType()}. Can not set value for a non atomic type.");

                Data = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <inheritdoc />
        public ILogixTag Root { get; }

        /// <inheritdoc />
        public ILogixTag Parent { get; }

        /// <inheritdoc />
        public Scope Scope => Root.Scope;

        /// <inheritdoc />
        public string Container => Root.Container;

        /// <summary>
        /// The overriden string comment of the tag member, if one exists. Empty string if not.
        /// </summary>
        /// <value>A <see cref="string"/> containing the tag member comment.</value>
        public string Comment
        {
            get => ((Tag)Root).Comments.TryGetValue(TagName.Operand, out var comment) ? comment : string.Empty;
            set => ((Tag)Root).Comments[TagName.Operand] = value;
        }

        /// <summary>
        /// The units of the tag member. This appears to only apply to module defined tags...
        /// </summary>
        /// <value>A <see cref="string"/> representing the scaled units of the tag member.</value>
        public string Unit
        {
            get => ((Tag)Root).Units.TryGetValue(TagName.Operand, out var units) ? units : string.Empty;
            set => ((Tag)Root).Units[TagName.Operand] = value;
        }

        /// <inheritdoc />
        public ILogixTag? Member(TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

            var member = GetMembers(Data)
                .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

            if (member is null) return default;

            var tagMember = new TagMember(member, Root, this);

            var remaining = TagName.Combine(tagName.Members.Skip(1));

            return remaining.IsEmpty ? tagMember : tagMember.Member(remaining);
        }

        /// <inheritdoc />
        public IEnumerable<ILogixTag> Members()
        {
            var members = new List<ILogixTag>();

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Root, this);
                members.Add(tagMember);
                members.AddRange(tagMember.Members());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<ILogixTag> MembersAndSelf()
        {
            var members = new List<ILogixTag> { this };

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Root, this);
                members.Add(tagMember);
                members.AddRange(tagMember.Members());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<ILogixTag> Members(Predicate<TagName> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var members = new List<ILogixTag>();

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Root, this);

                if (predicate.Invoke(tagMember.TagName))
                    members.Add(tagMember);

                members.AddRange(tagMember.Members(predicate));
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<ILogixTag> Members(Predicate<ILogixTag> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var members = new List<ILogixTag>();

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Root, this);

                if (predicate.Invoke(tagMember))
                    members.Add(tagMember);

                members.AddRange(tagMember.Members(predicate));
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<ILogixTag> MembersOf(TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

            var member = GetMembers(Data)
                .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

            if (member is null) return Enumerable.Empty<TagMember>();

            var tagMember = new TagMember(member, Root, this);

            var remaining = TagName.Combine(tagName.Members.Skip(1));

            return remaining.IsEmpty ? tagMember.Members() : tagMember.MembersOf(remaining);
        }

        /// <summary>
        /// Returns all <see cref="Member"/> objects contained by the <see cref="ILogixType"/> object.
        /// </summary>
        /// <param name="type">The logix type object.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="Member"/> objects.</returns>
        /// <remarks>
        /// This is a helper to easily get either a <see cref="StructureType"/> member collection or an
        /// <see cref="ArrayType{TLogixType}"/> element collection, both which are collection of members and defined
        /// the type structure. Calling this for <see cref="AtomicType"/> will return and empty collection.
        /// </remarks>
        private static IEnumerable<Member> GetMembers(ILogixType type)
        {
            return type switch
            {
                StructureType structureType => structureType.Members,
                ILogixArray<ILogixType> arrayType => arrayType.Elements,
                _ => Enumerable.Empty<Member>()
            };
        }
    }
}