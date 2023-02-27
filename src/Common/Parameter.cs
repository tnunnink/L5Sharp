using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Common
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Parameter : DataTypeMember
    {
        public TagType TagType { get; set; } = TagType.Base;
        public TagUsage Usage { get; set; } = TagUsage.Input;
        public bool Required { get; set; } = false;
        public bool Visible { get; set; } = false;
        public TagName Alias { get; set; } = TagName.Empty;
        public AtomicType? Default { get; set; } = null;
        public bool Constant { get; set; } = false;
    }
}