using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITag : ITagMember
    {
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; }
        void SetName(string name);
        void SetUsage(TagUsage usage);
        void SetExternalAccess(ExternalAccess externalAccess);
        ITag ChangeDataType(IDataType dataType);
        ITag ChangeDimensions(Dimensions dimensions);
        ITag ChangeTagType(TagType type);
    }

    public interface ITag<out TDataType> : ITag, ITagMember<TDataType> where TDataType : IDataType
    {
        
    }
}