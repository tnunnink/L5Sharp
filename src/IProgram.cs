using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix Program component.
    /// </summary>
    public interface IProgram : ILogixComponent
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
        ComponentName MainRoutineName { get; }
        
        /// <summary>
        /// Gets the name of the <see cref="IRoutine{TContent}"/> that represents the routine that is executed when
        /// a fault occurs for the <see cref="IProgram"/>. 
        /// </summary>
        ComponentName FaultRoutineName { get; }
        
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