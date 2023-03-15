using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// A logix <c>Tag</c> component. Contains the properties that comprise the L5X Tag element.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [XmlType(L5XName.Tag)]
    [LogixSerializer(typeof(TagSerializer))]
    public class Tag : ILogixComponent, ILogixTag
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc cref="ILogixComponent.Description" />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public TagName TagName => new(Name);

        /// <inheritdoc />
        public ILogixType Data { get; set; } = Logix.Null;

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
        public Tag Base => this;

        /// <inheritdoc />
        public ILogixTag Parent => this;

        /// <summary>
        /// The external access option indicating the read/write access of the tag.
        /// </summary>
        /// <value>A <see cref="Enums.ExternalAccess"/> option representing read/write access of the tag.</value>
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;

        /// <summary>
        /// A type indicating whether the current tag component is a base tag, or alias for another tag instance.
        /// </summary>
        /// <value>A <see cref="Enums.TagType"/> option representing the type of tag component.</value>
        public TagType TagType { get; set; } = TagType.Base;

        /// <summary>
        /// The usage option indicating the scope in which the tag is visible or usable from.
        /// </summary>
        /// <value>A <see cref="Enums.TagUsage"/> option representing the tag scope.</value>
        public TagUsage Usage { get; set; } = TagUsage.Normal;

        /// <summary>
        /// The tag name of the tag that is the alias of the current tag object.
        /// </summary>
        /// <value>A <see cref="Core.TagName"/> string representing the full tag name of the alias tag.</value>
        public TagName AliasFor { get; set; } = TagName.Empty;

        /// <summary>
        /// Indicates whether the tag is a constant.
        /// </summary>
        /// <value><c>true</c> if the tag is constant; otherwise, <c>false</c>.</value>
        /// <remarks>Only value type tags have the ability to be set as a constant. Default is <c>false</c>.</remarks>
        public bool Constant { get; set; }

        /// <summary>
        /// The collection of member comments for the tag component.
        /// </summary>
        /// <value>A <see cref="Dictionary{TKey,TValue}"/> of <see cref="Core.TagName"/>, <see cref="string"/> pairs.</value>
        public Dictionary<string, string> Comments { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The collection of member units for the tag component.
        /// </summary>
        /// <value>A <see cref="Dictionary{TKey,TValue}"/> of <see cref="Core.TagName"/>, <see cref="string"/> pairs.</value>
        public Dictionary<string, string> Units { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc />
        public TagMember? Member(TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

            var member = GetMembers(Data)
                .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

            if (member is null) return default;

            var tagMember = new TagMember(member, Base, this);

            var remaining = TagName.Combine(tagName.Members.Skip(1));

            return remaining.IsEmpty ? tagMember : tagMember.Member(remaining);
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members()
        {
            var members = new List<TagMember>();

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Base, this);
                members.Add(tagMember);
                members.AddRange(tagMember.Members());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members(Predicate<TagName> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var members = new List<TagMember>();

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Base, this);

                if (predicate.Invoke(tagMember.TagName))
                    members.Add(tagMember);

                members.AddRange(tagMember.Members(predicate));
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members(Predicate<TagMember> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var members = new List<TagMember>();

            foreach (var member in GetMembers(Data))
            {
                var tagMember = new TagMember(member, Base, this);

                if (predicate.Invoke(tagMember))
                    members.Add(tagMember);

                members.AddRange(tagMember.Members(predicate));
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> MembersOf(TagName tagName)
        {
            if (tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            var memberName = tagName.Members.FirstOrDefault() ?? string.Empty;

            var member = GetMembers(Data)
                .FirstOrDefault(m => string.Equals(m.Name, memberName, StringComparison.OrdinalIgnoreCase));

            if (member is null) return Enumerable.Empty<TagMember>();

            var tagMember = new TagMember(member, Base, this);

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