using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITag : IComponent
    {
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; }
        void SetName(string name);
        void SetUsage(TagUsage usage);
    }
    
    public interface ITag<out TDataType> : ITag, ITagMember<TDataType> where TDataType : IDataType
    {
        void SetExternalAccess(ExternalAccess externalAccess);
        ITag<IDataType> ChangeDataType(IDataType dataType);
        ITag<IDataType> ChangeDimensions(Dimensions dimensions);
        ITag<IDataType> ChangeTagType(TagType type);
    }
}