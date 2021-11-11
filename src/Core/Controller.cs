using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Controller : LogixComponent, IController
    {
        public Controller(string name, Revision revision = null, ProcessorType processorType = null,
            string description = null) : base(name, description)
        {
            ProcessorType = processorType;
            Revision = revision;
            ProjectCreationDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;

            DataTypes = new DataTypes(this);
            Tags = new Tags(this);
            Programs = new Programs();
            Tasks = new Tasks();
        }

        public ProcessorType ProcessorType { get; }

        public Revision Revision { get; set; }

        public int MajorRev => Revision.Major;

        public int MinorRev => Revision.Minor;

        public DateTime ProjectCreationDate { get; }

        public DateTime LastModifiedDate { get; }

        public IDataTypes DataTypes { get; }
        public ITags Tags { get; }
        public IPrograms Programs { get; }
        public ITasks Tasks { get; }
    }
}