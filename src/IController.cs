using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IController : ILogixComponent
    {
        ProcessorType ProcessorType { get; }
        Revision Revision { get; }
        IEnumerable<IDataType> DataTypes { get; }
        IEnumerable<ITag> Tags { get; }
        IEnumerable<IProgram> Programs { get; }
        IEnumerable<ITask> Tasks { get; }
        void AddDataType(IDataType dataType);
        void RemoveDataType(IDataType dataType);
        void AddTag(ITag tag);
        void RemoveTag(ITag tag);
        void AddProgram(IProgram program);
        void RemoveProgram(IProgram program);
        void AddTask(ITask task);
        void RemoveTask(ITask task);
    }
}