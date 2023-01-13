using System;
using L5Sharp.Core;

namespace L5Sharp.Components
{
    /// <inheritdoc />
    public class Controller : ILogixComponent
    {
        /// <summary>
        /// Creates a new default <see cref="Controller"/> object with empty or null parameters. This is primarily for
        /// use as a L5X container of other logix component objects.
        /// </summary>
        public Controller()
        {
            Name = string.Empty;
            Description = string.Empty;
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
        
        /// <summary>
        /// Creates a new <see cref="Controller"/> with the specified name and catalog number.
        /// </summary>
        /// <param name="name">The name of the controller to create.</param>
        /// <param name="processorType">The catalog number of the controller to create.</param>
        /// <param name="revision">The optional revision of the controller. Will default to latest revision.</param>
        /// <param name="projectCreationDate">The optional date time that the logix project was created.</param>
        /// <param name="lastModifiedDate">The optional date time that the project was last modified.</param>
        /// <param name="description">The optional description of the controller. Will default to empty string.</param>
        public Controller(ComponentName name, string? processorType = null, Revision? revision = null,
            DateTime? projectCreationDate = null, DateTime? lastModifiedDate = null, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = projectCreationDate ?? DateTime.Now;
            LastModifiedDate = lastModifiedDate ?? DateTime.Now;
            Description = description ?? string.Empty;
        }
        
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        
        public string? ProcessorType { get; }

        
        public Revision? Revision { get; }

        
        public DateTime ProjectCreationDate { get; }

        
        public DateTime LastModifiedDate { get; }
    }
}