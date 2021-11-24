using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Controller : IController
    {
        public Controller(string name, Revision revision = null, ProcessorType processorType = null,
            string description = null)
        {
            Name = name;
            Description = description;
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;

            DataTypes = new DataTypes(this);
            Tags = new Tags(this);
            Programs = new Programs();
            Tasks = new Tasks(this);
        }

        public ComponentName Name { get; }
        public string Description { get; }
        public ProcessorType ProcessorType { get; }

        public Revision Revision { get; set; }

        public int MajorRev => Revision.Major;

        public int MinorRev => Revision.Minor;

        public DateTime ProjectCreationDate { get; }

        public DateTime LastModifiedDate { get; }

        public IComponentCollection<IUserDefined> DataTypes { get; }
        public ITags Tags { get; }
        public IPrograms Programs { get; }
        public ITasks Tasks { get; }
    }
}