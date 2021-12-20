using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IParameter<out TDataType> : IMember<TDataType> where TDataType : IDataType
    {
        TagType TagType { get; }
        TagUsage Usage { get; }
        bool Required { get; }
        bool Visible { get; }
        ITag<TDataType> Alias { get; }
        IAtomicType Default { get; }
        bool Constant { get; }
    }
}