using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all Logix <see cref="DataTypeClass"/> for a given <see cref="ILogixType"/>.
    /// </summary>
    public sealed class DataTypeClass : SmartEnum<DataTypeClass, string>
    {
        private DataTypeClass(string name, string value) : base(name, value)
        {
        }
        
        /// <summary>
        /// Represents an <b>Unknown</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass Unknown = new(nameof(Unknown), nameof(Unknown));

        /// <summary>
        /// Represents an <b>Atomic</b> <see cref="DataTypeClass"/>.
        /// </summary>
        /// <example>
        /// BOOL, Sint, Int, Dint, Lint, Real.
        /// </example>
        public static readonly DataTypeClass Atomic = new(nameof(Atomic), nameof(Atomic));

        /// <summary>
        /// Represents an <b>Predefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        /// <example>
        /// String, Timer, Counter, Message.
        /// </example>
        public static readonly DataTypeClass Predefined = new(nameof(Predefined), nameof(Predefined));

        /// <summary>
        /// Represents an <b>ModuleDefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass Io = new(nameof(Io), nameof(Io));

        /// <summary>
        /// Represents an <b>UserDefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass User = new(nameof(User), nameof(User));
        
        /// <summary>
        /// Represents an <b>AddOnDefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass AddOnDefined = new(nameof(AddOnDefined), nameof(AddOnDefined));
    }
}