using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Program : IProgram
    {
        internal Program(ComponentName name, string? description = null,
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

            Tags = new ComponentCollection<ITag<IDataType>>(tags);
            Routines = new ComponentCollection<IRoutine<ILogixContent>>(routines);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public ProgramType Type => ProgramType.Normal;

        /// <inheritdoc />
        public bool TestEdits { get; }

        /// <inheritdoc />
        public bool Disabled { get; set; }

        /// <inheritdoc />
        public ComponentName MainRoutineName { get; }

        /// <inheritdoc />
        public ComponentName FaultRoutineName { get; }
        
        /// <inheritdoc />
        public bool UseAsFolder { get; }

        /// <inheritdoc />
        public IComponentCollection<ITag<IDataType>> Tags { get; }

        /// <inheritdoc />
        public IComponentCollection<IRoutine<ILogixContent>> Routines { get; }

        /// <summary>
        /// Creates a new <see cref="Program"/> instance with the provided arguments. 
        /// </summary>
        /// <param name="name">The name of the <see cref="IProgram"/>.</param>
        /// <param name="description">the description of the <see cref="IProgram"/>.</param>
        /// <returns>A new <see cref="IProgram"/> instance with the provided name and description.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public static Program Create(ComponentName name, string? description = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new Program(name, description);
        }
    }
}