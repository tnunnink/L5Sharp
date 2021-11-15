using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITag<out TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        new ComponentName Name { get; }
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; set; }
        void SetName(ComponentName name);
        void SetUsage(TagUsage usage);
        void SetExternalAccess(ExternalAccess externalAccess);
        ITag<TType> ChangeDataType<TType>(TType dataType) where TType : IDataType;
        ITag<TDataType> ChangeDimensions(Dimensions dimensions);
        ITag<TDataType> ChangeTagType(TagType type);
    }
}