using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IParameter<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        TagType TagType { get; }
        TagUsage Usage { get; }
        bool Required { get; set; }
        bool Visible { get; set; }
        ITag<TDataType> Alias { get; }
        IAtomic Default { get; }
        bool Constant { get; set; }
        void SetName(string name);
        void SetUsage(TagUsage usage);
        void SetDimensions(Dimensions dimensions);
        void SetExternalAccess(ExternalAccess access);
        void SetDefault(IAtomic value);
    }
}