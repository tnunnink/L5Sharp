using System;
using System.Linq;
using L5Sharp.Core;

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// A base class for all <see cref="IController"/> components.
    /// </summary>
    public abstract class ControllerBase : IController
    {
        internal ControllerBase(string name, string processorType, Revision revision, DateTime createdOn,
            DateTime modifiedOn, string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = createdOn;
            LastModifiedDate = modifiedOn;
        }
        
        internal ControllerBase(ComponentName name, CatalogNumber processorType, Revision? revision = null,
            string? description = null, ICatalogService? catalogService = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;

            if (processorType is null)
                throw new ArgumentNullException(nameof(processorType));

            catalogService ??= new LogixCatalog();
            var definition = catalogService.Lookup(processorType);

            ProcessorType = definition.CatalogNumber;
            Revision = revision ?? definition.Revisions.Max();
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
        
        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public CatalogNumber ProcessorType { get; }

        /// <inheritdoc />
        public Revision Revision { get; }

        /// <inheritdoc />
        public DateTime ProjectCreationDate { get; }

        /// <inheritdoc />
        public DateTime LastModifiedDate { get; }
    }
}