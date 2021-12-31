using System;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// A static component factory class for creating new instances of <see cref="IRoutine{TContent}"/>.
    /// </summary>
    public static class Routine
    {
        /// <summary>
        /// Creates a new <see cref="IRoutine{ILogixContent}"/> with the provided parameters.
        /// </summary>
        /// <param name="name">The <see cref="Core.ComponentName"/> of the <see cref="IRoutine{TContent}"/>.</param>
        /// <param name="description">The string description of the <see cref="IRoutine{TContent}"/>.</param>
        /// <returns>A new <see cref="IRoutine{ILogixContent}"/> instance with the provided name and description.</returns>
        /// <exception cref="ArgumentNullException">When name is null.</exception>
        /// <seealso cref="Create{TContent}"/>
        public static IRoutine<ILogixContent> Create(ComponentName name, string? description = null) =>
            Create<ILogixContent>(name, description);
        
        /// <summary>
        /// Creates a new <see cref="IRoutine{TContent}"/> with the provided parameters.
        /// </summary>
        /// <param name="name">The <see cref="Core.ComponentName"/> of the <see cref="IRoutine{TContent}"/>.</param>
        /// <param name="description">The string description of the <see cref="IRoutine{TContent}"/>.</param>
        /// <typeparam name="TContent">The <see cref="ILogixContent"/> type that the <see cref="IRoutine{TContent}"/> contains.</typeparam>
        /// <returns>A new <see cref="IRoutine{TContent}"/> instance with the provided name and description.</returns>
        /// <exception cref="ArgumentNullException">When name is null.</exception>
        /// <seealso cref="Create"/>
        public static IRoutine<TContent> Create<TContent>(ComponentName name, string? description = null)
            where TContent : ILogixContent
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new Routine<TContent>(name, RoutineType.ForType<TContent>(), description);
        }
    }
}