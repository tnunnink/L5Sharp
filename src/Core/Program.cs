using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a <see cref="Enums.ProgramType.Normal"/> Logix <see cref="IProgram"/> component implementation.
    /// </summary>
    public class Program : ProgramBase
    {
        internal Program(ComponentName name, string? description = null,
            string? mainRoutineName = null, string? faultRoutineName = null,
            bool useAsFolder = false, bool testEdits = false, bool disabled = false,
            IEnumerable<ITag<IDataType>>? tags = null, IEnumerable<IRoutine<ILogixContent>>? routines = null) :
            base(name, description, mainRoutineName, faultRoutineName, useAsFolder, testEdits, disabled, tags,
                routines)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Program"/> with the provided name.
        /// </summary>
        /// <param name="name">The name of the Program.</param>
        /// <param name="description">The optional description of the program. Will default to an empty string.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public Program(ComponentName name, string? description = null) : base(name, description)
        {
        }

        /// <inheritdoc />
        public override ProgramType Type => ProgramType.Normal;
    }
}