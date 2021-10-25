using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public interface ITag : IComponent, ISetComponentName, ISetComponentDescription
    {
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; }
        void SetUsage(TagUsage usage);
    }
    
    public interface ITag<out TDataType> : ITag, ITagMember<TDataType> where TDataType : IDataType
    {
        ITag<TDataType> SetDimensions(Dimensions dimensions);
        void SetExternalAccess(ExternalAccess externalAccess);
        ITag<IDataType> ChangeTagType(TagType type);
    }
}