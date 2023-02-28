using Ardalis.SmartEnum;
using L5Sharp.Components;
using L5Sharp.Types;

namespace L5Sharp.Enums
{
    /// <summary>
    /// Represents an enumeration of all Logix <see cref="TagUsage"/> options for a given <see cref="Tag"/>.
    /// </summary>
    public class TagUsage : SmartEnum<TagUsage, string>
    {
        private TagUsage(string name, string value) : base(name, value)
        {
        }
        
        /// <summary>
        /// Gets the default <see cref="Radix"/> type for the provided data type instance.
        /// </summary>
        /// <param name="logixType">The data type instance to evaluate.</param>
        /// <returns>
        /// <see cref="Input"/> for atomic types.
        /// <see cref="InOut"/> for complex types.
        /// </returns>
        public static TagUsage Default(ILogixType logixType) => logixType is AtomicType ? Input : InOut;


        /// <summary>
        /// Represents a Null <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Null = new(nameof(Null), nameof(Null));
        
        /// <summary>
        /// Represents a Normal <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Normal = new(nameof(Normal), nameof(Normal));
        
        /// <summary>
        /// Represents a Local <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Local = new(nameof(Local), nameof(Local));
        
        /// <summary>
        /// Represents a Public <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Public = new(nameof(Public), nameof(Public));
        
        /// <summary>
        /// Represents a Input <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Input = new(nameof(Input), nameof(Input));
        
        /// <summary>
        /// Represents a Output <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Output = new(nameof(Output), nameof(Output));
        
        /// <summary>
        /// Represents a InOut <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage InOut = new(nameof(InOut), nameof(InOut));
        
        /// <summary>
        /// Represents a Static <see cref="TagUsage"/> value.
        /// </summary>
        public static readonly TagUsage Static = new(nameof(Static), nameof(Static));
    }
}