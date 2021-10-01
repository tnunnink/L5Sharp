using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Loaders;
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
            
            _tagType = element.GetTagType() ?? TagType.Base;
            _dimensions = element.GetDimensions() ?? Dimensions.Empty;
            _radix = element.GetRadix() ?? Radix.Default(_dataType);
            _externalAccess = element.GetExternalAccess() ?? ExternalAccess.None;
            _aliasFor = element.GetAliasFor() ?? string.Empty;
            _usage = element.GetUsage() ?? TagUsage.Null;
            _description = element.GetDescription() ?? string.Empty;
            Constant = element.GetConstant();

            //if data has data value element then it is atomic and we just set the value
            var dataValue = element.Descendants(L5XNames.DataValue).SingleOrDefault();
            if (dataValue != null)
            {
                if (!(_dataType is Predefined predefined))
                    throw new InvalidOperationException();

                _value = predefined.ParseValue(dataValue.Attribute(L5XNames.Value)?.Value);
                return;
            }

            //otherwise use transforms to construct members
            var formatted = element
                .Descendants(L5XNames.Data).FirstOrDefault(x =>
                    x.HasAttributes && x.Attribute("Format") != null && x.Attribute("Format")?.Value != "L5K");

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

            _value = value ?? _dataType.Default;

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

        public object Value
        {
            get => _value;
            set
            {
                if (!IsValueMember)
                    throw new InvalidOperationException();

                if (!(_dataType is Predefined predefined))
                    throw new InvalidOperationException();

                if (!predefined.IsValidValue(value))
                    throw new InvalidOperationException();

                _value = value;
            }
        }

        public TagType TagType
        {
            get => _tagType;
            set { _tagType = value; }
        }

        public string AliasFor { get; set; }

        public bool Constant { get; set; }

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
        public object ForceValue { get; set; }
        public bool CanForce { get; set; }
        public IEnumerable<ITagMember> Members => _members.AsEnumerable();

        public bool IsValueMember => Value != null && DataType.IsAtomic;
        public bool IsArrayMember => Dimension.Length > 0;
        public bool IsArrayElement => false;
        public bool IsStructureMember => !IsValueMember && !IsArrayMember && _members.Count > 0;

        public XElement Serialize()
        {
            var element = new XElement(nameof(Tag));
            element.Add(new XAttribute(L5XNames.Name, Name));
            element.Add(new XAttribute(L5XNames.TagType, TagType.Name));
            element.Add(new XAttribute(L5XNames.DataType, DataType.Name));
            if (Dimension.Length > 0)
                element.Add(new XAttribute(L5XNames.Dimensions, Dimension.ToString()));
            if (Radix != Radix.Null)
                element.Add(new XAttribute(L5XNames.Radix, Radix.Name));
            element.Add(new XAttribute(L5XNames.Constant, Constant));
            element.Add(new XAttribute(L5XNames.ExternalAccess, ExternalAccess.Name));

            if (!string.IsNullOrEmpty(AliasFor))
                element.Add(new XAttribute(nameof(AliasFor), AliasFor));
            
            if (Usage != TagUsage.Null)
                element.Add(new XAttribute(nameof(Usage), Usage.Name));

            if (!string.IsNullOrEmpty(Description))
                element.Add(new XElement(nameof(Description), Description));

            var data = GenerateDataElement();
            element.Add(data);

            return element;
        }

        private XElement GenerateDataElement()
        {
            var data = new XElement(L5XNames.Data);
            data.Add(new XAttribute("Format", "Decorated"));

            if (IsValueMember)
            {
                var dataValue = new XElement(L5XNames.DataValue);
                dataValue.Add(new XAttribute(L5XNames.DataType, DataType.Name));
                dataValue.Add(new XAttribute(L5XNames.Radix, Radix.Name));
                dataValue.Add(new XAttribute(L5XNames.Value, Value));
                data.Add(dataValue);
                return data;
            }

            if (IsArrayMember)
            {
                var array = new XElement(L5XNames.Array);
                array.Add(new XAttribute(L5XNames.DataType, DataType.Name));
                array.Add(new XAttribute(L5XNames.Dimensions, Dimension.ToString()));
                array.Add(new XAttribute(L5XNames.Radix, Radix.Name));
                array.Add(_members.Select(m => ((TagMember)m).Serialize()));
                data.Add(array);
                return data;
            }

            if (!IsStructureMember) return null;

            var structure = new XElement(L5XNames.Structure);
            structure.Add(new XAttribute(L5XNames.DataType, DataType.Name));
            structure.Add(_members.Select(m => ((TagMember)m).Serialize()));
            data.Add(structure);
            return data;
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