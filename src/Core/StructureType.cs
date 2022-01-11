using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// A generic implementation of a <see cref="IComplexType"/> that generated from reading
    /// L5X <see cref="ITag{TDataType}"/> data structures.
    /// </summary>
    public class StructureType : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="StructureType"/> with the provided name, class, and member collection.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="class">The type's <see cref="Enums.DataTypeClass"/>.</param>
        /// <param name="members">The collection of members to initialize.</param>
        /// <exception cref="ArgumentNullException">name or class is null.</exception>
        internal StructureType(string name, DataTypeClass @class,
            IEnumerable<IMember<IDataType>>? members = null) : base(name, members)
        {
            Class = @class ?? throw new ArgumentNullException(nameof(@class));
        }

        /// <inheritdoc />
        public override DataTypeClass Class { get; }

        /// <inheritdoc />
        protected override IDataType New() => new StructureType(string.Copy(Name), Class, 
            Members.Select(m => new Member<IDataType>(string.Copy(m.Name), m.DataType.Instantiate(),
                m.Radix, m.ExternalAccess, string.Copy(m.Description))));
    }
}