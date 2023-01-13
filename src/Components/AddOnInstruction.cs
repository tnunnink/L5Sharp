using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <footer>
    /// See <a href="https://literature.rockwellautomation.com/idc/groups/literature/documents/rm/1756-rm084_-en-p.pdf">
    /// `Logix 5000 Controllers Import/Export`</a> for more information.
    /// </footer>
    public class AddOnInstruction : ILogixComponent
    {
        private const string LogicRoutineName = "Logic";

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        public RoutineType Type { get; set; } = RoutineType.Rll;
        public Revision Revision { get; set; } = new();
        public string RevisionExtension { get; set; } = string.Empty;
        public string RevisionNote { get; set; } = string.Empty;
        public string Vendor { get; set; } = string.Empty;
        public bool ExecutePreScan { get; set; } = false;
        public bool ExecutePostScan { get; set; } = false;
        public bool ExecuteEnableInFalse { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime EditedDate { get; set; } = DateTime.Now;
        public string EditedBy { get; set; } = string.Empty;
        public Revision SoftwareRevision { get; set; } = new();
        public string AdditionalHelpText { get; set; } = string.Empty;
        public bool IsEncrypted { get; set; } = false;
        public List<Parameter> Parameters { get; set; } = new(SystemParameters());
        public List<Tag> LocalTags { get; set; } = new();
        public List<Routine> Routines { get; set; } = new();

        private static IEnumerable<Parameter> SystemParameters()
        {
            yield return new Parameter
            {
                Name = "EnableIn",
                DataType = "BOOL",
                Radix = Radix.Decimal,
                ExternalAccess = ExternalAccess.ReadOnly,
                Default = new BOOL(true),
                Description = "Enable Input - System Defined Parameter",
                Usage = TagUsage.Input
            };
            
            yield return new Parameter
            {
                Name = "EnableOut",
                DataType = "BOOL",
                Radix = Radix.Decimal,
                ExternalAccess = ExternalAccess.ReadOnly,
                Default = new BOOL(),
                Description = "Enable Output - System Defined Parameter",
                Usage = TagUsage.Output
            };
        }

        /// <inheritdoc />
        public XElement Serialize()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Deserialize(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}