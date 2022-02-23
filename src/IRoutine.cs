using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Routine</b> component. 
    /// </summary>
    public interface IRoutine<out TContent> : ILogixComponent where TContent : ILogixContent 
    {
        /// <summary>
        /// Gets the <see cref="Enums.RoutineType"/> of the current <see cref="IRoutine{TContent}"/>.
        /// </summary>
        /// <value>
        /// The <c>RoutineType</c> enumeration option indicating what content the routine contains.
        /// </value>
        RoutineType Type { get; }
        
        /// <summary>
        /// Gets the content of the <see cref="IRoutine{TContent}"/>.
        /// </summary>
        /// <value>
        /// An object that implements the <see cref="ILogixContent"/>. The object is a collection of other value types
        /// that compose the logic of the routine.
        /// </value>
        /// <remarks>
        /// The content of a routine represents the logic or code of the routine...
        /// </remarks>
        TContent Content { get; }
    }
}