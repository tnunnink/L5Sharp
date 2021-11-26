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
        }

        public ComponentName Name { get; }
        public string Description { get; }
        
        public Use Use { get; }
        public ProcessorType ProcessorType { get; }

        public Revision Revision { get; }

        public DateTime ProjectCreationDate { get; }

        public DateTime LastModifiedDate { get; }
    }
}