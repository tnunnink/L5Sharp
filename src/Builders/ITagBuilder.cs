using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public interface ITagBuilder<TDataType> : IComponentBuilder<ITag<TDataType>> where TDataType : IDataType
    {
        ITagBuilder<TDataType> WithDimensions(Dimensions dimensions);
        ITagBuilder<TDataType> WithRadix(Radix radix);
        ITagBuilder<TDataType> WithAccess(ExternalAccess access);
        ITagBuilder<TDataType> IsAliasFor(string aliasName);
        ITagBuilder<TDataType> WithDescription(string description);
        ITagBuilder<TDataType> WithUsage(TagUsage usage);
        ITagBuilder<TDataType> WithValue(TDataType value);
        ITagBuilder<TDataType> IsConstant();
    }
}