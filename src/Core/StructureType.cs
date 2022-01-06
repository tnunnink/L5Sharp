using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// A generic implementation of a <see cref="IComplexType"/> that generated from reading L5X <see cref="ITag{TDataType}"/> data structures.
    /// </summary>
    public class StructureType : ComplexType
    {
        /// <summary>
        /// Creates a new <see cref="StructureType"/> with the provided name, class, and member collection.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="class"></param>
        /// <param name="members"></param>
        /// <exception cref="ArgumentNullException"></exception>
        internal StructureType(string name, DataTypeClass @class,
            IEnumerable<IMember<IDataType>>? members = null) : base(name, members)
        {
            Class = @class ?? throw new ArgumentNullException(nameof(@class));
        }


        /// <inheritdoc />
        public override DataTypeClass Class { get; }

        /// <inheritdoc />
        protected override IDataType New() => new StructureType(Name, Class, Members.Select(m => m.Copy()));
    }
}