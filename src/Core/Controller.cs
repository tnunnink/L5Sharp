using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class Controller : IController
    {
        internal Controller(string name, ProcessorType processorType, Revision revision, DateTime createdOn,
            DateTime modifiedOn, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = createdOn;
            LastModifiedDate = modifiedOn;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public ProcessorType ProcessorType { get; }

        /// <inheritdoc />
        public Revision Revision { get; }

        /// <inheritdoc />
        public DateTime ProjectCreationDate { get; }

        /// <inheritdoc />
        public DateTime LastModifiedDate { get; }

        /// <summary>
        /// Creates a new instance with the provided arguments. 
        /// </summary>
        /// <param name="name">The name of the controller.</param>
        /// <param name="revision">The revision of the controller.</param>
        /// <param name="processorType">The processor type of the controller.</param>
        /// <param name="description">The description of the controller.</param>
        /// <param name="createdOf">The date/time the project was created.</param>
        /// <param name="modifiedOn">The date/time the project was last modified.</param>
        public static IController Create(ComponentName name, ProcessorType processorType, Revision revision,
            string description = null, DateTime createdOf = default, DateTime modifiedOn = default)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return new Controller(name, processorType, revision, createdOf, modifiedOn, description);
        }
    }
}