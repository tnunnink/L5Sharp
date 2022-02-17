using System;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Controller : IController
    {
        internal Controller(string name, string processorType, Revision revision, DateTime createdOn,
            DateTime modifiedOn, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = createdOn;
            LastModifiedDate = modifiedOn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="processorType"></param>
        /// <param name="revision"></param>
        /// <param name="description"></param>
        public Controller(ComponentName name, string processorType, Revision revision,
            string? description = null) : this(name, processorType, revision, DateTime.Now, DateTime.Now)
        {
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public string ProcessorType { get; }

        /// <inheritdoc />
        public Revision Revision { get; }

        /// <inheritdoc />
        public DateTime ProjectCreationDate { get; }

        /// <inheritdoc />
        public DateTime LastModifiedDate { get; }
    }
}