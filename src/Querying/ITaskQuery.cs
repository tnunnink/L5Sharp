using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="ITask"/> elements
    /// within the L5X context.  
    /// </summary>
    public interface ITaskQuery : ILogixQuery<ITask>
    {
        /// <summary>
        /// Filters the source collection to include only tasks with the specified program scheduled to it.
        /// </summary>
        /// <param name="type">The program name for which to filter the task collection.</param>
        /// <returns>A new <see cref="ITaskQuery"/> with the filtered element collection.</returns>
        ITaskQuery OfType(TaskType type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ITaskQuery WithRate(Predicate<ScanRate> predicate);
        
        /// <summary>
        /// Filters the source collection to include only tasks with the specified program scheduled to it.
        /// </summary>
        /// <param name="program">The program name for which to filter the task collection.</param>
        /// <returns>A new <see cref="ITaskQuery"/> with the filtered element collection.</returns>
        ITaskQuery ForProgram(ComponentName program);
    }
}