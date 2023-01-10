using System;
using L5Sharp.Enums;

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
    public abstract class AtomicType : ILogixType
    {
        /// <summary>
        /// Creates a new <see cref="AtomicType"/> instance with the provided name.
        /// </summary>
        /// <param name="name">The name of the atomic type.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        protected internal AtomicType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;
        
        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Atomic;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Radix.Default(this).Format(this);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radix"></param>
        /// <returns></returns>
        public string ToString(Radix radix) => radix.Format(this);
    }
}