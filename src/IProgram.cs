using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix Program component.
    /// </summary>
    public interface IProgram : ILogixComponent
    {
        /// <summary>
        /// Gets the <see cref="Enums.ProgramType"/> value for the current <see cref="IProgram"/>.
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
        /// 
        /// </summary>
        IEnumerable<ITag<IDataType>> Tags { get; }
        
        IEnumerable<IRoutine<ILogixContent>> Routines { get; }
    }
}