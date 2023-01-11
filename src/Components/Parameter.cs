using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Parameter : ILogixMember
    {
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
        
        public MemberType MemberType { get; }
        public TagType TagType { get; set; } = TagType.Base;
        public TagUsage Usage { get; set; } = TagUsage.Input;
        public bool Required { get; set; } = false;
        public bool Visible { get; set; } = false;
        public TagName Alias { get; set; } = TagName.Empty;
        public AtomicType? Default { get; set; } = null;
        public bool Constant { get; set; } = false;
    }
}