using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// Represents a Logix <b>Module</b> component. A module is the basis for defining IO connection to field devices
    /// and other networking equipment.
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer> 
    public class Module : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The catalog number uniquely identifies the module. This is a rockwell defined convention.
        /// </summary>
        public string CatalogNumber { get; set; } = string.Empty;

        /// <summary>
        /// The vendor or manufacturer of the module.
        /// </summary>
        /// <value>A <see cref="Core.Vendor"/> entity that contains the id and name of the vendor.</value>
        /// <remarks>
        /// All modules have a vendor representing the manufacturer of the module.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup. When deserializing from L5X file, typically only the vendor
        /// id is available on the module element.
        /// </remarks>
        public Vendor Vendor { get; set; } = Vendor.Unknown;

        /// <summary>
        /// The product type of the module, representing a category of the module.
        /// </summary>
        /// <remarks>
        /// All modules have a product type representing the product category of the module.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup.
        /// This value will be validated by Logix upon import of the L5X. 
        /// </remarks>
        public ProductType ProductType { get; } = ProductType.Unknown;

        /// <summary>
        /// The unique product code value of the module.
        /// </summary>
        /// <remarks>
        /// This is a unique value that identifies the module and is assigned by Logix.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup, or when deserializing from an L5X file.
        /// </remarks>
        public ushort ProductCode { get; set; } = 0;

        /// <summary>
        /// The revision number or hardware version of the module.
        /// </summary>
        /// <remarks>
        /// All modules must have a specified revision number.
        /// This value can be retrieved as part of the <see cref="ModuleDefinition"/> object obtained using a
        /// <see cref="ICatalogService"/> for catalog lookup.
        /// </remarks>
        public Revision Revision { get; set; } = new();

        /// <summary>
        /// The name of the module that is the parent of the current module. This specified how the module is connected
        /// within the module tree.
        /// </summary>
        /// <value>A <see cref="string"/> representing the parent module name. Default is an empty string.</value>
        public string ParentModule { get; set; } = string.Empty;

        /// <summary>
        /// The port id of the parent module that the current module is connected to.
        /// This specified how the module is connected within the module tree.
        /// </summary>
        /// <value>A <see cref="int"/> representing the id of the parent port. Default is zero.</value>
        public int ParentPortId { get; set; } = 0;

        /// <summary>
        /// An indication of whether the module is inhibited or disabled.
        /// </summary>
        public bool Inhibited { get; set; } = false;

        /// <summary>
        /// An indication of whether the module the module will cause a major fault when faulted.
        /// </summary>
        public bool MajorFault { get; set; } = false;

        /// <summary>
        /// An indication of whether whether the module has safety features enabled.
        /// </summary>
        public bool SafetyEnabled { get; set; } = false;

        /// <summary>
        /// The electronic keying mode of the module.
        /// </summary>
        /// <value>A <see cref="ElectronicKeying"/> enum value representing the mode.</value>
        public ElectronicKeying Keying { get; set; } = ElectronicKeying.CompatibleModule;

        /// <summary>
        /// A collection of <see cref="Port"/> that define the module's connection within the module tree.
        /// </summary>
        public List<Port> Ports { get; set; } = new();

        /// <summary>
        /// A <see cref="Tag"/> containing the configuration data for the module.
        /// </summary>
        public Tag? Config { get; set; }

        /// <summary>
        /// A collection of <see cref="ModuleConnection"/> defining the input and output connection specific to the module.
        /// </summary>
        public List<ModuleConnection> Connections { get; set; } = new();
    }
}