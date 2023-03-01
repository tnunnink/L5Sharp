using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Attributes;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Serialization;
using L5Sharp.Types.Atomics;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    [XmlType(L5XName.AddOnInstructionDefinition)]
    [LogixSerializer(typeof(AddOnInstructionSerializer))]
    public class AddOnInstruction : ILogixComponent
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The revision or version of the instruction.
        /// </summary>
        /// <value>A <see cref="Core.Revision"/> representing the version of the instruction.</value>
        public Revision Revision { get; set; } = new();
        
        /// <summary>
        /// Additional text indicating or identifying the revision of the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> containing text of the revision extension.</value>
        public string RevisionExtension { get; set; } = string.Empty;
        
        /// <summary>
        /// Additional text describing release information or changes with the current revision(s).
        /// </summary>
        /// <value>A <see cref="string"/> containing the text of the revision note.</value>
        public string RevisionNote { get; set; } = string.Empty;

        /// <summary>
        /// The vendor or creator of the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> value representing the name of the vendor.</value>
        public string Vendor { get; set; } = string.Empty;

        /// <summary>
        /// Indicates that the instruction has and executes a pre scan routine.
        /// </summary>
        /// <value><c>true</c> if the instruction executes a pre scan routine; otherwise, <c>false</c>.</value>
        public bool ExecutePreScan { get; set; } = false;

        /// <summary>
        /// Indicates that the instruction has and executes a post scan routine.
        /// </summary>
        /// <value><c>true</c> if the instruction executes a post scan routine; otherwise, <c>false</c>.</value>
        public bool ExecutePostScan { get; set; } = false;

        /// <summary>
        /// Indicates that the instruction has and executes a enable in false routine.
        /// </summary>
        /// <value>A <see cref="bool"/> - <c>true</c> if the instruction executes a enable in false routine; otherwise, <c>false</c>.</value>
        public bool ExecuteEnableInFalse { get; set; } = false;

        /// <summary>
        /// The date and time that the instruction was created.
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing the creation date and time.</value>
        public DateTime CreatedDate { get; set; } = default;
        
        /// <summary>
        /// The name of the user that created the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the user.</value>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// The date and time that the instruction was last edited.
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing the edit date and time.</value>
        public DateTime EditedDate { get; set; } = default;
        
        /// <summary>
        /// The name of the user that last edited the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the user.</value>
        public string EditedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// The software version of the instruction.
        /// </summary>
        /// <value>A <see cref="Core.Revision"/> representing the version of the instruction.</value>
        public Revision SoftwareRevision { get; set; } = new();
        
        /// <summary>
        /// The software version of the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> .</value>
        public string AdditionalHelpText { get; set; } = string.Empty;
        
        /// <summary>
        /// The software version of the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> .</value>
        public bool IsEncrypted { get; set; }
        
        /// <summary>
        /// Gets the collection of <see cref="Core.Parameter"/> objects that define the AOI structure type.
        /// </summary>
        public List<Parameter> Parameters { get; set; } = new();
        
        /// <summary>
        /// Gets the collection of local <see cref="Tag"/> objects used within the AOI logic.
        /// </summary>
        public List<Tag> LocalTags { get; set; } = new();
        
        /// <summary>
        /// Gets the collection of <see cref="Routine"/> objects that contain the logic for the instruction.
        /// </summary>
        public List<Routine> Routines { get; set; } = new();
    }
}