using System.Net;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// A set of configurable properties of the <see cref="IModule"/> that are used for constructing new modules.
    /// </summary>
    public class ModuleConfiguration
    {
        /// <summary>
        /// Creates a new default <see cref="ModuleConfiguration"/> object with default property values.
        /// </summary>
        public ModuleConfiguration()
        {
            Description = string.Empty;
            Keying = ElectronicKeying.CompatibleModule;
            Slot = 0;
            IP = IPAddress.Any;
            ChassisSize = 0;
            Inhibited = false;
            MajorFault = false;
            SafetyEnabled = false;
        }
        
        /// <summary>
        /// Gets or sets the description of the module configuration object.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the revision of the module configuration object.
        /// </summary>
        public Revision? Revision { get; set; }
        
        /// <summary>
        /// Gets of sets the Slot of the module configuration.
        /// </summary>
        /// 
        public byte Slot { get; set; }
        
        /// <summary>
        /// Gets of sets the IP of the module configuration.
        /// </summary>
        public IPAddress IP { get; set; }
        
        /// <summary>
        /// Gets of sets the ChassisSize of the module configuration.
        /// </summary>
        public byte ChassisSize { get; set; }
        
        /// <summary>
        /// Gets of sets the Keying of the module configuration.
        /// </summary>
        public ElectronicKeying Keying { get; set; }
        
        /// <summary>
        /// Gets of sets the Inhibited of the module configuration.
        /// </summary>
        public bool Inhibited { get; set; }
        
        /// <summary>
        /// Gets of sets the MajorFault of the module configuration.
        /// </summary>
        public bool MajorFault { get; set; }
        
        /// <summary>
        /// Gets of sets the SafetyEnabled of the module configuration.
        /// </summary>
        public bool SafetyEnabled { get; set; }
    }
}