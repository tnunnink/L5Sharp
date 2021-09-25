using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Primitives
{
    public class Tag : IXSerializable
    {
        private readonly Tuple<int, int, int> _dimensions = new Tuple<int, int, int>(0, 0, 0);
        private readonly Dictionary<string, Tag> _tags = new Dictionary<string, Tag>();
        
        private Tag(string name, IDataType dataType, string description = null, string dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, TagType tagType = null, TagUsage usage = null,
            Scope scope = null, Program program = null, string aliasFor = null, bool constant = false,
            object value = null, Tag parent = null)
        {
            if (string.IsNullOrEmpty(name)) Throw.ArgumentNullOrEmptyException(nameof(name));
            if (dataType == null) throw new ArgumentNullException(nameof(dataType));

            Validate.TagName(name);

            Name = name;
            DataType = dataType;
            Description = description ?? string.Empty;
            Dimensions = dimensions ?? "[0]";
            Radix = radix ?? Radix.Default(dataType);
            ExternalAccess = externalAccess ?? ExternalAccess.None;
            TagType = tagType ?? TagType.Base;
            Usage = usage ?? TagUsage.Null;
            Scope = scope ?? Scope.Null;
            Program = program;
            AliasFor = aliasFor ?? string.Empty;
            Constant = constant;
            Value = value;
            Parent = parent;
            //todo generate tags based on data type
        }

        private Tag(XElement element)
        {
        }

        public Tag(string name, DataType dataType, Program program = null, string description = null)
        {
        }

        public string Name { get; set; }
        public IDataType DataType { get; }
        public string Description { get; set; }
        public string Dimensions { get; private set; }
        public Radix Radix { get; private set; }
        public ExternalAccess ExternalAccess { get; set; }
        public TagType TagType { get; }
        public TagUsage Usage { get; }
        public Scope Scope { get; set; }
        public Program Program { get; set; }
        public string AliasFor { get; set; }
        public bool Constant { get; set; }
        public object Value { get; }
        public object ForceValue { get; }
        public IEnumerable<Tag> Tags => _tags.Values.AsEnumerable();
        private Tag Parent { get; }
        private bool IsBaseTag => Parent == null;
        private bool IsValueMember => Value != null && DataType.IsAtomic;
        private bool IsArrayMember => Dimensions != "0";
        private bool IsStructureMember => !IsArrayMember && !IsValueMember;

        public string FullName { get; set; }
        public string AliasBase { get; set; }
        public bool CanForce { get; set; }
        
        public XElement Serialize()
        {
            throw new NotImplementedException();
        }
    }
}