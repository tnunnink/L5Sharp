using L5Sharp.Core;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="IComponentQuery{TResult}"/> that adds advanced querying for <see cref="ITask"/> elements
    /// within the L5X context.  
    /// </summary>
    public interface ITaskQuery : IComponentQuery<ITask>
    {
        /// <summary>
        /// Returns tasks from the queried collection with the specified program name. 
        /// </summary>
        /// <param name="program">The program name for which to filter the task collection.</param>
        /// <returns>A new <see cref="ITaskQuery"/> with the filtered element collection.</returns>
        ITaskQuery WithProgram(ComponentName program);
    }
}