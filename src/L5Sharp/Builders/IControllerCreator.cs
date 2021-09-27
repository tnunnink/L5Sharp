using System;

namespace L5Sharp.Builders
{
    public interface IControllerCreator
    {
        void DataType(string name, Action<IDataTypeBuilder> builder);
        void Tag(string name, Action<ITagBuilder> builder);
    }
}