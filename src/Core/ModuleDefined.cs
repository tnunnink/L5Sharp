using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class ModuleDefined : IModuleDefined
    {
        internal ModuleDefined(string name, IEnumerable<IMember<IDataType>> members)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        /// <inheritdoc />
        public string Name { get; }

        ComponentName ILogixComponent.Name => Name;

        /// <inheritdoc />
        public string Description => null;

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new ModuleDefined(Name.SafeCopy(), Members.Select(m => m.Copy()));
        }

        /// <inheritdoc />
        public Radix Radix => Radix.Null;

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.Io;

        /// <inheritdoc />
        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> Members { get; }
    }
}