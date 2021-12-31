using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IRoutine{TContent}" />
    public class Routine<TContent> : IRoutine<TContent>, IEquatable<Routine<TContent>>
        where TContent : ILogixContent
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

        /// <inheritdoc />
        public bool Equals(Routine<TContent>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name &&
                   Description == other.Description &&
                   Type.Equals(other.Type) &&
                   EqualityComparer<TContent>.Default.Equals(Content, other.Content);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Routine<TContent>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Type, Content);
        }

        public static bool operator ==(Routine<TContent>? left, Routine<TContent>? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Routine<TContent>? left, Routine<TContent>? right)
        {
            return !Equals(left, right);
        }
    }
}