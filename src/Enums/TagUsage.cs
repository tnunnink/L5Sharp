using System;
using Ardalis.SmartEnum;
using L5Sharp.Types;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents an enumeration of all Logix <see cref="TagUsage"/> options for a given <see cref="ITag{TDataType}"/>.
    /// </summary>
    public class TagUsage : SmartEnum<TagUsage, string>
    {
        private TagUsage(string name, string value) : base(name, value)
        {
        }

        /// <summary>
        /// Represents a Null <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage Null = new(nameof(Null), nameof(Null));
        
        /// <summary>
        /// Represents a Normal <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage Normal = new(nameof(Normal), nameof(Normal));
        
        /// <summary>
        /// Represents a Local <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage Local = new(nameof(Local), nameof(Local));
        
        /// <summary>
        /// Represents a Input <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage Input = new(nameof(Input), nameof(Input));
        
        /// <summary>
        /// Represents a Output <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage Output = new(nameof(Output), nameof(Output));
        
        /// <summary>
        /// Represents a InOut <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage InOut = new(nameof(InOut), nameof(InOut));
        
        /// <summary>
        /// Represents a Static <see cref="TagType"/> value.
        /// </summary>
        public static readonly TagUsage Static = new(nameof(Static), nameof(Static));
    }
}