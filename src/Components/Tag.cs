using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// A Logix Tag component...
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public class Tag : ILogixScopedComponent, ILogixTagMember
    {
        private readonly ILogixType? _dataType;

        /// <summary>
        /// Creates a new <see cref="Tag"/> instance.
        /// </summary>
        public Tag()
        {
        }

        /// <summary>
        /// Creates a new <see cref="Tag"/> instance with the provided name and <see cref="ILogixType"/> data.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <param name="dataType">The logix type data of the data.</param>
        /// <exception cref="ArgumentNullException"><c>data</c> is null.</exception>
        public Tag(string name, ILogixType dataType)
        {
            _dataType = dataType ?? throw new ArgumentNullException(nameof(dataType));

            Name = name;
            DataType = dataType.Name;
            Dimensions = dataType is ArrayType arrayType ? arrayType.Dimensions : Dimensions.Empty;
            Radix = dataType is AtomicType atomicType ? Radix.Default(atomicType) : Radix.Null;
        }

        /// <inheritdoc cref="ILogixScopedComponent.Name" />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc cref="ILogixScopedComponent.Description" />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public Scope Scope { get; set; } = Scope.Null;

        /// <summary>
        /// the name of the data type that the <see cref="Tag"/> instantiates.
        /// </summary>
        public string DataType { get; set; } = string.Empty;

        /// <inheritdoc />
        public Dimensions Dimensions { get; set; } = Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix { get; set; } = Radix.Null;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;

        /// <inheritdoc />
        public TagName TagName => new(Name);

        /// <inheritdoc />
        public MemberType MemberType => MemberType.FromType(_dataType);


        public TagType TagType { get; set; } = TagType.Base;

        
        public TagUsage Usage { get; set; } = TagUsage.Normal;

        /// <summary>
        /// The name of the alias tag that the current <see cref="Tag"/> refers to.
        /// </summary>
        /// <value>A <see cref="Core.TagName"/> string representing the full tag name of the alias tag.</value>
        public TagName Alias { get; set; } = TagName.Empty;

        /// <summary>
        /// The flag indicating whether the <see cref="Tag"/> is a constant.
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the tag is constant; otherwise, <c>false</c>.</value>
        /// <remarks>Only value type tags have the ability to be set as a constant. Default is <c>false</c>.</remarks>
        public bool Constant { get; set; } = false;

        
        public Dictionary<TagName, string> Comments { get; } = new();

        
        public Dictionary<TagName, string> Units { get; } = new();

        /// <inheritdoc />
        public TagMember? Member(TagName tagName)
        {
            if (_dataType is null)
                throw new InvalidOperationException("Tag has not internal data type structure.");

            Check.TagNameNotNullOrEmpty(tagName);

            var childName = tagName.Members.First();
            var childMember = GetMembers(_dataType).SingleOrDefault(m => m.Name == childName);

            if (childMember is null) return null;

            var tagMember = new TagMember(childMember, this, this);

            var next = TagName.Combine(tagName.Members.Skip(1));

            return next.IsEmpty ? tagMember : tagMember.Member(next);
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public IEnumerable<TagMember> Members(TagName tagName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public AtomicType GetValue()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public TAtomic GetValue<TAtomic>() where TAtomic : AtomicType
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void SetValue(AtomicType atomicType)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool TrySetValue(AtomicType atomicType)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }

        private static IEnumerable<Member> GetMembers(ILogixType dataType)
        {
            switch (dataType)
            {
                case StructureType structureType:
                    structureType.Members();
                    break;
                case ArrayType arrayType:
                    return arrayType.Members();
            }

            return Enumerable.Empty<Member>();
        }
    }
}