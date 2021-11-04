using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Configurations
{
    public interface IParameterConfiguration : IComponentConfiguration<IParameter>
    {
        IParameterConfiguration HasDescription(string description);
        IParameterConfiguration WithDimensions(Dimensions dimensions);
        IParameterConfiguration WithRadix(Radix radix);
        IParameterConfiguration WithAccess(ExternalAccess access);
        IParameterConfiguration WithUsage(TagUsage usage);
        IParameterConfiguration WithMin(object value);
        IParameterConfiguration WithMax(object value);
        IParameterConfiguration WithDefault(object value);
        IParameterConfiguration IsRequired();
        IParameterConfiguration IsVisible();
        IParameterConfiguration IsConstant();
    }
}