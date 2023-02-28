using System;
using L5Sharp.Attributes;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Types
{
    /// <summary>
    /// A fundamental <see cref="ILogixType"/> that represents value type object.
    /// </summary>
    /// <remarks>
    /// Logix atomic types are types that have value (i.e. BOOL, SINT, INT, DINT, REAL, etc.).
    /// These type are synonymous with value types in .NET.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(DataValueSerializer))]
    public abstract class AtomicType : ILogixType
    {
        /// <summary>
        /// Creates a new <see cref="AtomicType"/> instance with the provided name.
        /// </summary>
        /// <param name="name">The name of the atomic type.</param>
        /// <param name="radix"></param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        protected internal AtomicType(string name, Radix? radix = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));

            if (radix is not null && !radix.SupportsType(this))
                throw new ArgumentException();
            
            Radix = radix ?? Radix.Default(this);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <summary>
        /// The radix format for the <see cref="AtomicType"/>.
        /// </summary>
        /// <value>A <see cref="Enums.Radix"/> representing the format of the atomic type value.</value>
        public Radix Radix { get; }

        /// <summary>
        /// Return the atomic value formatted using the current <see cref="Radix"/> format.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
        public override string ToString() => Radix.Format(this);

        /// <summary>
        /// Returns the atomic value formatted using the provided <see cref="Enums.Radix"/> option.
        /// </summary>
        /// <param name="radix">The radix format.</param>
        /// <returns>A <see cref="string"/> representing the formatted atomic value.</returns>
        public string ToString(Radix radix) => radix.Format(this);
    }
}