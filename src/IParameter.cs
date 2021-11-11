using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IParameter<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        TagType TagType { get; }
        TagUsage Usage { get; }
        bool Required { get; }
        bool Visible { get; }
        IAtomic Min { get; }
        IAtomic Max { get; }
        IAtomic Default { get; }
        bool Constant { get; }
        void SetUsage(TagUsage usage);
        void IsRequired();
        void IsVisible();
        void SetDimensions(Dimensions dimensions);
        void SetMin(IAtomic value);
        void SetMax(IAtomic value);
        void SetDefault(IAtomic value);
        void SetExternalAccess(ExternalAccess access);
        void IsConstant();
    }
}