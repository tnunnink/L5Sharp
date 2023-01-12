using System.Collections;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    public class Tag : ILogixScopedComponent, IEnumerable<TagMember>
    {
        private readonly List<TagMember> _members = new();

        /// <summary>
        /// Creates a new <see cref="Tag"/> component.
        /// </summary>
        public Tag()
        {
        }

        public Tag(string name, AtomicType value)
        {
            
        }

        public Tag(string name)
        {
            
        }

        public static Tag Create<TLogixType>(string name) where TLogixType : ILogixType, new()
        {
            return new Tag();
        }

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public Scope Scope { get; set; } = Scope.Null;

        /// <summary>
        /// the name of the data type that the <see cref="Tag"/> instantiates.
        /// </summary>
        public string DataType { get; set; } = string.Empty;
        
        /// <summary>
        /// The array dimensions of the <see cref="Tag"/>. 
        /// </summary>
        public Dimensions Dimensions { get; set; } = Dimensions.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        public Radix Radix { get; set; } = Radix.Null;
        
        
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.None;
        
        

        public TagType TagType { get; set; } =  TagType.Base;


        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// The atomic value of the <see cref="TagMember"/>.
        /// </summary>
        /// <value>An <see cref="AtomicType"/> representing the value of the member.</value>
        /// <remarks>Value only applies to value type tags (i.e. BOOL, DINT, REAL, etc.). Structure types should have
        /// a null value.</remarks>
        public AtomicType? Value { get; set; } = default;

        /// <inheritdoc />
        public IEnumerator<TagMember> GetEnumerator() => _members.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}