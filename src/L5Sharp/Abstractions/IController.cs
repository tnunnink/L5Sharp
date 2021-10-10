using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface IController : IComponent
    {
        ProcessorType ProcessorType { get; }
        Revision Revision { get; }
        IEnumerable<IDataType> DataTypes { get; }
        IEnumerable<ITag> Tags { get; }
        void AddDataType(IDataType dataType);
        void RemoveDataType(IDataType dataType);
        void AddTag(ITag tag);
        void RemoveTag(ITag tag);
    }
}