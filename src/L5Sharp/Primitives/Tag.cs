using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Base;
using L5Sharp.Enumerations;
using L5Sharp.Parsers;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Tag : INamedComponent, IXSerializable
    {
        private string _name;
        private IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;
        private string _description;
        private TagType _tagType;
        private TagUsage _usage;
        private Scope _scope;
        private string _aliasFor;
        private bool _constant;
        private object _value;
        private readonly List<Tag> _tags = new List<Tag>();
        private readonly Dictionary<string, Func<Tag, IElementParser>> _parsers = 
            new Dictionary<string, Func<Tag, IElementParser>>
            {
                { "Decorated", t => new DataValueParser(t) },
                { "String", t => new DataValueParser(t) },
                { "Alarm", t => new DataValueParser(t) }
            };

        /// <summary>
        /// This constrictor is meant specifically for initializing data type member tags of a given base tag.
        /// </summary>
        /// <param name="member"></param>
        /// <param name="parent"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Based on testing, the following rules appear to be true:
        /// 1. The member's Data Type gets set by the data type member.
        /// 2. The member's Radix (Style) gets set by the data type member (but can be overridden).
        /// 3. The member's External Access is inherited from the parent/base tag
        /// 4. The member's Description (by default) is a concatenation of the parent and member description
        /// </remarks>
        private Tag(Member member, Tag parent)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            _name = member.Name ?? throw new ArgumentNullException(nameof(Name));
            _dataType = member.DataType ?? throw new ArgumentNullException(nameof(DataType));
            _dimensions = new Dimensions(member.Dimension);
            _radix = member.Radix;
            _externalAccess = parent.ExternalAccess;
            _description = $"{parent.Description} {member.Description}";
            _tagType = parent.TagType;
            _usage = parent.Usage;
            _scope = parent.Scope;
            _aliasFor = null;
            _constant = false;
            _value = _dataType.IsAtomic ? (object)0 : null; //todo I guess value will depend more on radix than datatype
            Parent = parent;

            InstantiateMembers();
        }

        /// <summary>
        /// This constrictor is meant specifically for initializing array element member tags of a given base tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private Tag(string name, Tag parent)
        {
            if (string.IsNullOrEmpty(name)) Throw.ArgumentNullOrEmptyException(nameof(name));
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            _name = name;
            _dataType = parent.DataType;
            _dimensions = Dimensions.Empty;
            _radix = parent.Radix;
            _externalAccess = parent.ExternalAccess;
            _description = parent.Description;
            _tagType = parent.TagType;
            _usage = parent.Usage;
            _scope = parent.Scope;
            _aliasFor = parent.AliasFor;
            _constant = parent.Constant;
            Value = parent.Value;
            Parent = parent;

            InstantiateMembers();
        }

        private Tag(XElement element)
        {
            _name = element.Attribute(nameof(Name))?.Value;
            _dataType = Predefined.TypeParseType(element.Attribute(nameof(DataType))?.Value);
            _dimensions = Dimensions.Parse(element.Attribute(nameof(Dimensions))?.Value);
            _radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value) ?? Radix.Null;
            _externalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value);
            _description = element.Element(nameof(Description))?.Value ?? string.Empty;
            _tagType = TagType.FromName(element.Attribute(nameof(TagType))?.Value);
            _aliasFor = element.Attribute(nameof(AliasFor))?.Value;
            _usage = TagUsage.FromName(element.Attribute(nameof(Usage))?.Value);
            _constant = Convert.ToBoolean(element.Attribute(nameof(Constant))?.Value);

            var formatted = element.Descendants("Data")
                .SingleOrDefault(x => x.HasAttributes && x.FirstAttribute.Name == "Format");
            if (formatted == null ) return;
            
            var format = formatted.Attribute("Format")?.Value;
            if (string.IsNullOrEmpty(format)) return;

            if (!_parsers.ContainsKey(format)) return;

            var parser = _parsers[format].Invoke(this);
            parser.Parse(formatted);
        }

        internal Tag(Tag parent, string name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess access = null, string description = null, object value = null)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            
            if (string.IsNullOrEmpty(name)) Throw.ArgumentNullOrEmptyException(nameof(name));
            _name = name;

            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            _dimensions = dimensions ?? Dimensions.Empty;

            if (radix != null && !_dataType.SupportsRadix(radix))
                Throw.RadixNotSupportedException(radix, _dataType);
            _radix = radix ?? Radix.Default(dataType);

            _externalAccess = access ?? parent.ExternalAccess;
            _description = description ?? string.Empty;
            _value = value; //todo should assign default value based on type

            _tagType = parent.TagType;
            _aliasFor = parent.AliasFor;
            _constant = parent.Constant;

            Usage = parent.Usage;
            Scope = parent.Scope;
        }

        public Tag(string name, IDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, TagType tagType = null, TagUsage usage = null,
            string description = null, Scope scope = null, string aliasFor = null, bool constant = false,
            object value = null)
        {
            if (string.IsNullOrEmpty(name)) Throw.ArgumentNullOrEmptyException(nameof(name));
            Validate.Name(name);
            _name = name;

            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            _dimensions = dimensions ?? Dimensions.Empty;

            if (radix != null && !_dataType.SupportsRadix(radix))
                Throw.RadixNotSupportedException(radix, _dataType);
            _radix = radix ?? Radix.Default(dataType);

            _externalAccess = externalAccess ?? ExternalAccess.None;
            _tagType = tagType ?? TagType.Base;
            _aliasFor = aliasFor;
            _description = description ?? string.Empty;
            _constant = constant;

            Usage = usage ?? TagUsage.Null;
            Scope = scope ?? Scope.Null;

            Value = value; //todo should assign default value based on type
            Parent = null;

            InstantiateMembers();
        }

        public string FullName => Parent == null ? Name
            : IsArrayMember ? $"{GetName(Parent)}{Name}"
            : $"{GetName(Parent)}.{Name}";

        public string Name
        {
            get => _name;
            set
            {
                if (!IsBaseTag)
                    Throw.NotConfigurableException(nameof(Name), GetType().Name,
                        "This instance's Name property is determined by it's base tag");

                Validate.Name(value);

                _name = value;
            }
        }

        public IDataType DataType
        {
            get => _dataType;
            set
            {
                if (!IsBaseTag)
                    Throw.NotConfigurableException(nameof(DataType), nameof(Tag),
                        "This property is determined by the base tag");

                _dataType = value;

                InstantiateMembers();
            }
        }

        public Dimensions Dimensions
        {
            get => _dimensions;
            set
            {
                if (!IsBaseTag)
                    Throw.NotConfigurableException(nameof(Dimensions), nameof(Tag),
                        "This property is determined by the base tag");

                _dimensions = value;

                InstantiateMembers();
            }
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                if (!_dataType.SupportsRadix(value))
                    Throw.RadixNotSupportedException(value, _dataType);

                _radix = value;

                PropagatePropertyValue((t, v) => t._radix = v, _radix);
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                if (!IsBaseTag)
                    Throw.NotConfigurableException(nameof(ExternalAccess), nameof(Tag),
                        "This instance's ExternalAccess property is determined by the base tag");

                _externalAccess = value;

                PropagatePropertyValue((t, v) => t._externalAccess = v, _externalAccess);
            }
        }

        public string Description { get; set; }

        public TagType TagType
        {
            get => _tagType;
            set
            {
                if (!IsBaseTag)
                    Throw.NotConfigurableException(nameof(TagType), nameof(Tag),
                        "This instance's TagType property is determined by the base tag");

                _tagType = value;

                PropagatePropertyValue((t, v) => t._tagType = v, _tagType);
            }
        }

        public TagUsage Usage { get; set; }
        public Scope Scope { get; }
        public string AliasFor { get; set; }

        public bool Constant
        {
            get => _constant;
            set
            {
                if (!IsBaseTag)
                    Throw.NotConfigurableException(nameof(Constant), nameof(Tag),
                        "This instance's Constant property is determined by the base tag");

                _constant = value;
            }
        }

        public object Value { get; set; }

        public object ForceValue { get; set; }

        public bool CanForce { get; set; }

        public IEnumerable<Tag> Tags => _tags.AsEnumerable();

        private Tag Parent { get; }

        private bool IsBaseTag => Parent == null;

        private bool IsValueMember => Value != null && DataType.IsAtomic;

        private bool IsArrayMember => _dimensions.Length > 0;

        private bool IsStructureMember => !IsArrayMember && !IsValueMember;

        internal void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        internal void AddTag(string name, IDataType dataType = null,
            Dimensions dimensions = null, Radix radix = null, object value = null, string description = null)
        {
            var tag = new Tag(name, dataType, dimensions, radix, _externalAccess, _tagType, _usage, description,
                _scope, _aliasFor, _constant, value);
            _tags.Add(tag);
        }


        public XElement Serialize()
        {
            throw new NotImplementedException();
        }

        private static string GetName(Tag tag)
        {
            return tag.Parent != null
                ? tag.Parent.IsArrayMember
                    ? $"{GetName(tag.Parent)}{tag.Name}"
                    : $"{GetName(tag.Parent)}.{tag.Name}"
                : tag.Name;
        }

        private void InstantiateMembers()
        {
            _tags.Clear();

            var tags = IsArrayMember ? GenerateMembers(_dimensions) : GenerateMembers(_dataType);

            _tags.AddRange(tags);
        }

        private IEnumerable<Tag> GenerateMembers(IDataType dataType)
        {
            var tags = new List<Tag>();

            foreach (var member in dataType.Members)
            {
                var tag = new Tag(member, this);
                tags.Add(tag);
                tags.AddRange(GenerateMembers(member.DataType));
            }

            return tags;
        }

        private IEnumerable<Tag> GenerateMembers(Dimensions dimensions)
        {
            var indices = dimensions.GenerateIndices();
            return indices.Select(index => new Tag(index, this)).ToList();
        }

        private void PropagatePropertyValue<TProperty>(Action<Tag, TProperty> setter, TProperty value)
        {
            foreach (var child in _tags.Cast<Tag>())
                setter.Invoke(child, value);
        }
    }
}