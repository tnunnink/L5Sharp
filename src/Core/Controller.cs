using System;
using L5Sharp.Abstractions;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public sealed class Controller : ControllerBase
    {
        internal Controller(string name, string processorType, Revision revision, DateTime createdOn,
            DateTime modifiedOn, string? description = null) : 
            base(name, processorType, revision, createdOn, modifiedOn, description)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Controller"/> with the specified name and catalog number.
        /// </summary>
        /// <param name="name">The name of the controller to create.</param>
        /// <param name="processorType">The catalog number of the controller to create.</param>
        /// <param name="revision">The optional revision of the controller. Will default to latest revision.</param>
        /// <param name="description">The optional description of the controller. Will default to empty string.</param>
        /// <param name="catalogService">The optional catalog service provided used to lookup the module definition
        /// for the controller module using the provided catalog number.
        /// By default will use <see cref="ModuleCatalog"/> service.</param>
        public Controller(ComponentName name, CatalogNumber processorType, Revision? revision = null,
            string? description = null, ICatalogService? catalogService = null) :
            base(name, processorType, revision, description, catalogService)
        {
        }
    }
}