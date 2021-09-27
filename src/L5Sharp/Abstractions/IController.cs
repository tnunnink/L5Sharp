using System;
using System.Collections.Generic;
using L5Sharp.Primitives;

namespace L5Sharp.Abstractions
{
    public interface IController
    {
        public string Name { get; }
        public string Description { get; }
        public string ProcessorType { get; }
        public ulong Major { get; }
        public ushort Minor { get; }
        public DateTime ProjectCreationDate { get; }
        public DateTime LastModifiedDate { get; }
        public IEnumerable<IDataType> DataTypes { get; }
        public IEnumerable<Module> Modules { get; }
        public IEnumerable<Instruction> Instructions { get; }
        public IEnumerable<Tag> Tags { get; }
        public IEnumerable<Program> Programs { get; }
        public IEnumerable<Task> Tasks { get; }
    }
}