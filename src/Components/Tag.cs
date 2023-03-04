using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    /// <summary>
    /// A logix <c>Tag</c> component. Contains the properties that comprise the L5X Tag element.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(TagSerializer))]
    public class Tag : ILogixComponent, ILogixTag, ICloneable<Tag>
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public TagName TagName => new(Name);

        /// <inheritdoc />
        public ILogixType Data { get; set; } = Logix.Null;

        /// <inheritdoc />
        public string DataType => Data.Name;

        /// <inheritdoc />
        public Dimensions Dimensions => Data is ArrayType<ILogixType> array ? array.Dimensions : Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix => Data is AtomicType atomic ? atomic.Radix : Radix.Null;

        /// <inheritdoc />
        public MemberType MemberType => MemberType.FromType(Data);

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
        public Dictionary<TagName, string> Comments { get; set; } = new();

        /// <summary>
        /// The collection of member units for the tag component.
        /// </summary>
        /// <value>A <see cref="Dictionary{TKey,TValue}"/> of <see cref="Core.TagName"/>, <see cref="string"/> pairs.</value>
        public Dictionary<TagName, string> Units { get; set; } = new();

        /// <inheritdoc />
        public TagMember? Member(TagName tagName)
        {
            var member = Data.FindMember(tagName);

            return member is not null
                ? new TagMember(TagName.Combine(TagName, tagName), member.DataType, this)
                : null;
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members()
        {
            var members = new List<TagMember>();

            foreach (var member in Data.FindMembers())
            {
                var tagName = TagName.Combine(TagName, member.Name);
                var tagMember = new TagMember(tagName, member.DataType, this);
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
                var tagMember = new TagMember(tagName, member.DataType, this);

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
                var tagMember = new TagMember(tagName, member.DataType, this);

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
                ? new TagMember(TagName.Combine(TagName, tagName), member.DataType, this)
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

        /// <inheritdoc />
        public Tag Clone()
        {
            return new Tag
            {
                Name = string.Copy(Name),
                Description = string.Copy(Description),
                Data = Data, //todo implement Cloneable on Logix type?
                ExternalAccess = ExternalAccess,
                Usage = Usage,
                TagType = TagType,
                AliasFor = new TagName(AliasFor),
                Constant = Constant,
                Comments = new Dictionary<TagName, string>(Comments.Select(c =>
                    new KeyValuePair<TagName, string>(c.Key, c.Value))),
                Units = new Dictionary<TagName, string>(Units.Select(c =>
                    new KeyValuePair<TagName, string>(c.Key, c.Value)))
            };
        }
    }
}