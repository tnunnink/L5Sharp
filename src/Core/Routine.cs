using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IRoutine{TContent}" />
    public sealed class Routine<TContent> : IRoutine<TContent> where TContent : ILogixContent
    {
        /// <summary>
        /// Creates a new <see cref="Routine{TContent}"/> instance with the provided arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> of the <c>RllRoutine</c>.</param>
        /// <param name="type"></param>
        /// <param name="description">The string description of the <c>RllRoutine</c>.</param>
        /// <param name="content"></param>
        /// <exception cref="ArgumentNullException">When name is null.</exception>
        internal Routine(string name, RoutineType? type = null, string? description = null,
            TContent? content = default)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? RoutineType.Rll;
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