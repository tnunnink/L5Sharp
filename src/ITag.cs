using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITag<out TDataType> : ITagMember<TDataType> where TDataType : IDataType
    {
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; }
        void SetName(string name);
        void SetUsage(TagUsage usage);
        void SetExternalAccess(ExternalAccess externalAccess);
        ITag<T> ChangeDataType<T>(T dataType) where T : IDataType;
        ITag<TDataType> ChangeDimensions(Dimensions dimensions);
        ITag<TDataType> ChangeTagType(TagType type);
    }
}