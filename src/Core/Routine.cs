using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IRoutine{TContent}" />
    public sealed class Routine<TContent> : IRoutine<TContent> where TContent : ILogixContent
    {
        /// <summary>
        /// Creates a new <see cref="Routine{TContent}"/> instance with the provided arguments.
        /// </summary>
        /// <param name="name">The name of the routine.</param>
        /// <param name="type">The type of content that the routine contains. See <see cref="RoutineType"/>.</param>
        /// <param name="description">The string description of the routine.</param>
        /// <param name="content">The content of the routine. See <see cref="ILogixContent"/>.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        internal Routine(string name, RoutineType? type = null, string? description = null,
            TContent? content = default)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type is null || type == RoutineType.Typeless ? RoutineType.Rll : type;
            Description = description ?? string.Empty;
            Content = content ?? (TContent)Type.CreateContent();
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public RoutineType Type { get; }

        /// <inheritdoc />
        public TContent Content { get; }
    }
}