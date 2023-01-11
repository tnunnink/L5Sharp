using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    public class TagMember : ILogixMember, IEnumerable<TagMember>
    {
        private readonly Dictionary<string, TagMember> _members = new();

        /// <summary>
        /// Creates a new default <see cref="TagMember"/> instance.
        /// </summary>
        public TagMember()
        {
        }

        /// <summary>
        /// A constructor for data value tag members.
        /// </summary>
        /// <param name="name">The name of the tag member.</param>
        /// <param name="value">The atomic value of the tag member.</param>
        /// <param name="radix">The optional radix format of the tag member.
        /// Will use <see cref="Enums.Radix.Default"/> if not provided</param>
        public TagMember(string name, AtomicType value, Radix? radix = null)
        {
            Name = name;
            DataType = value.Name;
            Value = value;
            Radix = radix ?? Radix.Default(value);
        }

        /// <summary>
        /// A constructor for structure type tag members.
        /// </summary>
        /// <param name="name">The name of the tag member.</param>
        /// <param name="dataType">The type name of the tag member.</param>
        /// <param name="members">The collection of structure members that compose the tag member.</param>
        public TagMember(string name, string dataType, IEnumerable<TagMember> members)
        {
            Name = name;
            DataType = dataType;
            _members = members.ToDictionary(t => t.Name);
        }

        public TagMember(Member member, string? parentName = null)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            Name = member.Name;
            Description = member.Description;
            DataType = member.DataType.Name;
            Dimensions = member.Dimensions;
            Radix = member.Radix;
            ExternalAccess = member.ExternalAccess;
            TagName = parentName is not null ? TagName.Combine(parentName, Name) : Name;

            //todo handle arrays?...
            
            switch (member.DataType)
            {
                case AtomicType atomicType:
                    //todo set value.
                    break;
                case StructureType structureType:
                    _members = structureType.Members().Select(m => new TagMember(m, Name)).ToDictionary(t => t.Name);
                    break;
            }
        }

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public string DataType { get; set; } = string.Empty;

        /// <inheritdoc />
        public Dimensions Dimensions { get; set; } = Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix { get; set; } = Radix.Null;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;

        /// <summary>
        /// The atomic value of the <see cref="TagMember"/>. Should only apply to value members.
        /// </summary>
        public AtomicType? Value { get; set; } = default;

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
        
        /// <summary>
        /// The full (dot-down) tag name of the <see cref="TagMember"/>.
        /// </summary>
        /// <value>A <see cref="TagName"/> value type representing the tag name of the member.</value>
        public TagName TagName { get; set; } = TagName.Empty;

        public TagMember this[int x] => _members["[{x}]"];
        public TagMember this[TagName tagName] => throw new NotImplementedException();

        /// <inheritdoc />
        public IEnumerator<TagMember> GetEnumerator() => _members.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}