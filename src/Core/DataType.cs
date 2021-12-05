using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    internal class DataType : ComplexType
    {
        public DataType(string name, DataTypeClass typeClass, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null) : base(name, description, members)
        {
            Class = typeClass;
        }

        /// <inheritdoc />
        public override DataTypeClass Class { get; }

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new DataType(string.Copy(Name), Class, string.Copy(Description), Members.Select(m => m.Copy()));
        }
    }
}