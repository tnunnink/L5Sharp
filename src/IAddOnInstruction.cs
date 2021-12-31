using System;
using System.Collections.Generic;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents the IAddOnInstructionDefinition component of an L5X export.
    /// </summary>
    public interface IAddOnInstruction : IComplexType
    {
        /// <summary>
        /// Gets the <c>RoutineType</c> of the logic routine for the current <c>IAddOnInstruction</c>. 
        /// </summary>
        RoutineType Type { get; }
        
        /// <summary>
        /// Gets the <c>Revision</c> value for the current <c>IAddOnInstruction</c>.
        /// </summary>
        Revision Revision { get; }
        
        /// <summary>
        /// Gets the value of the string that represent the 
        /// </summary>
        string RevisionExtension { get; }
        
        /// <summary>
        /// Gets the value of the revision note for the current <c>IAddOnInstruction</c>.
        /// </summary>
        string RevisionNote { get; }
        
        /// <summary>
        /// Gets the value of the vendor for the current <c>IAddOnInstruction</c>. 
        /// </summary>
        string Vendor { get; }
        
        /// <summary>
        /// Gets the value indicating whether the <c>IAddOnInstruction</c> will execute a pre-scan routine. 
        /// </summary>
        bool ExecutePreScan { get; set; }
        
        /// <summary>
        /// Gets the value indicating whether the <c>IAddOnInstruction</c> will execute a post-scan routine. 
        /// </summary>
        bool ExecutePostScan { get; set; }
        
        /// <summary>
        /// Gets the value indicating whether the <c>IAddOnInstruction</c> will execute a Enable-In routine.
        /// </summary>
        bool ExecuteEnableInFalse { get; set; }
        
        /// <summary>
        /// Gets the DateTime that the <c>IAddOnInstruction</c> was created.
        /// </summary>
        DateTime CreatedDate { get; }
        
        /// <summary>
        /// 
        /// </summary>
        string CreatedBy { get; }
        DateTime EditedDate { get; }
        string EditedBy { get; }
        Revision SoftwareRevision { get; }
        string AdditionalHelpText { get; }
        bool IsEncrypted { get; }
        IMembers<IParameter<IDataType>> Parameters { get; }
        IEnumerable<ITag<IDataType>> LocalTags { get; }
        IEnumerable<IRoutine<ILogixContent>> Routines { get; }
    }
}