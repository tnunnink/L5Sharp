using System;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>Controller</b> component. 
    /// </summary>
    /// <remarks>
    /// A Controller component is the root container of an L5X context and can be accessed using the <see cref="ILogixContext"/>.
    /// The controller entity only contains member properties or properties of the root controller element.
    /// It does not maintain child component collections (i.e. DataTypes, Tags, etc.).
    /// To access these components, use the <see cref="ILogixContext"/> repository instances.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IController : ILogixComponent
    {
        /// <summary>
        /// Gets the <see cref="Core.CatalogNumber"/> of the <see cref="IController"/>.
        /// </summary>
        CatalogNumber ProcessorType { get; }
        
        /// <summary>
        /// Gets the <see cref="Core.Revision"/> of the <see cref="IController"/>.
        /// </summary>
        Revision Revision { get; }
        
        /// <summary>
        /// Gets the project creation date time of the <see cref="IController"/>.
        /// </summary>
        DateTime ProjectCreationDate { get; }
        
        /// <summary>
        /// Gets the last modified date/time of the <see cref="IController"/>.
        /// </summary>
        DateTime LastModifiedDate { get; }
    }
}