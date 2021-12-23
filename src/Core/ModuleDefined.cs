using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ModuleDefined : ComplexType
    {
        /// <summary>
        /// Creates a new instance of a <c>ModuleDefined</c> data type.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="members">The collection of members to initialize the collection with</param>
        internal ModuleDefined(string name, IEnumerable<IMember<IDataType>> members) :
            base(name, string.Empty, members)
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.Io;

        /// <inheritdoc />
        protected override IDataType New() => new ModuleDefined(string.Copy(Name), Members.Select(m => m.Copy()));
    }
}