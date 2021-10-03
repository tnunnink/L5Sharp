using System;

namespace L5Sharp.Abstractions
{
    public interface IController : INamedComponent
    {
        public string Description { get; }
        public string ProcessorType { get; }
        public ulong MajorRev { get; }
        public ushort MinorRev { get; }
        public DateTime ProjectCreationDate { get; }
        public DateTime LastModifiedDate { get; }
        T Get<T>(string name) where T : INamedComponent;
        void Add<T>(T item) where T : INamedComponent;
        void Remove<T>(T item) where T : INamedComponent;
        void Update<T>(T item) where T : INamedComponent;
    }
}