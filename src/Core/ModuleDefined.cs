using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ModuleDefined : IComplexType
    {
        internal ModuleDefined(string name, IEnumerable<IMember<IDataType>> members)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        /// <inheritdoc />
        public string Name { get; }

        ComponentName ILogixComponent.Name => null; //todo dont love this. Type could be casted and get null for name...

        /// <inheritdoc />
        public string Description => null;

        /// <inheritdoc />
        public Radix Radix => Radix.Null;

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Io;

        /// <inheritdoc />
        public DataFormat Format => DataFormat.Decorated;

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> Members { get; }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new ModuleDefined(Name.SafeCopy(), Members.Select(m => m.Copy()));
        }
    }
}