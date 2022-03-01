using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc />
    public class AddOnInstruction : IAddOnInstruction
    {
        private const string LogicRoutineName = "Logic";
        private readonly List<IRoutine<ILogixContent>> _routines = new();

        internal AddOnInstruction(string name, string? description = null, Revision? revision = null,
            string? revisionExtension = null, string? revisionNote = null, string? vendor = null,
            bool executePreScan = default, bool executePostScan = default, bool executeEnableInFalse = default,
            DateTime createdDate = default, string? createdBy = null,
            DateTime editedDate = default, string? editedBy = null,
            Revision? softwareRevision = null, string? additionalHelpText = null, bool isEncrypted = default,
            IEnumerable<IParameter<IDataType>>? parameters = null,
            IEnumerable<ITag<IDataType>>? localTags = null,
            IEnumerable<IRoutine<ILogixContent>>? routines = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Revision = revision ?? new Revision();
            RevisionExtension = revisionExtension ?? string.Empty;
            RevisionNote = revisionNote ?? string.Empty;
            Vendor = vendor ?? string.Empty;
            ExecutePreScan = executePreScan;
            ExecutePostScan = executePostScan;
            ExecuteEnableInFalse = executeEnableInFalse;
            CreatedDate = createdDate;
            CreatedBy = createdBy ?? Environment.UserName;
            EditedDate = editedDate;
            EditedBy = editedBy ?? Environment.UserName;
            SoftwareRevision = softwareRevision ?? new Revision();
            AdditionalHelpText = additionalHelpText ?? string.Empty;
            IsEncrypted = isEncrypted;
            Parameters = new MemberCollection<IParameter<IDataType>>(this, parameters);
            LocalTags = new ComponentCollection<ITag<IDataType>>(localTags ?? Enumerable.Empty<ITag<IDataType>>());
            _routines.AddRange(routines ?? Enumerable.Empty<IRoutine<ILogixContent>>());
        }

        /// <summary>
        /// Creates a new <see cref="AddOnInstruction"/> object with the provided name and optional parameters.
        /// </summary>
        /// <param name="name">The name of the instruction to create.</param>
        /// <param name="type">The optional routine type of the instruction logic routine.
        /// Will default to ladder logic type routine.</param>
        /// <param name="revision">The optional revision of the instruction to create.
        /// Will default to '1.0'.</param>
        /// <param name="revisionExtension">The optional revision extension text that further defines the instruction
        /// revision. Will default to an empty string.</param>
        /// <param name="revisionNote">The notes associated with the current revision for the instruction.
        /// Will default to and empty string.</param>
        /// <param name="vendor">The name of the company or vendor who created the instruction.
        /// will default to and empty string.</param>
        /// <param name="executePreScan">A value indicating whether the instruction contains a routine that executes
        /// pre scan of the primary instruction logic. Will default to false.</param>
        /// <param name="executePostScan">A value indicating whether the instruction contains a routine that executes
        /// post scan of the primary instruction logic. Will default to false.</param>
        /// <param name="executeEnableInFalse">A value indicating whether the instruction contains a routine that executes
        /// when the enable in false bit becomes active. Will default to false.</param>
        /// <param name="createdDate">The date time that the instruction was created.
        /// Will default to <see cref="DateTime.Now"/>.</param>
        /// <param name="createdBy">The user or developer that created the instruction.
        /// Will default to an empty string.</param>
        /// <param name="editedDate">The date time that the instruction was last edited.
        /// Will default to <see cref="DateTime.Now"/>.</param>
        /// <param name="editedBy">The user or developer that last edited the instruction.
        /// Will default to an empty string.</param>
        /// <param name="softwareRevision">The revision of the controller used to create the instruction.
        /// Will default to '1.0'/</param>
        /// <param name="additionalHelpText">The text that represents additional information about the instruction.
        /// Will default to an empty string.</param>
        /// <param name="isEncrypted">A value indicating whether the current instruction is encrypted.
        /// Will default to false.</param>
        /// <param name="description">The description of the instruction. Will default to an empty string.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        public AddOnInstruction(ComponentName name, RoutineType? type = null, 
            Revision? revision = null, string? revisionExtension = null, string? revisionNote = null,
            string? vendor = null,
            bool executePreScan = default, bool executePostScan = default, bool executeEnableInFalse = default,
            DateTime createdDate = default, string? createdBy = null,
            DateTime editedDate = default, string? editedBy = null,
            Revision? softwareRevision = null, string? additionalHelpText = null, bool isEncrypted = default,
            string? description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Revision = revision ?? new Revision();
            RevisionExtension = revisionExtension ?? string.Empty;
            RevisionNote = revisionNote ?? string.Empty;
            Vendor = vendor ?? string.Empty;
            ExecutePreScan = executePreScan;
            ExecutePostScan = executePostScan;
            ExecuteEnableInFalse = executeEnableInFalse;
            CreatedDate = createdDate;
            CreatedBy = createdBy ?? Environment.UserName;
            EditedDate = editedDate;
            EditedBy = editedBy ?? Environment.UserName;
            SoftwareRevision = softwareRevision ?? new Revision();
            AdditionalHelpText = additionalHelpText ?? string.Empty;
            IsEncrypted = isEncrypted;
            Parameters = new MemberCollection<IParameter<IDataType>>(this);
            LocalTags = new ComponentCollection<ITag<IDataType>>();
            
            var routine = new Routine<ILogixContent>(LogicRoutineName, type);
            _routines.Add(routine);
        }


        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.AddOnDefined;

        /// <inheritdoc />
        public RoutineType Type => Routines.Single(r => r.Name == LogicRoutineName).Type;

        /// <inheritdoc />
        public Revision Revision { get; }

        /// <inheritdoc />
        public string RevisionExtension { get; }

        /// <inheritdoc />
        public string RevisionNote { get; }

        /// <inheritdoc />
        public string Vendor { get; }

        /// <inheritdoc />
        public bool ExecutePreScan { get; }

        /// <inheritdoc />
        public bool ExecutePostScan { get; }

        /// <inheritdoc />
        public bool ExecuteEnableInFalse { get; }

        /// <inheritdoc />
        public DateTime CreatedDate { get; }

        /// <inheritdoc />
        public string CreatedBy { get; }

        /// <inheritdoc />
        public DateTime EditedDate { get; }

        /// <inheritdoc />
        public string EditedBy { get; }

        /// <inheritdoc />
        public Revision SoftwareRevision { get; }

        /// <inheritdoc />
        public string AdditionalHelpText { get; }

        /// <inheritdoc />
        public bool IsEncrypted { get; }

        /// <inheritdoc />
        public IRoutine<ILogixContent> Logic => _routines.Single(r => r.Name == LogicRoutineName);

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> Members => Parameters.Where(p => p.Usage != TagUsage.InOut);

        /// <inheritdoc />
        public IMemberCollection<IParameter<IDataType>> Parameters { get; }

        /// <inheritdoc />
        public IEnumerable<ITag<IDataType>> LocalTags { get; }

        /// <inheritdoc />
        public IEnumerable<IRoutine<ILogixContent>> Routines => _routines;

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            //return new AddOnInstruction(Name, Description, Type, Revision, RevisionExtension, )

            throw new NotImplementedException();
        }
    }
}