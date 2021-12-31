using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// 
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
        /// Gets the <see cref="ILogixContent"/> of the current <see cref="IRoutine{TContent}"/>.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        /// <remarks>
        /// 
        /// </remarks>
        TContent Content { get; }
    }
}