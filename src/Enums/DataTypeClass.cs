using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all Logix <see cref="DataTypeClass"/> for a given <see cref="IDataType"/>.
    /// </summary>
    public sealed class DataTypeClass : SmartEnum<DataTypeClass>
    {
        private DataTypeClass(string name, int value) : base(name, value)
        {
        }
        
        /// <summary>
        /// Represents an <b>Unknown</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass Unknown = new(nameof(Unknown), -1);

        /// <summary>
        /// Represents an <b>Atomic</b> <see cref="DataTypeClass"/>.
        /// </summary>
        /// <example>
        /// Bool, Sint, Int, Dint, Lint, Real.
        /// </example>
        public static readonly DataTypeClass Atomic = new(nameof(Atomic), 0);

        /// <summary>
        /// Represents an <b>Predefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        /// <example>
        /// String, Timer, Counter, Message.
        /// </example>
        public static readonly DataTypeClass Predefined = new(nameof(Predefined), 1);

        /// <summary>
        /// Represents an <b>ModuleDefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass Io = new(nameof(Io), 2);

        /// <summary>
        /// Represents an <b>UserDefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass User = new(nameof(User), 3);
        
        /// <summary>
        /// Represents an <b>AddOnDefined</b> <see cref="DataTypeClass"/>.
        /// </summary>
        public static readonly DataTypeClass AddOnDefined = new(nameof(AddOnDefined), 4);
    }
}