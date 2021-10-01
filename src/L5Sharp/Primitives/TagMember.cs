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
        private Radix _radix;
        private object _value;
        private readonly Dictionary<string, ITagMember> _members = new Dictionary<string, ITagMember>();

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
        private TagMember(IMember parent, string name, IDataType dataType = null, Dimensions dimensions = null,
            Radix radix = null, string description = null, object value = null)
        {
           // Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            if (string.IsNullOrEmpty(name)) Throw.ArgumentNullOrEmptyException(nameof(name));

            Name = name;
            DataType = dataType ?? parent.DataType;
            Dimension = dimensions ?? Dimensions.Empty;
            _radix = radix ?? parent.Radix;
            ExternalAccess = parent.ExternalAccess;
            Description = string.IsNullOrEmpty(description)
                ? parent.Description
                : $"{parent.Description} {description}";
            _value = value ?? DataType.Default;

            var members = Instantiate(this);
            
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        /// <summary>
        /// Constructor that helps initialize members using the provided data type member and parent tag member
        /// </summary>
        /// <param name="parent">The parent tag member instance</param>
        /// <param name="member">The data type member that defines the properties for this member</param>
        private TagMember(IMember parent, IMember member) :
            this(parent, member.Name, member.DataType, member.Dimension, member.Radix, member.Description)
        {
        }

        private TagMember(XElement element, IController controller)
        {
            Name = element.GetName() ?? throw new ArgumentNullException(nameof(Name));

            var typeName = element.GetDataTypeName();
            DataType = controller.GetDataType(typeName);
            if (DataType == null) Throw.DataTypeNotFoundException(typeName, Name);
            
            Dimension = element.GetDimensions() ?? Dimensions.Empty;
            Radix = element.GetRadix() ?? Radix.Default(DataType);
            ExternalAccess = element.GetExternalAccess() ?? ExternalAccess.None;
            Description = element.GetDescription() ?? string.Empty;
            
            _value = element.GetValue();

            var members = element.Elements().Select(e => Materialize(e, controller));
            foreach (var member in members)
                _members.Add(member.Name, member);
        }

        /*public string FullName => Parent == null ? Name
            : IsArrayElement ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";*/

        public string Name { get; }
        public IDataType DataType { get; }
        public Dimensions Dimension { get; }

        public Radix Radix
        {
            get => _radix;
            set
            {
                if (!DataType.IsAtomic) return;

                if (!DataType.SupportsRadix(value))
                    Throw.RadixNotSupportedException(value, DataType);

                _radix = value;
                
                PropagatePropertyValue((t, v) => t.Radix = v, _radix);
            }
        }
        
        public ExternalAccess ExternalAccess { get; internal set; }
        public string Description { get; set; }

        public object Value
        {
            get => _value;
            set
            {
                if (!IsValueMember)
                    throw new InvalidOperationException();

                if (!(DataType is Predefined predefined))
                    throw new InvalidOperationException();

                if (!predefined.IsValidValue(value))
                    throw new InvalidOperationException();
                
                _value = value;
            }
        }

        public IEnumerable<ITagMember> Members => _members.Values.AsEnumerable();
        public bool IsValueMember => Value != null && DataType.IsAtomic;
        public bool IsArrayMember => Dimension.Length > 0;
        public bool IsArrayElement => Name.StartsWith('[') && Name.EndsWith(']');
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;

        public XElement Serialize()
        {
            return IsValueMember ? SerializeValueMember()
                : IsArrayMember ? SerializeArrayMember()
                : IsArrayElement ? SerializeArrayElement()
                : IsStructureMember ? SerializeStructureMember()
                : throw new InvalidOperationException();
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

        private XElement SerializeValueMember()
        {
            var element = new XElement(L5XNames.DataValueMember);
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType.Name));
            if (Radix != null) element.Add(new XAttribute(nameof(Radix), Radix.Name));
            element.Add(new XAttribute(L5XNames.Value, Value.ToString()));
            return element;
        }
        
        private XElement SerializeArrayMember()
        {
            var element = new XElement(L5XNames.ArrayMember);
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType.Name));
            element.Add(new XAttribute(L5XNames.Dimensions, Dimension.Length));
            element.Add(new XAttribute(L5XNames.Radix, Radix.Name));
            return element;
        }
        
        private XElement SerializeArrayElement()
        {
            var element = new XElement(L5XNames.Element);
            element.Add(new XAttribute(L5XNames.Index, Name));

            if (Value != null)
                element.Add(new XAttribute(L5XNames.Value, Value));

            if (IsStructureMember)
                element.Add(new XElement(L5XNames.Structure, new XAttribute(L5XNames.DataType, DataType)));
            
            return element;
        }
        
        private XElement SerializeStructureMember()
        {
            var element = new XElement(L5XNames.StructureMember);
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType));
            return element;
        }

        /*private static string GetName(ITagMember member)
        {
            return member.Parent != null
                ? member.Parent.IsArrayElement
                    ? $"{GetName(member.Parent)}{member.Name}"
                    : $"{GetName(member.Parent)}.{member.Name}"
                : member.Name;
        }*/
        
        private void PropagatePropertyValue<TProperty>(Action<TagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Cast<TagMember>())
                setter.Invoke(tagMember, value);
        }
    }
}