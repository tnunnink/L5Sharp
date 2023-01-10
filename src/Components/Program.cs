using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a <see cref="Enums.ProgramType.Normal"/> Logix <see cref="Program"/> component implementation.
    /// </summary>
    public class Program : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;


        public ProgramType Type { get; set; } = ProgramType.Normal;
        public bool TestEdits { get; set; } = false;

        public bool Disabled { get; set; } = false;

        public string MainRoutineName { get; set; } = string.Empty;


        public string FaultRoutineName { get; set; } = string.Empty;


        public bool UseAsFolder { get; set; } = false;


        public List<Tag> Tags { get; set; }


        public List<Routine> Routines { get; set; }
    }
}