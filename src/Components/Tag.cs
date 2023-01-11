using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Components
{
    public class Tag : ILogixScopedComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <inheritdoc />
        public Scope Scope { get; set; } = Scope.Null;

        public string DataType { get; set; } = string.Empty;
        public Dimensions Dimensions { get; set; } = Dimensions.Empty;
        public Radix Radix { get; set; } = Radix.Null;
        public ExternalAccess ExternalAccess { get; set; } = ExternalAccess.None;
        public TagType TagType { get; set; } =  TagType.Base;
        public TagUsage Usage { get; set; } = TagUsage.Null;
        public TagName Alias { get; set; } = TagName.Empty;
        public bool Constant { get; set; } = false;
        public TagName TagName => new(Name);
        public AtomicType? Value { get; set; } = null;
    }
}