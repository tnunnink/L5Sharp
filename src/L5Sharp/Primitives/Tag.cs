using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Transforms;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Tag : ITagMember, IXSerializable
    {
        private string _name;
        private IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;
        private string _description;
        private TagType _tagType;
        private TagUsage _usage;
        private string _aliasFor;
        private object _value;
        private readonly List<ITagMember> _members = new List<ITagMember>();

        private Tag(XElement element, IController controller)
        {
            _name = element.Attribute(nameof(Name))?.Value;

            //we have to get reference to existing data type
            var typeName = element.Attribute(nameof(DataType))?.Value;
            _dataType = controller.GetDataType(typeName);
            if (_dataType == null) Throw.DataTypeNotFoundException(typeName, _name);

            _dimensions = Dimensions.Parse(element.Attribute(nameof(Dimensions))?.Value);
            _radix = Radix.FromName(element.Attribute(nameof(Radix))?.Value) ?? Radix.Null;
            _externalAccess = ExternalAccess.FromName(element.Attribute(nameof(ExternalAccess))?.Value);
            _description = element.Element(nameof(Description))?.Value ?? string.Empty;
            _tagType = TagType.FromName(element.Attribute(nameof(TagType))?.Value);
            _aliasFor = element.Attribute(nameof(AliasFor))?.Value;
            _usage = TagUsage.FromName(element.Attribute(nameof(Usage))?.Value);
            Constant = Convert.ToBoolean(element.Attribute(nameof(Constant))?.Value);

            //if data has data value element then it is atomic and we just set the value
            var dataValue = element.Descendants(ElementNames.DataValue).SingleOrDefault();
            if (dataValue != null)
            {
                if (!(_dataType is Predefined predefined))
                    throw new InvalidOperationException();
                
                _value = predefined.ParseValue(dataValue.Attribute(ElementNames.Value)?.Value);
                return;
            }
            
            //otherwise use transforms to construct members
            var formatted = element.Descendants(ElementNames.Data)
                .SingleOrDefault(x => x.HasAttributes && x.Attribute("Format") != null);

            var transform = new FormattedDataTransform();
            var members = transform.TransformMany(formatted);
            _members.AddRange(members.Select(m => TagMember.Materialize(m, controller)));
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
            Constant = constant;

            Usage = usage ?? TagUsage.Null;
            Scope = scope ?? Scope.Null;

            Value = value; //todo should assign default value based on type

            _members.AddRange(TagMember.Instantiate(this));
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);

                _name = value;
            }
        }

        public IDataType DataType
        {
            get => _dataType;
            set
            {
                _dataType = value;

                _members.Clear();
                _members.AddRange(TagMember.Instantiate(this));
            }
        }

        public Dimensions Dimension
        {
            get => _dimensions;
            set
            {
                _dimensions = value;

                _members.Clear();
                _members.AddRange(TagMember.Instantiate(this));
            }
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                if (!_dataType.IsAtomic) return;

                if (!_dataType.SupportsRadix(value))
                    Throw.RadixNotSupportedException(value, _dataType);

                _radix = value;

                PropagatePropertyValue((t, v) => t.Radix = v, _radix);
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                _externalAccess = value;

                PropagatePropertyValue((t, v) => t.ExternalAccess = v, _externalAccess);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;

                PropagatePropertyValue((t, v) => t.Description = v, _description);
            }
        }

        public TagType TagType
        {
            get => _tagType;
            set { _tagType = value; }
        }

        public string AliasFor { get; set; }

        public bool Constant { get; set; }

        public object Value
        {
            get => _value;
            set
            {
                if (!IsValueMember) return;


                _value = value;
            }
        }

        public object ForceValue { get; set; }

        public TagUsage Usage
        {
            get => _usage;
            set
            {
                if (Scope == Scope.Controller) return;
                _usage = value;
            }
        }

        public Scope Scope { get; }

        public bool CanForce { get; set; }
        public IEnumerable<ITagMember> Members => _members.AsEnumerable();

        public bool IsValueMember => Value != null && DataType.IsAtomic;
        public bool IsArrayMember => Dimension.Length > 0;
        public bool IsArrayElement => false;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;

        public XElement Serialize()
        {
            var element = new XElement(nameof(Tag));
            element.Add(new XAttribute(nameof(Name), Name));
            element.Add(new XAttribute(nameof(TagType), TagType.Name));
            element.Add(new XAttribute(nameof(DataType), DataType.Name));
            element.Add(new XAttribute($"{nameof(Dimension)}s", Dimension.ToString()));
            element.Add(new XAttribute(nameof(Radix), Radix.Name));
            element.Add(new XAttribute(nameof(Constant), Constant));
            element.Add(new XAttribute(nameof(ExternalAccess), ExternalAccess.Name));
            element.Add(new XAttribute(nameof(AliasFor), AliasFor));
            element.Add(new XAttribute(nameof(Usage), Usage.Name));

            if (!string.IsNullOrEmpty(Description))
                element.Add(new XElement(nameof(Description), Description));

            return element;
        }

        public static Tag Materialize(XElement element, IController controller)
        {
            return new Tag(element, controller);
        }

        private void PropagatePropertyValue<TProperty>(Action<TagMember, TProperty> setter, TProperty value)
        {
            foreach (var tagMember in _members.Cast<TagMember>())
                setter.Invoke(tagMember, value);
        }
    }
}