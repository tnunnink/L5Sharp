using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    internal class DataType : ComplexType
    {
        public DataType(string name, DataTypeClass typeClass, string description = null,
            IEnumerable<IMember<IDataType>> members = null) : base(name, description, members)
        {
            Class = typeClass;
        }

        /// <inheritdoc />
        public override DataTypeClass Class { get; }

        public static IComplexType ModuleDefined(string name, IEnumerable<IMember<IDataType>> members)
        {
            return new DataType(name, DataTypeClass.Io, string.Empty, members);
        }

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new DataType(Name.Copy(), Class, Description.SafeCopy(), Members.Select(m => m.Copy()));
        }
    }
}