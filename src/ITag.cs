using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface ITag<out TDataType> : ILogixComponent, ITagMember<TDataType> where TDataType : IDataType
    {
        new ComponentName Name { get; }
        new string Description { get; }
        TagType TagType { get; }
        Scope Scope { get; }
        TagUsage Usage { get; }
        bool Constant { get; set; }
        ILogixComponent Container { get; }
        void SetName(ComponentName name);
        void SetDimensions(Dimensions dimensions);
        void SetUsage(TagUsage usage);
        void SetExternalAccess(ExternalAccess externalAccess);
        ITag<TType> ChangeDataType<TType>(TType dataType) where TType : IDataType;
    }
}