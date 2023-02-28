using L5Sharp.Attributes;
using L5Sharp.Enums;
using L5Sharp.Serialization;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [LogixSerializer(typeof(ProgramSerializer))]
    public class Program : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets the type of the program (Normal, Equipment Phase).
        /// </summary>
        /// <value>A <see cref="Enums.ProgramType"/> enum representing the type of the program.</value>
        public ProgramType Type { get; set; } = ProgramType.Normal;
        
        /// <summary>
        /// The value indicating whether the program has current test edits pending.
        /// </summary>
        /// <value>>A <see cref="bool"/>; <c>true</c>if the program has test edits; otherwise <c>false</c>.</value>
        public bool TestEdits { get; set; }

        /// <summary>
        /// The value indicating whether the program is disabled (or inhibited).
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the program is disabled; otherwise <c>false</c>.</value>
        public bool Disabled { get; set; }

        /// <summary>
        /// The name of the routine that serves as the entry point for the program (i.e. main routine).
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the main routine for the program.</value>
        public string MainRoutineName { get; set; } = string.Empty;

        /// <summary>
        /// The name of the routine that serves as the fault routine for the program.
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the fault routine for the program.</value>
        public string FaultRoutineName { get; set; } = string.Empty;

        /// <summary>
        /// A flag indicating whether the program is used as a folder or container for other programs,
        /// as opposed to a container of tags and logix.
        /// </summary>
        /// <value>A <see cref="bool"/>; <c>true</c> if the program is a folder; otherwise, <c>false</c>.</value>
        public bool UseAsFolder { get; set; }
    }
}