using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Program</b> component.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IProgram"/> is the base interface for all <see cref="Enums.ProgramType"/> implementations.
    /// <see cref="IProgram"/> is a container for <see cref="IRoutine{TContent}"/> and <see cref="ITag{TDataType}"/>
    /// components. 
    /// </para>
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IProgram : ILogixComponent, IEquatable<IProgram>
    {
        /// <summary>
        /// Gets the <see cref="Enums.ProgramType"/> value for the <see cref="IProgram"/>.
        /// </summary>
        ProgramType Type { get; }
        
        /// <summary>
        /// Gets the value indicating whether the <see cref="IProgram"/> has test edits. 
        /// </summary>
        bool TestEdits { get; }
        
        /// <summary>
        /// Gets the value indicating whether the <see cref="IProgram"/> is disabled.
        /// </summary>
        bool Disabled { get; }

        /// <summary>
        /// Gets the name of the <see cref="IRoutine{TContent}"/> that represents the main entry point routine
        /// for the <see cref="IProgram"/>.
        /// </summary>
        string MainRoutineName { get; }
        
        /// <summary>
        /// Gets the name of the <see cref="IRoutine{TContent}"/> that represents the routine that is executed when
        /// a fault occurs for the <see cref="IProgram"/>. 
        /// </summary>
        string FaultRoutineName { get; }
        
        /// <summary>
        /// A value indicating whether the current <see cref="IProgram"/> is a logical container for child programs.
        /// </summary>
        bool UseAsFolder { get; }

        /// <summary>
        /// Gets the collection of <see cref="ITag{TDataType}"/> objects contained in the current <see cref="IProgram"/>.
        /// </summary>
        IComponentCollection<ITag<IDataType>> Tags { get; }
        
        /// <summary>
        /// Gets the collection of <see cref="IRoutine{TContent}"/> objects contained in the current <see cref="IProgram"/>.
        /// </summary>
        IComponentCollection<IRoutine<ILogixContent>> Routines { get; }
    }
}