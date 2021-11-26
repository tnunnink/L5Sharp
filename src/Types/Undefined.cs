using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Types
{
    /// <summary>
    /// Represents a data type that is undefined.
    /// </summary>
    /// <remarks>
    /// This is a replacement for null type. If the data type that is read from a L5X is unknown and can not be created,
    /// Undefined will stand in as the placeholder for the data type.
    /// </remarks>
    public sealed class Undefined : IDataType
    {
        /// <summary>
        /// Creates a new instance of an undefined type with the provided type name.
        /// </summary>
        /// <param name="typeName">A name of the type that is not able to be created or inferred.</param>
        public Undefined(string typeName)
        {
            TypeName = typeName;
        }
        
        /// <summary>
        /// Gets the string name of the data type that has not definition.
        /// </summary>
        public string TypeName { get; }

        /// <inheritdoc />
        public ComponentName Name => nameof(Undefined);

        /// <inheritdoc />
        public string Description => "Undefined DataType";

        /// <inheritdoc />
        public Radix Radix => Radix.Null;

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => null;

        /// <inheritdoc />
        public DataFormat Format => null;

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new Undefined(TypeName);
        }
    }
}