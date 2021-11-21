using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IParameter<out TDataType> : ILogixComponent, IMember<TDataType> where TDataType : IDataType
    {
        new ComponentName Name { get; }
        new string Description { get; }
        TagType TagType { get; }
        TagUsage Usage { get; }
        bool Required { get; set; }
        bool Visible { get; set; }
        ITag<TDataType> Alias { get; }
        IAtomic Default { get; }
        bool Constant { get; set; }
        void SetName(ComponentName name);
        void SetUsage(TagUsage usage);
        void SetDimensions(Dimensions dimensions);
        void SetExternalAccess(ExternalAccess access);
        void SetDefault(IAtomic value);
    }
}