using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class AddOnInstruction : IAddOnInstruction
    {
        private const string LogicRoutineName = "Logic";
        private const string PreScanRoutineName = "Prescan";
        private const string PostScanRoutineName = "Postscan";
        private const string EnableInRoutineName = "EnableInFalse";
        private readonly List<IRoutine> _routines = new List<IRoutine>();

        public AddOnInstruction(string name,
            string description = null, RoutineType type = null, Revision revision = null,
            string revisionExtension = null, string revisionNote = null, string vendor = null,
            bool executePreScan = false, bool executePostScan = false, bool executeEnableInFalse = false,
            DateTime createdDate = default, string createdBy = null,
            DateTime editedDate = default, string editedBy = null,
            Revision softwareRevision = null, string additionalHelpText = null, bool isEncrypted = false,
            IEnumerable<IParameter<IDataType>> parameters = null, 
            IEnumerable<ITag<IDataType>> localTags = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Revision = revision ?? new Revision();
            RevisionExtension = revisionExtension;
            RevisionNote = revisionNote;
            Vendor = vendor;
            ExecutePreScan = executePreScan;
            ExecutePostScan = executePostScan;
            ExecuteEnableInFalse = executeEnableInFalse;
            CreatedDate = createdDate;
            CreatedBy = createdBy ?? Environment.UserName;
            EditedDate = editedDate;
            EditedBy = editedBy ?? Environment.UserName;
            SoftwareRevision = softwareRevision;
            AdditionalHelpText = additionalHelpText;
            IsEncrypted = isEncrypted;

            Parameters = new Parameters(this, parameters);
            /*LocalTags = new Tags(this, localTags);*/

            type ??= RoutineType.Ladder;
            InitializeRoutine(type);
        }


        public string Name { get; }
        public string Description { get; }
        public Radix Radix => Radix.Null;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.AddOnDefined;
        public DataFormat Format => DataFormat.Decorated;
        public RoutineType Type => Routines.Single(r => r.Name == LogicRoutineName).Type;
        public Revision Revision { get; private set; }
        public string RevisionExtension { get; private set; }
        public string RevisionNote { get; private set; }
        public string Vendor { get; private set; }
        public bool ExecutePreScan { get; set; }
        public bool ExecutePostScan { get; set; }
        public bool ExecuteEnableInFalse { get; set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime EditedDate { get; private set; }
        public string EditedBy { get; private set; }
        public Revision SoftwareRevision { get; private set; }
        public string AdditionalHelpText { get; private set; }
        public bool IsEncrypted { get; }
        public IRoutine Logic => _routines.Single(r => r.Name == LogicRoutineName);
        public IRoutine PreScan => _routines.Single(r => r.Name == PreScanRoutineName);
        public IRoutine PostScan => _routines.Single(r => r.Name == PostScanRoutineName);
        public IRoutine EnableInFalse => _routines.Single(r => r.Name == EnableInRoutineName);
        public IEnumerable<IMember<IDataType>> Members => Parameters.Where(p => p.Usage != TagUsage.InOut);

        public NeutralText Signature { get; }
        public IMemberCollection<IParameter<IDataType>> Parameters { get; }
        public IComponentCollection<ITag<IDataType>> LocalTags { get; }

        public IEnumerable<IRoutine> Routines => _routines.AsReadOnly();

        IEnumerable<IMember<IDataType>> IInstruction.Parameters => Parameters;


        public IMember<IDataType> GetParameter(string name)
        {
            throw new NotImplementedException();
        }

        public IMember<TType> GetParameter<TType>(string name) where TType : IDataType
        {
            throw new NotImplementedException();
        }

        public NeutralText Of(params ITagMember<IDataType>[] tags)
        {
            throw new NotImplementedException();
        }

        public NeutralText Of(params object[] values)
        {
            throw new NotImplementedException();
        }
        
        public void SetRevision(Revision revision)
        {
            if (revision == null)
                throw new ArgumentNullException(nameof(revision), "Value can not be null");

            Revision = revision;
        }

        public void SetRevisionExtension(string revisionExtension)
        {
            RevisionExtension = revisionExtension;
        }

        public void SetRevisionNote(string note)
        {
            RevisionExtension = note;
        }

        public void SetVendor(string vendor)
        {
            Vendor = vendor;
        }

        public void AddPreScanRoutine(RoutineType type)
        {
            var routine = type.Create(PreScanRoutineName);
            _routines.Add(routine);
        }

        public void AddPostScanRoutine(RoutineType type)
        {
            var routine = type.Create(PostScanRoutineName);
            _routines.Add(routine);
        }

        private void InitializeRoutine(RoutineType type)
        {
            var routine = type.Create(LogicRoutineName);
            _routines.Add(routine);
        }

        public IDataType Instantiate()
        {
            throw new NotImplementedException();
        }
    }
}