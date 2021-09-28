using System;

namespace L5Sharp.Builders.Abstractions
{
    public interface IControllerCreator
    {
        void DataType(string name, Action<IDataTypeBuilder> builder);
        void Tag(string name, Action<ITagBuilder> builder);
        void Task(string name, Action<ITaskBuilder> builder);
    }
}