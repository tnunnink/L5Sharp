using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// A base class for all <see cref="IProgram"/> components.
    /// </summary>
    public abstract class ProgramBase : IProgram
    {
        internal ProgramBase(ComponentName name, string? description = null,
            string? mainRoutineName = null, string? faultRoutineName = null,
            bool useAsFolder = false, bool testEdits = false, bool disabled = false,
            IEnumerable<ITag<IDataType>>? tags = null, IEnumerable<IRoutine<ILogixContent>>? routines = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            TestEdits = testEdits;
            Disabled = disabled;
            UseAsFolder = useAsFolder;
            MainRoutineName = mainRoutineName ?? string.Empty;
            FaultRoutineName = faultRoutineName ?? string.Empty;
            Tags = new ComponentCollection<ITag<IDataType>>(tags ?? Enumerable.Empty<ITag<IDataType>>());
            Routines = new ComponentCollection<IRoutine<ILogixContent>>(routines ?? Enumerable.Empty<IRoutine<ILogixContent>>());
        }
        
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public abstract ProgramType Type { get; }

        /// <inheritdoc />
        public bool TestEdits { get; }

        /// <inheritdoc />
        public bool Disabled { get; }

        /// <inheritdoc />
        public string MainRoutineName { get; }

        /// <inheritdoc />
        public string FaultRoutineName { get; }

        /// <inheritdoc />
        public bool UseAsFolder { get; }

        /// <inheritdoc />
        public IComponentCollection<ITag<IDataType>> Tags { get; }

        /// <inheritdoc />
        public IComponentCollection<IRoutine<ILogixContent>> Routines { get; }
    }
}