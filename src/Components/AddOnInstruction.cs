using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp.Components
{
    /// <summary>
    /// A logix <c>AddOnInstruction</c> component. Contains the properties that comprise the L5X
    /// AddOnInstructionDefinition element.
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
        /// The revision of the instruction.
        /// </summary>
        /// <value>A <see cref="Core.Revision"/> representing the version of the instruction.</value>
        /// <remarks>
        /// Specify the revision of the Add-On Instruction, in the form of MajorRevision.MinorRevision.
        /// Each revision number can be 1...65,535.
        /// If there is no period, the number is treated as a major revision only
        /// </remarks>
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
        public bool ExecutePreScan { get; set; }

        /// <summary>
        /// Indicates that the instruction has and executes a post scan routine.
        /// </summary>
        /// <value><c>true</c> if the instruction executes a post scan routine; otherwise, <c>false</c>.</value>
        public bool ExecutePostScan { get; set; }

        /// <summary>
        /// Indicates that the instruction has and executes a enable in false routine.
        /// </summary>
        /// <value>A <see cref="bool"/> - <c>true</c> if the instruction executes a enable in false routine; otherwise, <c>false</c>.</value>
        public bool ExecuteEnableInFalse { get; set; }

        /// <summary>
        /// The date and time that the instruction was created.
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing the creation date and time.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The name of the user that created the instruction.
        /// </summary>
        /// <value>A <see cref="string"/> representing the name of the user.</value>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The date and time that the instruction was last edited.
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing the edit date and time.</value>
        public DateTime EditedDate { get; set; }

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
        /// Gets the collection of <see cref="Parameter"/> objects that define the AOI structure type.
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

        /// <summary>
        /// Returns the AOI instruction logic with the parameters of the instruction replaced with the provided neutral
        /// text signature arguments.
        /// </summary>
        /// <param name="text">The text signature of the instruction arguments.</param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/> containing <see cref="NeutralText"/> representing all the instruction's
        /// logic, with each instruction parameter tag name replaced with the arguments from the provided text.
        /// </returns>
        /// <remarks>
        /// This is helpful when trying to perform deep analysis on logic. By "flattening" the logic we can
        /// reason or evaluate it as if it was written in line. Currently only support <see cref="RllRoutine"/>
        /// types.
        /// </remarks>
        public IEnumerable<NeutralText> Logic(NeutralText text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            // All instructions primary logic is contained in the routine names 'Logic'
            var logic = Routines.FirstOrDefault(r => r.Name == "Logic");

            if (logic is not RllRoutine rll)
                return Enumerable.Empty<NeutralText>();

            //Skip first operand as it is always the AOI tag, which does not have corresponding parameter within the logic.
            var arguments = text.Operands().Select(o => o.ToString()).Skip(1).ToList();

            //Only required parameters are part of the instruction signature
            var parameters = Parameters.Where(p => p.Required).Select(p => p.Name).ToList();

            //Create a mapping of the provided text operand arguments to instruction parameter names.
            var mapping = arguments.Zip(parameters, (a, p) => new { Argument = a, Parameter = p }).ToList();

            //Replace all parameter names with argument names in the instruction logic text, and return the results.
            return rll.Content.Select(r => r.Text)
                .Select(t => mapping.Aggregate(t, (current, pair) =>
                {
                    if (!pair.Argument.IsTagName()) return current;
                    var replace = $@"(?<=[^.]){pair.Parameter}\b";
                    return Regex.Replace(current, replace, pair.Argument.ToString());

                }))
                .ToList();
        }
    }
}