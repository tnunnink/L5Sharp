using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a <see cref="Enums.ProgramType.Normal"/> Logix <see cref="IProgram"/> component implementation.
    /// </summary>
    public class Program : IProgram, IEquatable<Program>
    {
        internal Program(ComponentName name, string? description = null,
            string? mainRoutineName = null, string? faultRoutineName = null,
            bool useAsFolder = false, bool testEdits = false, bool disabled = false,
            IEnumerable<ITag<IDataType>>? tags = null, IEnumerable<IRoutine<ILogixContent>>? routines = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            TestEdits = testEdits;
            Disabled = disabled;
            UseAsFolder = useAsFolder;
            MainRoutineName = mainRoutineName ?? string.Empty;
            FaultRoutineName = faultRoutineName ?? string.Empty;
            Tags = new ComponentCollection<ITag<IDataType>>(tags ?? Enumerable.Empty<ITag<IDataType>>());
            Routines = new ComponentCollection<IRoutine<ILogixContent>>(routines ?? Enumerable.Empty<IRoutine<ILogixContent>>());
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public ProgramType Type => ProgramType.Normal;

        /// <inheritdoc />
        public bool TestEdits { get; }

        /// <inheritdoc />
        public bool Disabled { get; }

        /// <inheritdoc />
        public string MainRoutineName { get; }

        /// <inheritdoc />
        public string FaultRoutineName { get; }

        /// <inheritdoc />
        public bool UseAsFolder { get; }

        /// <inheritdoc />
        public IComponentCollection<ITag<IDataType>> Tags { get; }

        /// <inheritdoc />
        public IComponentCollection<IRoutine<ILogixContent>> Routines { get; }

        /// <summary>
        /// Creates a new <see cref="IProgram"/> with the provided name and optional description. 
        /// </summary>
        /// <param name="name">The name of the <see cref="Program"/>.</param>
        /// <param name="description">the description of the <see cref="Program"/>.</param>
        /// <returns>A new <see cref="IProgram"/> instance with the provided name and description.</returns>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public static IProgram Create(ComponentName name, string? description = null) => new Program(name, description);

        /// <inheritdoc />
        public bool Equals(Program? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name &&
                   Description == other.Description &&
                   TestEdits == other.TestEdits &&
                   Disabled == other.Disabled &&
                   MainRoutineName.Equals(other.MainRoutineName) &&
                   FaultRoutineName.Equals(other.FaultRoutineName) &&
                   UseAsFolder == other.UseAsFolder &&
                   Tags.SequenceEqual(other.Tags) &&
                   Routines.SequenceEqual(other.Routines);
        }

        /// <inheritdoc />
        public bool Equals(IProgram other) => Equals(other as Program);

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as Program);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(Description);
            hashCode.Add(TestEdits);
            hashCode.Add(Disabled);
            hashCode.Add(MainRoutineName);
            hashCode.Add(FaultRoutineName);
            hashCode.Add(UseAsFolder);
            hashCode.Add(Tags);
            hashCode.Add(Routines);
            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(Program? left, Program? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Program? left, Program? right) => !Equals(left, right);
    }
}