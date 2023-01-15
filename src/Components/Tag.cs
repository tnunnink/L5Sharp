using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    /// <summary>
    /// A Logix Tag component...
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public class Tag : ILogixScopedComponent
    {
        private readonly ILogixType? _data;

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
        /// <param name="data">The logix type data of the data.</param>
        /// <exception cref="ArgumentNullException"><c>data</c> is null.</exception>
        public Tag(string name, ILogixType data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            
            Name = name;
            DataType = data.Name;
            Dimensions = data is ArrayType arrayType ? arrayType.Dimensions : Dimensions.Empty;
            Radix = data is AtomicType atomicType ? Radix.Default(atomicType) : Radix.Null;
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
        
        /// <summary>
        /// 
        /// </summary>
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.ReadWrite;
        
        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public Dictionary<TagName, string> Comments = new();
        
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<TagName, string> Units = new();

        public TagMember Member(TagName tagName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TagMember> Members()
        {
            throw new System.NotImplementedException();
        }
        
        public IEnumerable<TagMember> Members(TagName tagName)
        {
            throw new System.NotImplementedException();
        }

        public AtomicType GetValue()
        {
            throw new System.NotImplementedException();
        }
        
        public TAtomic GetValue<TAtomic>() where TAtomic : AtomicType
        {
            throw new System.NotImplementedException();
        }

        public void SetValue(AtomicType atomicType)
        {
            throw new System.NotImplementedException();
        }

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
    }
}