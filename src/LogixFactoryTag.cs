using L5Sharp.Builders;
using L5Sharp.Core;

namespace L5Sharp
{
    public static class Tag
    {
        public static ITag<IDataType> Create(ComponentName name, IDataType dataType)
        {
            return new Tag<IDataType>(name, dataType);
        }

        public static ITag<TDataType> Create<TDataType>(ComponentName name) where TDataType : IDataType, new()
        {
            return new Tag<TDataType>(name, new TDataType());
        }

        public static ITag<TDataType> Create<TDataType>(ComponentName name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new Tag<TDataType>(name, dataType);
        }

        public static ITagBuilder<IDataType> Build(ComponentName name, IDataType dataType)
        {
            return new TagBuilder<IDataType>(name, dataType);
        }

        public static ITagBuilder<TDataType> Build<TDataType>(ComponentName name)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new TagBuilder<TDataType>(name, dataType);
        }

        public static ITagBuilder<TDataType> Build<TDataType>(ComponentName name, TDataType dataType)
            where TDataType : IDataType, new()
        {
            return new TagBuilder<TDataType>(name, dataType);
        }
    }
}