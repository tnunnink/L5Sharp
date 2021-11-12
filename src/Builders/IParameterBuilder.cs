using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public interface IParameterBuilder<out TDataType> : IComponentBuilder<IParameter<TDataType>>
        where TDataType : IDataType
    {
        IParameterBuilder<TDataType> HasDescription(string description);
        IParameterBuilder<TDataType> WithDimensions(Dimensions dimensions);
        IParameterBuilder<TDataType> WithRadix(Radix radix);
        IParameterBuilder<TDataType> WithAccess(ExternalAccess access);
        IParameterBuilder<TDataType> WithUsage(TagUsage usage);
        IParameterBuilder<TDataType> WithMin(object value);
        IParameterBuilder<TDataType> WithMax(object value);
        IParameterBuilder<TDataType> WithDefault(object value);
        IParameterBuilder<TDataType> IsRequired();
        IParameterBuilder<TDataType> IsVisible();
        IParameterBuilder<TDataType> IsConstant();
    }
}