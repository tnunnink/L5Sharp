using System;
using L5Sharp.Repositories;

namespace L5Sharp.Abstractions
{
    public interface IController : IComponent
    {
        public string Description { get; }
        public string ProcessorType { get; }
        public ulong MajorRev { get; }
        public ushort MinorRev { get; }
        public DateTime ProjectCreationDate { get; }
        public DateTime LastModifiedDate { get; }
        public IDataTypeRepository DataTypes { get; }
        /*T Get<T>(string name) where T : IComponent;
        void Add<T>(T item) where T : IComponent;
        void Remove<T>(T item) where T : IComponent;
        void Update<T>(T item) where T : IComponent;*/
    }
}