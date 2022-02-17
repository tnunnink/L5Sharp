using System;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix Controller component. 
    /// </summary>
    /// <remarks>
    /// A Controller component is the root of the L5X content element and can be accessed using the <see cref="ILogixContext"/>.
    /// The controller entity only contains member properties. It does not maintain component collections (i.e. DataTypes, Tags, etc.).
    /// To access these components, use the <see cref="ILogixContext"/> repository instances.
    /// </remarks>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public interface IController : ILogixComponent
    {
        /// <summary>
        /// Gets processor type of the current controller.
        /// </summary>
        string ProcessorType { get; }
        
        /// <summary>
        /// Gets the controller revision.
        /// </summary>
        Revision Revision { get; }
        
        /// <summary>
        /// Gets the project creation date time of the controller.
        /// </summary>
        DateTime ProjectCreationDate { get; }
        
        /// <summary>
        /// Gets the last modified date/time of the controller.
        /// </summary>
        DateTime LastModifiedDate { get; }
    }
}