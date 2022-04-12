using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix <b>AddOnInstruction</b> component. 
    /// </summary>
    public interface IAddOnInstruction : IComplexType
    {
        /// <summary>
        /// Gets the <c>RoutineType</c> of the logic routine for the current <c>IAddOnInstruction</c>. 
        /// </summary>
        RoutineType Type { get; }

        /// <summary>
        /// Gets the <see cref="Revision"/> value for the current <see cref="IAddOnInstruction"/>.
        /// </summary>
        Revision Revision { get; }

        /// <summary>
        /// Gets the string text that represent an extension of the revision value for the <see cref="IAddOnInstruction"/>. 
        /// </summary>
        string RevisionExtension { get; }

        /// <summary>
        /// Gets the value of the revision note for the instruction.
        /// </summary>
        string RevisionNote { get; }

        /// <summary>
        /// Gets the string vendor name of the instruction. 
        /// </summary>
        string Vendor { get; }

        /// <summary>
        /// Gets the value indicating whether the <see cref="IAddOnInstruction"/> will execute a pre-scan routine. 
        /// </summary>
        bool ExecutePreScan { get; }

        /// <summary>
        /// Gets the value indicating whether the <see cref="IAddOnInstruction"/> will execute a post-scan routine. 
        /// </summary>
        bool ExecutePostScan { get; }

        /// <summary>
        /// Gets the value indicating whether the <see cref="IAddOnInstruction"/> will execute a Enable-In routine.
        /// </summary>
        bool ExecuteEnableInFalse { get; }

        /// <summary>
        /// Gets the DateTime that the <see cref="IAddOnInstruction"/> was created.
        /// </summary>
        DateTime CreatedDate { get; }

        /// <summary>
        /// Gets the name of the user that created the <see cref="IAddOnInstruction"/>.
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// Gets the date and time that the <see cref="IAddOnInstruction"/> was last edited.
        /// </summary>
        DateTime EditedDate { get; }

        /// <summary>
        /// Gets the name of the user that last edited the <see cref="IAddOnInstruction"/>.
        /// </summary>
        string EditedBy { get; }

        /// <summary>
        /// Gets the Logix Controller revision at which the <see cref="IAddOnInstruction"/> was developed.
        /// </summary>
        Revision SoftwareRevision { get; }

        /// <summary>
        /// Gets the value for the additional help text of the <see cref="IAddOnInstruction"/>.
        /// </summary>
        string AdditionalHelpText { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IAddOnInstruction"/> is encrypted.
        /// </summary>
        bool IsEncrypted { get; }

        /// <summary>
        /// Gets the routine that contains the logic for the <see cref="IAddOnInstruction"/>.
        /// </summary>
        IRoutine<ILogixContent> Logic { get; }

        /// <summary>
        /// Gets the collection of <see cref="IParameter{TDataType}"/> that define the <see cref="IAddOnInstruction"/>.
        /// </summary>
        IMemberCollection<IParameter<IDataType>> Parameters { get; }

        /// <summary>
        /// Gets the collection of <see cref="ITag{TDataType}"/> local to the <see cref="IAddOnInstruction"/>.
        /// </summary>
        IComponentCollection<ITag<IDataType>> LocalTags { get; }

        /// <summary>
        /// Gets the collection of <see cref="IRoutine{TContent}"/> objects that contain the content of the <see cref="IAddOnInstruction"/>.
        /// </summary>
        IEnumerable<IRoutine<ILogixContent>> Routines { get; }
    }
}