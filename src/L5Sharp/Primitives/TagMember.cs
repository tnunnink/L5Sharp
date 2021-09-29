using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class TagMember : ITagMember, IXSerializable
    {
        private readonly List<ITagMember> _members = new List<ITagMember>();

        /// <summary>
        /// Base constructor. Initialized fields if provided otherwise will opt to parent or default parameters. 
        /// </summary>
        /// <param name="parent">The parent Tag member instance</param>
        /// <param name="name">The name of the current member instance</param>
        /// <param name="dataType">The data type of the current member instance</param>
        /// <param name="dimensions">The dimensions of the current member instance</param>
        /// <param name="radix">The radix of the current member instance</param>
        /// <param name="description">The description/comment of the current member instance</param>
        /// <param name="value">The value of the current member instance</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Based on testing, the following rules appear to be true:
        /// 1. The member's Data Type gets set by the data type member.
        /// 2. The member's Radix (Style) gets set by the data type member (but can be overridden).
        /// 3. The member's External Access is inherited from the parent/base tag
        /// 4. The member's Description (by default) is a concatenation of the parent and member description
        /// </remarks>
        private TagMember(ITagMember parent, string name, IDataType dataType = null, Dimensions dimensions = null,
            Radix radix = null, string description = null, object value = null)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            if (string.IsNullOrEmpty(name)) Throw.ArgumentNullOrEmptyException(nameof(name));

            Name = name;
            DataType = dataType ?? parent.DataType;
            Dimension = dimensions ?? Dimensions.Empty;
            Radix = radix ?? parent.Radix;
            ExternalAccess = parent.ExternalAccess;
            Description = string.IsNullOrEmpty(description)
                ? parent.Description
                : $"{parent.Description} {description}";
            Value = value ?? null;

            _members.AddRange(Instantiate(this));
        }

        private TagMember(ITagMember parent, IMember member) :
            this(parent, member.Name, member.DataType, member.Dimension, member.Radix, member.Description)
        {
        }

        private TagMember(XElement element, IController controller)
        {
            Name = element.Attribute(nameof(Name))?.Value
                   ?? throw new ArgumentNullException(nameof(Name));

            var typeName = element.Attribute(nameof(DataType))?.Value;
            DataType = controller.GetDataType(typeName);
            if (DataType == null) Throw.DataTypeNotFoundException(typeName, Name);

            Dimension = Dimensions.Parse(element.Attribute(nameof(Dimension))?.Value)
                        ?? Dimensions.Empty;

            Radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value)
                    ?? Radix.Default(DataType);

            ExternalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value)
                             ?? ExternalAccess.None;

            Description = element.Attribute(nameof(Description))?.Value
                          ?? string.Empty;

            Value = element.Attribute(nameof(Value))?.Value;

            _members.AddRange(element.Elements().Select(e => Materialize(e, controller)));
        }

        public string FullName => Parent == null ? Name
            : IsArrayElement ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";

        public string Name { get; }
        public IDataType DataType { get; }
        public Dimensions Dimension { get; }
        public Radix Radix { get; set; }
        public ExternalAccess ExternalAccess { get; internal set; }
        public string Description { get; set; }
        public object Value { get; set; }

        public IEnumerable<ITagMember> Members => _members.AsEnumerable();
        public bool IsValueMember => Value != null && DataType.IsAtomic;
        public bool IsArrayMember => Dimension.Length > 0;
        public bool IsArrayElement => Parent is { IsArrayMember: true };
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;

        public ITagMember Parent { get; }

        public XElement Serialize()
        {
            throw new NotImplementedException();
        }

        public static TagMember Materialize(XElement element, IController controller)
        {
            return new TagMember(element, controller);
        }

        internal static IEnumerable<ITagMember> Instantiate(ITagMember tag)
        {
            var members = new List<ITagMember>();

            if (tag.IsValueMember) return members;

            if (tag.IsArrayMember)
            {
                var indices = tag.Dimension.GenerateIndices();
                var memberIndices = indices.Select(index => new TagMember(tag, index)).ToList();
                members.AddRange(memberIndices);
                return members;
            }

            members.AddRange(tag.DataType.Members.Select(m => new TagMember(tag, m)));
            return members;
        }

        private static string GetName(ITagMember member)
        {
            return member.Parent != null
                ? member.Parent.IsArrayElement
                    ? $"{GetName(member.Parent)}{member.Name}"
                    : $"{GetName(member.Parent)}.{member.Name}"
                : member.Name;
        }
    }
}