using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Builders
{
    public interface IParameterBuilder<TDataType>
    {
        IParameterBuilder<TDataType> WithUsage(TagUsage usage);
        IParameterBuilder<TDataType> WithDimensions(Dimensions dimensions);
        IParameterBuilder<TDataType> WithRadix(Radix radix);
        IParameterBuilder<TDataType> WithAccess(ExternalAccess access);
        IParameterBuilder<TDataType> IsAliasFor(string aliasName);
        IParameterBuilder<TDataType> WithDescription(string description);
        IParameterBuilder<TDataType> WithDefault(TDataType value);
        IParameterBuilder<TDataType> IsRequired();
        IParameterBuilder<TDataType> IsVisible();
        IParameterBuilder<TDataType> IsConstant();
    }
}